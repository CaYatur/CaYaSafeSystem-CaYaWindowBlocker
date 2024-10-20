const express = require('express');
const fs = require('fs');
const multer = require('multer');
const { exec } = require('child_process');
const http = require('http');
const path = require('path');
const socketIO = require('socket.io');
const screenshot = require('screenshot-desktop');
const Jimp = require('jimp');
const robot = require('robotjs');
const os = require('os');
const loudness = require('loudness');



const app = express();
const PORT = 4728;
const PORT2 = 80;

const server = http.createServer(app);
const server2 = http.createServer(app);
const io = socketIO(server);




app.use(express.static('img'));
app.use('/img', express.static(path.join(__dirname, 'img')));

//const server = http.createServer(app);

// Middleware
app.use(express.json());
app.use(express.urlencoded({ extended: false }));


// // Şu anki zamanı al
// const currentTime = new Date();
// const formattedTime = `${currentTime.getFullYear()}.${(currentTime.getMonth() + 1).toString().padStart(2, '0')}.${currentTime.getDate().toString().padStart(2, '0')}.${currentTime.getHours().toString().padStart(2, '0')}.${currentTime.getMinutes().toString().padStart(2, '0')}`;

// // Yazılacak metin
// const textToWrite = `verified (${formattedTime})`;

// // Dosya yolu
// const filePath = 'C:/ProgramData/verifyc.txt';







// Sunucudaki dosyaların bulunduğu dizin
const dosyaDizini = path.join(__dirname, 'uploads');
const yuklemeDizini = path.join(__dirname, 'uploads');

// Multer ayarları
const storage = multer.diskStorage({
  destination: function (req, file, cb) {
    cb(null, yuklemeDizini);
  },
  filename: function (req, file, cb) {
    cb(null, file.originalname);
  }
});
const upload = multer({ storage: storage });

// Middleware'ler
app.use(express.static('public'));

// Ana sayfa
app.get('/filesystem', (req, res) => {
  res.sendFile(path.join(__dirname, 'explorer.html'));
});

// Dosyaları listeleme endpoint'i
app.get('/dosyalar/*', (req, res) => {
  const hedefYol = path.join(dosyaDizini, req.params[0]);
  fs.readdir(hedefYol, (err, dosyalar) => {
    if (err) {
      return res.status(500).json({ error: err.message });
    }

    const dosyaListesi = dosyalar.map(dosya => {
      const dosyaYolu = path.join(hedefYol, dosya);
      const istatistik = fs.statSync(dosyaYolu);
      return {
        ad: dosya,
        tip: istatistik.isDirectory() ? 'klasor' : 'dosya',
        boyut: istatistik.size
      };
    });

    res.json({ dosyalar: dosyaListesi });
  });
});

// Dosya yükleme endpoint'i
app.post('/yukle', upload.single('dosya'), (req, res) => {
  const yuklenenDosya = req.file;
  if (!yuklenenDosya) {
    return res.status(400).json({ error: 'Dosya yüklenemedi' });
  }

  const hedefKlasor = path.join(dosyaDizini, req.body.klasor || '');

  fs.mkdir(hedefKlasor, { recursive: true }, (err) => {
    if (err) {
      console.error('Hedef klasör oluşturulamadı:', err);
      return res.status(500).json({ error: 'Hedef klasör oluşturulamadı' });
    }
    const hedefYol = path.join(hedefKlasor, yuklenenDosya.originalname);
    fs.renameSync(yuklenenDosya.path, hedefYol);
    res.json({ success: true });
  });
});

// Dosya silme endpoint'i
app.delete('/sil/*', (req, res) => {
  const dosyaYolu = path.join(dosyaDizini, req.params[0]);
  fs.unlink(dosyaYolu, (err) => {
    if (err) {
      return res.status(500).json({ error: 'Dosya silinemedi' });
    }
    res.json({ success: true });
  });
});


// Dosya çalıştırma endpoint'i
app.get('/calistir/*', (req, res) => {
  const dosyaYolu = path.join(dosyaDizini, req.params[0]);
  exec(`start "" "${dosyaYolu}"`, (error, stdout, stderr) => {
    if (error) {
      return res.status(500).json({ error: error.message });
    }
    res.json({ stdout, stderr });
  });
});

// Dosya indirme endpoint'i
app.get('/indir/*', (req, res) => {
  const dosyaYolu = path.join(dosyaDizini, req.params[0]);
  const dosyaAdi = path.basename(dosyaYolu);
  res.download(dosyaYolu, dosyaAdi);
});

// Manuel komut çalıştırma endpoint'i
app.post('/komut-calistir', express.json(), (req, res) => {
  const komut = req.body.komut;
  if (!komut) {
    return res.status(400).json({ error: 'Komut belirtilmedi' });
  }
  exec(komut, (error, stdout, stderr) => {
    if (error) {
      return res.status(500).json({ error: error.message });
    }
    res.json({ stdout, stderr });
  });
});



// Ses kapatma rotası
app.post('/ses-kapat', (req, res) => {
  // Sesi kapatmak için PowerShell komutunu kullanın
  exec('powershell.exe -Command "(New-Object -ComObject WScript.Shell).SendKeys([char]173)"', (error, stdout, stderr) => {
    if (error) {
      console.error('Ses kapatma hatası:', error);
      res.status(500).send('Ses kapatılamadı');
      return;
    }
    console.log('Ses kapatıldı!');
    res.status(200).send('Ses kapatıldı');
  });
});

// Ses açma rotası
app.post('/ses-ac', (req, res) => {
  console.log("Tıklandı!");
  // Sesi açma
  loudness.setVolume(100, (err) => {
    if (err) {
      console.error('Ses açılamadı:', err);
      res.status(500).send('Ses açılamadı');
      return;
    }
    console.log("Ses açıldı!");
    res.status(200).send('Ses açıldı');
  });
});



const directoryPath = 'C:/ProgramData/PVGCC'

