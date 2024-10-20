const WebSocket = require('ws');

const wss = new WebSocket.Server({ port: 303 });

let currentPassword = generatePassword();
let passwordInterval = setInterval(updatePassword, 20000); // Her 20 saniyede bir otomatik olarak şifre güncelle

function generatePassword() {
  return Math.floor(10000000 + Math.random() * 90000000);
}

function updatePassword() {
  currentPassword = generatePassword();
  console.log('Generated password: %d', currentPassword);

  // Tüm istemcilere güncellenmiş şifreyi gönder
  wss.clients.forEach((client) => {
    if (client.readyState === WebSocket.OPEN) {
      client.send(currentPassword.toString());
    }
  });
}

wss.on('connection', function connection(ws) {
  ws.on('message', function incoming(message) {
    // İstemci bir mesaj gönderdiğinde
    console.log('Received: %s', message);
  });

  // İstemciye mevcut şifreyi gönder
  ws.send(currentPassword.toString());
});

// Bağlantı kesildiğinde kontrolü geri al
wss.on('close', () => {
  if (wss.clients.size === 0) {
    // Eğer hiç istemci kalmazsa zamanlayıcıyı temizle
    clearInterval(passwordInterval);
    passwordInterval = null;
  }
});
console.log('Generated password: %d', currentPassword);