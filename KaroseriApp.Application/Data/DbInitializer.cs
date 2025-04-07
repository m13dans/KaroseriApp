using DbUp;

namespace KaroseriApp.Application.Data;

public class DbInitializer
{
    public static void Initialize(string connectionString)
    {
        EnsureDatabase.For.SqlDatabase(connectionString);

        var upgrader = DeployChanges.To
            .SqlDatabase(connectionString)
            .WithScriptsEmbeddedInAssembly(typeof(SqlConnectionFactory).Assembly)
            .WithTransaction()
            .LogToConsole()
            .Build();

        var result = upgrader.PerformUpgrade();

        if (!result.Successful)
        {
            Console.WriteLine(result.Error);
        }

        Console.WriteLine("Success upgrading database");
    }
}