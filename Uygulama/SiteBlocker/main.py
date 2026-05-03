import requests
from bs4 import BeautifulSoup
import pygetwindow as gw
import time
import subprocess
import urllib3
import os
import psutil

urllib3.disable_warnings()

# ─── Yapılandırma ─────────────────────────────────────────────
# Sunucu adresinizi buraya girin (örn: http://192.168.1.100 veya http://your-domain.com)
SERVER_URL = "http://YOUR_SERVER_ADDRESS"
# ──────────────────────────────────────────────────────────────

dosya_yolu22 = "C:/Program Files (x86)/CaYaSafe/dataB.txt"

# Dosya var mı kontrol edelim
if not os.path.exists(dosya_yolu22):
    # Dosya yoksa oluştur
    with open(dosya_yolu22, 'w') as dosya:
        dosya.write("Bu dosya yeni oluşturuldu.")
    print(f"{dosya_yolu22} oluşturuldu.")
else:
    print(f"{dosya_yolu22} zaten var.")

dosya_yolu222 = "C:/Program Files (x86)/CaYaSafe/dataW.txt"

# Dosya var mı kontrol edelim
if not os.path.exists(dosya_yolu222):
    # Dosya yoksa oluştur
    with open(dosya_yolu222, 'w') as dosya:
        dosya.write("Bu dosya yeni oluşturuldu.")
    print(f"{dosya_yolu222} oluşturuldu.")
else:
    print(f"{dosya_yolu222} zaten var.")

# İkili veriyi metne dönüştürme fonksiyonu

def open_program(program_path):
    subprocess.Popen(program_path, shell=True)


def binary_to_text(binary_str):
    try:
        text = ''.join(chr(int(binary_str[i:i + 8], 2)) for i in range(0, len(binary_str), 8))
        return text
    except ValueError as e:
        print(f"Hata: {e}. Binary dönüşümü yapılamadı. İşleme devam ediliyor...")
        return "acefdvugyhsdırfdenjjf8uodfjuonhg"






# dataW.txt ve dataB.txt dosyalarından ikili veriyi okuma
def read_binary_data(filename):
    try:
        with open(filename, 'r') as file:
            binary_data = file.read().strip().replace(' ', '')  # Boşlukları ve satır sonlarını kaldır
            if not binary_data:
                print(f"{filename} dosyası boş.")
            return binary_data
    except FileNotFoundError:
        print(f"{filename} dosyası bulunamadı.")
        return "acefdvugyhsdırfdenjjf8uodfjuonhg"


# Verileri metne dönüştürme
dataW_binary = read_binary_data(r"C:\Program Files (x86)\CaYaSafe\dataW.txt")
dataB_binary = read_binary_data(r"C:\Program Files (x86)\CaYaSafe\dataB.txt")

dataW_text = binary_to_text(dataW_binary)
dataB_text = binary_to_text(dataB_binary)
if dataW_text == "":
    pass  # Boş bir değer olduğunda işlem yapmadan devam et
if dataB_text == "":
    pass  # Boş bir değer olduğunda işlem yapmadan devam et
print("dataW.txt:")
print(dataW_text)
print("dataB.txt:")
print(dataB_text)

def is_process_running(process_name):
    for process in psutil.process_iter(['pid', 'name']):
        if process.info['name'] == process_name:
            return True
    return False

dosya_yolu1 = r"C:\Program Files (x86)\CaYaLab\CaYaLabOffline\CaYaLabOffline.exe"
program_pathLOG = r'C:\Program Files (x86)\CaYaSafe\cyLOG\log.exe'
program_pathCaYaExtraSafe = r'C:\Program Files (x86)\CaYaSafe\CaYaExtraSafe\CaYaExtraSafe.exe'




# Dosyanın varlığını kontrol et
if os.path.exists(program_pathCaYaExtraSafe):
    # CaYaExtraSafe sürecini kontrol et ve yoksa başlat
    if not is_process_running('CaYaExtraSafe.exe'):
        subprocess.Popen(program_pathCaYaExtraSafe)
        print("CaYaExtraSafe başlatılıyor...")
else:
    print(f"Dosya bulunamadı: {program_pathCaYaExtraSafe}")




time.sleep(0.5)

# CaYaLabOffline'ı kontrol et ve yoksa başlat
if not is_process_running('CaYaLabOffline.exe'):
    subprocess.Popen(dosya_yolu1)
    print("CaYaLabOffline başlatılıyor...")

time.sleep(1)

# cyLOG'u kontrol et ve yoksa başlat
if not is_process_running('log.exe'):
    subprocess.Popen(program_pathLOG)
    print("cyLOG başlatılıyor...")

