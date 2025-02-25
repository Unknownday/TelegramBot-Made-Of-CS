using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp5
{
    public class Parser : IDisposable
    {
        
        public async Task Main()
        {
            string[] currencyPairs = { "GBPUSD", "USDGBP", "EURUSD", "USDEUR", "NZDUSD", "USDNZD" };
            for (int i = 0; i < currencyPairs.Length; i++)
            {
                string apiUrl = $"https://www.freeforexapi.com/api/live?pairs={currencyPairs[i]}";

                using (HttpClient client = new HttpClient())
                {
                    try
                    {
                        string json = await client.GetStringAsync(apiUrl);

                        CurrencyApiResponse currencyApiResponse = JsonConvert.DeserializeObject<CurrencyApiResponse>(json);

                        if (currencyApiResponse != null && currencyApiResponse.Code == 200)
                        {
                            foreach (var rate in currencyApiResponse.Rates)
                            {
                                Console.WriteLine($"{rate.Key}: Rate - {rate.Value.Rate}");
                                CurrentRates.rates.Add(rate.Key, rate.Value.Rate);
                            }
                        }
                        else
                        {
                            Console.WriteLine("Error in API response");
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error: {ex.Message}");
                    }
                }
            }
        }
        public void Dispose()
        {

        }
    }
    public class CurrencyApiResponse
    {
        [JsonProperty("rates")]
        public Dictionary<string, CurrencyRate> Rates { get; set; }

        [JsonProperty("code")]
        public int Code { get; set; }
    }

    public class CurrencyRate
    {
        [JsonProperty("rate")]
        public double Rate { get; set; }
    }

    public static class CurrentRates
    {
        public static Dictionary<string, double> rates = new Dictionary<string, double>();
    }


}
