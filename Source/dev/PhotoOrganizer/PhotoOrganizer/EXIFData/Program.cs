using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Text;
//using Microsoft.Expression.Encoder;
//using Microsoft.Expression;
//using Microsoft.Expression.Encoder.Profiles;

namespace EXIFData
{
    class Program
    {
        static void Main(string[] args)
        {
            //MediaItem x = new MediaItem("");
            

             DirectoryInfo di = new DirectoryInfo(".");
             Console.WriteLine("Current dir: " + di.FullName + "\n");
             FileInfo[] rgFiles = di.GetFiles("*.*");
             foreach (FileInfo fi in rgFiles)
             {
                 ProcessFile(fi);
             }


        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="Folder"></param>
        private static void ProcessFile(FileInfo fi)
        {
            Encoding enc = new ASCIIEncoding();

            // we will start by processing JPGs only
            if (fi.Extension.ToUpper() != "JPG")
                return;

            string newFilePath = string.Empty;

            // Create an Image object corresponding to the JPEG file.
            using (Image img = Bitmap.FromFile(fi.FullName))
            {
                    // Extract all PropertyItems from the Image.
                    foreach (PropertyItem pi in img.PropertyItems)
                    {
                        // If the PropertyItem is a timestamp...
                        if (pi.Type == 2 /* text */ && pi.Id == 306 /* date and time */)
                        {
                            // Create a filename based on the timestamp.
                            string newFileName = enc.GetString(pi.Value).Replace(" ", "_").Replace(":", "_");
                            newFilePath = Path.GetDirectoryName(oldFilePath) + @"\" + newFileName.Substring(0, newFileName.Length - 1) + ".jpg";
                        }
                    }

                if (newFilePath != null && newFilePath != oldFilePath)
                {
                    Console.WriteLine(string.Format("Renaming {0} to {1}", oldFilePath, newFilePath));

                    // Rename the file.
                    File.Move(oldFilePath, newFilePath);
                }
            }

            // Rename JPEG files in each subfolder.
            foreach (string folderPath in Directory.GetDirectories(Folder))
            {
                RenameFiles(folderPath);
            }
        }

        private static void RenameFiles(string folderPath)
        {
            throw new NotImplementedException();
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="mimeType"></param>
        /// <returns></returns>
        private static ImageCodecInfo GetEncoderInfo(String mimeType)
        {
            int j;
            ImageCodecInfo[] encoders;
            encoders = ImageCodecInfo.GetImageEncoders();
            for (j = 0; j < encoders.Length; ++j)
            {
                if (encoders[j].MimeType == mimeType)
                    return encoders[j];
            }
            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Filename"></param>
        /// <param name="latDeg"></param>
        /// <param name="latMin"></param>
        /// <param name="latSec"></param>
        /// <param name="lonDeg"></param>
        /// <param name="lonMin"></param>
        /// <param name="lonSec"></param>
        private static void WriteLongLat(string Filename, byte latDeg, byte latMin, double latSec, byte lonDeg, byte lonMin, double lonSec)
        {
            const int length = 25;
            Image Pic;
            byte secHelper;
            byte secRemains;
            PropertyItem[] PropertyItems;
            string FilenameTemp;
            System.Drawing.Imaging.Encoder Enc = System.Drawing.Imaging.Encoder.Transformation;
            EncoderParameters EncParms = new EncoderParameters(1);
            EncoderParameter EncParm;
            ImageCodecInfo CodecInfo = GetEncoderInfo("image/jpeg");

            // load the image to change 
            Pic = Image.FromFile(Filename);
            PropertyItems = Pic.PropertyItems;
            int oldArrLength = PropertyItems.Length;
            PropertyItem[] newProperties = new PropertyItem[oldArrLength];
            Pic.PropertyItems.CopyTo(newProperties, 0);
            newProperties[0].Id = 0x0002;
            newProperties[0].Type = 5;//5-R 4-L 3-S 
            newProperties[0].Len = length;
            newProperties[0].Value =new byte[length];
            try
            {
                for (int i = 0; i < length; i++)
                {
                    newProperties[0].Value[i] = 0;
                }
            }
            catch
            {
            }
            //PropertyItems[0].Value = Pic.GetPropertyItem(4).Value; // bDescription; 
            newProperties[0].Value[0] = latDeg;
            newProperties[0].Value[8] = (byte)latMin;
            secHelper = (byte)(latSec / 2.56);
            secRemains = (byte)((latSec - (secHelper * 2.56)) * 100);
            newProperties[0].Value[16] = secRemains;             // add to the sum bellow x_x_*17_+16 
            newProperties[0].Value[17] = secHelper;             // multiply by 2.56 
            newProperties[0].Value[20] = 100;
            Pic.SetPropertyItem(newProperties[0]);
            newProperties[1].Id = 0x0004;
            newProperties[1].Type = 5;             //5-R 4-L 3-S 
            newProperties[1].Len = length;
            newProperties[1].Value = new byte[length];
            try
            {
                for (int i = 0; i < length; i++)
                {
                    newProperties[1].Value[i] = 0;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error {0}", e.ToString());
            }
            newProperties[1].Value[0] = lonDeg;
            newProperties[1].Value[8] = lonMin;
            secHelper = (byte)(lonSec / 2.56);
            secRemains = (byte)((lonSec - (secHelper * 2.56)) * 100);
            newProperties[1].Value[16] = secRemains;
            // add to the sum bellow x_x_*17_+16 
            newProperties[1].Value[17] = secHelper;
            // multiply by 2.56 
            newProperties[1].Value[20] = 100;
            // multiply by 2.56 

            //PropertyItem current = Pic.GetPropertyItem(2); 
            Pic.SetPropertyItem(newProperties[1]);
            //GPS Version 
            newProperties[0].Id = 0x0000;
            newProperties[0].Type = 1;
            newProperties[0].Len = 4;
            newProperties[0].Value[0] = 2;
            newProperties[0].Value[1] = 2;
            newProperties[0].Value[2] = 0;
            newProperties[0].Value[3] = 0;
            Pic.SetPropertyItem(newProperties[0]);

            //GPS Lat REF 
            newProperties[0].Id = 0x0001;
            newProperties[0].Type = 2;
            newProperties[0].Len = 2;
            newProperties[0].Value[0] = 78;
            newProperties[0].Value[1] = 0;
            Pic.SetPropertyItem(newProperties[0]);
            //GPS Lon REF 
            newProperties[0].Id = 0x0003;
            newProperties[0].Type = 2; //5-R 4-L 3-S 
            newProperties[0].Len = 2;
            newProperties[0].Value[0] = 69;
            newProperties[0].Value[1] = 0;
            Pic.SetPropertyItem(newProperties[0]);

            // we cannot store in the same image, so use a temporary image instead 
            FilenameTemp = Filename + ".temp";
            // for lossless rewriting must rotate the image by 90 degrees! 
            EncParm =
            new EncoderParameter(Enc, (long)EncoderValue.TransformRotate90);
            EncParms.Param[0] = EncParm;
            // now write the rotated image with new description 
            Pic.Save(FilenameTemp, CodecInfo, EncParms);
            // for computers with low memory and large pictures: release memory now 
            Pic.Dispose();
            Pic = null;
            GC.Collect();
            // delete the original file, will be replaced later 
            System.IO.
            File.Delete(Filename);
            // now must rotate back the written picture 
            Pic = Image.FromFile(FilenameTemp);
            EncParm = new EncoderParameter(Enc, (long)EncoderValue.TransformRotate270);
            EncParms.Param[0] = EncParm;
            Pic.Save(Filename, CodecInfo, EncParms);
            // release memory now 
            Pic.Dispose();
            Pic = null;
            GC.Collect();
            // delete the temporary picture 
            System.IO.
            File.Delete(FilenameTemp);
        }


        public static string oldFilePath { get; set; }

        public static string Folder { get; set; }
    } // class

} // namespace
