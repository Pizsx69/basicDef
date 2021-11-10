using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace basicDef_Block
{
    class Global_Variables
    {
        //Registry gathering
        public static BackgroundWorker Registry_BG;

        //First intall BG
        public static string ProgramData = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData);
        public static string AppName = "basicDef Block";
        public static BackgroundWorker Installer_BG;



        //Temp variable
        public static List<string> TEMP_APP_DISPLAYNAME= new List<string>();
        public static List<string> TEMP_CORRECT_APP_DISPLAYNAME = new List<string>();
        public static List<string> TEMP_REGISTRY_PATH = new List<string>();
        public static List<string> TEMP_INSTALL_PATH = new List<string>();
        public static List<string> TEMP_ICON_PATH = new List<string>();
        public static List<bool> TEMP_STATUS = new List<bool>();
        public static List<int> TEMP_BROWSED_APP = new List<int>();


        //Sort variable
        public static string[] _Temp_sort;

        //Registry variables
        public static string Blocked_Apps;
        public static string Browsed_Blocked_Apps;        
        public static string Current_user_exe_path = @"Software\Classes\";
        public static string Registry_key32 = @"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall";
        public static string Registry_key64 = @"SOFTWARE\WOW6432Node\Microsoft\Windows\CurrentVersion\Uninstall";

    }
}
