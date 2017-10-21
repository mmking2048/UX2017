using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using UX2017.Models;
using UX2017.Models.ProfileAndFinancialData;
using UX2017.Models.SplitsDividendsAndEarnings;

namespace UX2017
{
    public interface IBarchartClient
    {
        #region ProfileAndFinancialData

        Task<IEnumerable<Profile>> GetProfile(
            IEnumerable<string> symbols,
            IEnumerable<string> fields = null);

        Task<IEnumerable<FinancialHighlight>> GetFinancialHighlights(
            IEnumerable<string> symbols,
            IEnumerable<string> fields = null);

        Task<IEnumerable<FinancialRatio>> GetFinancialRatios(
            IEnumerable<string> symbols,
            IEnumerable<string> fields = null);

        Task<IEnumerable<IncomeStatement>> GetIncomeStatements(
            IEnumerable<string> symbols,
            Frequency frequency,
            int count = 1,
            int rawData = 0);

        Task<IEnumerable<BalanceSheet>> GetBalanceSheets(
            IEnumerable<string> symbols,
            Frequency frequency,
            int count = 1,
            int rawData = 0);

        Task<IEnumerable<Competitor>> GetCompetitors(
            string symbol,
            IEnumerable<string> fields = null,
            int maxRecords = 10);

        Task<IEnumerable<IndexMember>> GetIndexMembers(
            IndexSymbol symbol,
            IEnumerable<string> fields = null);

        Task<IEnumerable<CashFlow>> GetCashFlow(
            IEnumerable<string> symbols,
            string reportPeriod = "",
            IEnumerable<string> fields = null,
            int numberOfYears = 4);
        #endregion

        #region SplitsDividendsAndEarnings

        Task<IEnumerable<CorporateAction>> GetCorporateActions(
            IEnumerable<string> symbols,
            DateTime? startDate,
            DateTime? endDate,
            EventType eventType,
            int maxRecords = 20);

        Task<IEnumerable<EarningsEstimate>> GetEarningsEstimates(
            IEnumerable<string> symbols,
            IEnumerable<string> fields = null);

        #endregion
    }

    public class BarchartClient : IBarchartClient
    {
        private readonly HttpClient _httpClient;
        private readonly IJsonParser _jsonParser;
        private readonly string _apiKey = "barcharthackathon";
        private readonly string _baseUrl = "http://ondemand.websol.barchart.com/";

        public BarchartClient(HttpClient httpClient, IJsonParser jsonParser)
        {
            _httpClient = httpClient;
            _jsonParser = jsonParser;
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
            return _jsonParser.Parse<Profile>(json);
        }

        public async Task<IEnumerable<FinancialHighlight>> GetFinancialHighlights(
            IEnumerable<string> symbols,
            IEnumerable<string> fields = null)
        {
            var url = _baseUrl + $"getFinanicalHighlights.json?apikey={_apiKey}" +
                      $"&symbols={string.Join(",", symbols)}" +
                      $"&{(fields != null ? $"&fields={string.Join(",", fields)}" : "")}";
            var json = await _httpClient.GetStringAsync(url);
            return _jsonParser.Parse<FinancialHighlight>(json);
        }

        public async Task<IEnumerable<FinancialRatio>> GetFinancialRatios(
            IEnumerable<string> symbols,
            IEnumerable<string> fields = null)
        {
            var url = _baseUrl + "getFinancialRatios.json?apikey={_apiKey}" +
                      $"&symbols={string.Join(",", symbols)}" +
                      $"&{(fields != null ? $"&fields={string.Join(",", fields)}" : "")}";
            var json = await _httpClient.GetStringAsync(url);
            return _jsonParser.Parse<FinancialRatio>(json);
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
            return _jsonParser.Parse<IncomeStatement>(json);
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
            return _jsonParser.Parse<BalanceSheet>(json);
        }

        public async Task<IEnumerable<Competitor>> GetCompetitors(
            string symbol,
            IEnumerable<string> fields = null,
            int maxRecords = 10)
        {
            var url = _baseUrl + $"getCompetitors.json?apikey={_apiKey}" +
                      $"&symbol={symbol}" +
                      $"{(fields != null ? $"&fields={string.Join(",", fields)}" : "")}" +
                      $"&maxRecords={maxRecords}";
            var json = await _httpClient.GetStringAsync(url);
            return _jsonParser.Parse<Competitor>(json);
        }

        public async Task<IEnumerable<IndexMember>> GetIndexMembers(
            IndexSymbol symbol,
            IEnumerable<string> fields = null)
        {
            var url = _baseUrl + $"getIndexMembers.json?apikey={_apiKey}" +
                      $"&symbol=${symbol}" +
                      $"{(fields != null ? $"fields={string.Join(",", fields)}" : "")}";
            var json = await _httpClient.GetStringAsync(url);
            return _jsonParser.Parse<IndexMember>(json);
        }

        public async Task<IEnumerable<CashFlow>> GetCashFlow(
            IEnumerable<string> symbols,
            string reportPeriod = "",
            IEnumerable<string> fields = null,
            int numberOfYears = 4)
        {
            var url = _baseUrl + $"getCashFlow.json?apikey={_apiKey}" +
                      $"&symbols={string.Join(",", symbols)}" +
                      $"{(!string.IsNullOrWhiteSpace(reportPeriod) ? $"&numberOfYears={numberOfYears}" : "")}";
            var json = await _httpClient.GetStringAsync(url);
            return _jsonParser.Parse<CashFlow>(json);
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
            var url = _baseUrl + $"getCorporateActions.json?apikey={_apiKey}" +
                      $"&symbols={string.Join(",", symbols)}" +
                      $"{(startDate.HasValue ? $"startDate={startDate.Value.Date}" : "")}" +
                      $"{(endDate.HasValue ? $"endDate={endDate.Value.Date}" : "")}" +
                      $"&eventType={eventType}" +
                      $"&maxRecords={maxRecords}";
            var json = await _httpClient.GetStringAsync(url);
            return _jsonParser.Parse<CorporateAction>(json);
        }

        public async Task<IEnumerable<EarningsEstimate>> GetEarningsEstimates(
            IEnumerable<string> symbols,
            IEnumerable<string> fields = null)
        {
            var url = _baseUrl + $"getEarningsEstimates.json?apikey={_apiKey}" +
                      $"&symbols={string.Join(",", symbols)}" +
                      $"&{(fields != null ? $"fields={string.Join(",", fields)}" : "")}";
            var json = await _httpClient.GetStringAsync(url);
            return _jsonParser.Parse<EarningsEstimate>(json);
        }
        #endregion
    }
}