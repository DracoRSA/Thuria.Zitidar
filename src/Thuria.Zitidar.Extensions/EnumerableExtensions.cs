using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;

namespace Thuria.Zitidar.Extensions;

/// <summary>
/// Enumerable Extensions
/// </summary>
public static class EnumerableExtensions
{
    /// <summary>
    /// Foreach item in a collection perform a specified action
    /// </summary>
    /// <typeparam name="T">Collection Object Type</typeparam>
    /// <param name="inputCollection">Input Collection</param>
    /// <param name="actionToExecute">Action to be executed for each item in the collection</param>
    public static void ForEach<T>(this IEnumerable<T> inputCollection, Action<T> actionToExecute)
    {
        foreach (var currentItem in inputCollection)
        {
            actionToExecute(currentItem);
        }
    }

    /// <summary>
    /// Determine if a collection is Empty
    /// </summary>
    /// <typeparam name="T">Collection Type</typeparam>
    /// <param name="inputCollection">Input Collection</param>
    /// <returns>Boolean indicating if the collection is empty (true) or not (false)</returns>
    public static bool IsEmpty<T>(this IEnumerable<T>? inputCollection)
    {
        if (inputCollection == null)
        {
            return true;
        }

        return !inputCollection.Any();
    }

    /// <summary>
    /// Retrieve all the items in a collection as a newline delimited string
    /// </summary>
    /// <typeparam name="T">Collection Type</typeparam>
    /// <param name="inputCollection">Input Collection</param>
    /// <returns>A newline delimited string representing all the items in a collection</returns>
    public static string GetAllAsString<T>(this IEnumerable<T> inputCollection)
    {
        var allStrings = new StringBuilder();

        foreach (var currentObject in inputCollection)
        {
            if (allStrings.Length > 0)
            {
                allStrings.Append(Environment.NewLine);
            }

            allStrings.Append(currentObject);
        }

        return allStrings.ToString();
    }

    /// <summary>
    /// Concatenate the provided items to an existing collection
    /// </summary>
    /// <typeparam name="T">Collection Type</typeparam>
    /// <param name="inputCollection">Input Collection</param>
    /// <param name="itemsToAdd">Item(s) to add to the collection</param>
    /// <returns>The newly create collection of items</returns>
    public static IEnumerable<T> And<T>(this IEnumerable<T> inputCollection, params T[] itemsToAdd)
    {
        return inputCollection.Concat(itemsToAdd);
    }
}