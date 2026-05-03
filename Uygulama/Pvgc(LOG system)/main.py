import os
from datetime import datetime, timedelta
from pynput import mouse
import pygetwindow as gw

log_folder_path = r"C:\ProgramData\PVGCC"
last_window_title = ""

def on_mouse_click(x, y, button, pressed):
    global last_window_title

    if pressed:
        window_title = get_active_window_title()
        if window_title != last_window_title:
            # Unicode karakterleri temizle veya belirtilen karakterle değiştir
            cleaned_message = clean_unicode_characters(f"Pencere Açıldı - {window_title} - {datetime.now()}", replacement_char="?")
            log_window_activity(cleaned_message)
            last_window_title = window_title

def log_window_activity(message):
    # Günlük dosyasının adını belirle
    log_file_name = datetime.now().strftime("%d.%m.%Y_Log.txt")
    log_file_path = os.path.join(log_folder_path, log_file_name)

    # Günlük dosyasına mesajı ekle
    with open(log_file_path, "a", encoding="ISO-8859-9") as log_file:
        cleaned_message = clean_unicode_characters(message)
        log_file.write(f"{cleaned_message}\n")


def get_active_window_title():
    active_window = gw.getActiveWindow()
    return active_window.title if active_window else "Bilinmeyen Pencere"

def clean_unicode_characters(text, replacement_char="?"):
    # Unicode karakterleri temizle veya belirtilen karakterle değiştir
    return ''.join(char if ord(char) < 1024 else replacement_char for char in text).encode('ISO-8859-9').decode('ISO-8859-9')


def delete_old_logs(log_folder_path, days_to_keep=20):
    # Şu anki tarihi al
    current_date = datetime.now()

    # Silinecek tarihi hesapla
    delete_date = current_date - timedelta(days=days_to_keep)

    # Log dizinindeki tüm dosyaları kontrol et
    for file_name in os.listdir(log_folder_path):
        file_path = os.path.join(log_folder_path, file_name)

        # Dosyanın tarihini al
        file_creation_time = datetime.fromtimestamp(os.path.getctime(file_path))

        # Eğer dosyanın oluşturulma tarihi, silinecek tarihten eskiyse, dosyayı sil
        if file_creation_time < delete_date:
            os.remove(file_path)
            print(f"{file_path} silindi.")

def main():
    # Uygulama başlatıldığında başlangıç bilgisi ekle
    log_window_activity(f"Uygulama Başlatıldı - {datetime.now()}")

    # Fare olaylarını dinle
    with mouse.Listener(on_click=on_mouse_click) as listener:
        try:
            listener.join()
        except KeyboardInterrupt:
            pass

    # Uygulama kapatıldığında kapanış bilgisi ekle
    log_window_activity(f"Uygulama Kapatıldı - {datetime.now()}")



if __name__ == "__main__":
    # Log dizinini oluştur
    os.makedirs(log_folder_path, exist_ok=True)
    # Eski log dosyalarını temizle
    delete_old_logs(log_folder_path, days_to_keep=40)

    main()
