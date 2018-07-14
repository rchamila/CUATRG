using CUATRG.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CUATRG.ViewModels
{
    public class AddProcessedImageViewModel
    {
        public tblImage Image { get; set; }
        public List<tblAlbum> Albums { get; set; }
        public List<tblEnvironmentalCondition> Conditions { get; set; }
        public List<tblFilter> Filters { get; set; }
        public List<tblFeature> Features { get; set; }
        public tblProcessedImage ProcessedImage { get; set; }
    }
}