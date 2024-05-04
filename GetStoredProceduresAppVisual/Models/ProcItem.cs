namespace GetStoredProceduresAppVisual.Models;

public class ProcItem(string name, List<string> list)
{
    public string Name { get; } = name;
    public List<string> List { get; } = list;

    public override string ToString() => Name;

}