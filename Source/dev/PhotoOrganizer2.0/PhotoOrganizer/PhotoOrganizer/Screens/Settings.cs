using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;

namespace PhotoOrganizer
{
    public partial class frmSettings : Form
    {


        private string _SettingsSourcePath = string.Empty;
        public string SettingsSourcePath
        {
            get{
                _SettingsSourcePath = this.tbSourcePath.Text;
                return _SettingsSourcePath;
            }


            set {
                _SettingsSourcePath = value;
            }
        }


        private string _SettingsTargetPath = string.Empty;
        public string SettingsTargetPath
        {
            get
            {
                _SettingsTargetPath = this.tbtbTargetPath.Text;
                return _SettingsTargetPath;
            }


            set
            {
                _SettingsTargetPath = value;
            }
        }
        
        
        private string _SettingsTemporaryPath = string.Empty;
        public string SettingsTemporaryPath
        {
            get
            {
                _SettingsTemporaryPath = this.tbTempFolder.Text;
                return _SettingsTemporaryPath;
            }


            set
            {
                _SettingsTemporaryPath = value;
            }
        }
        
        
        private string _SettingsExtensions = string.Empty;
        public string SettingsExtensions
        {
            get
            {
                _SettingsExtensions = this.tbExtensions.Text;
                return _SettingsExtensions;
            }


            set
            {
                _SettingsExtensions = value;
            }
        }


        public frmSettings()
        {
            InitializeComponent();
        }

        private void Settings_Load(object sender, EventArgs e)
        {
            LoadConfigurationFromDefault();

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox cb = (ComboBox) sender;

            switch (cb.SelectedText)
            {
                case "New":
                    btnSave.Enabled = false;
                    LoadConfigurationFromDefault();
                    break;
                default:
                    btnSave.Enabled = true;
                    break;

            }
        }

        private void LoadConfigurationFromDefault()
        {
            tbSourcePath.Text = ConfigurationManager.AppSettings["startPath"];
            SettingsSourcePath = tbSourcePath.Text;
            tbtbTargetPath.Text = ConfigurationManager.AppSettings["targetPath"];
            SettingsTargetPath = tbtbTargetPath.Text;
            tbTempFolder.Text = ConfigurationManager.AppSettings["tempFolderPath"];
            SettingsTemporaryPath = tbTempFolder.Text;
            tbExtensions.Text = ConfigurationManager.AppSettings["allowedExtensions"];
            SettingsExtensions = tbExtensions.Text;
        }


        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            SaveConfiguration("");
            this.Close();
     
        }

        private void SaveConfiguration(string workarea = "")
        {
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            KeyValueConfigurationCollection settings = config.AppSettings.Settings;

            if (!String.IsNullOrEmpty(workarea))
                workarea += "_";

            try
            {
                settings[workarea + "targetPath"].Value = this.SettingsTargetPath;
            }
            catch (Exception) { }

            try
            {
                settings[workarea + "startPath"].Value = this.SettingsSourcePath;
            }
            catch (Exception) { }

            try
            {
                settings[workarea + "tempFolderPath"].Value = this.SettingsTemporaryPath;
            }
            catch (Exception) { }

            try
            {
                settings[workarea + "allowedExtensions"].Value = this.SettingsExtensions;
            }
            catch (Exception) { }

            config.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection("appSettings");

        }

    }
}
