namespace SqlServerLibrary.Extensions;
public static class CleanerExtensions
{
    /// <summary>
    /// Remove backslash and space with underscores
    /// </summary>
    public static string Clean(this string input) 
        => input.TrimStart('.').Replace("\\", "_").Replace(" ", "_").Trim();
    public static string CleanFileName(this string? fileName) 
        => Path.GetInvalidFileNameChars().Aggregate(fileName, 
            (current, c) => current.Replace(c.ToString(), string.Empty))
            .Replace(".","");
}
