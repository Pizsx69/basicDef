using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace basicDef_Block
{
    public partial class Main_Application : Form
    {
        //mouse y,x position.
        int MvalX;
        int MValY;



        Apps[] Applications;
        App_Flow _App_Flow = new App_Flow();

        public Main_Application()
        {
            InitializeComponent();






        }
        public void Installer_BG_DoWork(object sender, DoWorkEventArgs e)
        {
            Global_Methods.Registry_create();
        }
        public void Installer_BG_Complete(object sender, RunWorkerCompletedEventArgs e)
        {
            Registry_Worker_Starter();
        }
        public void Registry_Worker_Starter()
        {
            Global_Variables.Registry_BG = new BackgroundWorker();
            Global_Variables.Registry_BG.DoWork += new DoWorkEventHandler(Registry_BG_DoWork);
            Global_Variables.Registry_BG.RunWorkerCompleted += new RunWorkerCompletedEventHandler(Registry_BG_Complete);
            Global_Variables.Registry_BG.RunWorkerAsync();
        }
        public void Registry_BG_DoWork(object sender, DoWorkEventArgs e)
        {
            _App_Flow.Invoke(new MethodInvoker(() =>
            {
                _App_Flow.Enabled = false;
            }));


            Global_Variables.TEMP_APP_DISPLAYNAME.Clear();
            Global_Variables.TEMP_CORRECT_APP_DISPLAYNAME.Clear();
            Global_Variables.TEMP_REGISTRY_PATH.Clear();
            Global_Variables.TEMP_INSTALL_PATH.Clear();
            Global_Variables.TEMP_ICON_PATH.Clear();
            Global_Variables.TEMP_STATUS.Clear();
            Registry_Properties.GetInstalledPrograms(Global_Variables.Registry_key32, Global_Variables.Registry_key64);
        }

        public void Registry_BG_Complete(object sender,RunWorkerCompletedEventArgs e)
        {
            _App_Flow.Applications_Flow.Controls.Clear();

            int _blocked_apps = 0;

            Global_Variables._Temp_sort = new string[Global_Variables.TEMP_APP_DISPLAYNAME.Count];
            for (int i = 0; i < Global_Variables._Temp_sort.Count(); i++)
            {
                Global_Variables._Temp_sort[i] = Global_Variables.TEMP_APP_DISPLAYNAME[i];
            }
            Array.Sort(Global_Variables._Temp_sort);
            Applications = new Apps[Global_Variables.TEMP_APP_DISPLAYNAME.Count];
            for (int i = 0; i < Applications.Count(); i++)
            {
                int index = Global_Variables.TEMP_APP_DISPLAYNAME.IndexOf(Global_Variables._Temp_sort[i]);
                Applications[i] = new Apps
                {
                    DisplayName = Global_Variables.TEMP_APP_DISPLAYNAME[index],
                    KorrigaltDisplayName = Global_Variables.TEMP_CORRECT_APP_DISPLAYNAME[index],
                    RegistryLocate = Global_Variables.TEMP_REGISTRY_PATH[index],
                    InstallLocation = Global_Variables.TEMP_INSTALL_PATH[index],
                    IconPicture = Global_Variables.TEMP_ICON_PATH[index],
                    AppStatus = Global_Variables.TEMP_STATUS[index]
                };
                if (!Global_Variables.TEMP_STATUS[index])
                    _blocked_apps++;

                _App_Flow.Applications_Flow.Controls.Add(Applications[i]);
                _App_Flow.Enabled = true;
            }
            


        }

        private void Close_Label_Click(object sender, EventArgs e)
        {
            Close();
        }



        //Movable Form
        private void Main_Application_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.SetDesktopLocation(MousePosition.X - MvalX, MousePosition.Y - MValY);
            }
        }

        private void Main_Application_MouseDown(object sender, MouseEventArgs e)
        {
            MvalX = e.X;
            MValY = e.Y;
        }

        private void Main_Application_Shown(object sender, EventArgs e)
        {
            Flow_Panel_Holder.Controls.Add(_App_Flow);


            if (!Directory.Exists(Global_Variables.ProgramData + "\\" + Global_Variables.AppName))
            {
                Global_Variables.Installer_BG = new BackgroundWorker();
                Global_Variables.Installer_BG.DoWork += new DoWorkEventHandler(Installer_BG_DoWork);
                Global_Variables.Installer_BG.RunWorkerCompleted += new RunWorkerCompletedEventHandler(Installer_BG_Complete);
                Global_Variables.Installer_BG.RunWorkerAsync();
            }
            else
            {
                Registry_Worker_Starter();
            }
        }
        //End Movable Form
    }
}
