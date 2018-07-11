using CUATRG.Common;
using CUATRG.Models;
using CUATRG.ViewModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CUATRG.Controllers
{
    public class AdminController : Controller
    {
        private readonly log4net.ILog log = log4net.LogManager.GetLogger
                (System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private CUATRGEntities4 imageDB = new CUATRGEntities4();

        //
        // GET: /Admin/

        //public ActionResult Index()
        //{
             
        //    return View(viewModel);
        //}

        [Authorize]
        public ActionResult MasterIndex()
        {
            var viewModel = new ListImageViewModel()
            {
                Images = imageDB.tblImages.ToList()
            };
            return View(viewModel);
        }
        [Authorize]
        public ActionResult ProcessedIndex()
        {
            var viewModel = new ListImageViewModel()
            {
                ProcessedImages = imageDB.tblProcessedImages.ToList()
            };
            return View(viewModel);
        }
        [Authorize]
        public ActionResult AddImage()
        {
            var viewModel = new AddImageViewModel()
            {
                Albums = imageDB.tblAlbums.ToList(),
                Conditions = imageDB.tblEnvironmentalConditions.ToList(),
                Features = imageDB.tblFeatures.ToList()
            };

            return View(viewModel);
        }

        [HttpPost]
        [Authorize]
        public ActionResult AddImage(string name, string ddlAlbums, string ddlConditions,
                                    string ddlFeatures, HttpPostedFileBase masterimage, HttpPostedFileBase sensordata)
        {
            var image = new tblImage();

            try
            {
                int albumId = -1;
                if (!int.TryParse(ddlAlbums, out albumId)) 
                    throw new InvalidDataException("Error in album id"); 

                int conditionid = -1;
                if (!int.TryParse(ddlConditions, out conditionid)) 
                    throw new InvalidDataException("Error in condition id"); 

                int filterId = -1;
                if (!int.TryParse(ddlFeatures, out filterId)) 
                    throw new InvalidDataException("Error in filter id");  
               
                //set image data
                image.IMG_Name = name;
                var dbCtx = new CUATRGEntities4();
                tblAlbum album = dbCtx.tblAlbums.FirstOrDefault(a => a.ALB_IDPkey == albumId);
                tblEnvironmentalCondition condition = dbCtx.tblEnvironmentalConditions.FirstOrDefault(a => a.ENC_IDPkey == conditionid);
                tblFeature feature = dbCtx.tblFeatures.FirstOrDefault(a => a.FTR_IDPkey == filterId);

                image.tblAlbum = album;
                image.tblEnvironmentalCondition = condition;
                image.tblFeature = feature;

                image.IMG_Path = string.Format("Images/Albums/{0}", album.ALB_Name);
                image.IMG_SensorDataPath = string.Format("Images/Albums/{0}", album.ALB_Name);

                string[] imagaeData = masterimage.FileName.Split('\\');
                image.IMG_Name = imagaeData[imagaeData.Length - 1];

                if (ImageHelper.IsExists(image))
                    throw new InvalidOperationException("Image Exists"); 

                log.Info("Saving files started");

                FileHelper.SaveFile(image, masterimage, image.IMG_Name);
                FileHelper.SaveFile(image, sensordata, image.IMG_Name.Replace("IMG","SensorData").Replace("jpg","txt"));

                log.Info("Saving files completed");

                 
                log.Info("Retriving Exif data started");
                ImageHelper.ExtractMetaData(image); 

                log.Info("Saving db data started"); 
                //Add newStudent entity into DbEntityEntry and mark EntityState to Added
                dbCtx.Entry(image).State = EntityState.Added;

                // call SaveChanges method to save new Student into database
                dbCtx.SaveChanges();
                log.Info("Saving db data completed"); 
 
                log.Info("View generation completed"); 

                ViewBag.Message = "Successfully uploaded";
              
            }
            catch (Exception ex)
            {
                log.Error(string.Format("Error uploading image {0}", image.IMG_Name));
                ViewBag.Message = "ERROR:" + ex.Message.ToString(); 
            }
            var viewModel = new AddImageViewModel()
            {
                Albums = imageDB.tblAlbums.ToList(),
                Conditions = imageDB.tblEnvironmentalConditions.ToList(),
                Features = imageDB.tblFeatures.ToList()
            };
            return View(viewModel);
        }

        [Authorize]
        public ActionResult AddProcessedImage()
        {
            var viewModel = new AddProcessedImageViewModel()
            {
                Albums = imageDB.tblAlbums.ToList(),
                Conditions = imageDB.tblEnvironmentalConditions.ToList(),
                Features = imageDB.tblFeatures.ToList()
            };

            return View(viewModel);
        }

        [HttpPost]
        [Authorize]
        public ActionResult GetImagesByAlbumId(int albumid)
        {
            List<tblImage> objcity = new List<tblImage>();
            objcity = imageDB.tblAlbums.SingleOrDefault(m => m.ALB_IDPkey == albumid).tblImages.ToList();
            SelectList obgcity = new SelectList(objcity, "IMG_IDPkey", "IMG_Name", 0);
            return Json(obgcity);
        }

        [HttpPost]
        [Authorize]
        public ActionResult AddProcessedImage(string name, string ddlAlbums, string ddlConditions,
                                    string ddlFeatures, HttpPostedFileBase masterimage, HttpPostedFileBase sensordata)
        {
            var image = new tblImage();

            try
            {
                int albumId = -1;
                if (!int.TryParse(ddlAlbums, out albumId)) 
                    throw new InvalidDataException("Error in album id"); 

                int conditionid = -1;
                if (!int.TryParse(ddlConditions, out conditionid)) 
                    throw new InvalidDataException("Error in condition id"); 

                int filterId = -1;
                if (!int.TryParse(ddlFeatures, out filterId)) 
                    throw new InvalidDataException("Error in filter id");  

                //set image data
                image.IMG_Name = name;
                var dbCtx = new CUATRGEntities4();
                tblAlbum album = dbCtx.tblAlbums.FirstOrDefault(a => a.ALB_IDPkey == albumId);
                tblEnvironmentalCondition condition = dbCtx.tblEnvironmentalConditions.FirstOrDefault(a => a.ENC_IDPkey == conditionid);
                tblFeature feature = dbCtx.tblFeatures.FirstOrDefault(a => a.FTR_IDPkey == filterId);

                image.tblAlbum = album;
                image.tblEnvironmentalCondition = condition;
                image.tblFeature = feature;

                string[] imagaeData = masterimage.FileName.Split('\\');
                image.IMG_Name = imagaeData[imagaeData.Length - 1];

                image.IMG_Path = string.Format("Images/Albums/{0}", album.ALB_Name);
                image.IMG_SensorDataPath = string.Format("Images/Albums/{0}", album.ALB_Name);

                if (ImageHelper.IsExists(image)) 
                    throw new InvalidOperationException("Image Exists"); 

                FileHelper.SaveFile(image, masterimage , image.IMG_Name);
                FileHelper.SaveFile(image, sensordata , image.IMG_Name.Replace("IMG", "SensorData").Replace("jpg", "txt"));
                //image.IMG_MetaDataPath = FileHelper.SaveFile(image, metadata);

               
                ImageHelper.ExtractMetaData(image);

                //Add newStudent entity into DbEntityEntry and mark EntityState to Added
                dbCtx.Entry(image).State = EntityState.Added;

                // call SaveChanges method to save new Student into database
                dbCtx.SaveChanges();
                   

                var viewModel = new AddImageViewModel()
                {
                    Albums = imageDB.tblAlbums.ToList(),
                    Conditions = imageDB.tblEnvironmentalConditions.ToList(),
                    Features = imageDB.tblFeatures.ToList()
                };
                ViewBag.Message = "Successfully uploaded";
                return View(viewModel);
            }
            catch (Exception ex)
            {
                log.Error(string.Format("Error uploading image {0}", image.IMG_Name));
                ViewBag.Message = "ERROR:" + ex.Message.ToString();
                return View(new AddImageViewModel());
            }
        }

        [Authorize]
        public ActionResult CreateZipIndex()
        {
           return View();
        }

        [Authorize]
        [HttpPost]
        public ActionResult CreateZip()
        {
            string startPath = Server.MapPath("../") + @"Images\Albums";
            string zipPath = startPath + @"\Albums.zip";
             
            ZipFile.CreateFromDirectory(startPath, zipPath);

            //ZipFile.ExtractToDirectory(zipPath, extractPath);

            return Content("OK");
             
        }
    }
}
