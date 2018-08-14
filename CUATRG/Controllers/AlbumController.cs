using CUATRG.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CUATRG.ViewModels;
using System.Configuration;

//Here is the once-per-application setup information
[assembly: log4net.Config.XmlConfigurator(Watch = true)]
namespace CUATRG.Controllers
{
    public class AlbumController : Controller
    {
        private CUATRGEntities4 imageDB = new CUATRGEntities4();
        //
        // GET: /Album/
        //Here is the once-per-class call to initialize the log object
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        [HttpPost]
        public ActionResult Index(int pageIndex)
        {
            int recordsPerPage = 6;
            IQueryable<tblAlbum> results = imageDB.tblAlbums.OrderBy(i => i.ALB_Name).Where( i => i.tblImages.Count() > 0);

            int recordCount = results.Count();

            //Pagination
           
            if (pageIndex > 0)
            {
                results = results.Skip((pageIndex - 1) * recordsPerPage).Take(recordsPerPage);
            }
            else
            {
                pageIndex = 1;
                results = results.Take(recordsPerPage);
            }

            List<tblAlbum> albums = results.ToList();


            var data = new
            {
                Albums = albums.Select(i => new {
                    Name = i.ALB_Name,
                    Image = System.Configuration.ConfigurationManager.AppSettings["domain"] + i.tblImages.First().IMG_Path + "//" + i.tblImages.First().IMG_Name,
                    Path =  System.Configuration.ConfigurationManager.AppSettings["domain"] + "/Album/Details?albumId=" + i.ALB_IDPkey,
                    AlbumId = i.ALB_IDPkey,
                    Description = i.ALB_Description
                }).ToList(),
                PageCount = (recordCount + recordsPerPage - 1) / recordsPerPage,
                CurrentPage = pageIndex
            };
            return Json(data);
        }
        public ActionResult Index()
        { 
            log.Info("Retriving albums");
            try
            {
                var genres = new List<tblAlbum>();

                // Create our view model
                var viewModel = new AlbumIndexViewModel
                {
                    NumberOfAlbums = imageDB.tblAlbums.Where(a => a.tblImages.Count() > 0).Count(),
                    Albums = imageDB.tblAlbums.Where(a => a.tblImages.Count() > 0).OrderBy( a=> a.ALB_Name).ToList()
                };

                return this.View(viewModel);
            }
            catch (Exception ex)
            {
                
                log.Error("Error error logging", ex);
                return this.View(ex.ToString());
            }
        }

        // GET: /Store/Browse
        public ActionResult Details(int albumId, int? imageId = null, int? processedImageId = null,
                                        int? conditionId = null, int? featureId = null)
        {
            string message = HttpUtility.HtmlEncode("Store.Browse, Album = " + albumId);
            
            tblAlbum selectedAlbum = null;
            tblImage selectedImage = null;
            tblProcessedImage selectedProcessedImage = null;

            selectedAlbum = imageDB.tblAlbums.SingleOrDefault(a => a.ALB_IDPkey == albumId);
            var query = imageDB.tblImages.Where(i => i.ALB_IDFkey == albumId); 
            //if (conditionId != null)
            //{
            //    query = query.Where(i => i.ENC_IDFkey == conditionId);
            //}

            //if (featureId != null)
            //{
            //    query = query.Where(i => i.FTR_IDFkey == featureId);
            //}

            List<tblImage> images = query.ToList();
            if (imageId != null)
            {
                selectedImage = images.SingleOrDefault( i => i.IMG_IDPkey == imageId);
            }
            else
            {
                selectedImage = images.FirstOrDefault();
            }

            int selectedIndex = images.IndexOf(selectedImage);
            int nextIndex = images.Count() - 1;
            int previousIndex = 0;
            if(images.Count() - 1 > selectedIndex)
            {
                nextIndex = selectedIndex + 1;
            }

            if (selectedIndex - 1 >= 0)
            {
                previousIndex = selectedIndex - 1;
            }

            if(processedImageId != null)
            {
                selectedProcessedImage = selectedImage.tblProcessedImages.SingleOrDefault(p => p.PIM_IDPkey == processedImageId.Value);
            }

            var viewModel = new ImageDetailsViewModel
            {
                Image = selectedImage,
                NextImage = images.ElementAt(nextIndex),
                PreviousImage = images.ElementAt(previousIndex),
                ProcessedImage = selectedProcessedImage,
                AvailableConditions = selectedAlbum.tblImages.Select(i => i.tblEnvironmentalCondition).Distinct().ToList(),
                AvailableFeatures = selectedAlbum.tblImages.Select( i => i.tblFeature).Distinct().ToList()
            };


            return this.View(viewModel);
        }

    }
}
