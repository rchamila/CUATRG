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
            try
            {
                int albumId = -1;
                if (!int.TryParse(ddlAlbums, out albumId))
                {
                    throw new InvalidDataException("Error in album id");
                }

                int conditionId = -1;
                if (!int.TryParse(ddlConditions, out conditionId))
                {
                    throw new InvalidDataException("Error in condition id");
                } 

                int filterId = -1;
                if (!int.TryParse(ddlFeatures, out filterId))
                {
                    throw new InvalidDataException("Error in filter id");
                }
                var image = new tblImage();

                image.ALB_IDFkey = albumId;
                image.ENC_IDFkey = conditionId;
                image.FTR_IDFkey = filterId;

                log.Info("Saving files started");

                var files = HttpContext.Current.Request.Files.Count > 0 ?
                HttpContext.Current.Request.Files : null;
                var filePath = "";
                var relativePath = "";
                if (files != null)
                {
                    foreach (var file in files.AllKeys)
                    {
                        var fileName = Path.GetFileName(files[file].FileName);

                        filePath = string.Format("~/Images/Albums/{0}", ddlAlbums);
                        relativePath = string.Format("Images/Albums/{0}/{1}", ddlAlbums, fileName);


                        if (!Directory.Exists(filePath))
                        {
                            log.Info("Creating directory");
                            Directory.CreateDirectory(filePath);
                        }

                        files[file].SaveAs(filePath);

                        if (fileName.Contains("Image"))
                        {
                            image.IMG_Name = fileName;
                            image.IMG_Path = relativePath;
                        }
                        else if (fileName.Contains("Sensor"))
                        {
                            image.IMG_SensorDataPath = relativePath;
                        }
                        else if (fileName.Contains("Meta"))
                        {
                            image.IMG_MetaDataPath = relativePath;
                        }
                        else
                        {
                            throw new Exception("Invalid file");
                        }
                        //return HttpContext.Current.Request.;
                    }
                }

                log.Info("Saving files completed");

                string path = Path.Combine(image.IMG_Path, Path.GetFileName(image.IMG_Name));
                log.Info("Retriving Exif data started");

                try
                {


                    ExifTagCollection _exif = new ExifTagCollection(path);

                    foreach (ExifTag tag in _exif)
                    {
                        tblMetaType metaType = new tblMetaType();
                        metaType.MTT_IDPkey = tag.Id; ;
                        metaType.MTT_Name = tag.FieldName;
                        tblMetaData meta = new tblMetaData();
                        meta.MTD_Value = tag.Value.Length > 500 ? "" : tag.Value;
                        meta.MTT_IDFkey = tag.Id;
                        meta.tblImage = image;
                        image.tblMetaDatas.Add(meta);
                    }
                    log.Info("Retriving Exif data completed " + image.tblMetaDatas.Count());
                }
                catch (Exception ex)
                {
                    log.Error("Error reading exif", ex);
                    throw;
                }

                log.Info("Saving db data started");
                //create DBContext object
                using (var dbCtx = new CUATRGEntities4())
                {
                    try
                    {
                        //Add newStudent entity into DbEntityEntry and mark EntityState to Added
                        dbCtx.Entry(image).State = EntityState.Added;

                        // call SaveChanges method to save new Student into database
                        dbCtx.SaveChanges();
                        log.Info("Saving db data completed");
                    }
                    catch (Exception ex)
                    {
                        log.Error("Error saving data", ex);
                        throw;
                    }
                }
                //return IActionResult;
                return "All Good";
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }

        private string SaveFile(string ddlAlbums, HttpPostedFileBase file)
        {
            string filePath = string.Empty;
            string relativePath = string.Empty;
            if (file != null && file.ContentLength > 0)
            {
                try
                {
                    log.Info("Reading sensor data file");
                    filePath = string.Format("~/Images/Albums/{0}", ddlAlbums);
                    relativePath = string.Format("Images/Albums/{0}/{1}", ddlAlbums, file.FileName);
                    //string path = Path.Combine(Server.MapPath(filePath), Path.GetFileName(file.FileName));
                    //.Info(Server.MapPath(filePath));
                    if (!Directory.Exists(filePath))
                    {
                        log.Info("Creating directory");
                        Directory.CreateDirectory(filePath);
                    }
                    file.SaveAs(filePath);

                }
                catch (Exception ex)
                {
                    throw;
                }
            }
            return relativePath;
        }


        private bool IsExists(string imageData)
        {
            var dbCtx = new CUATRGEntities4();

            tblImage image = new tblImage();
            string[] data = imageData.Split('\\');
            image.IMG_Name = data[data.Length -1];
            image.tblFeature = dbCtx.tblFeatures.SingleOrDefault(f => f.FTR_Name == data[data.Length - 2]);
            image.tblEnvironmentalCondition = dbCtx.tblEnvironmentalConditions.SingleOrDefault(e => e.ENC_Name == data[data.Length - 3]);
            image.tblAlbum = dbCtx.tblAlbums.SingleOrDefault(a => a.ALB_Name == data[data.Length - 4]);

            return dbCtx.tblImages.Any<tblImage>(i => i.IMG_Name == image.IMG_Name
                                                    && i.ALB_IDFkey == image.tblAlbum.ALB_IDPkey
                                                    && i.FTR_IDFkey == image.tblFeature.FTR_IDPkey
                                                    && i.ENC_IDFkey == image.tblEnvironmentalCondition.ENC_IDPkey);

             
        }

        
    }
}
