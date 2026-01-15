using SQLite;
using SQLiteNetExtensions.Attributes;

namespace SilaghiDavidLab7.Models;

public class Shop
{
    [PrimaryKey, AutoIncrement]
    public int ID { get; set; }

    public string ShopName { get; set; } = string.Empty;

    public string Adress { get; set; } = string.Empty;  // ← așa e în enunț

    public string ShopDetails => $"{ShopName} {Adress}";

    [OneToMany]
    public List<ShopList>? ShopLists { get; set; }
}