app.get('/', (req, res) => {
  //res.redirect('/cysfsy/');
  //const imagePath = __dirname + '/img/404notfound.png';

  const response = `


  <!DOCTYPE html>
<html lang="tr">
<head>
    <link rel="icon" type="image/png" href="/img/PCCC2.ico">
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>CaYaSafeSystem LocalServer</title>
    <style>
        body {
            font-family: Arial, sans-serif;
            background-color: #f2f2f2;
            margin: 0;
            padding: 0;
            box-sizing: border-box;
        }
        .container {
            max-width: 600px;
            margin: 50px auto;
            background-color: #fff;
            padding: 20px;
            border-radius: 8px;
            box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
        }
        h1, p {
            text-align: center;
        }
        .logo {
            display: block;
            margin: 0 auto 20px;
            width: 150px;
        }
        .info {
            margin-bottom: 20px;
        }
    </style>
</head>
<body>
    <div class="container">
        <img class="logo" src="/PCCC2.ico" alt="CaYaSafeSystem Logo">
        <h1>CaYaWindowBlock - CaYaSafeSystem</h1>
        <div class="info">
            <p>This web site version 1.0</p>
            <p>AutoLoginSystem-AutoNotificationSystem-AutoYourLogFileSendAuthorizedPersonnelSystem</p>
            <p>LocalServer</p>
        </div>
    </div>
</body>
</html>


  
  `;

  res.send(response);

  //res.sendFile(imagePath);
});


// GET /cysfsy/:id endpoint'i
app.get('/cysfsy/:id', (req, res) => {
  const id = req.params.id;
  console.log('LockSystemBypass');



  // Şu anki zamanı al
  const currentTime = new Date();
  const formattedTime = `${currentTime.getFullYear()}.${(currentTime.getMonth() + 1).toString().padStart(2, '0')}.${currentTime.getDate().toString().padStart(2, '0')}.${currentTime.getHours().toString().padStart(2, '0')}.${currentTime.getMinutes().toString().padStart(2, '0')}`;

  // Yazılacak metin
  const textToWrite = `verified (${formattedTime})`;

  // Dosya yolu
  const filePath = 'C:/ProgramData/verifyc.txt';

  // Dosyaya metni yaz
  fs.writeFile(filePath, textToWrite, (err) => {
    if (err) {
      console.error('Dosyaya yazılırken bir hata oluştu:', err);
      return res.status(500).send('Dosyaya yazılırken bir hata oluştu');
    }
    console.log('Dosyaya başarıyla yazıldı.');
  })


  // // Dosyaya metni yaz
  // fs.writeFile(filePath, textToWrite, (err) => {
  //   if (err) {
  //     console.error('Dosyaya yazılırken bir hata oluştu:', err);
  //     return;
  //   }
  //   console.log('Dosyaya başarıyla yazıldı.');
  // });

  // Büyük ve tam ortada yazı için CSS ile stillendirme
  const response = `
    <html>
      <!DOCTYPE html>
      <link rel="icon" type="image/png" href="/img/PCV2.png">
      <html lang="tr">
        <style>
          body {
            font-family: Arial, sans-serif;
            background-color: #f0f0f0;
            margin: 0;
            padding: 0;
          }
        
          .container {
            width: 80%;
            margin: 20px auto;
            text-align: center;
          }
        
          .container p {
            margin: 0;
            font-size: 1.2em;
          }
        
          .con1 {
            color: #ff8c00;
            font-weight: bold;
          }
        
          .footer {
            background-color: #333;
            color: white;
            text-align: center;
            padding: 10px 0;
            position: fixed;
            bottom: 0;
            width: 100%;
          }
        
        
        </style>
        
        <head>
          <meta charset="UTF-8">
          <meta name="viewport" content="width=device-width, initial-scale=1.0">
          <title>Connection This Server Local PC CaYaSafePCServices</title>

        </head>
        <body>
          <div class="container">
            <p>Eğer otomatik açma işlemi başarısız oldu ise</p>
          </div>
          <div class="container">
            <p>Kodunuz:</p>
          </div>
          <div class="container con1">
            <p>${id}</p>
          </div>
          <div class="container">
            <p>Eğer kodsuz bir giriş ekranında iseniz ve giriş hala gerçekleşmedi ise bilgisayarı yeniden başlatmayı deneyin.</p>
          </div>
          <div class="footer">
            <p>CaYaSafeSystem This web site version: 1.0</p>
          </div>
        </body>
        </html>
  `;

  res.send(response);
});



// UTF-8 kodlamasını kullan
app.use(express.urlencoded({ extended: true }));

app.get('/cysfsylog', (req, res) => {
  const username = os.userInfo().username;
  // Belirtilen dizindeki .txt dosyalarını al
  fs.readdir(directoryPath, (err, files) => {
    if (err) {
      console.error(err);
      return res.status(500).send('Sunucu hatası.');
    }

    // Dosya listesi oluştur
    let fileList = '<ul>';
    files.forEach(file => {
      const filePath = path.join(directoryPath, file);
      // Sadece .txt dosyalarını listele
      if (file.endsWith('.txt')) {
        fileList += `<li><a href="/preview?file=${encodeURIComponent(file)}">${file}</a></li>`;
      }
    });
    fileList += '</ul>';

    // Dosya listesini istemciye gönder
    res.send(`
      <!DOCTYPE html>
      <html lang="tr">
        <head>
          <meta charset="UTF-8">
          <title>Dosya Listesi</title>
          <style>
            body {
              font-family: Arial, sans-serif;
            }
            h1 {
              color: #333;
            }
            ul {
              list-style-type: none;
              padding: 0;
            }
            li {
              margin-bottom: 10px;
            }
            a {
              color: #007bff;
              text-decoration: none;
            }
            a:hover {
              text-decoration: underline;
            }
          </style>
        </head>
        <body>
          <h1>${username} Adlı Bilgisayarda Tutulan Kayıtlar</h1>
          ${fileList}
          <h2>Hatırlatma: Bu kayıtlar en fazla 20 gün tutulmaktadır 20 günden fazla ise önceki kayıtlar silinmektedir.</h2>
        </body>
      </html>
    `);
  });
});

