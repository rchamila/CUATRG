
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
                if (album == null)
                {
                    album = dbCtx.tblAlbums.Create();
                    album.ALB_Name = ddlAlbums;
                    album.ALB_Description = ddlAlbums;
                }
                    

                tblEnvironmentalCondition condition = dbCtx.tblEnvironmentalConditions.FirstOrDefault(a => a.ENC_Name == ddlConditions);
                if(condition == null)
                {
                    condition = dbCtx.tblEnvironmentalConditions.Create();
                    condition.ENC_Name = ddlConditions;
                    condition.ENC_Description = ddlConditions;
                }

                tblFeature feature = dbCtx.tblFeatures.FirstOrDefault(a => a.FTR_Name == ddlFeatures);
                if(feature == null)
                {
                    feature = dbCtx.tblFeatures.Create();
                    feature.FTR_Name = ddlFeatures;
                }

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

        [HttpPost]
        public string AddProcessedImage(string name, string filterName)
        {
            var processedImage = new tblProcessedImage();
            try
            {
                var dbCtx = new CUATRGEntities4();

                var masterImage = dbCtx.tblImages.FirstOrDefault(i => i.IMG_Name == name);
                processedImage.tblImage = masterImage ?? throw new InvalidOperationException("Image not found");

                var filter = dbCtx.tblFilters.FirstOrDefault(i => i.FLT_Name == filterName);
                processedImage.tblFilter = filter ?? throw new InvalidOperationException("Filter not found");
                processedImage.CMD_IDFkey = 1;//Fixed color mode to RGP
                log.Info("Saving files started");

                var files = HttpContext.Current.Request.Files.Count > 0 ?
                                            HttpContext.Current.Request.Files : null;

                if (files != null)
                {
                    foreach (var file in files.AllKeys)
                    {
                        var path = string.Format("~/Images/Processed/{0}", filterName);
                        var relativePath = string.Format("Images/Processed/{0}", filterName);
                        var fileName = files[file].FileName;

                        if (fileName.Contains("Image") || fileName.Contains("IMG"))
                        {
                            processedImage.PIM_Name = fileName;
                            processedImage.PIM_Path = relativePath;
                        } 

                        if (ImageHelper.IsProcessedImageExists(processedImage))
                            throw new InvalidOperationException("Processed Image Exists");

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
                log.Info("Saving db data started");

                //Add newStudent entity into DbEntityEntry and mark EntityState to Added
                dbCtx.Entry(processedImage).State = EntityState.Added;

                // call SaveChanges method to save new Student into database
                dbCtx.SaveChanges();
                log.Info("Saving db data completed");

                return "OK";
            }
            catch (Exception ex)
            {
                log.Error(string.Format("Error uploading image {0}", processedImage.PIM_Name));
                return "ERROR:" + ex.Message.ToString();
            }
        }
    }
}
