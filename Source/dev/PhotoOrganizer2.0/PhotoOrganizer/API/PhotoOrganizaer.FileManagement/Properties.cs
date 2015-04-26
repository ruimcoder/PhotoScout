using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Imaging;


namespace PhotoOrganizaer.FileManagement
{
    public class Properties
    {
        
        public static Dictionary<String, String> GetFileProperties(string filePath)
        {

            if (string.IsNullOrEmpty(filePath))
                throw new ArgumentNullException("filePath", "Parameter filePath cannot be null.");

            if (filePath.IndexOfAny(Path.GetInvalidFileNameChars()) > -1)
            {
                throw new ArgumentException("filePath", "Parameter filePath contains invalid characters.");
            }

            Dictionary<String, String> result = new Dictionary<string, string>();

            Image fi = new Bitmap(filePath);

            // Get the PropertyItems property from image.
            PropertyItem[] propItems = fi.PropertyItems;

            // Convert the value of the second property to a string, and display  
            // it.
            System.Text.ASCIIEncoding encoding = new System.Text.ASCIIEncoding();


            foreach (PropertyItem propItem in propItems)
            {
                result.Add(propItem.Id.ToString(), encoding.GetString(propItem.Value));

            }

            return result;

        }



    }
}
