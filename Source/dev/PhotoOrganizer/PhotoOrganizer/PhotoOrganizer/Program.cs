using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using CommonFramework.Core;
//using Microsoft.Expression;

namespace PhotoOrganizer
{
    public partial class Program
    {

        #region Configuration zone

        static bool PackIntoContainerFolder = true;
        static long ContainerFolderMaxSize = 4402341478; // 4.1 GB

        static string ContainerFolderNameFormat = "#{0}";

        static int ContainerFolderCounterStart = 50;
        static int CurrentFolderCounterStart = -1;
        static long MinimumFreeSpaceInBytes = 52428800; // 50 MB

        static string[] allowedExtensions;

        #endregion



        static void Main(string[] args)
        {

            long availableContainerSpace = 0;

            Config myConfig = new Config();

            // load the config
            ContainerFolderCounterStart = int.Parse(myConfig.GetConfigValue("ContainerFolderCounterStart", ContainerFolderCounterStart.ToString()));
            ContainerFolderNameFormat = myConfig.GetConfigValue("ContainerFolderNameFormat", ContainerFolderNameFormat);
            allowedExtensions = myConfig.GetConfigValue("IncludedFileExtensions").Split('|');

             DirectoryInfo di = new DirectoryInfo(".");
             Console.WriteLine("Current dir: " + di.FullName + "\n");
             FileInfo[] rgFiles = GetProcessableFileList(di);


             // validar se é para empacotar em folders de DVD
             if (PackIntoContainerFolder)
             {
                //generate the pack folder name
                 string packFolderName = GenerateContainerFolderName(ContainerFolderNameFormat, ContainerFolderCounterStart);

                 try
                 {
                     DirectoryInfo pki = GetNextAvailableContainerFolder(di);
                     availableContainerSpace = ContainerFolderMaxSize - GetDirectorySize(di);

                     //DirectoryInfo[] diDVDCol = di.GetDirectories(packFolderName);

                     //// already exists, check for folder size
                     //if (diDVDCol.Length == 1)
                     //{
                     //    // get size
                     //    long dsize = ProcessDirectorySize(diDVDCol[0]);

                     //    // check if max is reached
                     //    if (dsize >= ContainerFolderMaxSize)
                     //    {
                     //        // if max is reached, create new one and upgrade counter

                     //    }
                     //    else
                     //    {
                     //        availableContainerSpace = dsize;
                     //    }
                     //}
                     //else
                     //{
                     //    di.CreateSubdirectory(packFolderName);
                     //}


                     foreach (FileInfo fi in rgFiles)
                     {
                         if (availableContainerSpace < MinimumFreeSpaceInBytes)
                         {
                             // we need to create a new container because the last one is full
                             
                             // reset aailable space
                             availableContainerSpace = ContainerFolderMaxSize;
                         }


                         try
                         {

                             Console.Write("Processing file [" + fi.Name + "]");
                             DateTime fileTs = fi.LastWriteTime;
                             string folderName = fileTs.Year + "-" + fileTs.Month + "-" + fileTs.Day;
                             Console.Write("   Target Folder -> [" + folderName + "]");

                             string newFolderName = "./" + folderName;
                             
                             try
                             {
                                 if (!Directory.Exists(newFolderName))
                                 {
                                     pki.CreateSubdirectory(newFolderName);
                                     Console.WriteLine("Created new folder: " + newFolderName);
                                 }
                             }
                             catch
                             { }

                             string newFileName = newFolderName + "/" + fi.Name;
                             Console.WriteLine("Moving file to: " + newFileName);

                             fi.MoveTo(newFileName);

                             // keep score with the available space
                             availableContainerSpace = availableContainerSpace - fi.Length;
                         }
                         catch (Exception ex)
                         {
                             Console.WriteLine(ex.Message);
                         }
                     }


                 } catch (DirectoryNotFoundException de)
                 {
                     di.CreateSubdirectory("./" + packFolderName);
                     
                 }

             }



        }

        /// <summary>
        /// Generates the name of the container folder.
        /// </summary>
        /// <param name="lContainerFolderNameFormat">The l container folder name format.</param>
        /// <param name="lContainerCounter">The l container counter.</param>
        /// <returns></returns>
        static string GenerateContainerFolderName(string lContainerFolderNameFormat, int lContainerCounter)
        {
            return string.Format(ContainerFolderNameFormat, lContainerCounter.ToString());
        }


