const express = require("express");
const bodyParser = require("body-parser");
const cors = require("cors");
const path = require("path");

const app = express();
const PORT = process.env.PORT || 80;

app.use(express.static("public"));
app.use(bodyParser.json());
app.use(cors());

require("dotenv").config();

// Kullanıcı bilgilerini depolamak için basit bir dizi kullanıyoruz
const connectedUsers = [];

app.get("/", (req, res) => {
    const clientIP = req.headers["x-forwarded-for"] || req.connection.remoteAddress;
    
    console.log("Yeni bir kullanıcı bağlandı. IP Adresi: " + clientIP);
    
    res.sendFile(path.join(__dirname, "index.html"));
});

// Kullanıcı bağlandığında bilgileri ekleyen endpoint
app.post("/connect", (req, res) => {
    const { username } = req.body;

    if (username) {
        connectedUsers.push(username);
        res.status(200).json({ message: "Bağlandı", connectedUsers });
    } else {
        res.status(400).json({ error: "Geçersiz istek" });
    }
});

// Bağlı olan kullanıcıları gösteren endpoint
app.get("/connected-users", (req, res) => {
    res.status(200).json({ connectedUsers });
});

app.listen(PORT, () => {
    console.log(`Server is running at http://localhost:${PORT}`);
});
