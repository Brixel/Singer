using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text.RegularExpressions;

namespace Singer.Services
{
   /// <summary>
   /// This class <see cref="Collection{T}"/> properties a specified type. Properties can be added and removed.
   /// If a property is added that is not present in the type <see cref="T"/>, an exception is thrown.
   /// It can be used to define a list of properties that should be used for example to sort on.
   /// </summary>
   /// <typeparam name="T">Type of which the properties should be.</typeparam>
   public class PropertyList<T> : ICollection<string>
   {
      #region FIELDS

      /// <summary>
      /// List of all the properties of type <see cref="T"/>.
      /// </summary>
      private readonly IReadOnlyList<string> _properties =
         new ReadOnlyCollection<string>(typeof(T).GetProperties().Select(x => x.Name).ToList());

      /// <summary>
      /// Internal list of stored properties.
      /// </summary>
      private readonly List<string> _storedProperties = new List<string>();

      /// <summary>
      /// Regex to control whether a given property follows the property rules of C#.
      /// It can only start with a letter or underscore and after t hat it can be any alfa-numeric character or
      /// an underscore.
      /// </summary>
      private readonly Regex _propertyRegex = new Regex("^[a-zA-Z_]\\w*$");

      #endregion FIELDS


      #region PROPERTIES

      /// <summary>
      /// Gets the number of elements contained in the <see cref="PropertyList{T}"/>
      /// </summary>
      /// <returns>The number of elements contained in the <see cref="PropertyList{T}"/></returns>
      public int Count => _storedProperties.Count;

      /// <summary>
      /// Gets a value indicating whether the <see cref="PropertyList{T}"/> is read only.
      /// </summary>
      /// <returns>false</returns>
      public bool IsReadOnly => false;

      #endregion PROPERTIES


      #region METHODS

      /// <summary>
      /// Returns an enumerator that iterates through the collection.
      /// </summary>
      /// <returns>An enumerator that can be used to iterate through the collection.</returns>
      public IEnumerator<string> GetEnumerator() => _storedProperties.GetEnumerator();

      /// <summary>
      /// Returns an enumerator that iterates through the collection.
      /// </summary>
      /// <returns>An <see cref="IEnumerator"/> object that can be used to iterate through the collection.</returns>
      IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

      /// <summary>
      /// Does a check whether the property-name exists in the class. If not, an exception is thrown.
      /// Adds the property-name to the collection.
      /// </summary>
      /// <param name="property">The name of the property to add.</param>
      /// <exception cref="ArgumentNullException">When <see cref="property"/> is null.</exception>
      /// <exception cref="ArgumentException">
      /// <ul>
      ///   <li><see cref="property"/> is "".</li>
      ///   <li><see cref="property"/> does not match the C# naming rules.</li>
      ///   <li><see cref="property"/> is not found in the type <see cref="T"/></li>
      /// </ul>
      /// </exception>
      public void Add(string property)
      {
         // first check whether the property name is valid before adding it.
         CheckPropertyName(property);
         // add the property name to the internal collection
         _storedProperties.Add(property);
      }

      /// <summary>
      /// Removes all items from the <see cref="PropertyList{T}"/>.
      /// </summary>
      public void Clear() => _storedProperties.Clear();

      /// <summary>
      /// Determines whether the <see cref="PropertyList{T}"/> contains a specific value.
      /// </summary>
      /// <param name="property">The object to locate in the <see cref="PropertyList{T}"/>.</param>
      /// <returns>
      /// true if <paramref name="property">property</paramref> is found in the <see cref="PropertyList{T}"></see>;
      /// otherwise, false.
      /// </returns>
      public bool Contains(string property) => _storedProperties.Contains(property);

      /// <summary>
      /// Copies the elements of the <see cref="PropertyList{T}"></see> to an <see cref="Array"></see>, starting at
      /// a particular <see cref="Array"></see> index.
      /// </summary>
      /// <param name="array">
      /// The one-dimensional <see cref="Array"></see> that is the destination of the elements copied from
      /// <see cref="PropertyList{T}"></see>. The <see cref="T:System.Array"></see> must have zero-based indexing.
      /// </param>
      /// <param name="arrayIndex">The zero-based index in array at which copying begins.</param>
      /// <exception cref="ArgumentNullException"><paramref name="array">array</paramref> is null.</exception>
      /// <exception cref="ArgumentOutOfRangeException"><paramref name="arrayIndex">arrayIndex</paramref> is less than 0.</exception>
      /// <exception cref="ArgumentException">
      /// The number of elements in the source <see cref="PropertyList{T}"></see> is greater than the available space from
      /// <paramref name="arrayIndex">arrayIndex</paramref> to the end of the destination <paramref name="array">array</paramref>.
      /// </exception>
      public void CopyTo(string[] array, int arrayIndex) => _storedProperties.CopyTo(array, arrayIndex);

      /// <summary>
      /// Removes the first occurrence of a specific object from the <see cref="PropertyList{T}"></see>.
      /// </summary>
      /// <param name="property">The object to remove from the <see cref="T:System.Collections.Generic.ICollection`1"></see>.</param>
      /// <returns>
      /// true if <paramref name="property">item</paramref> was successfully removed from the <see cref="PropertyList{T}"></see>;
      /// otherwise, false. This method also returns false if <paramref name="property">item</paramref> is not found in the
      /// original <see cref="PropertyList{T}"></see>.
      /// </returns>
      public bool Remove(string property) => _storedProperties.Remove(property);

      /// <summary>
      /// Checks whether a property name is valid for the type <see cref="T"/>.
      /// </summary>
      /// <param name="property">Property name to check.</param>
      public void CheckPropertyName(string property)
      {
         // the property cannot be null
         if (property == null)
            throw new ArgumentNullException(nameof(property));

         // the property name cannot be empty
         if (property == "")
            throw new ArgumentException("Property name must have a value");

         // the property name must follow certain rules
         if (!_propertyRegex.IsMatch(property))
            throw new ArgumentException("The property does not match the naming rules.");

         // the property must exist in the type
         if (_properties.All(x => x != property))
            throw new ArgumentException("No property found with the given name.");
      }

      #endregion METHODS
   }
}
