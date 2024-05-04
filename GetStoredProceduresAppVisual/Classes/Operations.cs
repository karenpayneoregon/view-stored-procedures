using CommonLibrary;
using SqlServerLibrary.Classes;

namespace GetStoredProceduresAppVisual.Classes;
internal class Operations
{
    /// <summary>
    /// Get stored procedures from all databases on a SQL-Server instance
    /// </summary>
    public static async Task<ListDictionary> GetStoredProcedureDetails()
    {
        
        ListDictionary listDictionary = new();
        StoredProcedureHelpers helpers = new();

        var service = new DatabaseService();
        List<string> dbNames = await service.DatabaseNamesFiltered();

        foreach (var name in dbNames)
        {

            var (hasStoredProcedures, list) = await helpers.GetStoredProcedureNameSafe(name, "'xp_', 'ms_'");
            
            if (hasStoredProcedures)
            {

                foreach (var item in list)
                {
                    var definition = await helpers.GetStoredProcedureDefinitionAsync(name,item);
                    if (definition is not null && !definition.Contains("diagram"))
                    {
                        listDictionary.Add(name, item);
                    }
                }
            }
        }

        return listDictionary; 
        
    }
}
