﻿namespace Thuria.Zitidar.Extensions;

/// <summary>
/// Various Type and Object Extensions
/// </summary>
public static class TypeExtensions
{
    private static readonly Dictionary<Type, Func<object?>> DefaultValueGenerators = new()
                                                                                     {
                                                                                         { typeof(string), () => default(string) },
                                                                                         { typeof(Guid), () => Guid.Empty },
                                                                                         { typeof(Guid?), () => null },
                                                                                         { typeof(DateTime), () => default(DateTime) },
                                                                                         { typeof(int), () => default(int) },
                                                                                         { typeof(int?), () => null },
                                                                                         { typeof(uint), () => default(uint) },
                                                                                         { typeof(uint?), () => null },
                                                                                         { typeof(long), () => default(long) },
                                                                                         { typeof(long?), () => null },
                                                                                         { typeof(decimal), () => default(decimal) },
                                                                                         { typeof(decimal?), () => null },
                                                                                         { typeof(float), () => default(float) },
                                                                                         { typeof(float?), () => null },
                                                                                         { typeof(bool), () => default(bool) },
                                                                                         { typeof(bool?), () => null }
                                                                                     };

    private static readonly Dictionary<Type, Func<object, object>> TypeConverters = new()
                                                                                    {
                                                                                        { typeof(Guid), inputValue => Guid.Parse(inputValue.ToString() ?? string.Empty) }
                                                                                    };

    /// <summary>
    /// Get Property Value
    /// </summary>
    /// <param name="currentObject">Current Object to get the value from</param>
    /// <param name="propertyName">Property Name</param>
    /// <returns>The value of the specified property</returns>
    public static object? GetPropertyValue(this object currentObject, string propertyName)
    {
        var propertyInfo = currentObject.GetType().GetProperty(propertyName);
        if (propertyInfo == null)
        {
            throw new ArgumentException($"Property [{propertyName}] does not exist on object [{currentObject.GetType().Name}]");
        }

        return propertyInfo.GetValue(currentObject);
    }

    /// <summary>
    /// Set Property Value
    /// </summary>
    /// <param name="currentObject">Object to set the property value on</param>
    /// <param name="propertyName">Property Name</param>
    /// <param name="propertyValue">Property Value</param>
    /// <param name="convertIfRequired">Indicator to determine if the value must be converted to the Property Data Type</param>
    public static void SetPropertyValue(this object currentObject, string propertyName, object propertyValue, bool convertIfRequired = false)
    {
        var propertyInfo = currentObject.GetType().GetProperty(propertyName);
        if (propertyInfo == null)
        {
            throw new ArgumentException($"Property [{propertyName}] does not exist on object [{currentObject.GetType().Name}]");
        }

        object valueToSet;
        if (convertIfRequired && propertyValue.GetType() != propertyInfo.PropertyType)
        {
            valueToSet = TypeConverters.TryGetValue(propertyInfo.PropertyType, out var converter)
                             ? converter(propertyValue)
                             : Convert.ChangeType(propertyValue, propertyInfo.PropertyType);
        }
        else
        {
            valueToSet = propertyValue;
        }

        propertyInfo.SetValue(currentObject, valueToSet);
    }

    /// <summary>
    /// Determine if a Property exist on an object
    /// </summary>
    /// <param name="currentObject">Object</param>
    /// <param name="propertyName">Property name</param>
    /// <returns>A boolean indicating whether the property exists on the given object</returns>
    public static bool DoesPropertyExist(this object currentObject, string propertyName)
    {
        if (currentObject == null)
        {
            throw new ArgumentNullException(nameof(currentObject));
        }

        var propertyInfo = currentObject.GetType().GetProperty(propertyName);
        return propertyInfo != null;
    }

    /// <summary>
    /// Retrieve the Default Value for a Type
    /// </summary>
    /// <param name="objectType">Type</param>
    /// <returns>Default Value or null</returns>
    public static object? GetDefaultData(this Type objectType)
    {
        if (objectType.IsGenericType && (objectType.GetGenericTypeDefinition() == typeof(IList<>)))
        {
            var listType         = typeof(List<>);
            var listInternalType = objectType.GetGenericArguments()[0];
            var genericType      = listType.MakeGenericType([listInternalType]);
            return Activator.CreateInstance(genericType);
        }

        return DefaultValueGenerators.TryGetValue(objectType, out var generator)
                   ? generator()
                   : default;
    }
}