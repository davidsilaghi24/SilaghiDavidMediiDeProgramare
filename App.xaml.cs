using SilaghiDavidLab7.Data;

namespace SilaghiDavidLab7;

public partial class App : Application
{
    static ShoppingListDatabase? database;

    public static ShoppingListDatabase Database
    {
        get
        {
            if (database == null)
            {
                database = new ShoppingListDatabase(new RestService());
                    Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), 
                    "ShoppingList.db3"));
            }
            return database;
        }
    }

    public App()
    {
        InitializeComponent();
        MainPage = new AppShell();
    }
}
