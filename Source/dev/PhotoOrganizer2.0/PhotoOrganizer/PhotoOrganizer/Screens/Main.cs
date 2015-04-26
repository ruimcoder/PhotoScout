using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using PhotoOrganizer.Core;
using PhotoOrganizer.Screens;



namespace PhotoOrganizer
{
    public partial class Main : Form
    {


        //TODO: REFACTURE to config
        string startPath = "c:\\temp\\source";
        string tempPath = "c:\\temp\\source";
        string targetPath = "c:\\temp\\target";
        string allowedExtensions = ".jpg;.jpeg;.raw;.tif;.tiff;.NEF";

        bool runInConsoleMode = false;


        bool interrupt = false;

        public Main(string[] args)
        {

            


            if (!runInConsoleMode)
            {
                InitializeComponent();
            }
            else
            {

            }

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ValidateConfiguration();
        }



        private bool ProcessCommandLineParameters(string[] args)
        {
            bool result = false;

            for (int i = 0; i < args.Length; i++)
            {
                Console.WriteLine("Arg[{0}] = [{1}]", i, args[i]);
            }
            return result;
        }

        private void ValidateConfiguration()
        {

            // load from configuration file

            if (Core.Configuration.LoadConfiguration(out this.startPath, out this.targetPath, out this.allowedExtensions))
            {
                this.tbSource.Enabled = false;
                this.tbSource.Text = this.startPath;
                this.tbSource.Enabled = true;

                this.tbTarget.Enabled = false;
                this.tbTarget.Text = this.targetPath;
                this.tbTarget.Enabled = true;

                LogManager.Trace(String.Format("Current Configuration:{3}Start Path: {0}{3}Target Path:{1}{3}Allowed Extensions:{2}{3}----------------------------------{3}", startPath, targetPath, allowedExtensions, Environment.NewLine), ref tbReport);

            }
            else
            {
                tbReport.AppendText("Error loading configuration");
            }

        }


        private void btnStart_Click(object sender, EventArgs e)
        {
            this.btnStart.Enabled = false;
            this.btnStop.Enabled = true;

            try
            {

                DirectoryInfo di = new DirectoryInfo(startPath);

                if (!di.Exists)
                {
                    tbReport.AppendText(Environment.NewLine + "Invalid source Directory." + Environment.NewLine);
                    this.btnStart.Enabled = true;
                    this.btnStop.Enabled = false;
                    return;
                }


                this.pgBar.Maximum = di.EnumerateFiles().Count();
                this.pgBar.Step = 1;

                if (this.pgBar.Maximum == 0)
                {
                    tbReport.AppendText(Environment.NewLine + "No files to process. Directory is empty." + Environment.NewLine);
                    this.btnStart.Enabled = true;
                    this.btnStop.Enabled = false;
                    return;
                }
                else
                {
                    tbReport.AppendText(String.Concat(Environment.NewLine,"Found ", this.pgBar.Maximum, " files to process.",  Environment.NewLine));
                }


                int counter = 0;

                foreach (FileInfo fi in di.GetFiles())
                {
                    UpdateProgressBar(counter++);

                    if(allowedExtensions.Contains(fi.Extension))
                    {

                        DateTime timeTaken = fi.LastWriteTime;
                        String tYear = timeTaken.Year.ToString();
                        String tMonth = timeTaken.Month.ToString("D2");
                        String tDay = timeTaken.Day.ToString("D2");
                        String tFinalFolder = String.Concat(tYear, "-", tMonth, "-", tDay);
                        DirectoryInfo tdi = null;

                        // check if target path exits
                        try
                        {
                            tdi = new DirectoryInfo(String.Format("{0}\\{1}\\{2}\\{3}", targetPath, tYear, tMonth, tFinalFolder));
                            if (!tdi.Exists)
                            {
                                tdi.Create();
                            }

                            string target = String.Format("{0}\\{1}\\{2}\\{3}\\{4}", targetPath, tYear, tMonth, tFinalFolder, fi.Name); 

                            fi.MoveTo(target);
                            tbReport.AppendText(String.Format("{0}{1} moved to {2}", Environment.NewLine, fi.FullName, target));
                        
                        } catch (Exception exp)
                        {
                            tbReport.AppendText(String.Format("{0}{1}", Environment.NewLine, exp.Message));
                            // path doesn't exist
                        }
                    }
                    else
                    {
                        tbReport.AppendText(String.Format("{0}SKIPPED: {1}{0}", Environment.NewLine, fi.FullName));
                    }

                    this.pgBar.PerformStep();

                    if (this.interrupt)
                    {
                        tbReport.AppendText("Operation interrupted by user." + Environment.NewLine);
                        break;
                    }

                    this.pgBar.Value = 0;


                }
            
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("PhotoOrganizer error occurred: {0}, \n{1}", ex.Message, ex.StackTrace);
                if(ExceptionHandler.HandleException(ex))
                {
                    ErrorBox errorBox = new ErrorBox();
                    errorBox.MessageTitle = string.Format("PhotoOrganizer error occurred: {0}", ex.Message);
                    errorBox.Message = ex.StackTrace;
                    errorBox.Show();
                }
            }

            this.btnStart.Enabled = true;
            this.btnStop.Enabled = false;


        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            this.interrupt = true;
        }

        private void tbSource_TextChanged(object sender, EventArgs e)
        {
            this.startPath = this.tbSource.Text;
        }

        private void tbTarget_TextChanged(object sender, EventArgs e)
        {
            this.targetPath = this.tbTarget.Text;
        }

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmSettings settings = new frmSettings();
            settings.FormClosed += new FormClosedEventHandler(Settings_FormClosed);
            settings.Show();
        }

        private void Settings_FormClosed(object sender, FormClosedEventArgs e)
        {
            tbReport.AppendText("Refreshing configuration settings." + Environment.NewLine);
            ValidateConfiguration();
        }

        private void folderBrowserDialog1_HelpRequest(object sender, EventArgs e)
        {
           
        }

        private void UpdateProgressBar(int current)
        {
            pgBar.PerformStep();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (folderBrowserSource.ShowDialog() == DialogResult.OK)
            {
                this.tbSource.Text = folderBrowserSource.SelectedPath;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (folderBrowserTarget.ShowDialog() == DialogResult.OK)
            {
                this.tbTarget.Text = folderBrowserTarget.SelectedPath;
            }
        }
    }
}