app.get('/preview', (req, res) => {
  const fileName = req.query.file;
  const filePath = path.join(directoryPath, fileName);

  // Dosya okuma işlemi
  fs.readFile(filePath, 'utf8', (err, data) => {
    if (err) {
      console.error(err);
      return res.status(500).send('Dosya okunamadı.');
    }

    // Dosya içeriğini istemciye gönder
    res.send(`
      <!DOCTYPE html>
      <html lang="tr">
        <head>
          <meta charset="UTF-8">
          <title>${fileName} Önizleme</title>
          <style>
            body {
              font-family: Arial, sans-serif;
            }
            h1 {
              color: #333;
            }
            pre {
              white-space: pre-wrap;
              word-wrap: break-word;
            }
            .btn {
              background-color: #007bff;
              color: #fff;
              border: none;
              padding: 10px 20px;
              text-align: center;
              text-decoration: none;
              display: inline-block;
              font-size: 16px;
              margin-bottom: 10px;
              cursor: pointer;
            }
            .btn:hover {
              background-color: #0056b3;
            }
          </style>
        </head>
        <body>
          <a href="/cysfsylog" class="btn">Geri</a>
          <a href="/download?file=${encodeURIComponent(fileName)}" class="btn">İndir</a>
          <h1>${fileName} Önizleme</h1>
          <pre>${data}</pre>
        </body>
      </html>
    `);
  });
});

// İndirme rotası
app.get('/download', (req, res) => {
  const fileName = req.query.file;
  const filePath = path.join(directoryPath, fileName);

  // Dosyanın mevcut olup olmadığını kontrol et
  fs.access(filePath, fs.constants.F_OK, (err) => {
    if (err) {
      console.error(err);
      const response = `
        <!DOCTYPE html>
        <html lang="tr">
        <head>
          <meta charset="UTF-8">
          <meta name="viewport" content="width=device-width, initial-scale=1.0">
          <link rel="icon" type="image/png" href="/img/WARNING.png">
          <title>404 Not Found</title>
          <style>
            body {
              font-family: Arial, sans-serif;
              background-color: #ffffff;
              margin: 0;
              padding: 0;
              display: flex;
              justify-content: center;
              align-items: center;
              height: 100vh;
            }
            
            .container {
              text-align: center;
            }
          
            img {
              max-width: 100%;
              height: auto;
            }
          </style>
        </head>
        <body>
          <div class="container">
            <img src="/404notfound.png" alt="404 Not Found">
          </div>
        </body>
        </html>
      `;
    
      return res.status(404).send(response);
    }

    // Dosya mevcut ise, stat bilgisini al
    fs.stat(filePath, (err, stats) => {
      if (err) {
        console.error(err);
        return res.status(500).send('Sunucu hatası.');
      }

      // Dosya ise indir
      if (stats.isFile()) {
        res.download(filePath, fileName);
      } else {
        res.status(404).send('Belirtilen yol bir dosya değil.');
      }
    });
  });
});






app.get('/warn/:label1/:label2/:label3/:label4/', (req, res) => {
  const { label1, label2, label3, label4 } = req.params;
  const command = `"C:\\Program Files (x86)\\CaYaLab\\Notif\\Notif.exe" -texthere${label1} -texthere${label2} -texthere${label3} -texthere${label4}`;

  exec(command, (error, stdout, stderr) => {
    if (error) {
      console.error(`Error executing command: ${error}`);
      //return res.status(500).send('Internal Server Error');
      return res.send('Command executed successfully');
    }
    console.log(`Command executed: ${command}`);
    res.send('Command executed successfully');
  });
});

app.get('/check', (req, res) => {
  res.send('Running this server');
});

app.get('/shutdown', (req, res) => {
  //const command = `start`;
  const command = `shutdown /s /f /t 60 /c "Bilgisayar 1 dakika içerisinde kapatılıyor. Lütfen kaydedilmemiş çalışmalarınızı kaydedin."`;

  exec(command, (error, stdout, stderr) => {
    if (error) {
      console.error(`Error executing command: ${error}`);
      //return res.status(500).send('Internal Server Error');
      return res.send('Command executed successfully');
    }
    console.log(`Command executed: ${command}`);
    res.send('Command executed successfully');
  });
});


app.get('/forceshutdown', (req, res) => {
  //const command = `start`;

  // Sunucunun kullanıcı adını al
  const username = os.userInfo().username;

  const response = `
  <!DOCTYPE html>
  <html lang="tr">
  <head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Anında bilgisayar kapatma sistemi</title>
    <style>
      body {
        font-family: Arial, sans-serif;
        background-color: #f0f0f0;
        margin: 0;
        padding: 0;
        display: flex;
        justify-content: center;
        align-items: center;
        height: 100vh;
      }
      .container {
        text-align: center;
        padding: 20px;
        background-color: #fff;
        border-radius: 10px;
        box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
      }
      h1 {
        color: #333;
        font-size: 2rem;
        margin-bottom: 20px;
      }
      p {
        color: #666;
        font-size: 1rem;
      }
    </style>
  </head>
  <body>
    <div class="container">
      <h1>${username} ADLI BİLGİSAYAR BAŞARILI BİR ŞEKİLDE KAPATMA İŞLEMİ UYGULANMIŞTIR.</h1>
      <p>Bu sayfayı yenilediğinizde veya kapattığınızda karşı bilgisayar tekrar açılana kadar kullanılamazdır.</p>
    </div>
  </body>
  </html>
  `;

  res.send(response);

  
  //const command = `start`;
  const command = `shutdown /s /f /t 1 /c "Bilgisayar kapatılıyor..."`;

  exec(command, (error, stdout, stderr) => {
    if (error) {
      console.error(`Error executing command: ${error}`);
      //return res.status(500).send('Internal Server Error');
      //return res.send('Command executed successfully');
      return
    }
    console.log(`Command executed: ${command}`);
    //res.send('Command executed successfully');
  });
});



