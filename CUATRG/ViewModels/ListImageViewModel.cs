using CUATRG.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CUATRG.ViewModels
{
    public class ListImageViewModel
    {
        public List<tblProcessedImage> ProcessedImages { get; set; }
        public List<tblImage> Images { get; set; }
    }
}