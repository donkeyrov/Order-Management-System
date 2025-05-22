using DbUp;
using Microsoft.IdentityModel.Protocols;
using System.Configuration;
using System.Reflection;

var connectionString = ConfigurationManager.ConnectionStrings["dbConnecton"].ConnectionString;
//DbUp.Engine.UpgradeEngine
DbUp.Engine.UpgradeEngine upgrader = null;

string show = args.FirstOrDefault() ?? "true";
if (show == "true")
{
    upgrader = DeployChanges.To
        .SqlDatabase(connectionString)
        .WithScriptsEmbeddedInAssembly(Assembly.GetExecutingAssembly())
        .LogToConsole()
        .Build();
}
else
{
    upgrader = DeployChanges.To
        .SqlDatabase(connectionString)
        .WithScriptsEmbeddedInAssembly(Assembly.GetExecutingAssembly())
        .Build();
}


var result = upgrader.PerformUpgrade();

if (!result.Successful)
{
    Console.ForegroundColor = ConsoleColor.Red;
    Console.WriteLine(result.Error);
    Console.ResetColor();
#if DEBUG
    Console.ReadLine();
#endif
    return;
}

Console.ForegroundColor = ConsoleColor.Green;
Console.WriteLine("Success!");
Console.ResetColor();
return;
