# CaYaSafeSystem — CaYaWindowBlocker

> **Ebeveyn/Kurumsal Denetim & Güvenli Tarayıcı Sistemi**  
> Windows tabanlı, çok katmanlı bir içerik filtreleme, pencere engelleme ve uzaktan yönetim platformu.

---

## İçindekiler

- [Proje Hakkında](#proje-hakkında)
- [Mimari Genel Bakış](#mimari-genel-bakış)
- [Bileşenler](#bileşenler)
  - [Sunucu Servisleri](#sunucu-servisleri)
  - [Masaüstü Uygulamaları (C#)](#masaüstü-uygulamaları-c)
  - [Python Araçları](#python-araçları)
  - [Tarayıcı Eklentisi — CaYaBlockWeb](#tarayıcı-eklentisi--cayablockweb)
- [Çalışma Akışı](#çalışma-akışı)
- [Kurulum & Yapılandırma](#kurulum--yapılandırma)
- [Dizin Yapısı](#dizin-yapısı)
- [Gereksinimler](#gereksinimler)
- [Güvenlik Notları](#güvenlik-notları)
- [Lisans](#lisans)

---

## Proje Hakkında

CaYaSafeSystem; okul, aile veya kurum ortamlarında **yetkisiz içeriklere ve uygulamalara erişimi engellemek**, sistem güvenliğini korumak ve uzaktan denetim sağlamak amacıyla geliştirilmiş açık kaynaklı bir güvenlik platformudur.

**Temel Yetenekler:**

- Hosts dosyası tabanlı site engelleme (kara/beyaz liste)
- Pencere başlığı bazlı uygulama kapatma
- Chrome eklenti bütünlüğünü koruma (CaYaBlockWeb)
- QR kod ile cihaz bağlantı doğrulama (MongoDB)
- WebSocket tabanlı zaman sınırlı iki faktörlü kimlik doğrulama (2FA)
- Uzaktan ekran görüntüsü, fare/klavye kontrolü (Socket.IO)
- Yönetici paneli ile kara/beyaz liste yönetimi
- Pencere/aktivite günlüğü

> ⚠️ **Bu yazılım yalnızca kendi cihazlarınızda ya da yazılı izin aldığınız cihazlarda kullanılabilir. İzinsiz cihazlarda kullanmak yasalara aykırıdır.**

---

## Mimari Genel Bakış

```
┌─────────────────────────────────────────────────────────────────┐
│                        SUNUCU TARAFI                            │
│                                                                 │
│  ┌─────────────┐  ┌──────────────┐  ┌──────────────────────┐  │
│  │ SAFEservice │  │ QRCODEservice│  │    TwoFactorAuth     │  │
│  │  (port 80)  │  │  (port 3001) │  │     (port 303)       │  │
│  │ Engel listeleri│ │ MongoDB/Cihaz│  │ WebSocket 2FA Token │  │
│  └──────┬──────┘  └──────┬───────┘  └──────────┬───────────┘  │
│         │                │                       │              │
│  ┌──────┴─────────────────┴───────────────────────┴──────────┐  │
│  │              Ana Sunucu (port 4728 / 80)                  │  │
│  │  Uzak kontrol · Ekran görüntüsü · Dosya yönetimi          │  │
│  └───────────────────────────────────────────────────────────┘  │
└────────────────────────────┬────────────────────────────────────┘
                             │ HTTP / WebSocket
┌────────────────────────────▼────────────────────────────────────┐
│                      İSTEMCİ TARAFI (Windows)                   │
│                                                                 │
│  ┌──────────────────────────────────────────────────────────┐  │
│  │       SiteEngelleyiciArayüz  (Ana Uygulama - C#)         │  │
│  │  Lock ekranı · QR kod · 2FA doğrulama · Ayarlar         │  │
│  └──────────────────────────────────────────────────────────┘  │
│  ┌──────────────┐  ┌──────────────┐  ┌─────────────────────┐  │
│  │  AdminPanel  │  │  SiteBlocker │  │    ekengelleme      │  │
│  │  (C# WinForms)│ │  (Python)    │  │     (Python)        │  │
│  │ B/K liste yön.│ │ hosts engeli │  │  Pencere engeli     │  │
│  └──────────────┘  └──────────────┘  └─────────────────────┘  │
│  ┌──────────────┐  ┌──────────────┐  ┌─────────────────────┐  │
│  │   autostart  │  │  AutoOpenCY  │  │      svpgcc         │  │
│  │   (Python)   │  │   (Python)   │  │     (Python)        │  │
│  │ Ana proc izle│  │ Watchdog #1  │  │   Watchdog #2       │  │
│  └──────────────┘  └──────────────┘  └─────────────────────┘  │
│  ┌──────────────┐  ┌──────────────────────────────────────┐    │
│  │    antiex    │  │       CaYaBlockWeb (Chrome Eklentisi) │    │
│  │  (Python)    │  │   Tarayıcı içi site/içerik engeli    │    │
│  │ Eklenti koru.│  └──────────────────────────────────────┘    │
│  └──────────────┘                                               │
└─────────────────────────────────────────────────────────────────┘
```

---

## Bileşenler

### Sunucu Servisleri

`Hizmetlerin Sunucusu/CaYaSafeServices/`

| Servis | Port | Teknoloji | Açıklama |
|--------|------|-----------|----------|
| **SAFEservice** | 80 | Node.js / Express | Engellenen site listelerini ve beyaz listeleri sunar. İstemci uygulamaları bu servisten engelleme listesini çeker. |
| **QRCODEservice** | 3001 | Node.js / Express / MongoDB | QR kod tarama yoluyla cihazların bağlantı durumunu (isConnected) MongoDB Atlas'a kaydeder ve günceller. |
| **TwoFactorAuth** | 303 | Node.js / WebSocket (ws) | Her 20 saniyede bir 8 haneli rastgele şifre üretir ve tüm bağlı istemcilere WebSocket üzerinden iletir. |
| **Server (Ana)** | 4728 / 80 | Node.js / Express / Socket.IO | Uzaktan ekran görüntüsü, fare/klavye kontrolü, dosya yükleme/silme ve komut çalıştırma endpoint'lerini barındırır. |

---

### Masaüstü Uygulamaları (C#)

`Uygulama/` — .NET 6 / Windows Forms

#### SiteEngelleyiciArayüz (Ana Uygulama)
Ana kilitleme ve yönetim arayüzü. Tüm diğer bileşenlerin merkezi.

| Form | Açıklama |
|------|----------|
| `Form1` | Ana pencere; uygulama başlatma, durum gösterimi, MongoDB bağlantısı |
| `Lock` | Kilit ekranı; QR kod ile cihaz doğrulama, 2FA token girişi, WebSocket bağlantısı |
| `Kilit` | Ek kilit formu |
| `settings` | Uygulama ayarları yönetimi |
| `DevPanel` | Geliştirici/hata ayıklama paneli |
| `DisableTaskManager` | Görev yöneticisi devre dışı bırakma |
| `CustomNotif` | Özel bildirim balonu |

**Yapılandırma:** `AppSettings.cs` içinde `MongoDbConnectionString` ve `TwoFactorAuthServerUrl` sabitlerini kendi sunucunuza göre ayarlayın.

#### AdminPanel
Yönetici tarafı kontrol paneli.

| Form | Açıklama |
|------|----------|
| `Form1` | Kara/Beyaz liste yönetimi (data.txt üzerinden) |
| `TwoFA` | Yönetici giriş doğrulaması; WebSocket ile sunucudan token alır ve PIN ile eşleştirir |
| `Log` | Aktivite loglarını görüntüleme |
| `Notif` | Bağlı cihazlara bildirim gönderme |
| `BlackWhiteListSystem` | Gelişmiş liste yönetim sistemi |

#### Diğer C# Projeleri

| Proje | Açıklama |
|-------|----------|
| `CheckUnAuth` | Yetkisiz işlem/bağlantı denetimi |
| `CYRS` | CaYa yeniden başlatma servisi |
| `CYSL` | CaYa servis başlatıcı |
| `CYSFSYsafeMode` | Güvenli mod yönetimi |
| `DisableSafeMode` | Windows güvenli modunu devre dışı bırakma |
| `EklentiNOTREMOVED` | Chrome eklentisinin kaldırılmamasını sağlayan servis |
| `FixCaYaSystem` | Sistem onarım aracı |
| `Notif` | Bildirim servisi |
| `ServerStartt` | Sunucu başlatıcı |
| `CaYaLabOffline` | Çevrimdışı CaYaLab modülü |
| `CaYaSafeSystemSetup` | Kurulum/setup projesi |
| `CYPCcheck` | PC bütünlük kontrol aracı |
| `CYPCFixer` | PC yapılandırma düzeltme aracı |

---

### Python Araçları

`Uygulama/`

#### SiteBlocker
**Hosts dosyası tabanlı site engelleyici.**  
SAFEservice'ten kara/beyaz listelerini çeker, Windows `hosts` dosyasını (`C:\Windows\System32\drivers\etc\hosts`) düzenleyerek DNS seviyesinde engelleme yapar. İkili (binary) kodlanmış liste verileri `dataB.txt` ve `dataW.txt` dosyalarından okunur.

```
SERVER_URL → Site listesi çek → hosts dosyasını güncelle → Döngü
```

#### ekengelleme
**Pencere başlığı tabanlı uygulama engelleyici.**  
SAFEservice'ten hedef pencere başlıklarını çeker. Açık Windows pencerelerini sürekli tarar; eşleşen pencereyi kapatır veya ilgili işlemi sonlandırır.

```
SERVER_URL → Engellenen başlıkları çek → Açık pencereleri tara → Eşleşeni kapat → Döngü
```

#### antiex
**Chrome eklenti bütünlük koruyucusu.**  
`C:\ProgramData\SecureSystem\Secure Preferences` ile Chrome'un gerçek `Secure Preferences` dosyasını karşılaştırır. Farklılık tespit edilirse Chrome'u kapatıp yetkili konfigürasyonu geri yükler.

```
ProgramData/SecureSystem/Secure Preferences ←karşılaştır→ Chrome/Secure Preferences
Farklıysa → Chrome'u kapat → Yetkili dosyayı geri yükle
```

#### autostart
Ana CaYaSafe32.exe sürecini izler; kapanırsa yeniden başlatır.

#### AutoOpenCY
**Watchdog #1.** `cyRUN.exe` ve `svpgcc.exe` süreçlerini sürekli izler; kapanırlarsa yeniden başlatır.

#### svpgcc
**Watchdog #2.** `cyRUN.exe` ve `AutoOpenCY.exe` süreçlerini izler; çapraz watchdog zinciri oluşturur.

#### Pvgc (LOG system)
**Aktivite kayıt sistemi.**  
Fare tıklaması algılandığında aktif pencere başlığını, zaman damgasıyla birlikte `C:\ProgramData\PVGCC\` altındaki günlük log dosyasına yazar. 20 günden eski loglar otomatik temizlenir.

#### pythonProject
Genel amaçlı yardımcı Python scripti.

---

### Tarayıcı Eklentisi — CaYaBlockWeb

`Uygulama/AAeklenti/` — Chrome Manifest V3

Tarayıcı tabanlı koruma katmanı. Yüklendikten sonra şeffaf biçimde arka planda çalışır.

**Özellikler:**

| Modül | Açıklama |
|-------|----------|
| `background.js` | Servis worker; site engelleme kararlarını koordine eder |
| `content.js` | Sayfa içi içerik analizi ve engelleme |
| `site_status_block_page.js` | Engellenen sayfa uyarı arayüzü |
| `site_status_site_report.js` | Site raporu/puanlama gösterimi |
| `sidebar_main.js` | Yan panel güvenlik özeti |
| `download_scan_popup.js` | İndirme tarama açılır penceresi |
| `topbar_crypto_block.js` | Kripto madenciliği engeli |
| `iframe_block_page.js` | Şüpheli iframe engeli |
| `whitelist.js` | Beyaz liste yönetimi |
| `settings.js` | Eklenti ayarları |

---

## Çalışma Akışı

### 1. Sistem Başlatma

```
Windows Başlar
    │
    ├─→ autostart.exe        → CaYaSafe32.exe'yi başlatır ve izler
    ├─→ AutoOpenCY.exe       → cyRUN.exe + svpgcc.exe izler (Watchdog #1)
    ├─→ svpgcc.exe           → cyRUN.exe + AutoOpenCY.exe izler (Watchdog #2)
    ├─→ SiteBlocker.exe      → Hosts dosyasını günceller
    ├─→ ekengelleme.exe      → Pencere tarama döngüsüne başlar
    ├─→ antiex.exe           → Chrome eklenti bütünlüğünü izler
    └─→ SiteEngelleyiciArayüz.exe → Ana kilit arayüzünü açar
```

### 2. Cihaz Kimlik Doğrulama (Lock Ekranı)

```
Kullanıcı Lock ekranını görür
    │
    ├─→ QR Kod oluşturulur (cihazın IP adresi içerir)
    │       │
    │       └─→ Yetkili kişi QR kodu tarar
    │               │
    │               └─→ QRCODEservice → MongoDB'ye isConnected: true
    │
    └─→ 2FA PIN girişi
            │
            └─→ WebSocket → TwoFactorAuth sunucusu
                    │
                    └─→ 20 saniyelik pencere içinde token eşleşirse → Kilit Açılır
```

### 3. Site Engelleme Akışı

```
SiteBlocker.py                          CaYaBlockWeb (Chrome)
    │                                         │
    ├─→ SAFEservice'ten liste çek             ├─→ Kara liste kontrolü
    ├─→ hosts dosyasını güncelle              ├─→ İçerik analizi
    └─→ DNS seviyesinde engel                 └─→ Engelleme sayfası göster

ekengelleme.py
    │
    ├─→ SAFEservice'ten başlık listesi çek
    └─→ Eşleşen pencereyi kapat
```

### 4. Yönetici Yönetimi

```
AdminPanel (Yönetici PC)
    │
    ├─→ TwoFA formu → WebSocket → TwoFactorAuth sunucusu
    │       └─→ Token eşleşirse → Yönetici erişimi açılır
    │
    ├─→ Kara/Beyaz liste düzenle → data.txt → SAFEservice
    ├─→ Bildirim gönder → İstemci cihazlar
    └─→ Log görüntüle
```

### 5. Uzak Kontrol (Ana Sunucu)

```
Ana Sunucu (port 4728)
    │
    ├─→ Socket.IO → Gerçek zamanlı ekran görüntüsü
    ├─→ RobotJS   → Fare ve klavye uzaktan kontrolü
    ├─→ /dosyalar → Dosya sistemi gezgini
    └─→ /yukle    → Dosya yükleme
```

---

## Kurulum & Yapılandırma

### Ön Gereksinimler

- Windows 10/11 (istemci tarafı)
- Node.js 18+ (sunucu servisleri)
- Python 3.9+ (Python araçları)
- .NET 6 SDK (C# uygulamaları derleme)
- MongoDB Atlas hesabı (QRCODEservice ve SiteEngelleyiciArayüz için)
- Google Chrome (CaYaBlockWeb eklentisi)

### Sunucu Kurulumu

```bash
# QRCODEservice
cd "Hizmetlerin Sunucusu/CaYaSafeServices/QRCODEservice"
cp .env.example .env
# .env içindeki MONGODB_URI değerini kendi MongoDB Atlas URI'niz ile değiştirin
npm install
node server.js

# SAFEservice
cd "Hizmetlerin Sunucusu/CaYaSafeServices/SAFEservice"
npm install
node server.js

# TwoFactorAuth
cd "Hizmetlerin Sunucusu/CaYaSafeServices/TwoFactorAuth"
npm install
node server.js

# Ana Sunucu
cd "Hizmetlerin Sunucusu/CaYaSafeServices/Server"
npm install
node app.js
```

### C# Uygulama Yapılandırması

`SiteEngelleyiciArayüz/SiteEngelleyiciArayüz/AppSettings.cs` dosyasını düzenleyin:

```csharp
public const string MongoDbConnectionString =
    "mongodb+srv://KULLANICI:SIFRE@cluster.mongodb.net/?retryWrites=true&w=majority";

public const string TwoFactorAuthServerUrl = "ws://SUNUCU_ADRESINIZ:303";
```

`AdminPanel/AdminPanel/AppSettings.cs` dosyasında da aynı `TwoFactorAuthServerUrl` değerini ayarlayın.

### Python Araç Yapılandırması

Her Python scriptinin üst kısmındaki `SERVER_URL` sabitini SAFEservice adresinizle güncelleyin:

```python
SERVER_URL = "http://SUNUCU_ADRESINIZ"
```

Python bağımlılıklarını yükleyin:

```bash
pip install -r Uygulama/SiteBlocker/requirements.txt
pip install -r Uygulama/ekengelleme/requirements.txt
# ... diğer projeler için de aynı şekilde
```

### Chrome Eklentisi Kurulumu

1. Chrome'da `chrome://extensions` adresini açın
2. "Geliştirici modu"nu aktif edin
3. "Paketlenmemiş öğe yükle" ile `Uygulama/AAeklenti/` klasörünü seçin

---

## Dizin Yapısı

```
CaYaSafeSystem-CaYaWindowBlocker/
│
├── Hizmetlerin Sunucusu/
│   └── CaYaSafeServices/
│       ├── SAFEservice/          # Engel listesi sunucusu (port 80)
│       ├── QRCODEservice/        # Cihaz bağlantı servisi (port 3001)
│       ├── TwoFactorAuth/        # 2FA WebSocket sunucusu (port 303)
│       └── Server/               # Uzak kontrol sunucusu (port 4728)
│
└── Uygulama/
    ├── AAeklenti/                 # CaYaBlockWeb Chrome eklentisi (MV3)
    ├── AdminPanel/                # Yönetici paneli (C# WinForms)
    ├── SiteEngelleyiciArayüz/     # Ana kilit uygulaması (C# WinForms)
    ├── SiteBlocker/               # Hosts tabanlı engelleyici (Python)
    ├── ekengelleme/               # Pencere engelleyici (Python)
    ├── antiex/                    # Chrome eklenti koruyucusu (Python)
    ├── autostart/                 # Ana süreç watchdog (Python)
    ├── AutoOpenCY/                # Watchdog #1 (Python)
    ├── svpgcc/                    # Watchdog #2 (Python)
    ├── Pvgc(LOG system)/          # Aktivite kayıt sistemi (Python)
    ├── CheckUnAuth/               # Yetki denetimi (C#)
    ├── CYRS/                      # Yeniden başlatma servisi (C#)
    ├── CYSL/                      # Servis başlatıcı (C#)
    ├── CYSFSYsafeMode/            # Güvenli mod yönetimi (C#)
    ├── DisableSafeMode/           # Güvenli mod devre dışı (C#)
    ├── EklentiNOTREMOVED/         # Eklenti koruma servisi (C#)
    ├── FixCaYaSystem/             # Onarım aracı (C#)
    ├── Notif/                     # Bildirim servisi (C#)
    ├── ServerStartt/              # Sunucu başlatıcı (C#)
    ├── CaYaLabOffline/            # Çevrimdışı modül (C#)
    ├── CaYaSafeSystemSetup/       # Kurulum projesi (C#)
    ├── CYPCcheck/                 # PC denetim aracı (C#)
    └── CYPCFixer/                 # PC düzeltme aracı (C#)
```

---

## Gereksinimler

### Node.js Servisleri

| Paket | Kullanım |
|-------|----------|
| express | HTTP sunucusu |
| socket.io | Gerçek zamanlı iletişim |
| ws | WebSocket sunucusu (2FA) |
| mongodb | MongoDB bağlantısı |
| dotenv | Ortam değişkenleri |
| multer | Dosya yükleme |
| robotjs | Uzak fare/klavye |
| screenshot-desktop | Ekran görüntüsü |
| jimp | Görüntü işleme |
| loudness | Ses seviyesi kontrolü |

### Python Araçları

```
requests
beautifulsoup4
pygetwindow
urllib3
psutil
pynput
```

### C# / .NET

- .NET 6 Windows Desktop Runtime
- MongoDB.Driver
- QRCoder
- ZXing.Net
- Newtonsoft.Json
- SharpCompress

---

## Güvenlik Notları

- `AppSettings.cs` dosyaları placeholder değerler içerir. Gerçek bilgilerinizi bu dosyalara ekledikten sonra dosyayı `.gitignore`'a eklemeyi veya ortam değişkeni kullanmayı düşünün.
- MongoDB şifrelerini ve sunucu adreslerini hiçbir zaman Git geçmişine eklemeyin.
- `Server/app.js` içindeki `/komut-calistir`, `/forceshutdown` gibi endpoint'ler üretim ortamında kimlik doğrulama katmanı gerektirmektedir.
- `.env.example` dosyasını kopyalayarak `.env` oluşturun; `.env` dosyası `.gitignore` tarafından korunmaktadır.

---

## Lisans

Bu proje [CaYaSafe Özel Lisansı](LICENSE) kapsamında lisanslanmıştır.  
Ayrıntılar için `LICENSE` dosyasına bakınız.

---

*CaYaSafeSystem — Güvenli bir dijital ortam için.*