app.get('/forcerestart', (req, res) => {
  //const command = `start`;

  // Sunucunun kullanıcı adını al
  const username = os.userInfo().username;

  const response = `
  <!DOCTYPE html>
  <html lang="tr">
  <head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Anında bilgisayar kapatma sistemi</title>
    <style>
      body {
        font-family: Arial, sans-serif;
        background-color: #f0f0f0;
        margin: 0;
        padding: 0;
        display: flex;
        justify-content: center;
        align-items: center;
        height: 100vh;
      }
      .container {
        text-align: center;
        padding: 20px;
        background-color: #fff;
        border-radius: 10px;
        box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
      }
      h1 {
        color: #333;
        font-size: 2rem;
        margin-bottom: 20px;
      }
      p {
        color: #666;
        font-size: 1rem;
      }
    </style>
  </head>
  <body>
    <div class="container">
      <h1>${username} ADLI BİLGİSAYAR BAŞARILI BİR ŞEKİLDE YENİDEN BAŞLATMA İŞLEMİ UYGULANMIŞTIR.</h1>
      <p>Bu sayfayı yenilediğinizde veya kapattığınızda karşı bilgisayar tekrar açılana kadar kullanılamazdır.</p>
    </div>
  </body>
  </html>
  `;

  res.send(response);

  
  const command = `shutdown /r /f /t 1 /c "Bilgisayar yeniden başlatılıyor..."`;
  //const command = `start`;

  exec(command, (error, stdout, stderr) => {
    if (error) {
      console.error(`Error executing command: ${error}`);
      //return res.status(500).send('Internal Server Error');
      //return res.send('Command executed successfully');
      return
    }
    console.log(`Command executed: ${command}`);
    //res.send('Command executed successfully');
  });
});










////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


// Function to convert binary (01) format to string
function binaryToString(binary) {
  let str = '';
  for (let i = 0; i < binary.length; i += 8) {
      // Extract 8-bit binary value
      const binaryValue = binary.substr(i, 8);
      // Convert binary value to decimal
      const decimalValue = parseInt(binaryValue, 2);
      // Convert decimal value to character
      const char = String.fromCharCode(decimalValue);
      // Append character to result
      str += char;
  }
  return str;
}



// Function to convert string to binary (01) format
function stringToBinary(str) {
  let binary = '';
  for (let i = 0; i < str.length; i++) {
      // Get ASCII value of character
      const asciiValue = str.charCodeAt(i);
      // Convert ASCII value to binary (8-bit format)
      const binaryValue = asciiValue.toString(2).padStart(8, '0');
      // Append binary value to result
      binary += binaryValue;
  }
  return binary;
}

// Route to handle adding an item to whitelist
app.get('/whitelist/add/:item', (req, res) => {
  const item = decodeURIComponent(req.params.item); // URL'den gelen değeri uygun biçime dönüştürme

  // Convert each character of the item to binary format
  let binaryItem = '';
  for (let i = 0; i < item.length; i++) {
      const charBinary = stringToBinary(item[i]);
      binaryItem += charBinary + ' '; // Her karakterin ardından bir boşluk ekleniyor
  }

  addItemToFile('dataW.txt', binaryItem); // Tüm ikili formatlı karakterler dosyaya ekleniyor
  res.status(200).send('Item added to whitelist successfully');
});







// Route to handle adding an item to blacklist
app.get('/blacklist/add/:item', (req, res) => {
  const item = decodeURIComponent(req.params.item);

  let binaryItem = '';
  for (let i = 0; i < item.length; i++) {
      const charBinary = stringToBinary(item[i]);
      binaryItem += charBinary + ' ';
  }

  addItemToFile('dataB.txt', binaryItem);
  res.status(200).send('Item added to blacklist successfully');
});






// Function to save data to file
function addItemToFile(filename, item) {
  try {
      fs.appendFileSync(filename, item + '\n'); // Satır sonu ekleniyor
      console.log(`Item added to ${filename}`);
  } catch (err) {
      console.error(`Error adding item to ${filename}:`, err);
  }
}




// Route to handle deleting an item from whitelist
app.get('/whitelist/delete/:item', (req, res) => {
  const item = decodeURIComponent(req.params.item);

  let binaryItem = '';
  for (let i = 0; i < item.length; i++) {
      const charBinary = stringToBinary(item[i]);
      binaryItem += charBinary + ' '; // Her karakterin ardından bir boşluk ekleniyor
  }

  deleteItemFromFile('dataW.txt', binaryItem); // Silinecek öğeyi doğrudan ikili formatta silmek için
  res.status(200).send('Item deleted from whitelist successfully');
});

// Route to handle deleting an item from blacklist
app.get('/blacklist/delete/:item', (req, res) => {
  const item = decodeURIComponent(req.params.item);

  let binaryItem = '';
  for (let i = 0; i < item.length; i++) {
      const charBinary = stringToBinary(item[i]);
      binaryItem += charBinary + ' '; // Her karakterin ardından bir boşluk ekleniyor
  }

  deleteItemFromFile('dataB.txt', binaryItem); // Silinecek öğeyi doğrudan ikili formatta silmek için
  res.status(200).send('Item deleted from blacklist successfully');
});



// Function to delete an item from file
function deleteItemFromFile(filename, item) {
  try {
      let data = fs.readFileSync(filename, 'utf8');
      let dataArray = data.split('\n');

      // Silinecek öğenin ikili formattaki karşılığını bul ve diziden çıkar
      let found = false;
      for (let i = 0; i < dataArray.length; i++) {
          if (dataArray[i].trim() === item.trim()) {
              dataArray.splice(i, 1);
              found = true;
              break;
          }
      }

      if (!found) {
          console.log(`Item ${item} not found in ${filename}`);
          return;
      }

      data = dataArray.join('\n');
      fs.writeFileSync(filename, data); // Dosyaya veriyi yeniden yaz
      console.log(`Item deleted from ${filename}`);
  } catch (err) {
      console.error(`Error deleting item from ${filename}:`, err);
  }
}
















