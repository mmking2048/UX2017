using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using Newtonsoft.Json;
using UX2017.Models.ProfileAndFinancialData;

namespace UX2017.Utilities
{
    public class RestClient
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey = "barcharthackathon";
        private readonly string _baseUrl = "http://ondemand.websol.barchart.com/";

        public RestClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public IEnumerable<Profile> GetProfile(IEnumerable<string> symbols, IEnumerable<string> fields = null)
        {
            var url = _baseUrl + "getProfile.json" +
                      $"?apikey={_apiKey}&symbols={string.Join(",", symbols)}" +
                      $"{(fields != null ? $"&fields={string.Join(",", fields)}":"")}";
            var json = _httpClient.GetStringAsync(url).Result;
            return JsonConvert.DeserializeObject<Profiles>(json).Results;
        }
    }
}