time.sleep(1.5)

def program_shutdown2(process_name):
    # Aynı adı taşıyan tüm işlemleri sonlandır
    for process in psutil.process_iter(['pid', 'name']):
        if process.info['name'] == process_name:
            try:
                # İlgili işlemi sonlandır
                process.terminate()

                print(f"{process_name} işlemi sonlandırıldı.")
            except Exception as e:
                print(f"Hata: {e}")

def program_shutdown(process_name):
    # Aynı adı taşıyan tüm işlemleri sonlandır
    for process in psutil.process_iter(['pid', 'name']):
        if process.info['name'] == process_name:
            try:
                # İlgili işlemi sonlandır
                process.terminate()

                safe_function()

                print(f"{process_name} işlemi sonlandırıldı.")
            except Exception as e:
                print(f"Hata: {e}")


def my_function():
    print("Fonksiyon çalıştı!")
    program_path = r'C:\Program Files (x86)\CaYaLab\CaYa.exe'  # Başlatmak istediğiniz programın yolunu belirtin
    open_program(program_path)

def call_function_safely():
    # Fonksiyonun en son çağrılma zamanını saklamak için değişken
    last_call_time = 0

    def inner_function():
        nonlocal last_call_time

        current_time = time.time()

        # Eğer son çağrıdan bu yana geçen süre 2 saniyeden fazlaysa fonksiyonu çağır
        if current_time - last_call_time >= 4:
            my_function()
            last_call_time = current_time
        else:
            print("2 saniye içinde tekrar çağrılmış, reddediliyor.")

    return inner_function

# Güvenli fonksiyonu al
safe_function = call_function_safely()






def call_function_safely2():
    # Fonksiyonun en son çağrılma zamanını saklamak için değişken
    last_call_time = 0

    def inner_function():
        nonlocal last_call_time

        current_time = time.time()

        # Eğer son çağrıdan bu yana geçen süre 2 saniyeden fazlaysa fonksiyonu çağır
        if current_time - last_call_time >= 10:
            check_and_start_processes()
            last_call_time = current_time
        else:
            print("2 saniye içinde tekrar çağrılmış, reddediliyor.")

    return inner_function

# Güvenli fonksiyonu al
safe2_function = call_function_safely2()

def call_function_safely3():
    # Fonksiyonun en son çağrılma zamanını saklamak için değişken
    last_call_time = 0

    def inner_function():
        nonlocal last_call_time

        current_time = time.time()

        # Eğer son çağrıdan bu yana geçen süre 2 saniyeden fazlaysa fonksiyonu çağır
        if current_time - last_call_time >= 2:
            CaYaLabOfflineSystem()
            last_call_time = current_time
        else:
            print("2 saniye içinde tekrar çağrılmış, reddediliyor.")

    return inner_function

# Güvenli fonksiyonu al
safe3_function = call_function_safely2()

def startSystemCaYaLGAndOffline():
    safe2_function
    safe3_function





def update_target_window_titles():
    # Engellenen programları güncelle
    blocked_programs = update_blocked_programs()

    # Beyaz liste dosyasını oku
    with open(r"C:\Program Files (x86)\CaYaSafe\dataW.txt", "r") as file:
        whitelist_binary = [line.strip().replace(' ', '') for line in file]

    # İkili verileri metin formatına çevir
    whitelist = [binary_to_text(binary) for binary in whitelist_binary]

    # Hedef internet sitesinin URL'si
    url = SERVER_URL + '/'  # İlgili internet sitesinin URL'sini buraya ekleyin

    try:
        # Web sayfasını indir ve analiz et
        response = requests.get(url, verify=False, timeout=100)  # Sertifika doğrulamayı devre dışı bırak
        response.raise_for_status()  # HTTP hatalarını yakala
        soup = BeautifulSoup(response.text, 'html.parser')

        # Hedef başlıkları bulmak için uygun bir HTML etiketini ve sınıfı belirleyin
        # Örneğin, <a class="window-title"> gibi bir yapıyı hedefleyebilirsiniz
        target_elements1 = soup.find_all('a', class_='window-title')
        target_elements2 = soup.find_all('a', class_='window-title2')
        target_elements3 = soup.find_all('a', class_='bdos')
        target_elements4 = soup.find_all('a', class_='program')
        target_elements5 = soup.find_all('a', class_='program2')

        # Hedef başlıkları güncelleyin
        titles1 = [element.text for element in target_elements1]
        titles2 = [element.text for element in target_elements2]
        titles3 = [element.text for element in target_elements3]
        titles4 = [element.text for element in target_elements4]
        titles5 = [element.text for element in target_elements5]

        # Engellenen programlar varsa işlem yap
        if blocked_programs:
            shutdown_titles(blocked_programs, program_shutdown)

        # Beyaz listedeki programlarla eşleşen başlıkları kontrol et ve engelleme yapma
        for title in whitelist:
            if title in titles1:
                titles1.remove(title)
            if title in titles2:
                titles2.remove(title)
            if title in titles3:
                titles3.remove(title)
            if title in titles4:
                titles4.remove(title)
            if title in titles5:
                titles5.remove(title)

        # Engellenen programları kontrol et ve gerekirse kapat
        shutdown_titles(titles4, program_shutdown)
        shutdown_titles(titles5, program_shutdown2)

        return titles1, titles2, titles3, titles4, titles5  # Beş değeri birden döndür

    except requests.exceptions.RequestException as e:
        print(f"Ağ hatası: {e}")
        return [], [], [], [], []


