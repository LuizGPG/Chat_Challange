using ChatChallange.Service.Interface;
using Microsoft.Extensions.Logging;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace ChatChallange.Service
{
    public class StooqService : IStooqService
    {
        private const string NotFound = "Não foi encontrado valor para o codigo enviado!";
        private const string Url = "https://stooq.com/";
        private readonly HttpClient _httpClient;
        private readonly ILogger<StooqService> _logger;

        public StooqService(HttpClient httpClient, ILogger<StooqService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<string> CallEndpointStooq(string message)
        {
            var value = await CallApi(message);
            var values = value.Split(',');
            var cotationValue = values[11];

            if (cotationValue != "N/D")
            {
                return $"A cotação da {message} é de US$ {cotationValue} por ação.";
            }

            return NotFound;
        }

        private async Task<string> CallApi(string message)
        {
            try
            {
                _httpClient.BaseAddress = new Uri(Url);
                HttpResponseMessage response = await _httpClient.GetAsync("q/l/?s=" + message + "&f=sd2t2ohlcv&h&e=csv");

                if (response.IsSuccessStatusCode)
                {
                    string data = await response.Content.ReadAsStringAsync();
                    return data;
                }
                else
                {
                    throw new Exception($"Failed to call" + Url + ": {response.ReasonPhrase}");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Error trying CallApi", ex.Message);
                throw;
            }
        }
    }
}