        /// <summary>
        /// Gets the processable file list.
        /// </summary>
        /// <param name="di">The di.</param>
        /// <returns></returns>
        static FileInfo[] GetProcessableFileList(DirectoryInfo di)
        {

            FileInfo[] fileList = di.GetFiles("*.*");

            List <FileInfo> result = new List<FileInfo>();
            foreach (FileInfo fi in fileList)
            {
                foreach( string ext in allowedExtensions)
                {
                    // we only add files with the allowed extensions
                    if (fi.Extension.ToLower() == ext.ToLower())
                    {
                        result.Add(fi);
                        break;
                    }

                }
            }

            return result.ToArray();
        }


        /// <summary>
        /// Gets the next available container folder.
        /// </summary>
        /// <param name="baseDi">The base di.</param>
        /// <returns></returns>
        static DirectoryInfo GetNextAvailableContainerFolder(DirectoryInfo baseDi)
        {
            DirectoryInfo result = null;
            bool folderExists = false;
            int ContainerCounter = -1;

            // approach 1: try to get the folder by incrementing the counter
            for (int i = ContainerFolderCounterStart; ; i++)
            {
                try
                {
                    DirectoryInfo[] dirs = baseDi.GetDirectories(string.Format(ContainerFolderNameFormat, i));

                    // we are giving a search scope that can only return 1
                    if (dirs.Length == 1)
                    {
                        if (HasContainerFreeSpace(dirs[0]))
                        {
                            result = dirs[0];
                            folderExists = true;
                            ContainerCounter = i;
                        }
                    }
                    else
                    {
                        // this should never occur
                        ///TODO: treat as exception??

                    }
                } catch (Exception)
                {
                    // either exception will mean the creation of a new folder
                    i++;
                    ContainerCounter = i;
                    break;
                } 
            }

            DirectoryInfo[] dvds = baseDi.GetDirectories(string.Format(ContainerFolderNameFormat, "*"));

            //// approach 2: 
            //foreach (DirectoryInfo dvd in dvds)
            //{
            //    if (result == null)
            //        result = dvd;
            //    else
            //    {
            //        int comp = result.Name.CompareTo(dvd.Name);
            //        if (result.Name.CompareTo(dvd.Name))
            //            result = dvd;
            //    }

            //}

            if (!folderExists)
            {
                // lets create it!
                if (ContainerCounter > -1)
                    result = baseDi.CreateSubdirectory(string.Format(ContainerFolderNameFormat, ContainerCounter.ToString()));
            }


            CurrentFolderCounterStart = ContainerCounter;
            // return the DirectoryInfo
            return result;

        }

        /// <summary>
        /// Processes the directory.
        /// </summary>
        /// <param name="ldi">The ldi.</param>
        /// <returns></returns>
        static long ProcessDirectorySize(DirectoryInfo ldi)
        {
            long result = 0;

            // recursive processing
            DirectoryInfo[] arrDi = ldi.GetDirectories();
            if (arrDi.Length > 0 )
            {

                for (int i = 0; i < arrDi.Length; i++)
                {
                    result += ProcessDirectorySize(arrDi[i]);
                }
            }
            
            // finaly process the files in the current 
            result += GetDirectorySize(ldi);

            return result;
        }


        /// <summary>
        /// Determines whether [has container free space] [the specified fdi].
        /// </summary>
        /// <param name="fdi">The fdi.</param>
        /// <returns>
        /// 	<c>true</c> if [has container free space] [the specified fdi]; otherwise, <c>false</c>.
        /// </returns>
        static bool HasContainerFreeSpace(DirectoryInfo fdi)
        {

            bool result = false;
            long dirSize = GetDirectorySize(fdi);
            if (dirSize < (ContainerFolderMaxSize - MinimumFreeSpaceInBytes))
                result = true;

            return result;

        }



        /// <summary>
        /// Gets the size of the directory.
        /// </summary>
        /// <param name="fdi">The fdi.</param>
        /// <returns></returns>
        static long GetDirectorySize(DirectoryInfo fdi)
        {
            // 1
            // Get array of all file names.
            FileInfo[] arrfi = fdi.GetFiles();

            // 2
            // Calculate total bytes of all files in a loop.
            long b = 0;
            foreach (FileInfo fi in arrfi)
            {
                b += fi.Length;
                Console.WriteLine("{0} -> {1}", fi.FullName, fi.Length);
            }

            // 4
            // Return total size
            return b;
        }


    }
}
