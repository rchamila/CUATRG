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
    
    public partial class tblSensorData
    {
        public int SND_IDPkey { get; set; }
        public int IMG_IDFkey { get; set; }
        public int SDT_IDFkey { get; set; }
        public decimal SDT_Value { get; set; }
    
        public virtual tblSensorDataType tblSensorDataType { get; set; }
        public virtual tblImage tblImage { get; set; }
    }
}
