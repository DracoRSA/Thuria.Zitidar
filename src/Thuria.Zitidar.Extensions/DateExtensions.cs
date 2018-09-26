using System;

namespace Thuria.Zitidar.Extensions
{
  /// <summary>
  /// Date and DateTime Extensions
  /// </summary>
  public static class DateExtensions
  {
    /// <summary>
    /// Return Start of Day for a specified Date
    /// </summary>
    /// <param name="inputDate">Input Date</param>
    /// <returns>DateTime with the Start of Day set</returns>
    public static DateTime StartOfDay(this DateTime inputDate)
    {
      return new DateTime(inputDate.Year, inputDate.Month, inputDate.Day, 0, 0, 0);
    }

    /// <summary>
    /// Return End of Day for a specified Date
    /// </summary>
    /// <param name="inputDate">Input Date</param>
    /// <returns>DateTime with the End of Day set</returns>
    public static DateTime EndOfDay(this DateTime inputDate)
    {
      return new DateTime(inputDate.Year, inputDate.Month, inputDate.Day, 23, 59, 59, 999);
    }
  }
}
