using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace PhotoOrganizer.Core
{
    public class ExceptionHandler
    {


        public static bool HandleException(Exception ex)
        {

            bool result = true;

            // prepare the message to be logged
            String message = String.Format( "PhotoOrganizer exception caught: {0} \n{1}", 
                                            ex.Message,
                                            ex.StackTrace);

            LogManager.Log(message);




            message = null;

            return result;
        }
    }
}
