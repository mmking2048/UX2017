using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using UX2017.Models;

namespace UX2017
{
    public interface ITextRazorClient
    {
        Task<IEnumerable<Topic>> GetTopics(string input);
    }

    public class TextRazorClient : ITextRazorClient
    {
        private readonly HttpClient _httpClient;
        private const string ApiKey = "2f9bfb9f49fd03be163c29c9339afe2f9d256786c70bd56f6cb32312";
        private const string BaseUrl = "https://api.textrazor.com";

        public TextRazorClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<Topic>> GetTopics(string input)
        {
            _httpClient.DefaultRequestHeaders.Add("x-textrazor-key", ApiKey);

            var values = new Dictionary<string, string>
            {
                {"extractors", "topics"},
                {"text", input}
            };

            var content = new FormUrlEncodedContent(values);
            var response = await _httpClient.PostAsync(BaseUrl, content);
            var json = await response.Content.ReadAsStringAsync();
            var processed = JsonConvert.DeserializeObject<TextRazorResponse>(json);
            if (processed.Ok)
                return processed.Response.Topics;
            return null;
        }
    }
}