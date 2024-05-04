using CommonLibrary;

using GetStoredProceduresApp.Classes;
using GetStoredProceduresApp.Models;
using SqlServerLibrary.Classes;

namespace GetStoredProceduresApp;

internal partial class Program
{
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

        SpectreConsoleHelpers.ExitPrompt();
    }


    public static async Task<List<DatabaseContainer>> GetStoredProcedureDetails()
    {
        ListDictionary listDictionary = new();
        StoredProcedureHelpers helpers = new();
       
        List<DatabaseContainer> databaseContainers = new();

        var service = new DatabaseService();
        List<string> dbNames = await service.DatabaseNamesFiltered();

        dbNames = dbNames.OrderBy(x => x).ToList();
        foreach (var dbName in dbNames)
        {

            var (hasStoredProcedures, list) = await helpers.GetStoredProcedureNameSafe(dbName, "'xp_', 'ms_'");
            if (hasStoredProcedures)
            {

                var root = databaseContainers.FirstOrDefault(x => x.Database == dbName);
                DatabaseContainer container = new DatabaseContainer { Database = dbName };
                if (root is null)
                {
                    container = new DatabaseContainer { Database = dbName };
                }
                
                
                foreach (var item in list)
                {
                    var definition = await helpers.GetStoredProcedureDefinitionAsync(dbName, item);
                    if (definition is not null && !item.Contains("diagram"))
                    {
                        listDictionary.Add(dbName, item);
                        container.List.Add(new ProcedureContainer { Procedure = item, Definition = definition });
                    }
                    
                }

                databaseContainers.Add(container);
            }
        }

        //if (!listDictionary.HasItems) return (List<DatabaseContainer>)Enumerable.Empty<DatabaseContainer>(); 
        //{
        //    foreach (var (key, value) in listDictionary.Dictionary)
        //    {
        //        Console.WriteLine(key);
        //        foreach (var procName in value)
        //        {
        //            Console.WriteLine($"   {procName}");
        //        }
        //    }
        //}



        return databaseContainers.Where(x => x.List.Count > 0).ToList();
    }

}

