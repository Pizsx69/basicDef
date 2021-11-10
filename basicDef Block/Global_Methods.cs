using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using CodeFluent.Runtime.BinaryServices;
using System.Security.Principal;
using System.Security.AccessControl;

namespace basicDef_Block
{
    class Global_Methods
    {

        //First install 




        public static void Registry_create()
        {
            try
            {
                Registry.CurrentUser.CreateSubKey(@"Software\Microsoft\Windows\CurrentVersion\Policies\Explorer\DisallowRun");
                RegistryKey rk = Registry.CurrentUser.CreateSubKey(@"Software\Microsoft\Windows\CurrentVersion\Policies\Explorer");
                rk.SetValue("DisallowRun", 0x00000001, RegistryValueKind.DWord);

            }
            catch (Exception)
            {
            }

            try
            {
                //Create AppName subkey
                Registry.ClassesRoot.CreateSubKey(@"SOFTWARE\" +Global_Variables.AppName);

                RegistryKey Game_RegistryVariable = Registry.ClassesRoot.CreateSubKey(@"SOFTWARE\" + Global_Variables.AppName);
                Game_RegistryVariable.SetValue("Games", "", RegistryValueKind.String);

                RegistryKey Browsed_RegistryVariable = Registry.ClassesRoot.CreateSubKey(@"SOFTWARE\" + Global_Variables.AppName);
                Browsed_RegistryVariable.SetValue("Browsed", "", RegistryValueKind.String);

                Directory.CreateDirectory(Global_Variables.ProgramData + "\\" + Global_Variables.AppName);

                using (System.IO.StreamWriter file = new System.IO.StreamWriter(Global_Variables.ProgramData +"\\"+ Global_Variables.AppName + "\\config.txt", true))
                {
                    file.WriteLine("¯\\_(ツ)_/¯");

                }
                NtfsAlternateStream.WriteAllText(Global_Variables.ProgramData +"\\"+ Global_Variables.AppName + "\\config.txt:Email.txt", "");
                
                

            }
            catch (Exception)
            {

            }




        }

        public static void App_Blocking_Method(string dispalyname_index)
        {

            int index = Global_Variables.TEMP_APP_DISPLAYNAME.FindIndex(x => x.Contains(dispalyname_index));

            string path = "";

            path = Global_Variables.TEMP_INSTALL_PATH[index].Replace("\"", "");
            if(path.Contains(".exe") || path.Contains(".EXE") || path.Contains(".Exe") || path.Contains(".eXe") || path.Contains(".exE") || path.Contains(".EXe") || path.Contains(".ExE") || path.Contains(".eXE"))
            {              
                path = Path.GetDirectoryName(path);
            }


            if (Directory.Exists(path))
            {

                if (Global_Variables.TEMP_BROWSED_APP.Contains(index))
                {
                    Browsed_blocked_apps(Global_Variables.TEMP_INSTALL_PATH[index] + "\\" + Global_Variables.TEMP_APP_DISPLAYNAME[index] + ".exe");
                }
                else
                {
                    Blocked_apps_registry(Registry_Properties.GetMD5(Registry_Properties.Display_Name_Corrector(Global_Variables.TEMP_APP_DISPLAYNAME[index])));

                }




                string[] DIRECTORY_FILES = Directory.GetFiles(path, "*.*", SearchOption.AllDirectories);
                string[] DIRECTORY_FILES_exe = Directory.GetFiles(path, "*.exe*", SearchOption.AllDirectories);

                
                using (System.IO.StreamWriter file = new System.IO.StreamWriter(Global_Variables.ProgramData + "\\" + Global_Variables.AppName+ "\\" + Registry_Properties.Display_Name_Corrector(Global_Variables.TEMP_APP_DISPLAYNAME[index]) + "_exe" + ".txt"))
                {
                    for (int i = 0; i < DIRECTORY_FILES_exe.Length; i++)
                    {
                        if (DIRECTORY_FILES_exe[i].Substring(DIRECTORY_FILES_exe[i].Length - 4, 4) == ".exe" ||
                            DIRECTORY_FILES_exe[i].Substring(DIRECTORY_FILES_exe[i].Length - 4, 4) == ".EXE" ||
                            DIRECTORY_FILES_exe[i].Substring(DIRECTORY_FILES_exe[i].Length - 4, 4) == ".Exe" ||
                            DIRECTORY_FILES_exe[i].Substring(DIRECTORY_FILES_exe[i].Length - 4, 4) == ".eXe" ||
                            DIRECTORY_FILES_exe[i].Substring(DIRECTORY_FILES_exe[i].Length - 4, 4) == ".exE" ||
                            DIRECTORY_FILES_exe[i].Substring(DIRECTORY_FILES_exe[i].Length - 4, 4) == ".EXe" ||
                            DIRECTORY_FILES_exe[i].Substring(DIRECTORY_FILES_exe[i].Length - 4, 4) == ".ExE" ||
                            DIRECTORY_FILES_exe[i].Substring(DIRECTORY_FILES_exe[i].Length - 4, 4) == ".eXE"
                            )
                        {
                            try
                            {
                                //TODO
                                //Védelem codolás bevezetés.
                                Exe_file_encode(DIRECTORY_FILES_exe[i]);

                                RegistryKey regKey = Registry.CurrentUser;
                                regKey = regKey.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Policies\Explorer\DisallowRun");
                                int listaszam = 1;
                                for (int j = 0; j < listaszam; j++)
                                {
                                    if (regKey.GetValue(j.ToString()) == null)
                                    {//maybe nem kell
                                        //folyamatok_list.Items.Add(DIRECTORY_FILES_exe[i]);
                                        file.WriteLine(DIRECTORY_FILES_exe[i] + " " + j);
                                        RegistryKey exetilto_registry = Registry.CurrentUser.CreateSubKey(@"Software\Microsoft\Windows\CurrentVersion\Policies\Explorer\DisallowRun");
                                        exetilto_registry.SetValue(j.ToString(), Path.GetFileName(DIRECTORY_FILES_exe[i]), RegistryValueKind.String);

                                        j = listaszam + 1;
                                    }
                                    else
                                    {
                                        listaszam++;
                                    }

                                }
                                //TODO
                                PrivilegAdd(DIRECTORY_FILES_exe[i], Registry_Properties.Display_Name_Corrector(Global_Variables.TEMP_APP_DISPLAYNAME[index]) + "_exe" + ".txt", listaszam - 1);
                                
                            }
                            catch (Exception )
                            {
                                //DEBUG_SCRIPT(Globalis_Valtozok.ERROR, "Hibás működés a Disallow registrynél.", ex.ToString());
                            }

                        }


                    }

                }

                using (System.IO.StreamWriter file = new System.IO.StreamWriter(Global_Variables.ProgramData +"\\" + Global_Variables.AppName + "\\" + Registry_Properties.Display_Name_Corrector(Global_Variables.TEMP_APP_DISPLAYNAME[index]) + ".txt"))
                {
                    for (int i = 0; i < DIRECTORY_FILES.Length; i++)
                    {
                        if (
                            DIRECTORY_FILES[i].Substring(DIRECTORY_FILES[i].Length - 4, 4) == ".dll" ||
                            DIRECTORY_FILES[i].Substring(DIRECTORY_FILES[i].Length - 4, 4) == ".jar" ||
                            DIRECTORY_FILES[i].Substring(DIRECTORY_FILES[i].Length - 4, 4) == ".xml" ||
                            DIRECTORY_FILES[i].Substring(DIRECTORY_FILES[i].Length - 4, 4) == ".bat" ||
                            DIRECTORY_FILES[i].Substring(DIRECTORY_FILES[i].Length - 4, 4) == ".cmd" ||
                            DIRECTORY_FILES[i].Substring(DIRECTORY_FILES[i].Length - 5, 5) == ".conf" ||
                            DIRECTORY_FILES[i].Substring(DIRECTORY_FILES[i].Length - 4, 4) == ".bin" ||
                            DIRECTORY_FILES[i].Substring(DIRECTORY_FILES[i].Length - 4, 4) == ".cfg" ||
                            DIRECTORY_FILES[i].Substring(DIRECTORY_FILES[i].Length - 3, 3) == ".py" ||
                            DIRECTORY_FILES[i].Substring(DIRECTORY_FILES[i].Length - 4, 4) == ".DLL" ||
                            DIRECTORY_FILES[i].Substring(DIRECTORY_FILES[i].Length - 4, 4) == ".JAR" ||
                            DIRECTORY_FILES[i].Substring(DIRECTORY_FILES[i].Length - 4, 4) == ".XML" ||
                            DIRECTORY_FILES[i].Substring(DIRECTORY_FILES[i].Length - 4, 4) == ".BAT" ||
                            DIRECTORY_FILES[i].Substring(DIRECTORY_FILES[i].Length - 4, 4) == ".CMD" ||
                            DIRECTORY_FILES[i].Substring(DIRECTORY_FILES[i].Length - 5, 5) == ".CONF" ||
                            DIRECTORY_FILES[i].Substring(DIRECTORY_FILES[i].Length - 4, 4) == ".BIN" ||
                            DIRECTORY_FILES[i].Substring(DIRECTORY_FILES[i].Length - 4, 4) == ".CFG" ||
                            DIRECTORY_FILES[i].Substring(DIRECTORY_FILES[i].Length - 3, 3) == ".PY"
                            )
                        {                          
                            file.WriteLine(DIRECTORY_FILES[i]);
                            //TODO
                            PrivilegAdd(DIRECTORY_FILES[i], Registry_Properties.Display_Name_Corrector(Global_Variables.TEMP_APP_DISPLAYNAME[index]) + ".txt", 0);

                        }


                    }

                }
            }
            else
            {

                //DEBUG_SCRIPT(Globalis_Valtozok.ERROR, "NINCS_INSTALL", Globalis_Valtozok.TEMP_INSTALL_LOCATION[index]);
            }

        }

        public static void Unblocking_Method(string korrigalt_displayname)
        {
            

            List<string> blocked_exe_reader = new List<string>();
            List<string> blocked_other_reader = new List<string>();
            // TXT.BŐL VALÓ FELTÖLTÉS. !
            int index = Global_Variables.TEMP_CORRECT_APP_DISPLAYNAME.FindIndex(x => x.Contains(korrigalt_displayname));

            blocked_exe_reader = File.ReadLines(Global_Variables.ProgramData+ "\\" +Global_Variables.AppName+ "\\" + Global_Variables.TEMP_CORRECT_APP_DISPLAYNAME[index] + "_exe" + ".txt").ToList();
            blocked_other_reader = File.ReadLines(Global_Variables.ProgramData + "\\" + Global_Variables.AppName + "\\" + Global_Variables.TEMP_CORRECT_APP_DISPLAYNAME[index] + ".txt").ToList();
            



            string delete_games_javitas = "," + Registry_Properties.GetMD5(Global_Variables.TEMP_CORRECT_APP_DISPLAYNAME[index]);

            string[] eleresi_utak = Global_Variables.Browsed_Blocked_Apps.Split(',');

            for (int i = 0; i < eleresi_utak.Length; i++)
            {
                if (eleresi_utak[i].Contains(Global_Variables.TEMP_INSTALL_PATH[index]))
                {
                    Global_Variables.Browsed_Blocked_Apps = Global_Variables.Browsed_Blocked_Apps.Replace(eleresi_utak[i] + ",", "");
                }
            }


            Global_Variables.Blocked_Apps = Global_Variables.Blocked_Apps.Replace(delete_games_javitas, "");


            try
            {
                RegistryKey Tiltott_games_has = Registry.ClassesRoot.CreateSubKey(@"SOFTWARE\"+Global_Variables.AppName);
                Tiltott_games_has.SetValue("Games", Global_Variables.Blocked_Apps, RegistryValueKind.String);
                Tiltott_games_has.SetValue("Browsed", Global_Variables.Browsed_Blocked_Apps, RegistryValueKind.String);
            }
            catch (Exception)
            {
                //DEBUG_SCRIPT(Globalis_Valtozok.ERROR, "Nem sikerült az adat felöltés  Games/ Tallozott_tiltott_app ", ex.ToString());
            }

            for (int i = 0; i < blocked_exe_reader.Count; i++)
            {
                try
                {

                    string keyName = @"Software\Microsoft\Windows\CurrentVersion\Policies\Explorer\DisallowRun";
                    using (RegistryKey key = Registry.CurrentUser.OpenSubKey(keyName, true))
                    {
                        if (key != null)
                        {
                            int space = 3;
                            for (int j = 2; j < space; j++)
                            {
                                if (blocked_exe_reader[i].Substring(blocked_exe_reader[i].Length - j, 1) == " ")
                                {
                                    key.DeleteValue(blocked_exe_reader[i].Substring(blocked_exe_reader[i].Length - (j - 1), j - 1));
                                }
                                else
                                {
                                    space++;
                                }
                            }


                        }
                        else
                        {

                        }
                    }
                }
                catch (Exception )
                {
                    //DEBUG_SCRIPT(Globalis_Valtozok.ERROR, "Nem sikerült a DisallowRun művelet ", ex.ToString());
                }


            }
            for (int i = 0; i < blocked_other_reader.Count; i++)
            {
                PrivilegDelete(blocked_other_reader[i], Global_Variables.TEMP_CORRECT_APP_DISPLAYNAME[index]);
            }
            for (int i = 0; i < blocked_exe_reader.Count(); i++)
            {
                if (blocked_exe_reader[i].Contains(".exe"))
                {
                    int a = blocked_exe_reader[i].IndexOf(".exe", 1);
                    string utvonal = blocked_exe_reader[i].Substring(0, a + 4);
                    PrivilegDelete(utvonal, Global_Variables.TEMP_CORRECT_APP_DISPLAYNAME[index] + "_exe" + ".txt");
                    Exe_file_decode(utvonal);

                }
                if (blocked_exe_reader[i].Contains(".EXE"))
                {
                    int a = blocked_exe_reader[i].IndexOf(".EXE", 1);
                    string utvonal = blocked_exe_reader[i].Substring(0, a + 4);
                    PrivilegDelete(utvonal, Global_Variables.TEMP_CORRECT_APP_DISPLAYNAME[index] + "_exe" + ".txt");
                    Exe_file_decode(utvonal);

                }
                if (blocked_exe_reader[i].Contains(".Exe"))
                {
                    int a = blocked_exe_reader[i].IndexOf(".Exe", 1);
                    string utvonal = blocked_exe_reader[i].Substring(0, a + 4);
                    PrivilegDelete(utvonal, Global_Variables.TEMP_CORRECT_APP_DISPLAYNAME[index] + "_exe" + ".txt");
                    Exe_file_decode(utvonal);

                }
                if (blocked_exe_reader[i].Contains(".EXe"))
                {
                    int a = blocked_exe_reader[i].IndexOf(".EXe", 1);
                    string utvonal = blocked_exe_reader[i].Substring(0, a + 4);
                    PrivilegDelete(utvonal, Global_Variables.TEMP_CORRECT_APP_DISPLAYNAME[index] + "_exe" + ".txt");
                    Exe_file_decode(utvonal);

                }
                if (blocked_exe_reader[i].Contains(".ExE"))
                {
                    int a = blocked_exe_reader[i].IndexOf(".ExE", 1);
                    string utvonal = blocked_exe_reader[i].Substring(0, a + 4);
                    PrivilegDelete(utvonal, Global_Variables.TEMP_CORRECT_APP_DISPLAYNAME[index] + "_exe" + ".txt");
                    Exe_file_decode(utvonal);

                }
                if (blocked_exe_reader[i].Contains(".eXE"))
                {
                    int a = blocked_exe_reader[i].IndexOf(".eXE", 1);
                    string utvonal = blocked_exe_reader[i].Substring(0, a + 4);
                    PrivilegDelete(utvonal, Global_Variables.TEMP_CORRECT_APP_DISPLAYNAME[index] + "_exe" + ".txt");
                    Exe_file_decode(utvonal);

                }
                if (blocked_exe_reader[i].Contains(".eXe"))
                {
                    int a = blocked_exe_reader[i].IndexOf(".eXe", 1);
                    string utvonal = blocked_exe_reader[i].Substring(0, a + 4);
                    PrivilegDelete(utvonal, Global_Variables.TEMP_CORRECT_APP_DISPLAYNAME[index] + "_exe" + ".txt");
                    Exe_file_decode(utvonal);

                }
                if (blocked_exe_reader[i].Contains(".exE"))
                {
                    int a = blocked_exe_reader[i].IndexOf(".exE", 1);
                    string utvonal = blocked_exe_reader[i].Substring(0, a + 4);
                    PrivilegDelete(utvonal, Global_Variables.TEMP_CORRECT_APP_DISPLAYNAME[index] + "_exe" + ".txt");
                    Exe_file_decode(utvonal);

                }
            }


        }

        public static void Browsed_blocked_apps(string path)
        {
            // Frissíteni a Tallozott_Tiltott_Alkalmazasok stringből a Tallozott_tiltott_app registry értéket.
            try
            {
                RegistryKey regKey = Registry.ClassesRoot;
                regKey = regKey.OpenSubKey(@"Software\"+Global_Variables.AppName);
                Global_Variables.Browsed_Blocked_Apps= regKey.GetValue("Browsed").ToString();
                Global_Variables.Browsed_Blocked_Apps = Global_Variables.Browsed_Blocked_Apps + path + ",";
            }
            catch (Exception )
            {
               // DEBUG_SCRIPT(Globalis_Valtozok.ERROR, "Nem sikerült a Tallozott_tiltott_app frissítése. 1 ", ex.ToString());
            }

            try
            {
                RegistryKey Tiltott_tallozott_games_has = Registry.ClassesRoot.CreateSubKey(@"SOFTWARE\"+Global_Variables.AppName);
                Tiltott_tallozott_games_has.SetValue("Tallozott_tiltott_app", Global_Variables.Browsed_Blocked_Apps, RegistryValueKind.String);
            }
            catch (Exception )
            {
                //DEBUG_SCRIPT(Globalis_Valtozok.ERROR, "Nem sikerült a Tallozott_tiltott_app frissítése. 2", ex.ToString());
            }
        }

        private static void Blocked_apps_registry(string name)
        {
            // Frissíteni  a Tiltott_Games stringből a Games registry értéket.
            try
            {
                RegistryKey regKey = Registry.ClassesRoot;
                regKey = regKey.OpenSubKey(@"Software\"+Global_Variables.AppName);
                Global_Variables.Blocked_Apps = regKey.GetValue("Games").ToString();
                Global_Variables.Blocked_Apps = Global_Variables.Blocked_Apps + "," + name;
            }
            catch (Exception )
            {
                //DEBUG_SCRIPT(Globalis_Valtozok.ERROR, "Nem sikerült a Tiltott_Games frissítése. 1 ", ex.ToString());
            }

            try
            {
                RegistryKey Blocked_Game_Hash = Registry.ClassesRoot.CreateSubKey(@"SOFTWARE\"+Global_Variables.AppName);
                Blocked_Game_Hash.SetValue("Games", Global_Variables.Blocked_Apps, RegistryValueKind.String);
            }
            catch (Exception )
            {
                //DEBUG_SCRIPT(Globalis_Valtozok.ERROR, "Nem sikerült a Games frissítése. 2", ex.ToString());
            }

        }
        private static void PrivilegAdd(string path, string name, int disallow)
        {
            // Privileg Hozzáadás 
            try
            {
                var sid = new SecurityIdentifier(WellKnownSidType.WorldSid, null);
                var account = (NTAccount)sid.Translate(typeof(NTAccount)); // minden nyelven a Mindenki 
                string user = System.IO.File.GetAccessControl(path).GetOwner(typeof(System.Security.Principal.NTAccount)).ToString();
                if (user.Contains("SYSTEM"))
                {
                    string filepath = path;

                    //Get Currently Applied Access Control
                    FileSecurity fileS = File.GetAccessControl(filepath);

                    //Update it, Grant Current User Full Control
                    SecurityIdentifier cu = WindowsIdentity.GetCurrent().User;
                    fileS.SetOwner(account);
                    //fileS.SetAccessRule(new FileSystemAccessRule(sid, FileSystemRights.FullControl, AccessControlType.Allow));

                    //Update the Access Control on the File
                    File.SetAccessControl(filepath, fileS);

                }


                DirectoryInfo dInfo = new DirectoryInfo(path);
                DirectorySecurity dSecurity = dInfo.GetAccessControl();




                dSecurity.AddAccessRule(new FileSystemAccessRule(new SecurityIdentifier(WellKnownSidType.WorldSid, null), FileSystemRights.FullControl, InheritanceFlags.ObjectInherit | InheritanceFlags.ContainerInherit, PropagationFlags.NoPropagateInherit, AccessControlType.Deny)); // HOZZÁADÁS

                dInfo.SetAccessControl(dSecurity);



            }
            catch (Exception )
            {

               // DEBUG_SCRIPT(Globalis_Valtozok.ERROR, "Nem sikerült a fájt tiltani", path + "  " + ex);
            }

        }
        private static void PrivilegDelete(string path, string name)
        {
            //Privileg eltávolítása.
            //folyamat_list.Items.Add(path);
            //folyamat_list.TopIndex = folyamat_list.Items.Count - 1;

            try
            {
                DirectoryInfo dInfo = new DirectoryInfo(path);

                DirectorySecurity dSecurity = dInfo.GetAccessControl();

                var sid = new SecurityIdentifier(WellKnownSidType.WorldSid, null);
                var account = (NTAccount)sid.Translate(typeof(NTAccount)); // minden nyelven a Mindenki   

                dSecurity.RemoveAccessRule(new FileSystemAccessRule(account, FileSystemRights.FullControl, AccessControlType.Deny)); // Törlés

                dInfo.SetAccessControl(dSecurity);

            }
            catch (Exception )
            {
               // DEBUG_SCRIPT(Globalis_Valtozok.ERROR, "Nem sikerült a fájt feloldani", path + "  " + ex);
            }

        }

        public static void Exe_file_encode(string path)
        {
            string file_text = File.ReadAllText(path, Encoding.GetEncoding("windows-1252"));
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(file_text);
            File.WriteAllText(path, System.Convert.ToBase64String(plainTextBytes), Encoding.GetEncoding("windows-1252"));
        }
        public static void Exe_file_decode(string path)
        {

           string file_text = File.ReadAllText(path, Encoding.GetEncoding("windows-1252"));
            var base64EncodedBytes = System.Convert.FromBase64String(file_text); 
            File.WriteAllText(path, System.Text.Encoding.UTF8.GetString(base64EncodedBytes), Encoding.GetEncoding("windows-1252"));

        }

    }
}
