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
    
    public partial class tblImage
    {
        public tblImage()
        {
            this.tblProcessedImages = new HashSet<tblProcessedImage>();
            this.tblSensorDatas = new HashSet<tblSensorData>();
            this.tblMetaDatas = new HashSet<tblMetaData>();
        }
    
        public int IMG_IDPkey { get; set; }
        public string IMG_Name { get; set; }
        public int ALB_IDFkey { get; set; }
        public int FTR_IDFkey { get; set; }
        public int ENC_IDFkey { get; set; }
        public string IMG_Path { get; set; }
        public string IMG_SensorDataPath { get; set; }
        public string IMG_MetaDataPath { get; set; }
    
        public virtual tblAlbum tblAlbum { get; set; }
        public virtual tblEnvironmentalCondition tblEnvironmentalCondition { get; set; }
        public virtual tblFeature tblFeature { get; set; }
        public virtual ICollection<tblProcessedImage> tblProcessedImages { get; set; }
        public virtual ICollection<tblSensorData> tblSensorDatas { get; set; }
        public virtual ICollection<tblMetaData> tblMetaDatas { get; set; }
    }
}
