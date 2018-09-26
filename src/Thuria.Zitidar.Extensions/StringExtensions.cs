using System;
using System.Text.RegularExpressions;

namespace Thuria.Zitidar.Extensions
{
  /// <summary>
  /// String Extensions
  /// </summary>
  public static class StringExtensions
  {
    private static readonly Regex SpaceAndCharacterRegex = new Regex("[^a-zA-Z0-9_]");

    /// <summary>
    /// Remove Spaces and Characters from a string
    /// </summary>
    /// <param name="inputString">Input String</param>
    /// <returns>String with the spaces and characters removed</returns>
    public static string RemoveSpaceAndCharacters(this string inputString)
    {
      return SpaceAndCharacterRegex.Replace(inputString, string.Empty);
    }

    /// <summary>
    /// Convert a string to Camel Case
    /// </summary>
    /// <param name="inputString">Input String</param>
    /// <returns>String converted to Camel Case</returns>
    public static string CamelCase(this string inputString)
    {
      var returnValue = inputString.PascalCase();
      return char.ToLowerInvariant(returnValue[0]) + returnValue.Substring(1);
    }

    /// <summary>
    /// Convert a string to Pascal Casing
    /// </summary>
    /// <param name="inputString">Input String</param>
    /// <returns>String converted to Pascal Casing</returns>
    public static string PascalCase(this string inputString)
    {
      var words = inputString.Split(new char[] { ' ', '_' }, StringSplitOptions.RemoveEmptyEntries);
      var result = "";

      foreach (var word in words)
      {
        result += $"{word.Substring(0, 1).ToUpper()}{word.Substring(1, word.Length - 1)}";
      }

      return result.RemoveSpaceAndCharacters();
    }

    /// <summary>
    /// Escape the Quotes in a string
    /// </summary>
    /// <param name="inputString">Input String</param>
    /// <returns>String with the Quotes escaped</returns>
    public static string EscapeQuotes(this string inputString)
    {
      return string.IsNullOrEmpty(inputString)
                              ? string.Empty
                              : inputString.Replace("\"", "\\\"");
    }
  }
}