//////





let isUserConnected = false;

app.get('/watch', (req, res) => {
  const username = os.userInfo().username;
  if (!isUserConnected) {
      isUserConnected = true;
      const response = `
      <!DOCTYPE html>
      <html lang="tr">
      <head>
          <meta charset="UTF-8">
          <meta name="viewport" content="width=device-width, initial-scale=1.0">
          <title>Karşı Bilgisayar Bağlantısı</title>
          <!-- Bootstrap CSS -->
          <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0-alpha1/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-eOJMYsd53ii+TbDE8evXhqTt43PzMH0s7U7o7FOn7R/E1N6az+JJvD3bWxJxN+td" crossorigin="anonymous">
          <style>
            body {
              background-color: #f0f0f0;
              font-family: Arial, sans-serif;
              display: flex;
              justify-content: center;
              align-items: center;
              height: 100vh;
              margin: 0;
            }
          
            #screenshotContainer {
              text-align: center;
              background-color: #fff;
              border-radius: 10px;
              box-shadow: 0 4px 6px rgba(0, 0, 0, 0.1);
              padding: 20px;
              max-width: 80%;
              width: 100%;
              box-sizing: border-box;
              overflow: auto; /* Değişiklik: auto olarak ayarlandı, taşan içeriği gösterir */
            }
          
            #screenshot {
              max-width: 100%;
              height: auto;
            }
          
            .btn-container {
              margin-top: 20px;
            }
          
            .program-list {
              list-style: none;
              padding: 0;
              margin: 20px 0;
              text-align: left;
              max-height: 300px;
              overflow-y: auto;
            }
          
            .program-list li {
              margin-bottom: 10px;
              display: flex;
              justify-content: space-between;
              align-items: center;
            }
          
            .program-name {
              flex: 1;
              overflow: hidden;
              text-overflow: ellipsis;
              white-space: nowrap;
              margin-right: 10px;
            }
          
            .container {
              display: flex;
              flex-direction: column;
              justify-content: center;
              align-items: center;
              height: 100vh;
              margin: 0 auto;
              padding: 20px;
              box-sizing: border-box;
              max-width: 800px;
              width: 100%;
            }
          
            @media (max-width: 768px) {
              .container {
                padding: 10px;
              }
            }
          
            @media (max-width: 576px) {
              .container {
                max-width: calc(100% - 20px); /* Maksimum genişlik 100% - 20px olacak */
              }
            }
          </style>
        
      </head>
      <body>
        <div class="container">
            <div id="screenshotContainer" class="mt-5">
                <h1 class="mb-4">${username} Ekran Görüntüsü</h1>
                <img id="screenshot" src="" alt="Ekran Görüntüsü">
                <div class="btn-container">
                    <button id="shutdownBtn" class="btn btn-primary">Karşı bilgisayarı kapat</button>
                    <button id="forcerestart" class="btn btn-danger">Karşı bilgisayarı yeniden başlat</button>
                    <button id="chat" class="btn btn-danger">Karşı bilgisayar ile sohbet başlat</button>
                </div>

                <!-- Program listesi -->
                <h2 class="mb-3 mt-5">Açık Programlar</h2>
                <ul id="programList" class="program-list"></ul>
                <div class="btn-container">
                    <button id="reload" class="btn btn-primary">Listeyi yenile</button>
                </div>
            </div>
        </div>

        <!-- Bootstrap JS and Socket.io -->
        <script src="https://code.jquery.com/jquery-3.6.0.min.js" integrity="sha384-KyZXEAg3QhqLMpG8r+Knujsl5+z48P2jhk8bDfQW6m6qVgxyR+3Rykf2Ib3Zgk3C" crossorigin="anonymous"></script>
        <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0-alpha1/dist/js/bootstrap.bundle.min.js" integrity="sha384-..."></script>
        <script src="https://cdnjs.cloudflare.com/ajax/libs/socket.io/4.4.1/socket.io.js"></script>
          <script>
              const socket = io();
      
              socket.on('connect', () => {
                  console.log('Connected to server');
                  // Sunucuya ekran görüntüsü talebi gönder
                  socket.emit('request-screenshot');
                  // Program listesini iste
                  updateProgramList();
              });
      
              socket.on('disconnect', () => {
                  console.log('Disconnected from server');
              });
      
              socket.on('screenshot', (imgDataUrl) => {
                console.log('Received screenshot');
                // Ekran görüntüsünü görüntülemek için img elementine ata
                const screenshotImg = document.getElementById('screenshot');
                screenshotImg.src = imgDataUrl;
              });
            
      
              // Düğmelere tıklama olaylarını dinleme
              document.getElementById('shutdownBtn').addEventListener('click', () => {
                  socket.emit('force-shutdown'); // Sunucuya kapanma isteği gönder
                  window.location.href = '/forceshutdown';
              });
      
              document.getElementById('forcerestart').addEventListener('click', () => {
                  socket.emit('force-restart'); // Sunucuya kapatma isteği gönder
                  window.location.href = '/forcerestart';
              });

              document.getElementById('chat').addEventListener('click', () => {
                window.location.href = '/chatadminpanel';
              });

              document.getElementById('reload').addEventListener('click', () => {
                console.log('AAAA');
                socket.emit('request-program-list');
              });
              
      
              // Program listesini güncelleme işlevi
              function updateProgramList() {
                  socket.emit('request-program-list');
              }
      
              // Sunucudan gelen program listesini işleme ve HTML'e ekleme
              socket.on('program-list', (programList) => {
                  const programListContainer = document.getElementById('programList');
                  // Program listesini temizle
                  programListContainer.innerHTML = '';
                  // Her bir program için bir liste öğesi oluştur
                  programList.forEach(program => {
                      const listItem = document.createElement('li');
                      listItem.classList.add('d-flex', 'justify-content-between', 'align-items-center');
                      
                      // Program adını içeren bir span oluştur
                      const programNameSpan = document.createElement('span');
                      programNameSpan.textContent = program.name;
                      programNameSpan.classList.add('program-name');
                      listItem.appendChild(programNameSpan);
                      
                      // Kapatma butonunu oluştur
                      const closeButton = document.createElement('button');
                      closeButton.textContent = 'Kapat';
                      closeButton.classList.add('btn', 'btn-danger');
                      closeButton.addEventListener('click', () => {
                          closeProgram(program.name);
                      });
                      listItem.appendChild(closeButton);
      
                      // Listeye öğeyi ekle
                      programListContainer.appendChild(listItem);
                  });
              });
      
              // Programı kapatma işlevi
              function closeProgram(programName) {
                  socket.emit('close-program', programName);
              }
            
              
          </script>
      </body>
      </html>
      
      
            
      `;

      res.send(response);
  } else {
      res.redirect('/full');
  }
});


