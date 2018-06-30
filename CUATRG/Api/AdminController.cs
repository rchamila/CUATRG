
using CUATRG.Common;
using CUATRG.Models;
using LevDan.Exif;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace CUATRG.Api
{
    //[Route("test")]
    public class AdminController : ApiController
    {
        private readonly log4net.ILog log = log4net.LogManager.GetLogger
                (System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        [ActionName("DefaultAction")]
        public string AddImage(string ddlAlbums, string ddlConditions,
                                    string ddlFeatures)
        {
            var image = new tblImage();
            try
            {
                var dbCtx = new CUATRGEntities4();
                tblAlbum album = dbCtx.tblAlbums.FirstOrDefault(a => a.ALB_Name == ddlAlbums);
                tblEnvironmentalCondition condition = dbCtx.tblEnvironmentalConditions.FirstOrDefault(a => a.ENC_Name == ddlConditions);
                tblFeature feature = dbCtx.tblFeatures.FirstOrDefault(a => a.FTR_Name == ddlFeatures);
                 
                image.tblAlbum = album;
                image.tblEnvironmentalCondition = condition;
                image.tblFeature = feature;

                log.Info("Saving files started");

                var files = HttpContext.Current.Request.Files.Count > 0 ?
                                            HttpContext.Current.Request.Files : null;

                if (files != null)
                {
                    foreach (var file in files.AllKeys)
                    {
                        var path = string.Format("~/Images/Albums/{0}", ddlAlbums);
                        var relativePath = string.Format("Images/Albums/{0}", ddlAlbums);
                        var fileName = files[file].FileName;

                        if (fileName.Contains("Image") || fileName.Contains("IMG"))
                        {
                            image.IMG_Name = fileName;
                            image.IMG_Path = relativePath;
                        }
                        else if (fileName.Contains("Sensor"))
                        {
                            image.IMG_SensorDataPath = relativePath;
                        } 

                        if (ImageHelper.IsExists(image))
                            throw new InvalidOperationException("Image Exists");

                        var mappedPath = System.Web.Hosting.HostingEnvironment.MapPath(path);
                        var filePath = Path.Combine(mappedPath, Path.GetFileName(fileName));
                        if (!Directory.Exists(mappedPath))
                        {
                            log.Info("Creating directory");
                            Directory.CreateDirectory(mappedPath);
                        }
                        string user = System.Security.Principal.WindowsIdentity.GetCurrent().User.Value;

                        var uploadedFile = files[file];

                        uploadedFile.SaveAs(filePath);

                       
                    }
                }

                log.Info("Saving files completed"); 

                log.Info("Retriving Exif data started");  
                ImageHelper.ExtractMetaData(image);

                log.Info("Saving db data started");

                //Add newStudent entity into DbEntityEntry and mark EntityState to Added
                dbCtx.Entry(image).State = EntityState.Added;

                // call SaveChanges method to save new Student into database
                dbCtx.SaveChanges();
                log.Info("Saving db data completed");

                return "OK";
            }
            catch (Exception ex)
            {
                log.Error(string.Format("Error saving image {0}", image.IMG_Name), ex);
                return ex.Message;
            }
        } 
    }
}
