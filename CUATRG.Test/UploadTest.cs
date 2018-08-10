using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net.Http;
using System.Collections.Generic;
using System.Web;

namespace CUATRG.Test
{
    [TestClass]
    public class UploadTest
    {
        [TestMethod]
        public void Test()
        {
            string error = @"System.Net.Mail.SmtpException%3A%20Failure%20sending%20mail.%20--->%20System.Net.WebException%3A%20Unable%20to%20connect%20to%20the%20remote%20server%20--->%20System.Net.Sockets.SocketException%3A%20An%20attempt%20was%20made%20to%20access%20a%20socket%20in%20a%20way%20forbidden%20by%20its%20access%20permissions%2074.125.195.109%3A587%0D%0A%20%20%20at%20System.Net.Sockets.Socket.DoConnect%28EndPoint%20endPointSnapshot%2C%20SocketAddress%20socketAddress%29%0D%0A%20%20%20at%20System.Net.ServicePoint.ConnectSocketInternal%28Boolean%20connectFailure%2C%20Socket%20s4%2C%20Socket%20s6%2C%20Socket%26%20socket%2C%20IPAddress%26%20address%2C%20ConnectSocketState%20state%2C%20IAsyncResult%20asyncResult%2C%20Exception%26%20exception%29%0D%0A%20%20%20---%20End%20of%20inner%20exception%20stack%20trace%20---%0D%0A%20%20%20at%20System.Net.ServicePoint.GetConnection%28PooledStream%20PooledStream%2C%20Object%20owner%2C%20Boolean%20async%2C%20IPAddress%26%20address%2C%20Socket%26%20abortSocket%2C%20Socket%26%20abortSocket6%29%0D%0A%20%20%20at%20System.Net.PooledStream.Activate%28Object%20owningObject%2C%20Boolean%20async%2C%20GeneralAsyncDelegate%20asyncCallback%29%0D%0A%20%20%20at%20System.Net.PooledStream.Activate%28Object%20owningObject%2C%20GeneralAsyncDelegate%20asyncCallback%29%0D%0A%20%20%20at%20System.Net.ConnectionPool.GetConnection%28Object%20owningObject%2C%20GeneralAsyncDelegate%20asyncCallback%2C%20Int32%20creationTimeout%29%0D%0A%20%20%20at%20System.Net.Mail.SmtpConnection.GetConnection%28ServicePoint%20servicePoint%29%0D%0A%20%20%20at%20System.Net.Mail.SmtpTransport.GetConnection%28ServicePoint%20servicePoint%29%0D%0A%20%20%20at%20System.Net.Mail.SmtpClient.GetConnection%28%29%0D%0A%20%20%20at%20System.Net.Mail.SmtpClient.Send%28MailMessage%20message%29%0D%0A%20%20%20---%20End%20of%20inner%20exception%20stack%20trace%20---%0D%0A%20%20%20at%20System.Net.Mail.SmtpClient.Send%28MailMessage%20message%29%0D%0A%20%20%20at%20System.Net.Mail.SmtpClient.Send%28String%20from%2C%20String%20recipients%2C%20String%20subject%2C%20String%20body%29%0D%0A%20%20%20at%20CUATRG.Controllers.HomeController.ContactUs%28String%20name%2C%20String%20email%2C%20String%20message%29%20in%20D%3A%5CGITProjects%5CCUATRG%5CCUATRG%5CControllers%5CHomeController.cs%3Aline%2063";
            string error1 = HttpUtility.UrlDecode(error);
            int i = 0;
        }
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
