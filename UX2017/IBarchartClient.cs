﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using UX2017.Models;
using UX2017.Models.PriceData;
using UX2017.Models.ProfileAndFinancialData;
using UX2017.Models.SplitsDividendsAndEarnings;

namespace UX2017
{
    public interface IBarchartClient
    {
        #region PriceData

        Task<QuoteEod> GetQuoteEod(string symbol);

        Task<IEnumerable<QuoteEod>> GetQuoteEod(
            IEnumerable<string> symbols,
            IEnumerable<string> exchanges = null);

        #endregion

        #region ProfileAndFinancialData

        Task<Profile> GetProfiles(string symbol);
        Task<FinancialHighlight> GetFinancialHighlights(string symbol);
        Task<FinancialRatio> GetFinancialRatios(string symbol);
        Task<IncomeStatement> GetIncomeStatements(string symbol, Frequency frequency);
        Task<BalanceSheet> GetBalanceSheets(string symbol, Frequency frequency);
        Task<CashFlow> GetCashFlows(string symbol);

        Task<IEnumerable<Profile>> GetProfiles(
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

        Task<IEnumerable<CashFlow>> GetCashFlows(
            IEnumerable<string> symbols,
            string reportPeriod = "",
            IEnumerable<string> fields = null,
            int numberOfYears = 4);
        #endregion

        #region SplitsDividendsAndEarnings

        Task<CorporateAction> GetCorporateActions(string symbol, EventType eventType);
        Task<EarningsEstimate> GetEarningsEstimates(string symbol);

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
        private const string ApiKey = "barcharthackathon";
        private const string BaseUrl = "http://ondemand.websol.barchart.com/";

        public BarchartClient(HttpClient httpClient, IJsonParser jsonParser)
        {
            _httpClient = httpClient;
            _jsonParser = jsonParser;
        }

        #region PriceData

        public async Task<QuoteEod> GetQuoteEod(string symbol)
        {
            return (await GetQuoteEod(new[] {symbol})).ElementAt(0);
        }

        public async Task<IEnumerable<QuoteEod>> GetQuoteEod(
            IEnumerable<string> symbols,
            IEnumerable<string> exchanges = null)
        {
            var url = BaseUrl + $"getProfile.json?apikey={ApiKey}" +
                      $"&symbols={string.Join(",", symbols)}" +
                      $"{(exchanges != null ? $"&fields={string.Join(",", exchanges)}" : "")}";
            var json = await _httpClient.GetStringAsync(url);
            return _jsonParser.Parse<QuoteEod>(json);
        }

        #endregion

        #region ProfileAndFinancialData

        public async Task<Profile> GetProfiles(string symbol)
        {
            return (await GetProfiles(new[] {symbol})).ElementAt(0);
        }

        public async Task<FinancialHighlight> GetFinancialHighlights(string symbol)
        {
            return (await GetFinancialHighlights(new[] {symbol})).ElementAt(0);
        }

        public async Task<FinancialRatio> GetFinancialRatios(string symbol)
        {
            return (await GetFinancialRatios(new[] {symbol})).ElementAt(0);
        }

        public async Task<IncomeStatement> GetIncomeStatements(string symbol, Frequency frequency)
        {
            return (await GetIncomeStatements(new[] {symbol}, frequency)).ElementAt(0);
        }

        public async Task<BalanceSheet> GetBalanceSheets(string symbol, Frequency frequency)
        {
            return (await GetBalanceSheets(new[] {symbol}, frequency)).ElementAt(0);
        }

        public async Task<CashFlow> GetCashFlows(string symbol)
        {
            return (await GetCashFlows(new[] {symbol})).ElementAt(0);
        }

        public async Task<IEnumerable<Profile>> GetProfiles(
            IEnumerable<string> symbols,
            IEnumerable<string> fields = null)
        {
            var url = BaseUrl + $"getProfile.json?apikey={ApiKey}" +
                      $"&symbols={string.Join(",", symbols)}" +
                      $"{(fields != null ? $"&fields={string.Join(",", fields)}":"")}";
            var json = await _httpClient.GetStringAsync(url);
            return _jsonParser.Parse<Profile>(json);
        }

        public async Task<IEnumerable<FinancialHighlight>> GetFinancialHighlights(
            IEnumerable<string> symbols,
            IEnumerable<string> fields = null)
        {
            var url = BaseUrl + $"getFinancialHighlights.json?apikey={ApiKey}" +
                      $"&symbols={string.Join(",", symbols)}" +
                      $"{(fields != null ? $"&fields={string.Join(",", fields)}" : "")}";
            var json = await _httpClient.GetStringAsync(url);
            return _jsonParser.Parse<FinancialHighlight>(json);
        }

        public async Task<IEnumerable<FinancialRatio>> GetFinancialRatios(
            IEnumerable<string> symbols,
            IEnumerable<string> fields = null)
        {
            var url = BaseUrl + $"getFinancialRatios.json?apikey={ApiKey}" +
                      $"&symbols={string.Join(",", symbols)}" +
                      $"{(fields != null ? $"&fields={string.Join(",", fields)}" : "")}";
            var json = await _httpClient.GetStringAsync(url);
            return _jsonParser.Parse<FinancialRatio>(json);
        }

        public async Task<IEnumerable<IncomeStatement>> GetIncomeStatements(
            IEnumerable<string> symbols,
            Frequency frequency,
            int count = 1,
            int rawData = 0)
        {
            var url = BaseUrl + $"getIncomeStatements.json?apikey={ApiKey}" +
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
            var url = BaseUrl + $"getBalanceSheets.json?apikey={ApiKey}" +
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
            var url = BaseUrl + $"getCompetitors.json?apikey={ApiKey}" +
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
            var url = BaseUrl + $"getIndexMembers.json?apikey={ApiKey}" +
                      $"&symbol=${symbol}" +
                      $"{(fields != null ? $"fields={string.Join(",", fields)}" : "")}";
            var json = await _httpClient.GetStringAsync(url);
            return _jsonParser.Parse<IndexMember>(json);
        }

        public async Task<IEnumerable<CashFlow>> GetCashFlows(
            IEnumerable<string> symbols,
            string reportPeriod = "",
            IEnumerable<string> fields = null,
            int numberOfYears = 4)
        {
            var url = BaseUrl + $"getCashFlow.json?apikey={ApiKey}" +
                      $"&symbols={string.Join(",", symbols)}" +
                      $"{(!string.IsNullOrWhiteSpace(reportPeriod) ? $"&numberOfYears={numberOfYears}" : "")}";
            var json = await _httpClient.GetStringAsync(url);
            return _jsonParser.Parse<CashFlow>(json);
        }
        #endregion

        #region SplitsDividendsAndEarnings

        public async Task<CorporateAction> GetCorporateActions(string symbol, EventType eventType)
        {
            return (await GetCorporateActions(new[] {symbol}, null, null, eventType)).ElementAt(0);
        }

        public async Task<EarningsEstimate> GetEarningsEstimates(string symbol)
        {
            return (await GetEarningsEstimates(new[] {symbol})).ElementAt(0);
        }

        public async Task<IEnumerable<CorporateAction>> GetCorporateActions(
            IEnumerable<string> symbols,
            DateTime? startDate,
            DateTime? endDate,
            EventType eventType,
            int maxRecords = 20)
        {
            var url = BaseUrl + $"getCorporateActions.json?apikey={ApiKey}" +
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
            var url = BaseUrl + $"getEarningsEstimates.json?apikey={ApiKey}" +
                      $"&symbols={string.Join(",", symbols)}" +
                      $"&{(fields != null ? $"fields={string.Join(",", fields)}" : "")}";
            var json = await _httpClient.GetStringAsync(url);
            return _jsonParser.Parse<EarningsEstimate>(json);
        }
        #endregion
    }
}