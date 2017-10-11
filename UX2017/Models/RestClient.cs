using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using Microsoft.Ajax.Utilities;
using Newtonsoft.Json;
using UX2017.Models;
using UX2017.Models.ProfileAndFinancialData;
using UX2017.Models.SplitsDividendsAndEarnings;

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

        #region ProfileAndFinancialData
        public async Task<IEnumerable<Profile>> GetProfile(
            IEnumerable<string> symbols,
            IEnumerable<string> fields = null)
        {
            var url = _baseUrl + $"getProfile.json?apikey={_apiKey}" +
                      $"&symbols={string.Join(",", symbols)}" +
                      $"&{(fields != null ? $"&fields={string.Join(",", fields)}":"")}";
            var json = await _httpClient.GetStringAsync(url);
            return JsonConvert.DeserializeObject<Output<Profile>>(json).Results;
        }

        public async Task<IEnumerable<FinancialHighlight>> GetFinancialHighlights(
            IEnumerable<string> symbols,
            IEnumerable<string> fields = null)
        {
            var url = _baseUrl + $"getFinanicalHighlights.json?apikey={_apiKey}" +
                      $"&symbols={string.Join(",", symbols)}" +
                      $"&{(fields != null ? $"&fields={string.Join(",", fields)}" : "")}";
            var json = await _httpClient.GetStringAsync(url);
            return JsonConvert.DeserializeObject<Output<FinancialHighlight>>(json).Results;
        }

        public async Task<IEnumerable<FinancialRatio>> GetFinancialRatios(
            IEnumerable<string> symbols,
            IEnumerable<string> fields = null)
        {
            var url = _baseUrl + "getFinancialRatios.json?apikey={_apiKey}" +
                      $"&symbols={string.Join(",", symbols)}" +
                      $"&{(fields != null ? $"&fields={string.Join(",", fields)}" : "")}";
            var json = await _httpClient.GetStringAsync(url);
            return JsonConvert.DeserializeObject<Output<FinancialRatio>>(json).Results;
        }

        public async Task<IEnumerable<IncomeStatement>> GetIncomeStatements(
            IEnumerable<string> symbols,
            Frequency frequency,
            int count = 1,
            int rawData = 0)
        {
            var url = _baseUrl + $"getFinanicalHighlights.json?apikey={_apiKey}" +
                      $"&symbols={string.Join(",", symbols)}" +
                      $"&frequency={frequency}" +
                      $"&count={count}" +
                      $"&rawData={rawData}";
            var json = await _httpClient.GetStringAsync(url);
            return JsonConvert.DeserializeObject<Output<IncomeStatement>>(json).Results;
        }

        public async Task<IEnumerable<BalanceSheet>> GetBalanceSheets(
            IEnumerable<string> symbols,
            Frequency frequency,
            int count = 1,
            int rawData = 0)
        {
            var url = _baseUrl + $"getBalanceSheets.json?apikey={_apiKey}" +
                      $"&symbols={string.Join(",", symbols)}" +
                      $"&frequency={frequency}" +
                      $"&count={count}" +
                      $"&rawData={rawData}";
            var json = await _httpClient.GetStringAsync(url);
            return JsonConvert.DeserializeObject<Output<BalanceSheet>>(json).Results;
        }

        public async Task<IEnumerable<Competitor>> GetCompetitors(
            string symbol,
            IEnumerable<string> fields = null,
            int maxRecords = 10)
        {
            var url = _baseUrl + $"getCompetitors.json?apiKey={_apiKey}" +
                      $"&symbol={symbol}" +
                      $"{(fields != null ? $"&fields={string.Join(",", fields)}" : "")}" +
                      $"&maxRecords={maxRecords}";
            var json = await _httpClient.GetStringAsync(url);
            return JsonConvert.DeserializeObject<Output<Competitor>>(json).Results;
        }

        public async Task<IEnumerable<IndexMember>> GetIndexMembers(
            IndexSymbol symbol,
            IEnumerable<string> fields = null)
        {
            var url = _baseUrl + $"getIndexMembers.json?apiKey={_apiKey}" +
                      $"&symbol=${symbol}" +
                      $"{(fields != null ? $"fields={string.Join(",", fields)}" : "")}";
            var json = await _httpClient.GetStringAsync(url);
            return JsonConvert.DeserializeObject<Output<IndexMember>>(json).Results;
        }

        public async Task<IEnumerable<CashFlow>> GetCashFlow(
            IEnumerable<string> symbols,
            string reportPeriod = "",
            IEnumerable<string> fields = null,
            int numberOfYears = 4)
        {
            var url = _baseUrl + $"getCashFlow.json?apiKey={_apiKey}" +
                      $"&symbols={string.Join(",", symbols)}" +
                      $"{(!string.IsNullOrWhiteSpace(reportPeriod) ? $"&numberOfYears={numberOfYears}" : "")}";
            var json = await _httpClient.GetStringAsync(url);
            return JsonConvert.DeserializeObject<Output<CashFlow>>(json).Results;
        }
        #endregion

        #region SplitsDividendsAndEarnings

        public async Task<IEnumerable<CorporateAction>> GetCorporateActions(
            IEnumerable<string> symbols,
            DateTime? startDate,
            DateTime? endDate,
            EventType eventType,
            int maxRecords = 20)
        {
            
        }
        #endregion
    }
}