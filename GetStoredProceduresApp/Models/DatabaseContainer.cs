namespace GetStoredProceduresApp.Models;
public class DatabaseContainer
{
    public string Database { get; set; }
    public List<ProcedureContainer> List { get; set; } = new();
    public override string ToString() => Database;

}


public class ProcedureContainer
{
    public string Procedure { get; set; }
    public string Definition { get; set; }
    public override string ToString() => Procedure;
}