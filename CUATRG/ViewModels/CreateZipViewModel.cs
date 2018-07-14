using CUATRG.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CUATRG.ViewModels
{
    public class CreateZipViewModel
    {
        public List<tblFile> Files { get; set;}
        public string Message { get; set; }
    }
}