app.get('/full', (req, res) => {
  if (!isUserConnected) {
    res.redirect('/watch'); // Zaten bağlıysa /full sayfasına yönlendir
  } else {
    console.log('Full');
    //res.send('Zaten başka bir kullanıcı tarafından izlenmektedir. Lütfen karşı bağlantıyı kaptıp tekrar deneyiniz.'); // Bağlı olan bir kullanıcı varsa /full sayfasına yönlendir

    const response = `
        <!DOCTYPE html>
        <html lang="tr">
        <head>
          <meta charset="UTF-8">
          <meta name="viewport" content="width=device-width, initial-scale=1.0">
          <link rel="icon" type="image/png" href="/img/WARNING.png">
          <title>429 too many requests</title>
          <style>
            body {
              font-family: Arial, sans-serif;
              background-color: #ffffff;
              margin: 0;
              padding: 0;
              display: flex;
              justify-content: center;
              align-items: center;
              height: 100vh;
            }
            
            .container {
              text-align: center;
              max-width: 20%;
            }
          
            img {
              max-width: 100%;
              height: auto;
            }
          </style>
        </head>
        <body>
          <div class="container">
            <img src="/429.jpg" alt="Error 429 too many requests">
          </div>
        </body>
        </html>
      `;
    
      return res.status(429).send(response);
  }
});


let isLocked = false; // Kilidin durumunu izlemek için bir değişken

// Kilidi kontrol eden aralıklı işlev
const lockInterval = setInterval(() => {
    if (isLocked) {
        console.log('Locking input...');
        lockInput();
    }
}, 500); // Her 0.5 saniyede bir kontrol et (ayarlayabilirsiniz)






// Program listesini istemciye gönderme işlevi
function sendProgramListToClient(socket, programList) {
  socket.emit('program-list', programList);
}
















// SSE bağlantılarını ve konsol yönetimini takip eden bir nesne
const consoleConnections = [];

app.get('/chatadminpanel', (req, res) => {
  const command = `start chrome "http://localhost:4728/chat"`;
  exec(command, (error, stdout, stderr) => {
    if (error) {
      console.error(`Error executing command: ${error}`);
      return;
    }
    console.log(`Command executed: ${command}`);
  });

  res.send(generateHTML('Yönetici'));
});

app.get('/chat', (req, res) => {
  res.send(generateHTML('Kullanıcı'));
});

app.get('/events/:role', (req, res) => {
  const { role } = req.params;
  res.setHeader('Content-Type', 'text/event-stream');
  res.setHeader('Cache-Control', 'no-cache');
  res.setHeader('Connection', 'keep-alive');

  const originalConsoleLog = console.log; // Orijinal console.log'u sakla

  // Konsol çıktılarını her bir satırı istemciye gönder
  console.log = function(message) {
    res.write(`data: ${message}\n\n`);
    originalConsoleLog(message); // Orijinal console.log'u çağır
  };

  // Bağlantı sonlandığında istemciye bir mesaj gönder
  req.on('close', () => {
    console.log(`${role}: Bağlantı sonlandırıldı.`);
  });
});

app.post('/send-message/:role', express.json(), (req, res) => {
  const { role } = req.params;
  const { message } = req.body;
  
  if (!message) {
    return res.sendStatus(400);
  }

  console.log(`${role}: ${message}`);
  res.sendStatus(200);
});

function generateHTML(role) {
  let backButton = '';
  if (role === 'Yönetici') {
    backButton = `<a href="/watch" style="text-decoration: none; color: #007bff; font-size: 14px;">Geri</a>`;
  }

  return `
    <!DOCTYPE html>
    <html lang="tr">
    <head>
      <meta charset="UTF-8">
      <meta name="viewport" content="width=device-width, initial-scale=1.0">
      <title>Sohbet - Konsol Sistemi</title>
      <style>
        body {
          font-family: Arial, sans-serif;
          background-color: #f0f0f0;
          color: #333;
          margin: 0;
          padding: 0;
          display: flex;
          justify-content: center;
          align-items: center;
          height: 100vh;
        }
        .container {
          width: 400px;
          padding: 20px;
          background-color: #fff;
          border-radius: 8px;
          box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
        }
        .container h1 {
          font-size: 24px;
          margin-bottom: 20px;
          text-align: center;
          color: #333;
        }
        .console {
          height: 200px;
          overflow-y: auto;
          background-color: #f5f5f5;
          border: 1px solid #ddd;
          border-radius: 4px;
          padding: 10px;
          margin-bottom: 20px;
        }
        .message-form {
          display: flex;
        }
        .message-input {
          flex: 1;
          padding: 8px;
          border-radius: 4px;
          border: 1px solid #ddd;
          font-size: 16px;
          margin-right: 10px;
        }
        .submit-button {
          background-color: #007bff;
          color: #fff;
          border: none;
          border-radius: 4px;
          padding: 8px 16px;
          font-size: 16px;
          cursor: pointer;
        }
        .submit-button:hover {
          background-color: #0056b3;
        }
      </style>
    </head>
    <body>
      <div class="container">
        <h1>Konsol - Sohbet - ${role}</h1>
        ${backButton}
        <div class="console" id="console"></div>
      
        <form class="message-form" id="messageForm">
          <input type="text" class="message-input" id="messageInput" placeholder="Mesajınızı buraya yazın" autocomplete="off">
          <button type="submit" class="submit-button">Gönder</button>
        </form>
      </div>

      <script>
        const eventSource = new EventSource('/events/${role}');

        eventSource.onmessage = function(event) {
          const consoleElement = document.getElementById('console');
          consoleElement.innerHTML += event.data + '<br>';
          consoleElement.scrollTop = consoleElement.scrollHeight;
        };
      
        document.getElementById('messageForm').addEventListener('submit', function(event) {
          event.preventDefault();

          const messageInput = document.getElementById('messageInput');
          const message = messageInput.value;

          if (!message.trim()) {
            return;
          }

          fetch('/send-message/${role}', {
            method: 'POST',
            headers: {
              'Content-Type': 'application/json'
            },
            body: JSON.stringify({ message: message })
          });

          messageInput.value = '';
        });
      </script>
    </body>
    </html>
  `;
}


