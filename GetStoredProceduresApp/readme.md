# About

Provides access to user stored procedures.

In the code below, `list` has databases with user stored procedures which the database name and stored procedure names are printed.

Each stored procedure has its definition but not outputed to the screen.

The main purpose of this project is to show how to get user stored procedures with no user interface unlike the project `GetStoredProceduresAppVisual` which displays user stored procedures to the screen.

```csharp
static async Task Main(string[] args)
{

    List<DatabaseContainer> list = await GetStoredProcedureDetails();

    foreach (var container in list)
    {
        AnsiConsole.MarkupLine($"[cyan]{container.Database}[/]");
        foreach (var procedureContainer in container.List)
        {
            Console.WriteLine($"    {procedureContainer.Procedure}");
        }
    }
}
```
