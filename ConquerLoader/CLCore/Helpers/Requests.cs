using System;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Windows.Forms;

namespace CLCore
{
    public static class HttpRequests
    {
        private static bool DebugMode = false;
        public static HttpResponseMessage GET(string Uri)
        {
            HttpClient HttpClient = new HttpClient();
            HttpClient.Timeout = TimeSpan.FromSeconds(5);
            try
            {
                HttpResponseMessage res = HttpClient.GetAsync(Uri).Result;
                return res;
            } catch(Exception e)
            {
                if (Debugger.IsAttached && DebugMode)
                {
                    MessageBox.Show(e.Message);
                }
                return new HttpResponseMessage() { StatusCode = System.Net.HttpStatusCode.InternalServerError };
            }
        }
        public static HttpResponseMessage POST(string Uri, string Content)
        {
            HttpClient HttpClient = new HttpClient();
            HttpClient.Timeout = TimeSpan.FromSeconds(5);
            try
            {
                var buffer = System.Text.Encoding.UTF8.GetBytes(Content);
                var byteContent = new ByteArrayContent(buffer);
                HttpResponseMessage result = HttpClient.PostAsync(Uri, byteContent).Result;
                return result;
            }
            catch (Exception e)
            {
                if (Debugger.IsAttached && DebugMode)
                {
                    MessageBox.Show(e.Message);
                }
                return new HttpResponseMessage() { StatusCode = System.Net.HttpStatusCode.InternalServerError };
            }
        }
        public static HttpResponseMessage JsonPOST<T>(string Uri, T Model)
        {
            HttpClient HttpClient = new HttpClient();
            HttpClient.Timeout = TimeSpan.FromSeconds(5);
            try
            {
                string JSON = Newtonsoft.Json.JsonConvert.SerializeObject(Model);
                var content = new StringContent(JSON, Encoding.UTF8, "application/json");
                HttpResponseMessage result = HttpClient.PostAsync(Uri, content).Result;
                return result;
            }
            catch (Exception e)
            {
                if (Debugger.IsAttached && DebugMode)
                {
                    MessageBox.Show(e.Message);
                }
                return new HttpResponseMessage() { StatusCode = System.Net.HttpStatusCode.InternalServerError };
            }
        }
        public static HttpResponseMessage PUT(string Uri, string Content)
        {
            HttpClient HttpClient = new HttpClient();
            HttpClient.Timeout = TimeSpan.FromSeconds(5);
            try
            {
                var buffer = System.Text.Encoding.UTF8.GetBytes(Content);
                var byteContent = new ByteArrayContent(buffer);
                var result = HttpClient.PutAsync(Uri, byteContent).Result;
                return result;
            }
            catch (Exception e)
            {
                if (Debugger.IsAttached && DebugMode)
                {
                    MessageBox.Show(e.Message);
                }
                return new HttpResponseMessage() { StatusCode = System.Net.HttpStatusCode.InternalServerError };
            }
        }
    }
}
