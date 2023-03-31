namespace Singer.Helpers.Extensions;

public static class StringExtensions
{
    public static bool HasData(this string input)
    {
        return !string.IsNullOrWhiteSpace(input);
    }
}