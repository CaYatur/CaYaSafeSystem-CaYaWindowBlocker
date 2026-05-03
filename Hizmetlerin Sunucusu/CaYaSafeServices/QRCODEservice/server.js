const express = require("express");
const bodyParser = require("body-parser");
const cors = require("cors");
const { MongoClient } = require("mongodb");

require("dotenv").config();

const app = express();
const PORT = process.env.PORT || 3001;

app.use(express.static("public"));
app.use(bodyParser.json());
app.use(cors());

const mongoURI = process.env.MONGODB_URI;

app.get("/", (req, res) => {
  res.sendFile(path.join(__dirname, "public", "index.html"));
});




app.post("/setAllSavedDevicesTrue", async (req, res) => {
  try {
    const client = await MongoClient.connect(mongoURI, {
      useNewUrlParser: true,
      useUnifiedTopology: true,
    });

    const db = client.db("CaYa");
    const collection = db.collection("CaYa");

    // Sadece çerezlerdeki cihazları bul ve isConnected değerini true yap
    const savedDevices = getCookies("savedDevices") || [];
    for (const device of savedDevices) {
      const existingDevice = await collection.findOne({ _id: device._id });

      if (existingDevice) {
        await collection.updateOne(
          { _id: device._id },
          { $set: { isConnected: true } },
        );
      }
    }

    res.status(200).send("Tüm kaydedilen cihazlar başarıyla aktif yapıldı.");

    client.close();
  } catch (error) {
    console.error("Error setting all saved devices true:", error);
    res.status(500).send("Bir hata oluştu. Lütfen tekrar deneyin.");
  }
});



app.post("/saveToDatabase", async (req, res) => {
  const qrContent = req.body.qrContent;

  try {
    const client = await MongoClient.connect(mongoURI, {
      useNewUrlParser: true,
      useUnifiedTopology: true,
    });

    const db = client.db("CaYa");
    const collection = db.collection("CaYa");

    const ipAddressMatch = qrContent.match(/IpAddress:([^ ]+)/);
    const ipAddress = ipAddressMatch ? ipAddressMatch[1] : null;

    if (ipAddress) {
      const existingRecord = await collection.findOne({
        ipAddress: ipAddress,
      });

      if (!existingRecord) {
        //const newRecord = {
        //  ipAddress: ipAddress,
        //  isConnected: true,
        //};

        //await collection.insertOne(newRecord);

        res.status(200).send("Veri başarıyla kaydedildi.");
      } else {
        await collection.updateOne(
          { ipAddress: ipAddress },
          { $set: { isConnected: true } },
        );

        res.status(200).send("Veri başarıyla güncellendi.");
      }
    } else {
      res.status(400).send("QR kodunda IP adresi bulunamadı.");
    }

    client.close();
  } catch (error) {
    console.error("Error saving to database:", error);
    res.status(500).send("Bir hata oluştu. Lütfen tekrar deneyin.");
  }
});

app.post("/openRemoteDevice", async (req, res) => {
  const ipAddress = req.body.ipAddress;

  try {
    const client = await MongoClient.connect(mongoURI, {
      useNewUrlParser: true,
      useUnifiedTopology: true,
    });

    const db = client.db("CaYa");
    const collection = db.collection("CaYa");

    const existingRecord = await collection.findOne({ ipAddress: ipAddress });

    if (existingRecord) {
      await collection.updateOne(
        { ipAddress: ipAddress },
        { $set: { isConnected: true } },
      );
      res.status(200).send("Uzaktan cihaz başarıyla açıldı.");
    } else {
      res
        .status(400)
        .send("Belirtilen IP adresi ile eşleşen bir kayıt bulunamadı.");
    }

    client.close();
  } catch (error) {
    console.error("Error opening remote device:", error);
    res.status(500).send("Bir hata oluştu. Lütfen tekrar deneyin.");
  }
});



app.listen(PORT, () => {
  console.log(`Server is running at http://localhost:${PORT}`);
});






















const http = require('http');
const WebSocket = require('ws');


const server = http.createServer(app);
const wss = new WebSocket.Server({ server });

let currentPassword = generatePassword();
let lastUpdateTime = Date.now();
const updateInterval = 30000; // Parola güncelleme süresi (milisaniye cinsinden)
let passwordInterval = setInterval(updatePassword, updateInterval);

function generatePassword() {
  return Math.floor(100000 + Math.random() * 900000);
}

function updatePassword() {
  currentPassword = generatePassword();
  lastUpdateTime = Date.now();
  console.log('Generated password: %d', currentPassword);

  wss.clients.forEach((client) => {
    if (client.readyState === WebSocket.OPEN) {
      const nextUpdateTime = lastUpdateTime + updateInterval;
      client.send(JSON.stringify({ password: currentPassword.toString(), nextUpdateTime, updateInterval }));
    }
  });
}

wss.on('connection', function connection(ws) {
  ws.on('message', function incoming(message) {
    console.log('Received: %s', message);
  });

  const nextUpdateTime = lastUpdateTime + updateInterval;
  ws.send(JSON.stringify({ password: currentPassword.toString(), nextUpdateTime, updateInterval }));
});

wss.on('close', () => {
  if (wss.clients.size === 0) {
    clearInterval(passwordInterval);
    passwordInterval = null;
  }
});

app.get('/', (req, res) => {
  res.sendFile(__dirname + '/index.html');
});

app.get('/code', (req, res) => {
  const nextUpdateTime = lastUpdateTime + updateInterval;
  res.send(JSON.stringify({ password: currentPassword.toString(), nextUpdateTime, updateInterval }));
});


server.listen(303, () => {
  console.log('Server is running on port 303');
});








