using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Windows.Forms;



namespace PhotoOrganizer.Core
{
    public class LogManager
    {


        public static void Log(string message)
        {
            // todo 
            System.Diagnostics.Debug.WriteLine(message);


        }


        public static void Log(string message, ref TextBox output)
        {
            // todo 
            System.Diagnostics.Debug.WriteLine(message);
            output.AppendText(message);
        }


        public static void Trace(string message, ref TextBox output)
        {
            // todo 
            System.Diagnostics.Debug.WriteLine(message);
            output.AppendText(String.Format("TRACE: {0}", message));
        }



    }
}
