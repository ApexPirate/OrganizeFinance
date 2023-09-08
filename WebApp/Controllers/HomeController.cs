using Microsoft.AspNetCore.Mvc;
using RestSharp;
using System.Diagnostics;
using System.Text.Json;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly Uri _twseDomain = new("https://openapi.twse.com.tw/v1");

        public HomeController(ILogger<HomeController> logger, IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            _httpClientFactory = httpClientFactory; 
        }

        public async Task<IActionResult> Index()
        {
            var stockPrices = await GetStockPrices();
            var stocks = JsonSerializer.Deserialize<List<Stock>>(stockPrices) ?? new();

            var vm = new StockViewModel() { Stocks = stocks };
            return View(vm);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        private async Task<string> GetStockPrices()
        {
            var methodUrl = "/exchangeReport/STOCK_DAY_AVG_ALL";

            var client = new RestClient(_twseDomain, useClientFactory: true);
            var request = new RestRequest(methodUrl, Method.Get);
            var response = await client.ExecuteAsync(request);

            return response.Content ?? string.Empty;
        }
    }
}