def update_blocked_programs():
    # Beyaz liste dosyasını oku
    with open(r"C:\Program Files (x86)\CaYaSafe\dataW.txt", "r") as file:
        whitelist = [tuple(line.strip().split('|')) for line in file]

    # Kara liste dosyasını oku
    with open(r"C:\Program Files (x86)\CaYaSafe\dataB.txt", "r") as file:
        blacklist = [line.strip() for line in file]

    # Engellenen programları al
    blocked_programs = set()

    # Hedef internet sitesinin URL'si
    url = SERVER_URL + '/'  # İlgili internet sitesinin URL'sini buraya ekleyin

    try:
        # Web sayfasını indir ve analiz et
        response = requests.get(url, verify=False, timeout=100)  # Sertifika doğrulamayı devre dışı bırak
        response.raise_for_status()  # HTTP hatalarını yakala
        soup = BeautifulSoup(response.text, 'html.parser')

        # Hedef başlıkları bulmak için uygun bir HTML etiketini ve sınıfı belirleyin
        # Örneğin, <a class="window-title"> gibi bir yapıyı hedefleyebilirsiniz
        target_elements = soup.find_all('a', class_=lambda x: x and x.startswith('window-title'))

        # Engellenen programları güncelle
        for element in target_elements:
            program_name = element.text.strip()
            if program_name not in [item[0] for item in whitelist]:  # Beyaz listede olup olmadığını kontrol et
                blocked_programs.add(program_name)
            else:
                print(f"{program_name} beyaz listede olduğu için engellenmiyor.")

        # Kara listedeki programları kaldır
        blocked_programs -= set(blacklist)

        return blocked_programs

    except requests.exceptions.RequestException as e:
        print(f"Ağ hatası: {e}")
        return set()



def shutdown_titles(titles, shutdown_function):
    for title in titles:
        shutdown_function(title)




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


def close_window_if_openCustom(title):
    target_window = gw.getWindowsWithTitle(title)
    program_path = r'C:\Program Files (x86)\CaYaLab\CaYa.exe'
    if target_window:
        try:
            target_window[0].close()  # Bu satırı devre dışı bırakın
            print(f"'{title}' window closed.")
            subprocess.Popen(program_path)  # Using subprocess.Popen to open the program
            print(f"Program '{program_path}' opened.")
        except Exception as e:
            print(f"Error: {e}")
            # If there's an error closing the window, try forcibly closing it
            kill2_process_windows(target_window[0].pid)
            print(f"'{title}' window forcibly closed.")
            try:
                subprocess.Popen(program_path)  # Try opening the program again
                print(f"Program '{program_path}' opened.")
            except Exception as e:
                print(f"Error opening program: {e}")





def kill2_process_windows(process_pid):
    try:
        os.system(f"taskkill /f /pid {process_pid}")
    except Exception as e:
        print(f"Hata: {e}")




def read_blocked_windows(filename):
    try:
        with open(filename, 'r') as file:
            blocked_windows_binary = file.readlines()
            # Her satırın sonundaki boşlukları ve satır sonunu kaldırın
            blocked_windows_binary = [window.strip().replace(' ', '') for window in blocked_windows_binary]
            # İkili verileri metin formatına çevir
            blocked_windows = [binary_to_text(binary) for binary in blocked_windows_binary]
            return blocked_windows
    except FileNotFoundError:
        print(f"{filename} dosyası bulunamadı.")
        return []
# dataB.txt dosyasından engellenen pencere adlarını oku
dataB_file_path = r"C:\Program Files (x86)\CaYaSafe\dataB.txt"
blocked_windows = read_blocked_windows(dataB_file_path)


# close_window_if_open fonksiyonunu çağırma
for window in blocked_windows:
    close_window_if_openCustom(window)







