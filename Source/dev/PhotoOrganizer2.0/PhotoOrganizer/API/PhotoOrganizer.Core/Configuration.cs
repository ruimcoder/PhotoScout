using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;


namespace PhotoOrganizer.Core
{
    public class Configuration
    {



        public static bool LoadConfiguration(out string startPath, out string targetPath, out string allowedExtensions )
        {

            startPath = string.Empty;
            targetPath = string.Empty;
            allowedExtensions = string.Empty;

            bool result = false; 
            // load from configuration file
            try
            {
                startPath = ConfigurationManager.AppSettings["startPath"].ToString();
                targetPath = ConfigurationManager.AppSettings["targetPath"].ToString();
                allowedExtensions = ConfigurationManager.AppSettings["allowedExtensions"].ToString();
                result = true;
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                //tbReport.AppendText(Environment.NewLine + "Unnable to load configuration from file." + Environment.NewLine);
            }

            return result;

        }


    }
}
