using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using Newtonsoft.Json;
using UX2017.Models;

namespace UX2017
{
    public interface IYahooClient
    {
        Task<YahooResponse> GetSymbol(string company);
    }

    public class YahooClient : IYahooClient
    {
        private readonly HttpClient _httpClient;
        private const string BaseUrl = "http://d.yimg.com/autoc.finance.yahoo.com/autoc?lang=en&query=";

        public YahooClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<YahooResponse> GetSymbol(string company)
        {
            var url = BaseUrl + company;
            var json = await _httpClient.GetStringAsync(url);
            return JsonConvert.DeserializeObject<YahooResponse>(json);
        }
    }
}