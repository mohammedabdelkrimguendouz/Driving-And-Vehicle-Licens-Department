using DVLD_Buisness;
using EASendMail;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD.Global_Classes
{
    public class clsUtil
    {
        static public string GenerateGuid()
        {
            Guid NewGuid = Guid.NewGuid();
            return NewGuid.ToString();
        }

        static public bool CreateFolderIsNotExist(string FolderPath)
        {
            if (!Directory.Exists(FolderPath)) 
            {
                try
                {
                    Directory.CreateDirectory(FolderPath);
                    return true;
                }
                catch(Exception Ex)
                {
                    clsEventLog.WriteEvent($" Message : {Ex.Message} \n\n Source : {Ex.Source} \n\n Target Site :  {Ex.TargetSite} \n\n Stack Trace :  {Ex.StackTrace}", EventLogEntryType.Error);
                    return false;
                }
                
            }
            return true;
        }

        
        static public string ReplaceFileNameWithGuid(string FileName)
        {
            FileInfo fi = new FileInfo(FileName) ;
            string Ext=fi.Extension;
            return GenerateGuid() + Ext;
        }

        static public async Task<string?> CopyImageToProjectImagesFolder(IFormFile ImageFile)
        {
            string DestinationFolder = @"C:\DVLD-People-Images\";
            if (!CreateFolderIsNotExist(DestinationFolder))
                return null;

            
            string DestinationFile = DestinationFolder + ReplaceFileNameWithGuid(ImageFile.FileName);

       

            try
            {
                using (var stream = new FileStream(DestinationFile, FileMode.Create))
                {
                    await ImageFile.CopyToAsync(stream);
                }
                
            }
            catch(Exception Ex)
            {
                clsEventLog.WriteEvent($" Message : {Ex.Message} \n\n Source : {Ex.Source} \n\n Target Site :  {Ex.TargetSite} \n\n Stack Trace :  {Ex.StackTrace}", EventLogEntryType.Error);
                return null;
            }
           
            return DestinationFile;
        }

        static public bool DeleteImageFromProjectImagesFolder(string SourceFile)
        {
            if (SourceFile == "")
                return true;
            try
            {
                File.Delete(SourceFile);
            }
            catch (Exception Ex)
            {
                clsEventLog.WriteEvent($" Message : {Ex.Message} \n\n Source : {Ex.Source} \n\n Target Site :  {Ex.TargetSite} \n\n Stack Trace :  {Ex.StackTrace}", EventLogEntryType.Error);
                return false;
            }
            return true;
        }


    }
}
