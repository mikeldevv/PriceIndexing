using PriceIndexing.Api.Models;

namespace PriceIndexing.Api.Services;

public class BinanceService
{
    private const string BinanceEndpoint = "https://api.binance.com/api/v3/ticker/price";

    private readonly HttpClient _httpClient;

    public BinanceService()
    {
        _httpClient = new HttpClient();
    }

    public async Task<Ticker> GetPrice(string symbol)
    {
        try
        {
            var response = await _httpClient.GetAsync($"{BinanceEndpoint}?symbol={symbol}");
            response.EnsureSuccessStatusCode();

            var responseBody = await response.Content.ReadAsStringAsync();
            
            var ticker = Newtonsoft.Json.JsonConvert.DeserializeObject<Ticker>(responseBody);

            return ticker;
        }
        catch (Exception ex)
        {
            throw new Exception($"Error retrieving price from Binance API: {ex.Message}");
        }
    }
}