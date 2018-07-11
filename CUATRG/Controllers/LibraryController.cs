using CUATRG.Models;
using CUATRG.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CUATRG.Controllers
{
    public class LibraryController : Controller
    {
        private CUATRGEntities4 imageDB = new CUATRGEntities4();
        //
        // GET: /Search/
        [HttpGet]
        public ActionResult Index()
        {
            var viewModel = new LibraryViewModel()
            {
                Albums = imageDB.tblAlbums.ToList(),
                ColorModes = imageDB.tblColorModes.ToList(),
                Conditions = imageDB.tblEnvironmentalConditions.ToList(),
                Features = imageDB.tblFeatures.ToList(),
                Filters = imageDB.tblFilters.ToList()
            };

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Index(string ddlAlbums, string ddlColormodes, string ddlConditions, string ddlFeatures, string ddlFilters)
        //public ActionResult Search()
        {
           
            List<tblImage> results = new List<tblImage>();
            
            int albumId = -1;
            if (int.TryParse(ddlAlbums, out albumId))
            {
                results = imageDB.tblImages.ToList().FindAll(i => i.ALB_IDFkey == albumId);
            }

            int modeid = -1;
            if (int.TryParse(ddlColormodes, out modeid))
            {
               // results = imageDB.tblImages.ToList().FindAll(i => i.cm == modeid);
            }

            int conditionid = -1;
            if (int.TryParse(ddlConditions, out conditionid))
            {
                results = results.FindAll(i => i.ENC_IDFkey == conditionid);
            }

            int featureId = -1;
            if (int.TryParse(ddlFeatures, out featureId))
            {
                results = results.FindAll(i => i.FTR_IDFkey == featureId);
            }

            int filterId = -1;
            if (int.TryParse(ddlFeatures, out filterId))
            {
                results = results.FindAll(i => i.FTR_IDFkey == featureId);
            }

            var viewModel = new LibraryViewModel()
            {
                Albums = imageDB.tblAlbums.ToList(),
                ColorModes = imageDB.tblColorModes.ToList(),
                Conditions = imageDB.tblEnvironmentalConditions.ToList(),
                Features = imageDB.tblFeatures.ToList(),
                Filters = imageDB.tblFilters.ToList(),
                Images = results
            };

            return View(viewModel);
        }

    }
}
