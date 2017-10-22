using Castle.Windsor;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UX2017;
using UX2017.Models;

namespace UX2017Tests
{
    [TestClass]
    public class IBarchartClientTests
    {
        private readonly string _symbol = "AAPL";
        private WindsorContainer _container;
        private IBarchartClient _barchartClient;

        [TestInitialize]
        public void TestInitialize()
        {
            _container = new WindsorContainer();
            _container.Install(new Installer());
            _barchartClient = _container.Resolve<IBarchartClient>();
        }

        #region PriceData

        [TestMethod]
        public void GetQuote()
        {
            var quote = _barchartClient.GetQuote(_symbol).Result;
            Assert.AreEqual(quote.Symbol, _symbol);
        }

        [TestMethod]
        public void GetQuoteEod()
        {
            var quoteEod = _barchartClient.GetQuoteEod(_symbol).Result;
            Assert.AreEqual(quoteEod.Symbol, _symbol);
        }

        #endregion

        #region ProfileAndFinancialData

        [TestMethod]
        public void GetProfileTest()
        {
            var profile = _barchartClient.GetProfiles(_symbol).Result;
            Assert.AreEqual(profile.Symbol, _symbol);
        }

        [TestMethod]
        public void GetFinancialHighlights()
        {
            var highlight = _barchartClient.GetFinancialHighlights(_symbol).Result;
            Assert.AreEqual(highlight.Symbol, _symbol);
        }

        [TestMethod]
        public void GetFinancialRatios()
        {
            var ratio = _barchartClient.GetFinancialRatios(_symbol).Result;
            Assert.AreEqual(ratio.Symbol, _symbol);
        }

        [TestMethod]
        public void GetIncomeStatements()
        {
            var incomeStatement = _barchartClient.GetIncomeStatements(_symbol, Frequency.Annual).Result;
            Assert.AreEqual(incomeStatement.Symbol, _symbol);
        }

        [TestMethod]
        public void GetBalanceSheets()
        {
            var balanceSheet = _barchartClient.GetBalanceSheets(_symbol, Frequency.Annual).Result;
            Assert.AreEqual(balanceSheet.Symbol, _symbol);
        }

        [TestMethod]
        public void GetCashFlows()
        {
            var cashFlow = _barchartClient.GetCashFlows(_symbol).Result;
            Assert.AreEqual(cashFlow.Symbol, _symbol);
        }

        #endregion

        #region SplitsDividendsAndEarnings

        [TestMethod]
        public void GetCorporateActions()
        {
            var corporateAction = _barchartClient.GetCorporateActions(_symbol, EventType.dividend).Result;
            Assert.AreEqual(corporateAction.Symbol, _symbol);
        }

        [TestMethod]
        public void GetEarningsEstimates()
        {
            var earningsEstimate = _barchartClient.GetEarningsEstimates(_symbol).Result;
            Assert.AreEqual(earningsEstimate.Symbol, _symbol);
        }

        #endregion

        [TestCleanup]
        public void TestCleanup()
        {
            _container.Dispose();
        }
    }
}
