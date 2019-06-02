using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SmartHome.Core.Helper
{
    /// <summary>
    ///     Helper for web implementations
    /// </summary>
    public static class WebHelper
    {
        /// <summary>
        ///     Performs a post request
        /// </summary>
        /// <param name="uri">URI</param>
        /// <param name="data">Data to post</param>
        /// <param name="contentType">Request content type</param>
        /// <returns>Response</returns>
        public static async Task<string> PostAsync(string uri, string data, string contentType)
        {
            var dataBytes = Encoding.UTF8.GetBytes(data);

            var request = (HttpWebRequest)WebRequest.Create(uri);
            request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
            request.ContentLength = dataBytes.Length;
            request.ContentType = contentType;
            request.Method = "POST";

            await using (var requestBody = request.GetRequestStream())
            {
                await requestBody.WriteAsync(dataBytes, 0, dataBytes.Length);
            }

            using var response = (HttpWebResponse)await request.GetResponseAsync();
            await using var stream = response.GetResponseStream();
            using var reader = new StreamReader(stream);
            {
                return await reader.ReadToEndAsync();
            }
        }
    }
}