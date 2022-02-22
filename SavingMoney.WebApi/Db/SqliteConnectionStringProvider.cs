namespace SavingMoney.WebApi.Db;

public class SqliteConnectionStringProvider
{
    public static string Get()
    {
        var folder = Environment.SpecialFolder.LocalApplicationData;
        var path = Environment.GetFolderPath(folder);
        return $"Data Source={Path.Join(path, "SavingMoneyContext.db")}";
    }
}