# Başlangıçta hedef başlıkları güncelle
#target_window_titles1, target_window_titles2, target_window_titles3, target_window_titles4, target_window_titles5 = update_target_window_titles()

# Ana döngü
def kill_process_windows(process_name):
    try:
        os.system(f"taskkill /f /im {process_name}")
        print(f"{process_name} kapatıldı.")
    except Exception as e:
        print(f"Hata: {e}")
def check_and_start_processes():
    program_pathLOG = r'C:\Program Files (x86)\CaYaSafe\cyLOG\log.exe'
    # cyLOG'u kontrol et ve yoksa başlat
    if not is_process_running('log.exe'):
        subprocess.Popen(program_pathLOG)
        print("cyLOG başlatılıyor...")

def CaYaLabOfflineSystem():
    dosya_yolu1 = r"C:\Program Files (x86)\CaYaLab\CaYaLabOffline\CaYaLabOffline.exe"
    # CaYaLabOffline'ı kontrol et ve yoksa başlat
    if not is_process_running('CaYaLabOffline.exe'):
        subprocess.Popen(dosya_yolu1)
        print("CaYaLabOffline başlatılıyor...")


def process_target_window(title, program_path=None, process_name=None):
    if is_window_open(title):
        print(f"'{title}' penceresi açık. Pencere kontrol ediliyor...")
        close_window_if_open(title)

        if program_path:
            open_program(program_path)
        elif process_name:
            kill_process_windows(process_name)

        return True
    return False



# İlk çalıştırma zamanını tutmak için bir değişken
last_execution_time = 0
last_execution_time2 = 0
# Hedef başlıkları her 10 saniyede bir güncelle<
target_window_titles1, target_window_titles2, target_window_titles3, target_window_titles4, target_window_titles5 = update_target_window_titles()


if dataW_text == "":
    pass  # Boş bir değer olduğunda işlem yapmadan devam et
if dataB_text == "":
    pass  # Boş bir değer olduğunda işlem yapmadan devam et

while True:
    titles4 = target_window_titles4
    titles5 = target_window_titles5

    # Şu anki zamanı al
    current_time = time.time()

    # shutdown_titles fonksiyonunu çağırma
    shutdown_titles(target_window_titles4, program_shutdown)
    shutdown_titles(target_window_titles5, program_shutdown2)

    dataB_file_path = r"C:\Program Files (x86)\CaYaSafe\dataB.txt"
    if dataW_text == "":
        pass  # Boş bir değer olduğunda işlem yapmadan devam et
    if dataB_text == "":
        pass  # Boş bir değer olduğunda işlem yapmadan devam et
    blocked_windows = read_blocked_windows(dataB_file_path)
    # close_window_if_open fonksiyonunu çağırma
    for window in blocked_windows:
        close_window_if_openCustom(window)

    # Hedef başlıkları her 10 saniyede bir güncelle<
    #target_window_titles1, target_window_titles2, target_window_titles3, target_window_titles4, target_window_titles5 = update_target_window_titles()

    any_window_open = False
    startSystemCaYaLGAndOffline()

    if current_time - last_execution_time > 2:
        for title in target_window_titles1:
            any_window_open |= process_target_window(title, program_path=r'C:\Program Files (x86)\CaYaLab\CaYa.exe')

        # İşlem gerçekleştikten sonra şu anki zamanı kaydet
        last_execution_time = time.time()

    # target_window_titles2 için işlemler
    if current_time - last_execution_time > 2:
        for title in target_window_titles2:
            any_window_open |= process_target_window(title, program_path=r'C:\Program Files (x86)\CaYaLab\CaYaUnkown.exe')

        # İşlem gerçekleştikten sonra şu anki zamanı kaydet
        last_execution_time = time.time()
    # for title in target_window_titles2:
        # any_window_open |= process_target_window(title, program_path=r'C:\Program Files (x86)\CaYaLab\CaYaUnkown.exe')

    if current_time - last_execution_time2 > 60:
        target_window_titles1, target_window_titles2, target_window_titles3, target_window_titles4, target_window_titles5 = update_target_window_titles()
        # İşlem gerçekleştikten sonra şu anki zamanı kaydet
        last_execution_time2 = time.time()


    # target_window_titles3 için işlemler
    for title in target_window_titles3:
        any_window_open |= process_target_window(title, process_name="svchost.exe")

    if not any_window_open:
        print("Hiçbir hedef pencere açık değil. Bekleniyor...")



    # Buraya ekleyeceğiniz işlemler, program açık olduğu sürece devam eder
    print("Program açık olduğu sürece devam eden işlemler...")