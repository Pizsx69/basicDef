using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace basicDef_Block
{
    class Registry_Properties:Global_Variables
    {
        
        public static List<string> GetInstalledPrograms(string path, string path64)
        {

            var result = new List<string>();
            try
            {
                RegistryKey regKey = Registry.ClassesRoot;
                regKey = regKey.OpenSubKey(@"Software\"+AppName);
                Blocked_Apps = regKey.GetValue("Games").ToString();
                Browsed_Blocked_Apps = regKey.GetValue("Browsed").ToString();
            }
            catch (Exception)
            {
                //Globalis_Fuggvenyek.DEBUG_SCRIPT(ERROR, "Nem sikerült a Games / Tallozott_Tiltott_Alkalmazasok lekérdezése.", ex.ToString());
            }


            result.AddRange(GetInstalledProgramsFromRegistry(RegistryView.Registry32, path, RegistryHive.LocalMachine)); // 32 bites registry listázás localmachine 32 es pathel
            result.AddRange(GetInstalledProgramsFromRegistry(RegistryView.Registry64, path, RegistryHive.LocalMachine)); // 64 bites registry listázás localmachine 32 es pathel
            result.AddRange(GetInstalledProgramsFromRegistry(RegistryView.Registry32, path, RegistryHive.CurrentUser)); // 32 bites registry listázás Currentuser 32 es pathel
            result.AddRange(GetInstalledProgramsFromRegistry(RegistryView.Registry64, path, RegistryHive.CurrentUser)); // 64 bites registry listázás currentuser 32 es pathel
            try
            {
                result.AddRange(GetInstalledProgramsFromRegistry(RegistryView.Registry32, path64, RegistryHive.LocalMachine)); // 32 bites registry listázás localmachine 64 es pathel
            }
            catch (Exception )
            {
                //Globalis_Fuggvenyek.DEBUG_SCRIPT(LOG, "Nincs ilyen registry hely : 1 ", ex.ToString());

            }
            try
            {
                result.AddRange(GetInstalledProgramsFromRegistry(RegistryView.Registry64, path64, RegistryHive.LocalMachine));  // 64 bites registry listázás localmachine 64 es pathel
            }
            catch (Exception )
            {
                ///Globalis_Fuggvenyek.DEBUG_SCRIPT(LOG, "Nincs ilyen registry hely : 2 ", ex.ToString());

            }
            // 
            try
            {
                result.AddRange(GetInstalledProgramsFromRegistry(RegistryView.Registry32, path64, RegistryHive.CurrentUser));  // 32 bites registry listázás CurrentUser 64 es pathel
            }
            catch (Exception )
            {
                //Globalis_Fuggvenyek.DEBUG_SCRIPT(LOG, "Nincs ilyen registry hely : 3 ", ex.ToString());

            }
            try
            {
                result.AddRange(GetInstalledProgramsFromRegistry(RegistryView.Registry64, path64, RegistryHive.CurrentUser)); //64 bites registry listázás CurrentUser 64 es pathel
            }
            catch (Exception )
            {

                //Globalis_Fuggvenyek.DEBUG_SCRIPT(LOG, "Nincs ilyen registry hely : 4 ", ex.ToString());
            }


            // Számítógépen minden felhasználó registry-jért végig nézni.
            List<string> Simauserregistry = new List<string>();
            try
            {
                using (RegistryKey key = RegistryKey.OpenBaseKey(RegistryHive.Users, RegistryView.Registry32).OpenSubKey(@""))
                {
                    // int count = key.SubKeyCount; később hasznos lehet

                    foreach (string subkey_name in key.GetSubKeyNames())
                    {
                        RegistryKey Key2 = Registry.Users.OpenSubKey(subkey_name + "\\" + Registry_key32, false);
                        if (Key2 != null)
                        {
                            Simauserregistry.Add(subkey_name.ToString());
                            result.AddRange(GetInstalledProgramsFromRegistry(RegistryView.Registry32, subkey_name + "\\" + Registry_key32, RegistryHive.Users));

                        }

                    }

                }
            }
            catch (Exception )
            {
                //Globalis_Fuggvenyek.DEBUG_SCRIPT(LOG, "Simauserregistry", ex.ToString());
            }

            try
            {
                if (Browsed_Blocked_Apps != "")
                {
                    string[] eleresi_utak = Browsed_Blocked_Apps.Split(',');

                    for (int i = 0; i < eleresi_utak.Length - 1; i++)
                    {
                        TEMP_ICON_PATH.Add(eleresi_utak[i]);
                        TEMP_APP_DISPLAYNAME.Add(Path.GetFileName(eleresi_utak[i]).Replace(".exe", ""));
                        TEMP_CORRECT_APP_DISPLAYNAME.Add(Display_Name_Corrector(Path.GetFileName(eleresi_utak[i]).Replace(".exe", "")));
                        TEMP_REGISTRY_PATH.Add(Path.GetDirectoryName(eleresi_utak[i]));
                        TEMP_INSTALL_PATH.Add(Path.GetDirectoryName(eleresi_utak[i]));
                        TEMP_STATUS.Add(false);



                        //Globalis_Fuggvenyek.DEBUG_SCRIPT(LOG, "TiltottTallozott programok indítási betöltése ", Path.GetDirectoryName(eleresi_utak[i]));
                    }
                }
            }
            catch (Exception)
            {

                //Globalis_Fuggvenyek.DEBUG_SCRIPT(LOG, "1 TiltottTallozott programok indítási betöltése ", ex.ToString()); ;
            }

            return result;
        }

        private static IEnumerable<string> GetInstalledProgramsFromRegistry(RegistryView registryView, string regpath, RegistryHive regloc)
        {
            var result = new List<string>();

            using (RegistryKey key = RegistryKey.OpenBaseKey(regloc, registryView).OpenSubKey(regpath))
            {


                foreach (string subkey_name in key.GetSubKeyNames())
                {




                    using (RegistryKey subkey = key.OpenSubKey(subkey_name))
                    {
                        int count = subkey.ValueCount; //később hasznos lehet
                        if (count <= 3)
                        {
                            //Globalis_Fuggvenyek.DEBUG_SCRIPT(LOG, "ELLENÖRZÉS ", subkey.ToString() + "  " + count);
                            continue;
                        }



                        try
                        {
                            string temp_display_name = (string)subkey.GetValue("DisplayName");
                            string _temp_install_location = (string)subkey.GetValue("InstallLocation");
                            string temp_uninstall_location = (string)subkey.GetValue("UninstallString");


                            if (temp_display_name != null && _temp_install_location!=null &&temp_uninstall_location!=null && !_temp_install_location.Contains("Package Cache") && !temp_uninstall_location.Contains("Package Cache"))
                            {
                                //Globalis_Fuggvenyek.DEBUG_SCRIPT(LOG, "Levizsgálva: ", subkey.ToString() + " | " + (string)subkey.GetValue("DisplayName"));
                                if (regloc == RegistryHive.CurrentUser && (TEMP_CORRECT_APP_DISPLAYNAME.Contains(Display_Name_Corrector(temp_display_name))))
                                {

                                    try
                                    {
                                        RegistryKey regKey = Registry.CurrentUser;

                                        regKey = regKey.OpenSubKey(Current_user_exe_path + temp_display_name);
                                        if (TEMP_CORRECT_APP_DISPLAYNAME.Contains(Display_Name_Corrector(temp_display_name)))
                                        {
                                            for (int i = 0; i < TEMP_CORRECT_APP_DISPLAYNAME.Count; i++)
                                            {   //
                                                for (int y = 0; y < TEMP_BROWSED_APP.Count(); y++)
                                                {
                                                    if (TEMP_CORRECT_APP_DISPLAYNAME[TEMP_BROWSED_APP[y]] == Display_Name_Corrector(temp_display_name))
                                                    {
                                                        continue;
                                                    }

                                                }
                                                if (Display_Name_Corrector(temp_display_name) == TEMP_CORRECT_APP_DISPLAYNAME[i] && TEMP_STATUS[i])
                                                {


                                                    if (TEMP_INSTALL_PATH[i] == "")
                                                    {

                                                        TEMP_INSTALL_PATH[i] = (string)regKey.GetValue("InstallLocation");
                                                    }
                                                    if (TEMP_REGISTRY_PATH[i] == "")
                                                        TEMP_REGISTRY_PATH[i] = subkey.ToString();
                                                    if (TEMP_CORRECT_APP_DISPLAYNAME[i] == "")
                                                        TEMP_CORRECT_APP_DISPLAYNAME[i] = Display_Name_Corrector(temp_display_name);
                                                    if (TEMP_APP_DISPLAYNAME[i] == "")
                                                        TEMP_APP_DISPLAYNAME[i] = temp_display_name;
                                                    if (TEMP_ICON_PATH[i] == "")
                                                        TEMP_ICON_PATH[i] = iconpahtszeletelo((string)regKey.GetValue("DisplayIcon"));
                                                    TEMP_STATUS[i] = true;


                                                    //Globalis_Fuggvenyek.DEBUG_SCRIPT(LOG, "korrigalt display name ", TEMP_CORRECT_APP_DISPLAYNAME[i] + "  ");


                                                }
                                                if (Display_Name_Corrector(temp_display_name) == TEMP_CORRECT_APP_DISPLAYNAME[i] && !TEMP_STATUS[i])
                                                {
                                                    if (TEMP_INSTALL_PATH[i] == "")
                                                    {
                                                        TEMP_INSTALL_PATH[i] = (string)regKey.GetValue("InstallLocation");
                                                        //Globalis_Fuggvenyek.DEBUG_SCRIPT(ERROR, "9Viber", temp_uninstall_location);
                                                    }

                                                    if (TEMP_REGISTRY_PATH[i] == "")
                                                        TEMP_REGISTRY_PATH[i] = subkey.ToString();
                                                    if (TEMP_CORRECT_APP_DISPLAYNAME[i] == "")
                                                        TEMP_CORRECT_APP_DISPLAYNAME[i] = Display_Name_Corrector(temp_display_name);
                                                    if (TEMP_APP_DISPLAYNAME[i] == "")
                                                        TEMP_APP_DISPLAYNAME[i] = temp_display_name;
                                                    if (TEMP_ICON_PATH[i] == "")
                                                        TEMP_ICON_PATH[i] = iconpahtszeletelo((string)regKey.GetValue("DisplayIcon"));
                                                    TEMP_STATUS[i] = false;

                                                }

                                            }
                                        }
                                        continue;
                                    }
                                    catch (Exception )
                                    {
                                        //Globalis_Fuggvenyek.DEBUG_SCRIPT(LOG, " ", ex.ToString());
                                    }

                                }




                                if ((string)subkey.GetValue("Publisher") == "Microsoft Corporation" || (string)subkey.GetValue("Publisher") == "NVIDIA Corporation")
                                {


                                }
                                else
                                {
                                    if (!TEMP_CORRECT_APP_DISPLAYNAME.Contains(Display_Name_Corrector(temp_display_name)))
                                    {


                                        if (_temp_install_location != null || _temp_install_location == null)
                                        {

                                            if (_temp_install_location == "" || _temp_install_location == null)
                                            {
                                                if (temp_uninstall_location != null)
                                                {
                                                    if (temp_uninstall_location != "" && temp_uninstall_location != null)
                                                    {
                                                        string uninstall_temp = (string)subkey.GetValue("UninstallString");

                                                        if (uninstall_temp.Substring(0, 11) == "MsiExec.exe")
                                                        {
                                                            continue;


                                                        }
                                                        else
                                                        {
                                                            if (!temp_uninstall_location.Contains("\\Windows"))
                                                            {
                                                                switch (displayname(subkey))
                                                                {
                                                                    case 1:
                                                                        TEMP_INSTALL_PATH.Add(uninstall_temp);
                                                                        //Globalis_Fuggvenyek.DEBUG_SCRIPT(ERROR, "8Viber", temp_uninstall_location);
                                                                        //Globalis_Fuggvenyek.DEBUG_SCRIPT(ALK_LOG, "1 Aktiv ", temp_uninstall_location);
                                                                        break;
                                                                    case 2:
                                                                        TEMP_INSTALL_PATH.Add(temp_uninstall_location);
                                                                        //Globalis_Fuggvenyek.DEBUG_SCRIPT(ERROR, "7Viber", temp_uninstall_location);
                                                                        //Globalis_Fuggvenyek.DEBUG_SCRIPT(ALK_LOG, "2 Tiltott ", temp_display_name);
                                                                        break;
                                                                    case 3:
                                                                        string javitott_path = temp_uninstall_location;
                                                                        string directroy_javitas = Path.GetDirectoryName(javitott_path.Replace("\"", ""));
                                                                        if (Directory.Exists(Path.GetDirectoryName(directroy_javitas)))
                                                                        {
                                                                            TEMP_ICON_PATH.Add(imagejavitas(directroy_javitas));
                                                                            TEMP_APP_DISPLAYNAME.Add(temp_display_name);
                                                                            TEMP_CORRECT_APP_DISPLAYNAME.Add(Display_Name_Corrector(temp_display_name));
                                                                            TEMP_REGISTRY_PATH.Add(subkey.ToString());
                                                                            TEMP_INSTALL_PATH.Add(temp_uninstall_location);
                                                                            //Globalis_Fuggvenyek.DEBUG_SCRIPT(ERROR, "6Viber", temp_uninstall_location);
                                                                            TEMP_STATUS.Add(true);

                                                                            //Globalis_Fuggvenyek.DEBUG_SCRIPT(ALK_LOG, "3 Aktiv ", temp_display_name);
                                                                        }
                                                                        else
                                                                        {
                                                                            //Globalis_Fuggvenyek.DEBUG_SCRIPT(LOG, "Nem Talalt új icont: ", javitott_path);
                                                                        }
                                                                        break;
                                                                    case 4:
                                                                        string javitott_path2 = temp_uninstall_location;
                                                                        string directroy_javitas2 = Path.GetDirectoryName(javitott_path2.Replace("\"", ""));
                                                                        if (Directory.Exists(Path.GetDirectoryName(directroy_javitas2)))
                                                                        {
                                                                            TEMP_ICON_PATH.Add(imagejavitas(directroy_javitas2));
                                                                            TEMP_APP_DISPLAYNAME.Add(temp_display_name);
                                                                            TEMP_CORRECT_APP_DISPLAYNAME.Add(Display_Name_Corrector(temp_display_name));
                                                                            TEMP_REGISTRY_PATH.Add(subkey.ToString());
                                                                            TEMP_INSTALL_PATH.Add(temp_uninstall_location);
                                                                            //Globalis_Fuggvenyek.DEBUG_SCRIPT(ERROR, "5Viber", temp_uninstall_location);
                                                                            TEMP_STATUS.Add(false);

                                                                            //Globalis_Fuggvenyek.DEBUG_SCRIPT(ALK_LOG, "4 Tiltott ", temp_display_name);
                                                                        }
                                                                        else
                                                                        {
                                                                            //Globalis_Fuggvenyek.DEBUG_SCRIPT(LOG, "Nem Talalt új icont: ", javitott_path2);
                                                                        }
                                                                        break;
                                                                    default:
                                                                        break;

                                                                }
                                                                continue;
                                                            }


                                                        }



                                                    }

                                                }
                                                else
                                                {

                                                    switch (displayname(subkey))
                                                    {
                                                        case 1:
                                                            TEMP_INSTALL_PATH.Add("1");

                                                            //Globalis_Fuggvenyek.DEBUG_SCRIPT(ALK_LOG, "5 Aktiv ", temp_display_name);
                                                            break;
                                                        case 2:
                                                            TEMP_INSTALL_PATH.Add("1");
                                                            //Globalis_Fuggvenyek.DEBUG_SCRIPT(ALK_LOG, "6 Tiltott ", temp_display_name);
                                                            break;

                                                        default:
                                                            break;

                                                    }
                                                    continue;
                                                }
                                            }
                                            else
                                            {
                                                if (!_temp_install_location.ToString().Contains("\\Windows"))
                                                {
                                                    switch (displayname(subkey))
                                                    {
                                                        case 1:
                                                            TEMP_INSTALL_PATH.Add(_temp_install_location);
                                                            //Globalis_Fuggvenyek.DEBUG_SCRIPT(ERROR, "4Viber", temp_install_location);
                                                            //Globalis_Fuggvenyek.DEBUG_SCRIPT(ALK_LOG, "9 Aktiv ", temp_display_name);
                                                            break;
                                                        case 2:
                                                            TEMP_INSTALL_PATH.Add(_temp_install_location);
                                                            //Globalis_Fuggvenyek.DEBUG_SCRIPT(ERROR, "3Viber", temp_install_location);
                                                            //Globalis_Fuggvenyek.DEBUG_SCRIPT(ALK_LOG, "10 Tiltott ", temp_display_name);
                                                            break;
                                                        case 3:
                                                            string javitott_path = _temp_install_location;
                                                            string directroy_javitas = Path.GetDirectoryName(javitott_path.Replace("\"", ""));
                                                            if (Directory.Exists(Path.GetDirectoryName(directroy_javitas)))
                                                            {
                                                                TEMP_ICON_PATH.Add(imagejavitas(directroy_javitas));
                                                                TEMP_APP_DISPLAYNAME.Add(temp_display_name);
                                                                TEMP_CORRECT_APP_DISPLAYNAME.Add(Display_Name_Corrector(temp_display_name));
                                                                TEMP_REGISTRY_PATH.Add(subkey.ToString());
                                                                TEMP_INSTALL_PATH.Add(_temp_install_location);
                                                                //Globalis_Fuggvenyek.DEBUG_SCRIPT(ERROR, "2Viber", temp_install_location);
                                                                TEMP_STATUS.Add(true);

                                                                //Globalis_Fuggvenyek.DEBUG_SCRIPT(ALK_LOG, "11 Aktiv ", "\n" + imagejavitas(directroy_javitas) + "\n " + temp_display_name + "\n " + Display_Name_Corrector(temp_display_name) + "\n " + subkey.ToString() + "\n " + temp_uninstall_location + "\n");
                                                            }
                                                            else
                                                            {
                                                                //Globalis_Fuggvenyek.DEBUG_SCRIPT(LOG, "Nem Talalt új icont: ", javitott_path);
                                                            }
                                                            break;
                                                        case 4:
                                                            string javitott_path2 = _temp_install_location;
                                                            string directroy_javitas2 = Path.GetDirectoryName(javitott_path2.Replace("\"", ""));
                                                            if (Directory.Exists(Path.GetDirectoryName(directroy_javitas2)))
                                                            {
                                                                TEMP_ICON_PATH.Add(imagejavitas(directroy_javitas2));
                                                                TEMP_APP_DISPLAYNAME.Add(temp_display_name);
                                                                TEMP_CORRECT_APP_DISPLAYNAME.Add(Display_Name_Corrector(temp_display_name));
                                                                TEMP_REGISTRY_PATH.Add(subkey.ToString());
                                                                TEMP_INSTALL_PATH.Add(_temp_install_location);
                                                                //Globalis_Fuggvenyek.DEBUG_SCRIPT(ERROR, "1Viber", temp_install_location);
                                                                TEMP_STATUS.Add(false);
                                                            }
                                                            else
                                                            {
                                                                //Globalis_Fuggvenyek.DEBUG_SCRIPT(LOG, "Nem Talalt új icont: ", javitott_path2);
                                                            }
                                                            break;
                                                        default:
                                                            break;

                                                    }
                                                    continue;
                                                }

                                            }

                                        }
                                    }

                                }


                            }
                            else
                            {
                                //Globalis_Fuggvenyek.DEBUG_SCRIPT(LOG, "Levizsgálva Display Nelkül: ", subkey.ToString());
                            }

                        }
                        catch
                        {

                        }

                    }
                }

            }


            return result;
        }

        public static int displayname(RegistryKey subkey)
        {
            //DISPLAY NÉV  / ICON  LISTÁZÁS JAVÍTÁS FELTÖLTÉSEK SZÜRÉS / TILTOT VAGY SEM KÜLÖN LISTÁZÁS STB.
            int i = 1;
            int tiltottvsem = 0;

            try
            {
                if (subkey.GetValue("DisplayIcon") != null && (string)subkey.GetValue("DisplayIcon") != null && (string)subkey.GetValue("DisplayIcon") != "")
                {
                    if (!Blocked_Apps.Contains(GetMD5((Display_Name_Corrector((string)subkey.GetValue("DisplayName"))))))
                    {
                        TEMP_ICON_PATH.Add(iconpahtszeletelo((string)subkey.GetValue("DisplayIcon")));
                        TEMP_APP_DISPLAYNAME.Add((string)subkey.GetValue("DisplayName"));
                        TEMP_CORRECT_APP_DISPLAYNAME.Add(Display_Name_Corrector((string)subkey.GetValue("DisplayName")));
                        TEMP_REGISTRY_PATH.Add(subkey.ToString());
                        TEMP_STATUS.Add(true);
                        //Globalis_Fuggvenyek.DEBUG_SCRIPT(ALK_LOG,"11/1 Aktív", "\n"+iconpahtszeletelo((string)subkey.GetValue("DisplayIcon"))+"\n "+ (string)subkey.GetValue("DisplayName")+"\n "+ Display_Name_Corrector((string)subkey.GetValue("DisplayName"))+"\n "+ subkey.ToString()+"\n");
                        i = 0;
                        tiltottvsem = 1;
                        return tiltottvsem;

                    }
                    if (Blocked_Apps.Contains(GetMD5((Display_Name_Corrector((string)subkey.GetValue("DisplayName"))))))
                    {
                        TEMP_ICON_PATH.Add(iconpahtszeletelo((string)subkey.GetValue("DisplayIcon")));
                        TEMP_APP_DISPLAYNAME.Add((string)subkey.GetValue("DisplayName"));
                        TEMP_CORRECT_APP_DISPLAYNAME.Add(Display_Name_Corrector((string)subkey.GetValue("DisplayName")));
                        TEMP_REGISTRY_PATH.Add(subkey.ToString());
                        TEMP_STATUS.Add(false);
                        i = 0;
                        tiltottvsem = 2;
                        return tiltottvsem;

                    }

                }

                if (subkey.GetValue("DisplayIcon") == null || (string)subkey.GetValue("DisplayIcon") == null || (string)subkey.GetValue("DisplayIcon") == "")
                {
                    if (subkey.GetValue("UninstallString") != null && (string)subkey.GetValue("UninstallString") != null && (string)subkey.GetValue("UninstallString") != "" && van_msi_v_nincs((string)subkey.GetValue("UninstallString")))
                    {
                        if (!Blocked_Apps.Contains(GetMD5((Display_Name_Corrector((string)subkey.GetValue("DisplayName"))))))
                        {
                            TEMP_ICON_PATH.Add(iconpahtszeletelo((string)subkey.GetValue("UninstallString")));
                            TEMP_APP_DISPLAYNAME.Add((string)subkey.GetValue("DisplayName"));
                            TEMP_CORRECT_APP_DISPLAYNAME.Add(Display_Name_Corrector((string)subkey.GetValue("DisplayName")));
                            TEMP_REGISTRY_PATH.Add(subkey.ToString());
                            TEMP_STATUS.Add(true);

                            //Globalis_Fuggvenyek.DEBUG_SCRIPT(ALK_LOG, "11/2 aktív", "\n"+iconpahtszeletelo((string)subkey.GetValue("UninstallString"))+"\n "+ (string)subkey.GetValue("DisplayName")+"\n"+ Display_Name_Corrector((string)subkey.GetValue("DisplayName"))+"\n "+ subkey.ToString()+"\n");
                            i = 0;
                            tiltottvsem = 1;
                            return tiltottvsem;

                        }
                        if (Blocked_Apps.Contains(GetMD5((Display_Name_Corrector((string)subkey.GetValue("DisplayName"))))))
                        {
                            TEMP_ICON_PATH.Add(iconpahtszeletelo((string)subkey.GetValue("UninstallString")));
                            TEMP_APP_DISPLAYNAME.Add((string)subkey.GetValue("DisplayName"));
                            TEMP_CORRECT_APP_DISPLAYNAME.Add(Display_Name_Corrector((string)subkey.GetValue("DisplayName")));
                            TEMP_REGISTRY_PATH.Add(subkey.ToString());
                            TEMP_STATUS.Add(false);

                            i = 0;
                            tiltottvsem = 2;
                            return tiltottvsem;

                        }
                    }



                }

            }
            catch (Exception)
            {
                //Globalis_Fuggvenyek.DEBUG_SCRIPT(LOG, "Display nev függvény hiba: " + subkey.ToString(), ex.ToString());

            }
            if (i == 1)
            {
                if (!Blocked_Apps.Contains(GetMD5((Display_Name_Corrector((string)subkey.GetValue("DisplayName"))))))
                {

                    tiltottvsem = 3;
                }
                if (Blocked_Apps.Contains(GetMD5((Display_Name_Corrector((string)subkey.GetValue("DisplayName"))))))
                {

                    tiltottvsem = 4;
                }

            }
            return tiltottvsem;
        }

        public static string imagejavitas(string path)
        {
            string exe_path = "";
            long current_exe_size = 0;
            //long temp_exe_size;
            string new_path = path.Replace("\"", "");
            string[] DIRECTORY_FILES_exe = Directory.GetFiles(new_path, "*.exe*", SearchOption.TopDirectoryOnly);
            for (int i = 0; i < DIRECTORY_FILES_exe.Length; i++)
            {
                FileInfo f = new FileInfo(DIRECTORY_FILES_exe[i]);

                if (exe_path != "")
                {
                    if (current_exe_size <= f.Length)
                    {
                        exe_path = DIRECTORY_FILES_exe[i];
                        current_exe_size = f.Length;
                    }
                }
                if (exe_path == "")
                {
                    exe_path = DIRECTORY_FILES_exe[i];
                    current_exe_size = f.Length;
                }

            }


            return exe_path;
        }


        public static string Display_Name_Corrector(string _displayname)
        {
            //Speciális karakterek eltávolítása a display névből.
            string pattern = @"[^a-zA-Z0-9]";
            return Regex.Replace(_displayname, pattern, " ");

        }

        public static string iconpahtszeletelo(string path)
        {
            // Speciális karaktere string végi levágások a helyes icon megrajzoláshoz.

            string kuld_path = "";

            if (path == "" || path == null)
            {
                return kuld_path;
            }

            if (path.Substring(0, 1) == "\"")
            {
                string temp = path;
                path = path.Replace("\"", "");
                //Globalis_Fuggvenyek.DEBUG_SCRIPT(LOG, "van benne idézőjel:" + temp + "javítva: ", path);


            }

            if (path.Substring(path.Length - 4, 4) == ".exe" || path.Substring(path.Length - 4, 4) == ".ico")
            {
                kuld_path = path;
            }
            if (path.Substring(path.Length - 4, 4) != ".exe")
            {
                if (path.Substring(path.Length - 4, 4) != ".ico")
                {
                    kuld_path = path.Substring(0, path.Length - 2);
                }


            }

            //Globalis_Fuggvenyek.DEBUG_SCRIPT(ERROR, "FATAL ERROR debug " + "teszt ", kuld_path);
            return kuld_path;
        }
        public static string GetMD5(string text)
        {
            // MD5 jelszó nevek stb. csere. meghívás  után vissza adja a kész verziót.
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            md5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(text));
            byte[] result = md5.Hash;
            StringBuilder str = new StringBuilder();
            for (int i = 1; i < result.Length; i++)
            {
                str.Append(result[i].ToString("x2"));
            }
            // ide még annyi kéne hogy X. karakter saját és akkor nem lehet simán md5 el felül írni.
            return str.ToString();
        }
        public static bool van_msi_v_nincs(string msi_name)
        {
            if (!msi_name.Contains("MsiExec") && !msi_name.Contains("msiexec"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}