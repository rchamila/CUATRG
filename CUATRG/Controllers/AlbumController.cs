using CUATRG.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CUATRG.ViewModels;

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

        

        public ActionResult Index()
        {
            log.Info("Retriving albums");
            try
            {
                var genres = new List<tblAlbum>();

                // Create our view model
                var viewModel = new AlbumIndexViewModel
                {
                    NumberOfAlbums = imageDB.tblAlbums.Count(),
                    Albums = imageDB.tblAlbums.ToList()
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
        public ActionResult Details(int albumId, int? imageId = null, int? processedImageId = null)
        {
            string message = HttpUtility.HtmlEncode("Store.Browse, Album = " + albumId);
            
            tblAlbum selectedAlbum = null;
            tblImage selectedImage = null;
            tblProcessedImage selectedProcessedImage = null;

            selectedAlbum = imageDB.tblAlbums.SingleOrDefault(i => i.ALB_IDPkey == albumId);
            if (selectedAlbum != null && selectedAlbum.tblImages != null && selectedAlbum.tblImages.Count() > 0)
            {
                if (imageId == null)
                {
                    selectedImage = selectedAlbum.tblImages.FirstOrDefault();
                }
                else
                {
                    selectedImage = selectedAlbum.tblImages.SingleOrDefault(i => i.IMG_IDPkey == imageId);
                }
            }
            int selectedIndex = selectedAlbum.tblImages.ToList().IndexOf(selectedImage);
            int nextIndex = selectedAlbum.tblImages.Count() - 1;
            int previousIndex = 0;
            if(selectedAlbum.tblImages.Count() - 1 > selectedIndex)
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
                NextImage = selectedAlbum.tblImages.ElementAt(nextIndex),
                PreviousImage = selectedAlbum.tblImages.ElementAt(previousIndex),
                ProcessedImage = selectedProcessedImage
            };


            return this.View(viewModel);
        }

    }
}
