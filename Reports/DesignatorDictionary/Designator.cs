using System.Text.RegularExpressions;

public class Designator
{
    public static string GetSymbolsFromDesignator(string designator)
    {
        Regex regEx = new Regex("[^][\\d+]");
        Match match = regEx.Match(designator);
        designator = match.Success ? match.Value : designator;
        return designator;
    }
}
