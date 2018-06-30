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
        private static string baseDir = @"D:\Temp Photos\Upload";
        private static string api = @"http://localhost:55892/api";
        private static log4net.ILog log = log4net.LogManager.GetLogger
              (System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        static void Main(string[] args)
        {
            log4net.Config.XmlConfigurator.Configure();

            log.Warn("Job started");
            //Task.Run(() => MainAsync());
            List<string> files = DirSearch(baseDir);
            foreach(string file in files)
            {
                Task.Run(() => MainAsync(file));
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

                    Console.WriteLine("{0} - Image uploading started", imageName);

                    string[] param = filePath.Replace(baseDir + "\\", "").Split('\\');
                    var masterimage = new ByteArrayContent(System.IO.File.ReadAllBytes(filePath));

                    masterimage.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
                    {
                        FileName = imgInfo.Name
                    };

                    content.Add(masterimage, "masterimage");

                    string sensorDataFilePath = filePath.Replace("IMG", "SensorData").Replace("jpg","txt");
                    FileInfo dataInfo = new FileInfo(sensorDataFilePath);
                    var sensordata = new ByteArrayContent(System.IO.File.ReadAllBytes(sensorDataFilePath));

                    sensordata.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
                    {
                        FileName = dataInfo.Name
                    };
                    content.Add(sensordata, "sensordata");
                  

                    var result = client.PostAsync(string.Format("{0}/Admin/AddImage?ddlAlbums={1}&ddlConditions={2}&ddlFeatures={3}",api, param[0], param[1], param[2]), content);
                    string resultContent = await result.Result.Content.ReadAsStringAsync();

                    Console.WriteLine("{0} - Image uploading completed . Result {1}", imageName, resultContent);

                    if (string.IsNullOrWhiteSpace(resultContent))
                    {
                        MoveFile(filePath);
                        Console.WriteLine(" {0} - Image moved", filePath);
                    }
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(" {0} - Error uploading image. Exception - {1}", filePath, ex.Message);
                log.Error(ex);
            }
        }

        static void MoveFile(string file)
        {
            string[] data = file.Split('\\');
            string toDir = string.Join("\\", data.Take(data.Length - 1)) + "\\Uploaded\\";
            string to = toDir + data[data.Length - 1];

            if (!Directory.Exists(toDir))
            {
                Directory.CreateDirectory(toDir);
            }
            File.Move(file, to);
        }
    }
}
