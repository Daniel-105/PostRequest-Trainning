using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using PostRequest_Trainning.Models;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace PostRequest_Trainning.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly HttpClient _httpClient;

        public HomeController(ILogger<HomeController> logger, IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            _httpClient = httpClientFactory.CreateClient();
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<JsonResult> GetClientToken()
        {
            // Replace with your actual API endpoint URL
            string apiUrl = "https://api.playground.klarna.com/payments/v1/sessions";

            // Replace with your Basic Authentication credentials
            string username = "PK131523_f3731dbad121";
            string password = "qSNhaFgn6Ls3bj1P";

            // Create JSON data to send in the request
            string jsonData = @"
        {
            ""purchase_country"": ""PT"",
            ""purchase_currency"": ""EUR"",
            ""locale"": ""pt-PT"",
            ""order_amount"": 20000,
            ""order_tax_amount"": 0,
            ""order_lines"": [
                {
                    ""type"": ""physical"",
                    ""reference"": ""19-402"",
                    ""name"": ""black T-Shirt"",
                    ""quantity"": 2,
                    ""unit_price"": 5000,
                    ""tax_rate"": 0,
                    ""total_amount"": 10000,
                    ""total_discount_amount"": 0,
                    ""total_tax_amount"": 0
                },
                {
                    ""type"": ""physical"",
                    ""reference"": ""123123"",
                    ""name"": ""red trousers"",
                    ""quantity"": 1,
                    ""unit_price"": 10000,
                    ""tax_rate"": 0,
                    ""total_amount"": 10000,
                    ""total_discount_amount"": 0,
                    ""total_tax_amount"": 0
                }
            ]
        }";

            try
            {
                // Set Basic Authentication credentials
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(Encoding.ASCII.GetBytes($"{username}:{password}")));

                // Set the Content-Type header to application/json
                _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                // Send a POST request with JSON content
                HttpResponseMessage response = await _httpClient.PostAsync(apiUrl, new StringContent(jsonData, Encoding.UTF8, "application/json"));

                if (response.IsSuccessStatusCode)
                {
                    // Read the response body as a string
                    string responseContent = await response.Content.ReadAsStringAsync();
                    //JObject json = JObject.Parse(responseContent);

                    // Return the JSON data as a JsonResult
                    return Json(responseContent);
                }
                else
                {
                    // Handle the case where the request was not successful
                    return Json(new { error = "API request failed" });
                }
            }
            catch (Exception ex)
            {
                return Json(new { error = "An error occurred: " + ex.Message });
            }
        }

        //[IgnoreAntiforgeryToken]
        [HttpPost]
        public async Task<JsonResult> PlaceOrder(string authorizationToken)
        {
            // Replace with your actual API endpoint URL
            string apiUrl = "https://api.playground.klarna.com/payments/v1/authorizations/" + authorizationToken + "/order";

            // Replace with your Basic Authentication credentials
            string username = "PK131523_f3731dbad121";
            string password = "qSNhaFgn6Ls3bj1P";

            // Create JSON data to send in the request
            string jsonData = @"
        {
            ""purchase_country"": ""PT"",
            ""purchase_currency"": ""EUR"",
            ""locale"": ""pt-PT"",
            ""order_amount"": 20000,
            ""order_tax_amount"": 0,
            ""order_lines"": [
                {
                    ""type"": ""physical"",
                    ""reference"": ""19-402"",
                    ""name"": ""black T-Shirt"",
                    ""quantity"": 2,
                    ""unit_price"": 5000,
                    ""tax_rate"": 0,
                    ""total_amount"": 10000,
                    ""total_discount_amount"": 0,
                    ""total_tax_amount"": 0
                },
                {
                    ""type"": ""physical"",
                    ""reference"": ""123123"",
                    ""name"": ""red trousers"",
                    ""quantity"": 1,
                    ""unit_price"": 10000,
                    ""tax_rate"": 0,
                    ""total_amount"": 10000,
                    ""total_discount_amount"": 0,
                    ""total_tax_amount"": 0
                }
            ]
        }";

            try
            {
                // Set Basic Authentication credentials
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(Encoding.ASCII.GetBytes($"{username}:{password}")));

                // Set the Content-Type header to application/json
                _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                // Send a POST request with JSON content
                HttpResponseMessage response = await _httpClient.PostAsync(apiUrl, new StringContent(jsonData, Encoding.UTF8, "application/json"));

                if (response.IsSuccessStatusCode)
                {
                    // Read the response body as a string
                    string responseContent = await response.Content.ReadAsStringAsync();
                    JObject json = JObject.Parse(responseContent);

                    var typeJson = json.GetType();
                    //var jsonResponse = JsonSerializer.Deserialize<JsonObject>(responseContent);

                    // Return the JSON data as a JsonResult
                    return Json(responseContent);
                }
                else
                {
                    // Handle the case where the request was not successful
                    return Json(new { error = "API request failed" });
                }
            }
            catch (Exception ex)
            {
                return Json(new { error = "An error occurred: " + ex.Message });
            }
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
    }
}
