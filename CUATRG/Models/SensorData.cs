using FileHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CUATRG.Models
{

    public class SensorData
    {
        [FieldConverter(ConverterKind.Date, "dd-MM-yyyy")]
        public DateTime AddedDate;

        public int Ax;

        public string Ay;

        public decimal Az;

        public decimal Gx;

        public decimal Gy;

        public decimal Gz;

        public decimal Ox;

        public decimal Oy;

        public decimal Oz;

       
    }
}