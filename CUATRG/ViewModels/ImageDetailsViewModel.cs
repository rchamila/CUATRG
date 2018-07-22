using CUATRG.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CUATRG.ViewModels
{
    public class ImageDetailsViewModel
    {
        public tblImage Image;
        public tblImage PreviousImage;
        public tblImage NextImage;
        public tblProcessedImage ProcessedImage;
        public List<tblEnvironmentalCondition> AvailableConditions { get; set; }
        public List<tblFeature> AvailableFeatures { get; set; }
    }
}