using SilaghiDavidLab7.Models;

namespace SilaghiDavidLab7;

public partial class ShopPage : ContentPage
{
    public ShopPage() { InitializeComponent(); }

    async void OnSaveButtonClicked(object sender, EventArgs e)
    {
        var shop = (Shop)BindingContext;
        await App.Database.SaveShopAsync(shop);
        await Navigation.PopAsync();
    }

    async void OnShowMapButtonClicked(object sender, EventArgs e)
    {
        var shop = (Shop)BindingContext;
        if (string.IsNullOrWhiteSpace(shop.Adress)) { await DisplayAlert("Eroare", "Introdu adresa!", "OK"); return; }
        try
        {
            var locations = await Geocoding.GetLocationsAsync(shop.Adress);
            var location = locations?.FirstOrDefault();
            if (location != null)
                await Map.OpenAsync(location, new MapLaunchOptions { Name = shop.ShopName });
            else
                await DisplayAlert("Eroare", "Adresa nu găsită.", "OK");
        }
        catch { await DisplayAlert("Eroare", "Nu s-a putut deschide harta.", "OK"); }
    }

    async void OnDeleteButtonClicked(object sender, EventArgs e)
    {
        var shop = (Shop)BindingContext;
        if (shop.ID == 0) { await Navigation.PopAsync(); return; }
        bool ok = await DisplayAlert("Ștergere", $"Ștergi \"{shop.ShopName}\"?", "Da", "Nu");
        if (ok)
        {
            await App.Database.DeleteShopAsync(shop);
            await Navigation.PopAsync();
        }
    }
}
