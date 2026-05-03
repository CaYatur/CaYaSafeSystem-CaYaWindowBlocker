import subprocess
import time

import psutil

program_adı = "CaYaSafe32.exe"
dosya_yolu = "C:\\Program Files (x86)\\CaYaSafe\\CaYaSafe32\\CaYaSafe32.exe"


while True:
    try:
        # Belirtilen programı başlat
        subprocess.Popen([dosya_yolu])
        print(f"{program_adı} başlatıldı.")

        # Programın çalıştığını kontrol et
        while True:
            program_var_mı = any(
                program_adı.lower() in p.name().lower() for p in psutil.process_iter(attrs=['pid', 'name']))
            if not program_var_mı:
                print(f"{program_adı} kapatıldı, tekrar başlatılıyor...")
                break
            time.sleep(1)
    except Exception as e:
        print(f"Hata oluştu: {e}")

    time.sleep(5)  # Programı her 60 saniyede bir kontrol et
