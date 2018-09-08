using System;

namespace Thuria.Zitidar.Extensions
{
  public static class TypeExtensions
  {
    public static object GetPropertyValue(this object currentObject, string propertyName)
    {
      var propertyInfo = currentObject.GetType().GetProperty(propertyName);
      if (propertyInfo == null)
      {
        throw new ArgumentException($"Property [{propertyName}] does not exist on object [{currentObject.GetType().Name}]");
      }

      return propertyInfo.GetValue(currentObject);
    }

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

    public static bool DoesPropertyExist(this object currentObject, string propertyName)
    {
      if (currentObject == null) { throw new ArgumentNullException(nameof(currentObject)); }

      var propertyInfo = currentObject.GetType().GetProperty(propertyName);
      return propertyInfo != null;
    }
  }
}
