using CUATRG.Models;
using CUATRG.ViewModels;
using FileHelpers;
using LevDan.Exif;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
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

        public ActionResult Index()
        {
            var viewModel = new ListImageViewModel()
            {
                Images = imageDB.tblImages.ToList()
            };
            return View(viewModel);
        }

        public ActionResult ProcessedIndex()
        {
            var viewModel = new ListImageViewModel()
            {
                ProcessedImages = imageDB.tblProcessedImages.ToList()
            };
            return View(viewModel);
        }

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
        public ActionResult AddImage(string name, string ddlAlbums, string ddlConditions,
                                    string ddlFeatures, HttpPostedFileBase masterimage, HttpPostedFileBase sensordata, HttpPostedFileBase metadata)
        {
            try
            {
                int albumId = -1;
                if (int.TryParse(ddlAlbums, out albumId))
                {
                    //results = imageDB.tblImages.ToList().FindAll(i => i.ALB_IDFkey == albumId);
                }

                int conditionid = -1;
                if (int.TryParse(ddlConditions, out conditionid))
                {
                    //results = results.FindAll(i => i.ENC_IDFkey == conditionid);
                }

                int featureId = -1;
                if (int.TryParse(ddlFeatures, out featureId))
                {
                    //results = results.FindAll(i => i.FTR_IDFkey == featureId);
                }

                int filterId = -1;
                if (int.TryParse(ddlFeatures, out filterId))
                {
                    //results = results.FindAll(i => i.FTR_IDFkey == featureId);
                }
                var image = new tblImage();

                //set student name
                image.IMG_Name = name;
                image.ALB_IDFkey = albumId;
                image.ENC_IDFkey = conditionid;
                image.FTR_IDFkey = filterId;

                log.Info("Saving files started");
                
                image.IMG_Path = SaveFile(ddlAlbums, masterimage);
                image.IMG_SensorDataPath = SaveFile(ddlAlbums, sensordata);
                image.IMG_MetaDataPath = SaveFile(ddlAlbums, metadata);

                log.Info("Saving files completed");

                string filePath = string.Format("~/Images/Albums/{0}", ddlAlbums);
                string relativePath = string.Format("Images/Albums/{0}/{1}", ddlAlbums, masterimage.FileName);
                string path = Path.Combine(Server.MapPath(filePath), Path.GetFileName(masterimage.FileName));


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
                    log.Info("Retriving Exif data completed "+ image.tblMetaDatas.Count());
                }
                catch (Exception ex)
                {
                    log.Error("Error reading exif", ex);
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
                    }
                }

             


                var viewModel = new AddImageViewModel()
                {
                    Albums = imageDB.tblAlbums.ToList(),
                    Conditions = imageDB.tblEnvironmentalConditions.ToList(),
                    Features = imageDB.tblFeatures.ToList()
                };

                log.Info("View generation completed");


                ViewBag.Message = "Successfully uploaded";
                return View(viewModel);
            }
            catch (Exception ex)
            {
                ViewBag.Message = "ERROR:" + ex.Message.ToString();
                return View(new AddImageViewModel());
            }           
        }


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
        public ActionResult GetImagesByAlbumId(int albumid)
        {
            List<tblImage> objcity = new List<tblImage>();
            objcity = imageDB.tblAlbums.SingleOrDefault(m => m.ALB_IDPkey == albumid).tblImages.ToList();
            SelectList obgcity = new SelectList(objcity, "IMG_IDPkey", "IMG_Name", 0);
            return Json(obgcity);
        }

        [HttpPost]
        public ActionResult AddProcessedImage(string name, string ddlAlbums, string ddlConditions,
                                    string ddlFeatures, HttpPostedFileBase masterimage, HttpPostedFileBase sensordata, HttpPostedFileBase metadata)
        {
            try
            {
                int albumId = -1;
                if (int.TryParse(ddlAlbums, out albumId))
                {
                    //results = imageDB.tblImages.ToList().FindAll(i => i.ALB_IDFkey == albumId);
                }

                int conditionid = -1;
                if (int.TryParse(ddlConditions, out conditionid))
                {
                    //results = results.FindAll(i => i.ENC_IDFkey == conditionid);
                }

                int featureId = -1;
                if (int.TryParse(ddlFeatures, out featureId))
                {
                    //results = results.FindAll(i => i.FTR_IDFkey == featureId);
                }

                int filterId = -1;
                if (int.TryParse(ddlFeatures, out filterId))
                {
                    //results = results.FindAll(i => i.FTR_IDFkey == featureId);
                }
                var image = new tblImage();

                //set student name
                image.IMG_Name = name;
                image.ALB_IDFkey = albumId;
                image.ENC_IDFkey = conditionid;
                image.FTR_IDFkey = filterId;
                image.IMG_Path = SaveFile(ddlAlbums, masterimage);
                image.IMG_SensorDataPath = SaveFile(ddlAlbums, sensordata);
                image.IMG_MetaDataPath = SaveFile(ddlAlbums, metadata);

                string filePath = string.Format("~/Images/Albums/{0}", ddlAlbums);
                string relativePath = string.Format("Images/Albums/{0}/{1}", ddlAlbums, masterimage.FileName);
                string path = Path.Combine(Server.MapPath(filePath), Path.GetFileName(masterimage.FileName));

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
                //create DBContext object
                using (var dbCtx = new CUATRGEntities4())
                {
                    try
                    {
                        //Add newStudent entity into DbEntityEntry and mark EntityState to Added
                        dbCtx.Entry(image).State = EntityState.Added;

                        // call SaveChanges method to save new Student into database
                        dbCtx.SaveChanges();
                    }
                    catch (Exception ex)
                    {
                        int i = 0;
                    }

                }

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
                ViewBag.Message = "ERROR:" + ex.Message.ToString();
                return View(new AddImageViewModel());
            }
        }

        private string  SaveFile(string ddlAlbums, HttpPostedFileBase file)
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
                    string path = Path.Combine(Server.MapPath(filePath), Path.GetFileName(file.FileName));
                    log.Info(Server.MapPath(filePath));
                    if (!Directory.Exists(Server.MapPath(filePath)))
                    {
                        log.Info("Creating directory");
                        Directory.CreateDirectory(Server.MapPath(filePath));
                    }
                    file.SaveAs(path);

                }
                catch (Exception ex)
                {
                    ViewBag.Message = "ERROR:" + ex.Message.ToString();
                    throw;
                }                
            }
            return relativePath;
        }

    }
}
