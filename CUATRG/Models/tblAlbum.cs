//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CUATRG.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class tblAlbum
    {
        public tblAlbum()
        {
            this.tblImages = new HashSet<tblImage>();
        }
    
        public int ALB_IDPkey { get; set; }
        public string ALB_Name { get; set; }
        public string ALB_Description { get; set; }
    
        public virtual ICollection<tblImage> tblImages { get; set; }
    }
}
