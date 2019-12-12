using System;

namespace Singer.Helpers.Attributes
{
   [AttributeUsage(AttributeTargets.Property, Inherited = true, AllowMultiple = false)]
   sealed class CsvPropertyAttribute : Attribute
   {
      private string _propertyName;

      public CsvPropertyAttribute()
      {
      }

      public string PropertyName
      {
         get => _propertyName ?? PropertyNameResourceType?.GetProperty(PropertyNmeResourceName)?.GetValue(null, null) as string;
         set => _propertyName = value;
      }
      public string PropertyNmeResourceName { get; set; }
      public Type PropertyNameResourceType { get; set; }
   }
}
