using CUATRG.Models;
using LevDan.Exif;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
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

        public static Bitmap MakeGrayscale(Bitmap original)
        {
            //make an empty bitmap the same size as original
            Bitmap newBitmap = new Bitmap(original.Width, original.Height);

            for (int i = 0; i < original.Width; i++)
            {
                for (int j = 0; j < original.Height; j++)
                {
                    //get the pixel from the original image
                    Color originalColor = original.GetPixel(i, j);

                    //create the grayscale version of the pixel
                    int grayScale = (int)((originalColor.R * .3) + (originalColor.G * .59)
                        + (originalColor.B * .11));

                    //create the color object
                    Color newColor = Color.FromArgb(grayScale, grayScale, grayScale);

                    //set the new image's pixel to the grayscale version
                    newBitmap.SetPixel(i, j, newColor);
                }
            }

            return newBitmap;
        }
    }
}