using System.Threading.Tasks;
using SilaghiDavidLab7.Models;

namespace SilaghiDavidLab7.Data;

public class ShoppingListDatabase
{
    private readonly IRestService _restService;

    public ShoppingListDatabase(IRestService service)
    {
        _restService = service;
    }

    public Task<List<ShopList>> GetShopListsAsync()
    {
        return _restService.RefreshDataAsync();
    }

    public Task SaveShopListAsync(ShopList item, bool isNewItem = true)
    {
        return _restService.SaveShopListAsync(item, isNewItem);
    }

    public Task DeleteShopListAsync(ShopList item)
    {
        return _restService.DeleteShopListAsync(item.ID);
    }
}
