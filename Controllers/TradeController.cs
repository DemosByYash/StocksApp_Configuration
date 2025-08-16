using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileSystemGlobbing.Internal.PathSegments;
using Microsoft.Extensions.Options;
using StocksApp_Configuration.Configuration;
using StocksApp_Configuration.ServicesContracts;
using StocksApp.Models;
using StocksApp_Configuration.Services;

namespace StocksApp_Configuration.Controllers
{
    public class TradeController : Controller
    {
        private readonly  StocksService _finnhubService;
        private readonly IOptions<TradingOptions> _tradingOptions;

        public TradeController(StocksService finnhubService, IOptions<TradingOptions> tradingOptions) 
        {
            _finnhubService = finnhubService;
            _tradingOptions = tradingOptions;
        }

        [Route("/")]
        public async Task<IActionResult> Index()
        {
            if (_tradingOptions.Value.DefaultStockSymbol == null) 
            {
                _tradingOptions.Value.DefaultStockSymbol = "AAPL"; // Default to Apple if no symbol is set
            }

            Dictionary<string,object>? responseDictionary = await _finnhubService.GetStockData(_tradingOptions.Value.DefaultStockSymbol);

            Stock stock = new Stock()
            {
                StockSymbol = _tradingOptions.Value.DefaultStockSymbol,
                CurrentPrice = Convert.ToDouble(responseDictionary["c"].ToString()),
                HighestPrice = Convert.ToDouble(responseDictionary["h"].ToString()),
                LowestPrie = Convert.ToDouble(responseDictionary["l"].ToString()),
                OpenPrice = Convert.ToDouble(responseDictionary["o"].ToString())
            };

            return View("Index", stock);
        }
    }
}