function retryFetch(url, options, maxRetries = 3, interval = 1000) {
  return new Promise((resolve, reject) => {
    let retries = 0;

    function doFetch() {
      fetch(url, options)
        .then(response => {
          if (response.ok) {
            resolve(response);
          } else {
            throw new Error('Network response was not ok');
          }
        })
        .catch(error => {
          retries++;
          console.error(`Error fetching ${url}. Retrying (${retries}/${maxRetries})`);
          if (retries < maxRetries) {
            setTimeout(doFetch, interval);
          } else {
            reject(error);
          }
        });
    }

    doFetch();
  });
}








          
          // // Kullanım örneği
          // retryFetch('/send-message/${role}', {
          //   method: 'POST',
          //   headers: {
          //     'Content-Type': 'application/json'
          //   },
          // })
          // .then(response => {
          //   // İstek başarılı oldu
          // })
          // .catch(error => {
          //   // İstek başarısız oldu ve yeniden deneme sınırına ulaşıldı
          //   console.error(role + ', Bağlantı sorunu nedeniyle sohbete mesaj yazamaz veya göremez.');
          // });






io.on('connection', (socket) => {
  console.log('Client connected');
  // Sunucuya program listesi talebi gönder
  socket.emit('request-program-list');

  // Ekran görüntüsü alıp istemciye gönderme
  const sendScreenshot = () => {
    screenshot().then(img => {
      // Resmi küçültme işlemi
      Jimp.read(img)
        .then(image => {
          return image.resize(800, Jimp.AUTO) // Genişliği 800 piksele küçült, yükseklik otomatik olarak orantılı şekilde ayarlanır
            .getBufferAsync(Jimp.MIME_JPEG);
        })
        .then(resizedImg => {
          const imgDataUrl = `data:image/jpeg;base64,${resizedImg.toString('base64')}`;
          io.to(socket.id).emit('screenshot', imgDataUrl); // Sadece bağlı olan istemciye gönder
        })
        .catch(err => {
          console.error('Error resizing screenshot:', err);
          io.to(socket.id).emit('error', 'Error resizing screenshot'); // Hata durumunda istemciye hata gönder
        });
    }).catch(err => {
      console.error('Error capturing screenshot:', err);
      io.to(socket.id).emit('error', 'Error capturing screenshot'); // Hata durumunda istemciye hata gönder
    });
  };




  const interval = setInterval(sendScreenshot, 1000); // Her saniye ekran görüntüsü gönder



  // Klavye ve fareyi kilitleme işlemini gerçekleştiren olay
  socket.on('lock-input', () => {
    if (!isLocked) {
        console.log('Locking input...');
        lockInput();
    }
  });

  // Klavye ve fareyi açma işlemini gerçekleştiren olay
  socket.on('unlock-input', () => {
    if (isLocked) {
        console.log('Unlocking input...');
        unlockInput();
    }
  });

  socket.on('force-shutdown', () => {
    console.log('Sunucu kapatılma isteği gönderildi.');
  });

  socket.on('force-restart', () => {
    console.log('Sunucu yeniden başlatılma isteği gönderildi.');
  });

  // Sunucu tarafında program kapatma işlemi için
  socket.on('close-program', (programName) => {
    console.log(`Kapatma isteği alındı: ${programName}`);
    
    // Program kapatma işlemi
    closeProgram(socket, programName);
  });



  socket.on('program-list', (programList) => {
    const programListContainer = document.getElementById('programList');
    // Program listesini temizle
    programListContainer.innerHTML = '';
    // Her bir program için bir liste öğesi oluştur
    programList.forEach(program => {
        const listItem = document.createElement('li');
        listItem.textContent = program.name;
        programListContainer.appendChild(listItem);
    });
  });



  // Sunucu tarafında program listesini gönderme işlevi
  socket.on('request-program-list', () => {
    console.log('Program listesi talebi alındı');
    // Program listesini al ve istemciye gönder
    getProgramList(socket); // socket parametresini ekledik
  });



  // Bağlantı kesildiğinde
  socket.on('disconnect', () => {
    console.log('Client disconnected');
    isUserConnected = false; // Kullanıcı bağlantısı kesildiğinde bayrağı sıfırla
});
});

// Program kapatma işlevi
function closeProgram(socket, programName) { // Burada socket parametresini closeProgram fonksiyonuna ekleyin
  const command = `taskkill /IM "${programName}" /F`;
  exec(command, (error, stdout, stderr) => {
      if (error) {
          console.error(`Error: ${error.message}`);
          return;
      }
      if (stderr) {
          console.error(`Error: ${stderr}`);
          return;
      }
      console.log(`Program kapatıldı: ${programName}`);
      // Program kapatıldıktan sonra program listesini güncelleme sinyali gönder
      updateProgramList(socket); // Burada socket parametresini kullanarak updateProgramList fonksiyonunu çağırın
  });
}

