using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net.Http;
using System.Collections.Generic;

namespace CUATRG.Test
{
    [TestClass]
    public class UploadTest
    {
        [TestMethod]
        public async System.Threading.Tasks.Task TestMethod1Async()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:62309/");
                var content = new FormUrlEncodedContent(new[]
                {
                new KeyValuePair<string, string>("", "login")
                });
                var result = client.PostAsync("/admin/AddImage", content);
                string resultContent = await result.Result.Content.ReadAsStringAsync();

                Console.WriteLine(resultContent);
            }
        }
    }
}
