using CUATRG.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CUATRG.ViewModels
{
    public class AlbumIndexViewModel
    {
        
        public int NumberOfAlbums { get; set; }

        public List<tblAlbum> Albums { get; set; }
    }
}