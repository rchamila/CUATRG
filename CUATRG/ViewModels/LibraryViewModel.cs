using CUATRG.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CUATRG.ViewModels
{
    public class LibraryViewModel
    {
        public List<tblImage> Images { get; set; }
        public List<tblAlbum> Albums { get; set; }
        public List<tblEnvironmentalCondition> Conditions { get; set; }
        public List<tblColorMode> ColorModes { get; set; }
        public List<tblFeature> Features { get; set; }
        public List<tblFilter> Filters { get; set; }
        public decimal PageCount { get; set; }
        public decimal CurrentPage { get; set; }
    }
}