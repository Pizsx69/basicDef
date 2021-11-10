using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace basicDef_Block
{
    public partial class Apps : UserControl
    {
        public string App_DisplayName;
        public string Correct_App_DisplayName;
        public string Registry_Path;
        public string Install_Path;
        public string Icon_Path;
        public bool status;





        public Apps()
        {
            InitializeComponent();
        }

        public string DisplayName
        {
            get { return App_DisplayName; }
            set
            {
                App_DisplayName = value;
                Display_name.Text = App_DisplayName.ToLower();
            }
        }

        public string InstallLocation
        {
            get { return Install_Path; }
            set
            {
                Install_Path = value;
                //App_InstallPath.Text = install_Locale;
            }
        }

        public string KorrigaltDisplayName
        {
            get { return Correct_App_DisplayName; }
            set
            {
                Correct_App_DisplayName = value;

            }
        }

        public string RegistryLocate
        {
            get { return Registry_Path; }
            set
            {
                Registry_Path = value;

            }
        }

        public bool AppStatus
        {
            get { return status; }
            set
            {
                status = value;
                if (status)
                {
                    app_button_picture.BackgroundImage = Properties.Resources.Button_active;
                    //App_Status.Text = "Elérhető";

                    //Alkalmazas_gomb_label.Text = "Tiltás";
                    //Alkalmazas_gomb_label.Location = new Point(Alkalmazas_Gomb_picture.Width / 2 - Alkalmazas_gomb_label.Width / 2, Alkalmazas_Gomb_picture.Height / 2 - Alkalmazas_gomb_label.Height / 2);
                }
                else
                {
                    app_button_picture.BackgroundImage = Properties.Resources.unblock_button;

                   // Alkalmazas_gomb_label.Text = "Feloldás";
                   // Alkalmazas_gomb_label.Enabled = true;
                   // Alkalmazas_Gomb_picture.Enabled = true;
                   // Alkalmazas_gomb_label.Location = new Point(Alkalmazas_Gomb_picture.Width / 2 - Alkalmazas_gomb_label.Width / 2, Alkalmazas_Gomb_picture.Height / 2 - Alkalmazas_gomb_label.Height / 2);
                   //
                   // Alkalmazas_Gomb_picture.Image = Properties.Resources.Red_button_Static;
                   // Alkalmazas_Gomb_picture.MouseDown += new System.Windows.Forms.MouseEventHandler(Alkalmazas_Gomb_picture_MouseDown_tiltott);
                   // Alkalmazas_Gomb_picture.MouseEnter += new System.EventHandler(Alkalmazas_Gomb_picture_MouseEnter_tiltott);
                   // Alkalmazas_Gomb_picture.MouseLeave += new System.EventHandler(Alkalmazas_Gomb_picture_MouseLeave_tiltott);
                   // Alkalmazas_gomb_label.MouseEnter += new System.EventHandler(Alkalmazas_gomb_label_MouseEnter_tiltott);
                   //App_Status.Text = "Tiltott";
                }


            }
        }

        public string IconPicture
        {
            get { return Icon_Path; }
            set
            {
                Icon_Path = value;
                try
                {
                    Icon_List.Images.Add(System.Drawing.Icon.ExtractAssociatedIcon(Icon_Path));
                    App_displayicon.Image = Icon_List.Images[0];
                }
                catch (Exception)
                {


                }
            }
        }



        private void app_button_picture_Click(object sender, EventArgs e)
        {
            if (status)
            {
                app_button_picture.BackgroundImage = Properties.Resources.unblock_button;
                Global_Methods.App_Blocking_Method(App_DisplayName);
                Global_Variables.Registry_BG.RunWorkerAsync();
            }
            else
            {
                app_button_picture.BackgroundImage = Properties.Resources.Button_active;
                Global_Methods.Unblocking_Method(App_DisplayName);
                Global_Variables.Registry_BG.RunWorkerAsync();
            }

        }
    }
}
