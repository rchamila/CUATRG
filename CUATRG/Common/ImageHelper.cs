using CUATRG.Models;
using LevDan.Exif;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Web;

namespace CUATRG.Common
{
    public class ImageHelper
    {
        public static bool IsExists(tblImage image)
        {
            var dbCtx = new CUATRGEntities4();
            return dbCtx.tblImages.Any<tblImage>(i => i.IMG_Name == image.IMG_Name
                                                    && i.ALB_IDFkey == image.tblAlbum.ALB_IDPkey
                                                    && i.FTR_IDFkey == image.tblFeature.FTR_IDPkey
                                                    && i.ENC_IDFkey == image.tblEnvironmentalCondition.ENC_IDPkey);


        }

        public static bool IsProcessedImageExists(tblProcessedImage image)
        {
            var dbCtx = new CUATRGEntities4();
            return dbCtx.tblProcessedImages.Any<tblProcessedImage>(i => i.PIM_Name == image.PIM_Name
                                                    && i.FLT_IDFkey == image.FLT_IDFkey
                                                    && i.IMG_IDFkey == image.IMG_IDFkey);


        }

        public static void ExtractMetaData(tblImage image)
        {
            var path = string.Format("~/Images/Albums/{0}", image.tblAlbum.ALB_Name);
            var mappedPath = System.Web.Hosting.HostingEnvironment.MapPath(path);
            var imagePath = Path.Combine(mappedPath, Path.GetFileName(image.IMG_Name));


            //string filePath = string.Format("~/Images/Albums/{0}", image.tblAlbum.ALB_Name);
            //string relativePath = string.Format("Images/Albums/{0}/{1}", ddlAlbums, masterimage.FileName);
           // string path = Path.Combine(Server.MapPath(filePath), Path.GetFileName(masterimage.FileName));



            ExifTagCollection _exif = new ExifTagCollection(imagePath);

            foreach (ExifTag tag in _exif)
            {
                tblMetaType metaType = new tblMetaType();
                metaType.MTT_IDPkey = tag.Id; ;
                metaType.MTT_Name = tag.FieldName;
                tblMetaData meta = new tblMetaData();
                meta.MTD_Value = tag.Value.Length > 500 ? "" : tag.Value;
                meta.MTT_IDFkey = tag.Id;
                meta.tblImage = image;
                image.tblMetaDatas.Add(meta);
            }
        }

        public static byte[] GetMetaDataByteStream(string imagePath)
        {
            ExifTagCollection _exif = new ExifTagCollection(imagePath);
            StringBuilder builder = new StringBuilder();
            foreach (ExifTag tag in _exif)
            {
                builder.Append(tag.FieldName);
                builder.Append(":");
                builder.Append(tag.Value.Length > 500 ? "" : tag.Value); 
            }

            BinaryFormatter bf = new BinaryFormatter();
            using (MemoryStream ms = new MemoryStream())
            {
                bf.Serialize(ms, builder.ToString());
                return ms.ToArray();

            }
        }
    }
}