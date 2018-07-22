using CUATRG.Models;
using CUATRG.ViewModels;
using System;
using System.Collections.Generic;
using System.Configuration;
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
        public ActionResult Search(string ddlAlbums, string ddlConditions, string ddlFeatures, string ddlFilters,
                                        string pageIndex) 
        {
            int recordsPerPage = 10;  
            IQueryable<tblImage> results = imageDB.tblImages.OrderBy(i => i.tblAlbum.ALB_Name);
            
            int albumId = -1;
             
            if (int.TryParse(ddlAlbums, out albumId))
            {
                results = results.Where(i => i.ALB_IDFkey == albumId);
            }

            int conditionid = -1;
            if (int.TryParse(ddlConditions, out conditionid))
            {
                results = results.Where(i => i.ENC_IDFkey == conditionid);
            }

            int featureId = -1;
            if (int.TryParse(ddlFeatures, out featureId))
            {
                results = results.Where(i => i.FTR_IDFkey == featureId);
            }

            int filterId = -1;
            if (int.TryParse(ddlFeatures, out filterId))
            {
                results = results.Where(i => i.FTR_IDFkey == featureId);
            }

            int recordCount = results.Count();

            //Pagination
            int page = -1;
            if (int.TryParse(pageIndex, out page))
            {
                results = results.Skip((page - 1) * recordsPerPage).Take(recordsPerPage);
            }
            else
            {
                page = 1;
                results = results.Take(recordsPerPage);
            } 
            
            List<tblImage> images = results.ToList();
            

            var data = new 
            {
                Images = images.Select( i => new {
                    Name = i.IMG_Name,
                    Path = ConfigurationManager.AppSettings["domain"] + i.IMG_Path + "//" + i.IMG_Name,
                    Album = i.tblAlbum.ALB_Name,
                    Condition = i.tblEnvironmentalCondition.ENC_Name,
                    Feature = i.tblFeature.FTR_Name,
                    AlbumId = i.tblAlbum.ALB_IDPkey,
                    ImageId = i.IMG_IDPkey
                }).ToList(),
                PageCount =(recordCount + recordsPerPage - 1) / recordsPerPage,
                CurrentPage = page
            }; 
            return Json(data);
        }

    }
}
