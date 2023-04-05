using ChatChallange.Service.Interface;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace ChatChallange.Service
{
    public class StooqService : IStooqService
    {
        private const string NotFound = "Não foi encontrado valor para o codigo enviado!";
        private const string Url = "https://stooq.com/";

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

        private static async Task<string> CallApi(string message)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(Url);

                    HttpResponseMessage response = await client.GetAsync("q/l/?s=" + message + "&f=sd2t2ohlcv&h&e=csv");

                    if (response.IsSuccessStatusCode)
                    {
                        string data = await response.Content.ReadAsStringAsync();
                        return data;
                    }
                    else
                    {
                        throw new Exception($"Failed to call"+Url+": {response.ReasonPhrase}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }
        }
    }
}
