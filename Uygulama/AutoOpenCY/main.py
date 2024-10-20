import psutil
import subprocess
import time

def is_program_running(program_name):
    for process in psutil.process_iter(['pid', 'name']):
        if program_name.lower() in process.info['name'].lower():
            return True
    return False

def terminate_program(program_name):
    for process in psutil.process_iter(['pid', 'name']):
        if program_name.lower() in process.info['name'].lower():
            try:
                process.terminate()
                print(f"{program_name} kapatıldı.")
            except Exception as e:
                print(f"Hata: {e}")

def start_program(program_path):
    try:
        subprocess.Popen(program_path)
        print(f"{program_path} başlatıldı.")
    except Exception as e:
        print(f"Hata: {e}")

program_path = r"C:\Program Files (x86)\CaYaSafe\cyRUN\cyRUN.exe"

while True:
    if not is_program_running("cyRUN.exe"):
        terminate_program("cyRUN.exe")
        start_program(program_path)

program_path2 = r"C:\ProgramData\svpgcc\svpgcc.exe"

while True:
    if not is_program_running("svpgcc.exe"):
        terminate_program("svpgcc.exe")
        start_program(program_path2)
