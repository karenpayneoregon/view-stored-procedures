using System.Runtime.CompilerServices;

// ReSharper disable once CheckNamespace
namespace GetStoredProceduresApp;
internal partial class Program
{
    [ModuleInitializer]
    public static void Init()
    {
        AnsiConsole.MarkupLine("");
        Console.Title = "Stored procedure definitions";
        WindowUtility.SetConsoleWindowPosition(WindowUtility.AnchorWindow.Center);
    }

}
