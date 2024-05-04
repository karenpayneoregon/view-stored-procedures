namespace CommonLibrary;

public class ListDictionary
{
    private Dictionary<string, List<string>> _internalDictionary = new();
    public Dictionary<string, List<string>> Dictionary 
        => _internalDictionary;
    
    public bool HasItems => _internalDictionary.Count > 0;

    public void Add(string key, string value)
    {
        if (_internalDictionary.TryGetValue(key, out var item))
        {
            if (item.Contains(value) == false)
            {
                item.Add(value);
            }
        }
        else
        {
            List<string> list = [value];
            _internalDictionary.Add(key, list);
        }
    }
}