// Program listesini güncelleme işlevi
function updateProgramList(socket) {
  socket.emit('request-program-list');
}

// Program listesini alma işlevi
function getProgramList(socket) {
  const command = 'tasklist /fo csv /nh';
  exec(command, (error, stdout, stderr) => {
      if (error) {
          console.error(`Error: ${error.message}`);
          return;
      }
      if (stderr) {
          console.error(`Error: ${stderr}`);
          return;
      }
      // Çıktıyı parçalayarak program listesini oluştur
      const programs = parseProgramList(stdout);
      // İstemciye program listesini gönder
      sendProgramListToClient(socket, programs); // socket parametresini ekledik
  });
}



// Çıktıyı işleyerek program listesini oluşturma işlevi
function parseProgramList(stdout) {
  const lines = stdout.trim().split('\n');
  return lines.map(line => {
      const parts = line.split('","');
      return {
          name: parts[0].replace('"', ''),
          pid: parts[1],
          // Diğer bilgileri eklemek için buraya bakabilirsiniz
      };
  });
}




// Klavye ve fareyi kilitleme işlemi
function lockInput() {
  //isLocked = true; // Kilidi kapat
  // Fareyi rastgele bir konuma taşı
  //robot.moveMouse(0, 0);
  // O konumda bir tıklama simüle et
  //robot.mouseClick();
  // Klavyeyi kilitliyor
  //robot.keyToggle('shift', 'down');
  //robot.keyToggle('capslock', 'down');


  exec('powershell -Command "(Get-PnpDevice -PresentOnly).InstanceId | ForEach { Disable-PnpDevice -InstanceId $_ -Confirm:$false }"', (error, stdout, stderr) => {
    if (error) {
        console.error(`Hata oluştu: ${error.message}`);
        return;
    }
    if (stderr) {
        console.error(`Hata çıktısı: ${stderr}`);
        return;
    }
    console.log('Tüm aygıtlar başarıyla kapatıldı.');
  })
}

// Klavye ve fareyi açma işlemi
function unlockInput() {
  isLocked = false; // Kilidi aç
  // Kilitlemek için basılan tuşları bırak
  //robot.keyToggle('shift', 'up');
  //robot.keyToggle('capslock', 'up');


  exec('powershell -Command "(Get-PnpDevice -PresentOnly).InstanceId | ForEach { Enable-PnpDevice -InstanceId $_ -Confirm:$false }"', (error, stdout, stderr) => {
    if (error) {
        console.error(`Hata oluştu: ${error.message}`);
        return;
    }
    if (stderr) {
        console.error(`Hata çıktısı: ${stderr}`);
        return;
    }
    console.log('Tüm aygıtlar başarıyla açıldı.');
  });
}






////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////








///////////////////404SYSTEM///////////////////////////DONTUSE//////////////////




// /cysfsy yoluna gelen GET isteklerini /cysfsy/ yönlendirir
app.get('/cysfsy', (req, res) => {
  //res.redirect('/cysfsy/');
  //const imagePath = __dirname + '/img/404notfound.png';

  const response = `
  <!DOCTYPE html>
  <html lang="tr">
  <head>
  <meta charset="UTF-8">
  <meta name="viewport" content="width=device-width, initial-scale=1.0">
  <link rel="icon" type="image/png" href="/img/WARNING.png">
  <title>404 Not Found</title>
  <style>
    body {
      font-family: Arial, sans-serif;
      background-color: #ffffff;
      margin: 0;
      padding: 0;
      display: flex;
      justify-content: center;
      align-items: center;
      height: 100vh;
    }
    
    .container {
      text-align: center;
    }
  
    img {
      max-width: 100%;
      height: auto;
    }
  </style>
  </head>
  <body>
    <div class="container">
      <img src="/404notfound.png" alt="404 Not Found">
    </div>
  </body>
  </html>
  
  `;

  res.send(response);

  //res.sendFile(imagePath);
});

// 404 Hatası İşleyicisi
app.use((req, res, next) => {
  //res.status(404).send('404 Not Found');
  //const imagePath = __dirname + '/img/404notfound.png';

  
  const response = `
  <!DOCTYPE html>
  <html lang="tr">
  <head>
  <meta charset="UTF-8">
  <meta name="viewport" content="width=device-width, initial-scale=1.0">
  <link rel="icon" type="image/png" href="/img/WARNING.png">
  <title>404 Not Found</title>
  <style>
    body {
      font-family: Arial, sans-serif;
      background-color: #ffffff;
      margin: 0;
      padding: 0;
      display: flex;
      justify-content: center;
      align-items: center;
      height: 100vh;
    }
    
    .container {
      text-align: center;
    }
  
    img {
      max-width: 100%;
      height: auto;
    }
  </style>
  </head>
  <body>
    <div class="container">
      <img src="/404notfound.png" alt="404 Not Found">
    </div>
  </body>
  </html>
  
  `;

  res.send(response);

  //res.sendFile(imagePath);
});








server.listen(PORT, () => {
  console.log(`Server is running on port ${PORT}`);
});



server2.listen(PORT2, () => {
  console.log(`Server is running on port ${PORT2}`);
});


// Dinleme işlemi sırasında oluşan hataları ele al
server.on('error', (err) => {
  if (err.code !== 'EADDRINUSE') {
    console.error(err);
  }
});

server2.on('error', (err) => {
  if (err.code !== 'EADDRINUSE') {
    console.error(err);
  }
});




// const server1 = app.listen(PORT, () => {
//   console.log(`Server is running on port ${PORT}`);
// });

// // İkinci portta dinleme işlemi
// const server2 = app.listen(PORT2, () => {
//   console.log(`Server is running on port ${PORT2}`);
// });

// // Dinleme işlemi sırasında oluşan hataları ele al
// server1.on('error', (err) => {
//   if (err.code !== 'EADDRINUSE') {
//     console.error(err);
//   }
// });

// server2.on('error', (err) => {
//   if (err.code !== 'EADDRINUSE') {
//     console.error(err);
//   }
// });






