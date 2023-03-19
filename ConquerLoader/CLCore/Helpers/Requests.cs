using System.Net.Http;

namespace CLCore
{
    public static class HttpRequests
    {
        public static HttpResponseMessage GET(string Uri)
        {
            HttpClient HttpClient = new HttpClient();
            return HttpClient.GetAsync(Uri).Result;
        }
        public static HttpResponseMessage POST(string Uri, string Content)
        {
            HttpClient HttpClient = new HttpClient();
            var buffer = System.Text.Encoding.UTF8.GetBytes(Content);
            var byteContent = new ByteArrayContent(buffer);
            return HttpClient.PostAsync(Uri, byteContent).Result;
        }
        public static HttpResponseMessage PUT(string Uri, string Content)
        {
            HttpClient HttpClient = new HttpClient();
            var buffer = System.Text.Encoding.UTF8.GetBytes(Content);
            var byteContent = new ByteArrayContent(buffer);
            var result = HttpClient.PutAsync(Uri, byteContent).Result;
            return result;
        }
    }
}
