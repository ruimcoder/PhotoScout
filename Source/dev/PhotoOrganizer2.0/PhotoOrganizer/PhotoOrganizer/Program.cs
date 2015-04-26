using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PhotoOrganizer
{
    static class Program
    {

        static List<string> parameters = new List<string>();


        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {

            if (args != null && args.Count() > 0)
                ProcessCommandLineParameters(args);


            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Main(args));
        }


        static void ProcessCommandLineParameters(string[] args)
        {
            parameters = args.ToList<string>();

        }




    }
}
