using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using SilaghiDavidLab7.Models;

namespace SilaghiDavidLab7.Data;

public class RestService : IRestService
{
    private readonly HttpClient _client;
    private readonly string _restUrl = "https://localhost:7123/api/ShopLists/"\;  // ← SCHIMBĂ PORTUL AICI !!!

    public RestService()
    {
        var handler = new HttpClientHandler
        {
            ServerCertificateCustomValidationCallback = (msg, cert, chain, errors) => true  // pentru localhost, acceptă certificat invalid
        };
        _client = new HttpClient(handler);
    }

    public async Task<List<ShopList>> RefreshDataAsync()
    {
        var items = new List<ShopList>();
        try
        {
            var response = await _client.GetAsync(_restUrl);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                items = JsonConvert.DeserializeObject<List<ShopList>>(content) ?? new();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Refresh error: {ex.Message}");
        }
        return items;
    }

    public async Task SaveShopListAsync(ShopList item, bool isNewItem = true)
    {
        try
        {
            var json = JsonConvert.SerializeObject(item);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage response;
            if (isNewItem)
                response = await _client.PostAsync(_restUrl, content);
            else
                response = await _client.PutAsync(_restUrl + item.ID, content);

            if (response.IsSuccessStatusCode)
                Console.WriteLine("Saved OK");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Save error: {ex.Message}");
        }
    }

    public async Task DeleteShopListAsync(int id)
    {
        try
        {
            var response = await _client.DeleteAsync(_restUrl + id);
            if (response.IsSuccessStatusCode)
                Console.WriteLine("Deleted OK");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Delete error: {ex.Message}");
        }
    }
}
