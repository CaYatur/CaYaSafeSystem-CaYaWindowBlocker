import subprocess
import time

import psutil

#program_adı = "CaYaSafe32.exe"
#dosya_yolu = "C:\\Program Files (x86)\\CaYaSafe\\CaYaSafe32\\CaYaSafe32.exe"


#while True:
    # try:
        # Belirtilen programı başlat
        #   subprocess.Popen([dosya_yolu])
        #    print(f"{program_adı} başlatıldı.")

        # Programın çalıştığını kontrol et
        #     while True:
        #       program_var_mı = any(
        #            program_adı.lower() in p.name().lower() for p in psutil.process_iter(attrs=['pid', 'name']))
            #         if not program_var_mı:
            #            print(f"{program_adı} kapatıldı, tekrar başlatılıyor...")
                #             break
            #          time.sleep(1)
            #  except Exception as e:
        #print(f"Hata oluştu: {e}")

    #  time.sleep(5)  # Programı her 60 saniyede bir kontrol et


import psutil
import subprocess
import os

# Klasörün bulunduğu dizin
klasor_yolu = r'C:\Program Files (x86)\CaYa\CaYaWindowBlocker'

# Eski ve yeni klasör adları
eski_klasor_adi = 'unknown'
yeni_klasor_adi = 'AAeklenti'

# Eski klasör yolunu oluştur
eski_klasor_yolu = os.path.join(klasor_yolu, eski_klasor_adi)

try:
    # Klasör adını değiştir
    os.rename(eski_klasor_yolu, os.path.join(klasor_yolu, yeni_klasor_adi))
    print("Klasör adı başarıyla değiştirildi.")
except Exception as e:
    print("Klasör adı değiştirilirken bir hata oluştu:", e)


def is_program_running(program_name):
    for process in psutil.process_iter(['pid', 'name']):
        if program_name.lower() in process.info['name'].lower():
            return True
    return False

def start_program(program_path):
    try:
        subprocess.Popen(program_path)
        print(f"{program_path} başlatıldı.")
    except Exception as e:
        print(f"Hata: {e}")





program_path = r"C:\ProgramData\svpgcc\svpgcc.exe"
program_path2 = r"C:\Program Files (x86)\CaYaSafe\CaYaSafe32\CaYaSafe32.exe"

while True:
    if is_program_running("CYRS"):
        print("CYRS Hizmeti çalışıyor, program açma işlemi duraklatıldı")
    else:
        if not is_program_running("CaYaSafe32.exe"):
            start_program(program_path2)
    if not is_program_running("svpgcc.exe"):
        start_program(program_path)