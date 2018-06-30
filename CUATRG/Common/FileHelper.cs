using CUATRG.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace CUATRG.Common
{
    public class FileHelper
    {
        public static void SaveFile(tblImage image, HttpPostedFileBase file, string fileName)
        {
            string filePath = string.Empty;
            string relativePath = string.Empty;
            if (file != null)
            {

                filePath = string.Format("~/Images/Albums/{0}", image.tblAlbum.ALB_Name);
                //relativePath = string.Format("~/Images/Albums/{0}/{1}", image.tblAlbum.ALB_Name, fileName);
                var mappedPath = System.Web.Hosting.HostingEnvironment.MapPath(filePath);

                string path = Path.Combine(mappedPath, Path.GetFileName(file.FileName));
                if (!Directory.Exists(filePath))
                    Directory.CreateDirectory(filePath);

                file.SaveAs(path);

            }
        }
    }
}