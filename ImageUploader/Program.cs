using CUATRG.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace ImageUploader
{
    class Program
    {
        private static string baseDir = @"D:\Temp\Upload\";
        private static string api = @"http://www.cuatrg.net/api";//@"http://localhost:55892/api";
        private static log4net.ILog log = log4net.LogManager.GetLogger
              (System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        static void Main(string[] args)
        {
            log4net.Config.XmlConfigurator.Configure();

            log.Warn("Job started");
            //Task.Run(() => MainAsync());
            List<string> files = DirSearch(baseDir + "Albums");
            Console.WriteLine("Image uploading started, No of images trying to upload : {0}", files.Count);
            foreach (string file in files)
            {
                //Task.Run(() => MainAsync(file));
                MainAsync(file);
            }


            List<string> processedfiles = DirSearch(baseDir + "Processed");
            Console.WriteLine("Processed Image uploading started, No of procesed images trying to upload : {0}", processedfiles.Count);

            foreach (string file in processedfiles)
            {
                //Task.Run(() => MainAsync(file));
                ProcessedAsync(file);
            }
            Console.ReadLine();
        }


        private static List<String> DirSearch(string sDir)
        {
            List<String> files = new List<String>();
            try
            {
                foreach (string f in Directory.GetFiles(sDir))
                {
                    if (!f.Contains(".txt"))
                    {
                        files.Add(f);
                    }
                }

                foreach (string d in Directory.GetDirectories(sDir))
                {
                    if (!d.Contains("Uploaded"))
                    {
                        
                        files.AddRange(DirSearch(d));
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error("Error selecting images", ex);
            }
            return files;
        }


        static async Task MainAsync(string filePath)
        {
            string imageName;
            try
            {
                using (var client = new HttpClient())
                {
                    var content = new MultipartFormDataContent();

                    FileInfo imgInfo = new FileInfo(filePath);

                    imageName = imgInfo.Name;

                    //Console.WriteLine("{0} - Image uploading started", imageName);

                    string[] param = filePath.Replace(baseDir + "\\", "").Split('\\');
                    var masterimage = new ByteArrayContent(System.IO.File.ReadAllBytes(filePath));

                    masterimage.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
                    {
                        FileName = imgInfo.Name
                    };

                    content.Add(masterimage, "masterimage");

                    string sensorDataFilePath = filePath.Replace("Image", "SensorData").Replace("jpg","txt").Replace("JPG", "txt");
                    FileInfo sensordataInfo = new FileInfo(sensorDataFilePath);
                    var sensordata = new ByteArrayContent(System.IO.File.ReadAllBytes(sensorDataFilePath));

                    sensordata.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
                    {
                        FileName = sensordataInfo.Name
                    };
                    content.Add(sensordata, "sensordata");

                    var metaData = new ByteArrayContent(ImageHelper.GetMetaDataByteStream(filePath));

                    metaData.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
                    {
                        FileName = imageName.Replace("Image", "MetaData").Replace("jpg", "txt").Replace("JPG", "txt")
                    };
                    content.Add(metaData, "metadata");


                    var url = string.Format("{0}/Admin/AddImage?ddlAlbums={1}&ddlConditions={2}&ddlFeatures={3}", api, param[4], param[5], "Normal");
                    var result = client.PostAsync(url, content);
                    string resultContent = await result.Result.Content.ReadAsStringAsync();

                    Console.WriteLine("{0} - Image uploading completed . Result {1}", imageName, resultContent);

                    if (!string.IsNullOrWhiteSpace(resultContent) &&
                            (resultContent.Contains("OK") || resultContent.Contains("Exists")) )
                    {
                        MoveFile(filePath, true);
                        Console.WriteLine(" {0} - Image moved to uploaded directory", filePath);
                    }
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(" {0} - Error uploading image. Exception - {1}", filePath, ex.Message);
                log.Error(ex);
            }
        }

        static async Task ProcessedAsync(string filePath)
        {
            string imageName;
            try
            {
                using (var client = new HttpClient())
                {
                    var content = new MultipartFormDataContent();

                    FileInfo imgInfo = new FileInfo(filePath);

                    imageName = imgInfo.Name;

                    //Console.WriteLine("{0} - Image uploading started", imageName);

                    string[] param = filePath.Replace(baseDir + "\\", "").Split('\\');
                    var processedimage = new ByteArrayContent(System.IO.File.ReadAllBytes(filePath));

                    processedimage.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
                    {
                        FileName = imgInfo.Name
                    };

                    content.Add(processedimage, "processedimage");

                   
                    var result = client.PostAsync(string.Format("{0}/Admin/AddProcessedImage?filtername={1}&name={2}", api, param[4], param[5]), content);
                    //var result = client(string.Format("{0}/Admin/AddImage?ddlAlbums={1}&ddlConditions={2}&ddlFeatures={3}", api, param[0], param[1], param[2]), content);
                    string resultContent = await result.Result.Content.ReadAsStringAsync();

                    Console.WriteLine("{0} - Image uploading completed . Result {1}", imageName, resultContent);

                    if (!string.IsNullOrWhiteSpace(resultContent) &&
                            (resultContent.Contains("OK") || resultContent.Contains("Exists")))
                    {
                        MoveFile(filePath, false);
                        Console.WriteLine(" {0} - Image moved to uploaded directory", filePath);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(" {0} - Error uploading image. Exception - {1}", filePath, ex.Message);
                log.Error(ex);
            }
        }

        static void MoveFile(string file, bool isMainFile)
        {
            string[] data = file.Split('\\');
            string toDir = string.Join("\\", data.Take(data.Length - 1)) + "\\Uploaded\\";
            string to = toDir + data[data.Length - 1];

            if (!Directory.Exists(toDir))
            {
                Directory.CreateDirectory(toDir);
            }
            if (File.Exists(file))
            {
                File.Move(file, to);
                File.Delete(file);
            }
            if (isMainFile)
            {
                string sensorDataFileFromPath = file.Replace("Image", "SensorData").Replace("jpg", "txt").Replace("JPG", "txt");
                string sensorDataFileToPath = to.Replace("Image", "SensorData").Replace("jpg", "txt").Replace("JPG", "txt");

                if (File.Exists(sensorDataFileFromPath))
                {
                    File.Move(sensorDataFileFromPath, sensorDataFileToPath);
                    File.Delete(sensorDataFileFromPath);
                }
            }
        }
    }
}
