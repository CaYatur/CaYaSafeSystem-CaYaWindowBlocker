import subprocess
import os
import psutil
import time
def is_process_running(process_name):
    for process in psutil.process_iter(['pid', 'name']):
        if process.info['name'] == process_name:
            return True
    return False

program_pathCpgXAUTORUNCAYA = r'C:\Windows\SysWOW64\CpgX\CpgX.exe'

# Dosyanın varlığını kontrol et
if os.path.exists(program_pathCpgXAUTORUNCAYA):
    if not is_process_running('CpgX.exe'):
            subprocess.Popen(program_pathCpgXAUTORUNCAYA)
            print("CpgX.exe başlatılıyor...")
    else:
        print(f"Dosya bulunamadı: {program_pathCpgXAUTORUNCAYA}")


def AutorunCY():
    program_pathCpgXAUTORUNCAYA = r'C:\Windows\SysWOW64\CpgX\CpgX.exe'

    # Dosyanın varlığını kontrol et
    if os.path.exists(program_pathCpgXAUTORUNCAYA):
        if not is_process_running('CpgX.exe'):
            subprocess.Popen(program_pathCpgXAUTORUNCAYA)
            print("CpgX.exe başlatılıyor...")
    else:
        print(f"Dosya bulunamadı: {program_pathCpgXAUTORUNCAYA}")



while (True):
    AutorunCY()
    time.sleep(1)


