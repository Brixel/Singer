using System.Linq;
using System.Reflection;

namespace Singer.Services;

/// <summary>
/// Filter is a class that holds a filter model and the list of the properties to filter on.
/// The filter can check whether a certain value passes the filter by looking whether the
/// specified properties match.
/// </summary>
/// <typeparam name="T">Type of value to filter.</typeparam>
public class Filter<T>
{
    private readonly PropertyInfo[] _typeProperties = typeof(T).GetProperties();


    /// <summary>
    /// Model that holds the values to compare to a value to check.
    /// </summary>
    public T FilterModel { get; set; }

    /// <summary>
    /// List of properties that should be compared.
    /// </summary>
    public PropertyList<T> PropertiesToFilterOn { get; set; }

    /// <summary>
    /// Checks whether all the properties specified in the <see cref="PropertiesToFilterOn"/> are
    /// equal on the <see cref="FilterModel"/> as on the <see cref="valueToCheck"/>. This is done
    /// with the Equals() method.
    /// </summary>
    /// <param name="valueToCheck">Value to compare to the <see cref="FilterModel"/></param>
    /// <returns>
    /// true if the all specified properties are the same or there are no properties specified,
    /// otherwise false
    /// </returns>
    public bool CheckAnd(T valueToCheck)
    {
        // if there are no properties specified, return true
        if (PropertiesToFilterOn == null || PropertiesToFilterOn.Count == 0)
            return true;

        // check whether all the specified properties are equal
        return _typeProperties.All(p => Equals(p.GetValue(valueToCheck), p.GetValue(FilterModel)));
    }

    /// <summary>
    /// Checks whether any the properties specified in the <see cref="PropertiesToFilterOn"/> are
    /// equal on the <see cref="FilterModel"/> as on the <see cref="valueToCheck"/>. This is done
    /// with the Equals() method.
    /// </summary>
    /// <param name="valueToCheck">Value to compare to the <see cref="FilterModel"/></param>
    /// <returns>
    /// true if the any specified properties are the same or there are no properties specified,
    /// otherwise false
    /// </returns>
    public bool CheckOr(T valueToCheck)
    {
        // if there are no properties specified, return true
        if (PropertiesToFilterOn == null || PropertiesToFilterOn.Count == 0)
            return true;

        // check whether all the specified properties are equal
        return _typeProperties.Any(p => Equals(p.GetValue(valueToCheck), p.GetValue(FilterModel)));
    }
}
