using System;

namespace Thuria.Zitidar.Extensions
{
  /// <summary>
  /// Various Type and Object Extensions
  /// </summary>
  public static class TypeExtensions
  {
    /// <summary>
    /// Get Property Value
    /// </summary>
    /// <param name="currentObject">Current Object to get the value from</param>
    /// <param name="propertyName">Property Name</param>
    /// <returns></returns>
    public static object GetPropertyValue(this object currentObject, string propertyName)
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

      var valueToSet = convertIfRequired && propertyValue.GetType() != propertyInfo.PropertyType
                              ? Convert.ChangeType(propertyValue, propertyInfo.PropertyType)
                              : propertyValue;

      propertyInfo.SetValue(currentObject, valueToSet);
    }

    /// <summary>
    /// Determin if a Property exist on an object
    /// </summary>
    /// <param name="currentObject">Object</param>
    /// <param name="propertyName">Property name</param>
    /// <returns></returns>
    public static bool DoesPropertyExist(this object currentObject, string propertyName)
    {
      if (currentObject == null) { throw new ArgumentNullException(nameof(currentObject)); }

      var propertyInfo = currentObject.GetType().GetProperty(propertyName);
      return propertyInfo != null;
    }
  }
}
