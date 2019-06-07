using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Singer.Services
{
   public class Sorter<T> : IList<string>
   {
      #region FIELDS

      private readonly List<string> _properties = typeof(T).GetProperties().Select(x => x.Name).ToList();
      private readonly List<string> _propertiesToSortOn = new List<string>();
      private readonly Regex _propertyRegex = new Regex("^[a-zA-Z_]\\w*$");

      #endregion FIELDS

      public IEnumerator<string> GetEnumerator() => _properties.GetEnumerator();

      IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

      public void Add(string property)
      {
         CheckPropertyName(property);
         _properties.Add(property);
      }

      public void Clear() => _properties.Clear();

      public bool Contains(string item) => _properties.Contains(item);

      public void CopyTo(string[] array, int arrayIndex) => _properties.CopyTo(array, arrayIndex);

      public bool Remove(string property) => _properties.Remove(property);

      public int Count => _properties.Count;
      public bool IsReadOnly => false;

      public int IndexOf(string property) => _properties.IndexOf(property);

      public void Insert(int index, string property)
      {
         CheckPropertyName(property);
         _properties.Insert(index, property);
      }

      public void RemoveAt(int index) => _properties.RemoveAt(index);

      public string this[int index]
      {
         get => _properties[index];
         set
         {
            CheckPropertyName(value);
            _properties[index] = value;
         }
      }

      public bool CheckPropertyName(string property)
      {
         if (property == null)
            throw new ArgumentNullException(nameof(property));
         if (property == "")
            throw new ArgumentException("Property name must have a value");
         if (!_propertyRegex.IsMatch(property))
            throw new ArgumentException("The property does not match the naming rules.");
         if (_properties.All(x => x != property))
            throw new ArgumentException("No property found with the given name.");

         return true;
      }
   }
}
