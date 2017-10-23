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
        static void Main(string[] args)
        {
            //Task.Run(() => MainAsync());
            List<string> files = DirSearch(@"D:\Temp Photos\Upload");
            foreach(string file in files)
            {
                Task.Run(() => MainAsync(file));

                string[] data = file.Split('\\');
                string toDir = string.Join("\\", data.Take(data.Length - 1)) + "\\Uploaded\\";
                string to = toDir + data[data.Length - 1];

                if (!Directory.Exists(toDir))
                {
                    Directory.CreateDirectory(toDir);
                }
                File.Move(file, to);
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
                    files.Add(f);
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
                 
            }

            return files;
        }


        static async Task MainAsync(string filePath)
        {
            using (var client = new HttpClient())
            {
                //client.BaseAddress = new Uri("http://localhost/CUATRG/");

                var content = new MultipartFormDataContent();

                var masterimage = new ByteArrayContent(System.IO.File.ReadAllBytes(filePath));
                masterimage.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
                {
                    FileName = "Foo1"
                };

                content.Add(masterimage, "masterimage");

                var sensordata = new ByteArrayContent(System.IO.File.ReadAllBytes(@"D:\Temp\print2.txt"));
                sensordata.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
                {
                    FileName = "Foo2"
                };

                content.Add(sensordata, "sensordata");

                var metadata = new ByteArrayContent(System.IO.File.ReadAllBytes(@"D:\Temp\print3.txt"));
                metadata.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
                {
                    FileName = "Foo3"
                };

                content.Add(metadata, "metadata");

                var result = client.PostAsync("http://localhost:62309/api/Admin/AddImage?id=test", content);
         
                string resultContent = await result.Result.Content.ReadAsStringAsync();
             
                Console.WriteLine(resultContent);
            }
        }
    }


    
}
