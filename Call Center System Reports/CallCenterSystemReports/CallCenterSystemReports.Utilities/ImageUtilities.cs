using System;
using System.Configuration;
using System.IO;

namespace CallCenterSystemReports.Utilities
{
   public static class ImageUtilities
    {
        public static byte[] GetBytesBase64(string str,out bool result)
        {
            try
            {
                if (str == null || str == "")
                {
                    result = false;
                    return null;
                }
                byte[] bytes = Convert.FromBase64String(str);
                result=true;
                return bytes;
            }
            catch (Exception)
            {
                result = false;
                return null;
            }
        }

        public static string SaveImage(byte[] Img, string ImgName,out bool result)
        {
            try
            {
                string path = ConfigurationManager.AppSettings["ImageFolderPath"]; //Path
                path += DateTime.Now.Year.ToString() + "\\" + DateTime.Now.Month.ToString() + "\\" + DateTime.Now.Day.ToString() + "\\";

                //Check if directory exist
                if (!System.IO.Directory.Exists(path))
                {
                    System.IO.Directory.CreateDirectory(path); //Create directory if it doesn't exist
                }

                string Imagepath = path + ImgName;
                //set the image path
                string imgPath = Path.Combine(Imagepath);
                File.WriteAllBytes(imgPath, Img);
                result = true;
                return Imagepath;
            }
            catch (Exception ex)
            {
                result = false;
                return null;
            }
        }

        public static string ImageToBase64(string imagePath)
        {
            try
            {
                byte[] imageArray = File.ReadAllBytes(imagePath);
                string base64ImageRepresentation = Convert.ToBase64String(imageArray);
                return base64ImageRepresentation;

            }
            catch (Exception ex)
            {
                return "";
            }
        }
    }
}
