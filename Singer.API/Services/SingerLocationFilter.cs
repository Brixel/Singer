using System;
using System.Linq.Expressions;

using Singer.Models;

namespace Singer.Services;

public static class SingerLocationFilter
{
    public static Expression<Func<SingerLocation, bool>> FilterByText(string keyword) =>
       location => (!string.IsNullOrEmpty(location.Name) &&
             location.Name.Contains(keyword, StringComparison.OrdinalIgnoreCase)) ||
             (!string.IsNullOrEmpty(location.Address) &&
             location.Address.Contains(keyword, StringComparison.OrdinalIgnoreCase)) ||
             (!string.IsNullOrEmpty(location.PostalCode) &&
             location.PostalCode.Contains(keyword, StringComparison.OrdinalIgnoreCase)) ||
             (!string.IsNullOrEmpty(location.City) &&
             location.City.Contains(keyword, StringComparison.OrdinalIgnoreCase)) ||
             (!string.IsNullOrEmpty(location.Country) &&
             location.Country.Contains(keyword, StringComparison.OrdinalIgnoreCase));
}
