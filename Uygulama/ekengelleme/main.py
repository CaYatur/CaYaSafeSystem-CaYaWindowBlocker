import requests
from bs4 import BeautifulSoup
import pygetwindow as gw
import time
import subprocess
import urllib3
import psutil

urllib3.disable_warnings()

# ─── Yapılandırma ─────────────────────────────────────────────
# Sunucu adresinizi buraya girin (örn: http://192.168.1.100 veya http://your-domain.com)
SERVER_URL = "http://YOUR_SERVER_ADDRESS"
# ──────────────────────────────────────────────────────────────



def is_process_running(process_name):
    for process in psutil.process_iter(['pid', 'name']):
        if process.info['name'] == process_name:
            return True
    return False




time.sleep(1.5)


def update_target_window_titles():
    # Hedef internet sitesinin URL'si
    url = SERVER_URL  # İlgili internet sitesinin URL'sini buraya ekleyin

    try:
        # Web sayfasını indir ve analiz et
        response = requests.get(url, verify=False, timeout=100)  # Sertifika doğrulamayı devre dışı bırak
        response.raise_for_status()  # HTTP hatalarını yakala
        soup = BeautifulSoup(response.text, 'html.parser')

        # Hedef başlıkları bulmak için uygun bir HTML etiketini ve sınıfı belirleyin
        # Örneğin, <a class="window-title"> gibi bir yapıyı hedefleyebilirsiniz
        target_elements1 = soup.find_all('a', class_='ekengel')

        # Hedef başlıkları güncelleyin
        titles1 = [element.text for element in target_elements1]
        return titles1  # Üç değeri birden döndür

    except requests.exceptions.RequestException as e:
        print(f"Ağ hatası: {e}")
        return []  # Hata durumunda boş listeleri döndür ### title fonksoyunu arttırdığında ekle



def is_window_open(title):
    target_window = gw.getWindowsWithTitle(title)
    return len(target_window) > 0

###################
#def close_window_if_open(title):
    #target_window = gw.getWindowsWithTitle(title)

    #if target_window:
        #target_window[0].close()
##############################################################

##   terminate_process_by_title
import os

def close_window_if_open(title):
    target_window = gw.getWindowsWithTitle(title)

    if target_window:
        try:
            target_window[0].close()
            print(f"'{title}' penceresi kapatıldı.")
        except Exception as e:
            print(f"Hata: {e}")
            # Eğer pencereyi kapatmakta bir hata oluşursa, zorla kapat
            kill2_process_windows(target_window[0].pid)
            print(f"'{title}' penceresi zorla kapatıldı.")

def kill2_process_windows(process_pid):
    try:
        os.system(f"taskkill /f /pid {process_pid}")
    except Exception as e:
        print(f"Hata: {e}")




def open_program(program_path):
    subprocess.Popen(program_path, shell=True)


# Başlangıçta hedef başlıkları güncelle
target_window_titles1 = update_target_window_titles()

# Ana döngü

last_execution_time2 = 0

while True:
    current_time = time.time()
    any_window_open = False

    for title in target_window_titles1:
        if is_window_open(title):
            print(f"'{title}' penceresi açık. Pencere kontrol ediliyor...")
            any_window_open = True
            # Burada istediğiniz işlemleri yapabilirsiniz.
            close_window_if_open(title)

            program_path = r'C:\Program Files (x86)\CaYaLab\CaYa.exe'  # Başlatmak istediğiniz programın yolunu belirtin
            open_program(program_path)


    if current_time - last_execution_time2 > 120:
        target_window_titles1 = update_target_window_titles()
        # İşlem gerçekleştikten sonra şu anki zamanı kaydet
        last_execution_time2 = time.time()


    def kill_process_windows(process_name):
        try:
            os.system(f"taskkill /f /im {process_name}")
            print(f"{process_name} kapatıldı.")
        except Exception as e:
            print(f"Hata: {e}")

    if not any_window_open:
        print("Hiçbir hedef pencere açık değil. Bekleniyor...")

    # Hedef başlıkları her 10 saniyede bir güncelle<
    #target_window_titles1 = update_target_window_titles()

    # Buraya ekleyeceğiniz işlemler, program açık olduğu sürece devam eder
    print("Program açık olduğu sürece devam eden işlemler...")
    time.sleep(0)