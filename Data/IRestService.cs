using System.Threading.Tasks;
using SilaghiDavidLab7.Models;   // schimbă namespace-ul dacă e altul (ex: NumePrenLab7.Models)

namespace SilaghiDavidLab7.Data;

public interface IRestService
{
    Task<List<ShopList>> RefreshDataAsync();
    Task SaveShopListAsync(ShopList item, bool isNewItem = true);
    Task DeleteShopListAsync(int id);
}
