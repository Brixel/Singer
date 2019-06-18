using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Singer.Services
{
   /// <summary>
   /// Filter is a class that holds a filter model and the list of the properties to filter on.
   /// The filter can check whether a certain value passes the filter by comparing all specified
   /// properties with the string 
   /// </summary>
   /// <typeparam name="T">Type of value to filter.</typeparam>
   public class StringFilter<T>
   {
      private readonly PropertyInfo[] _typeProperties = typeof(T).GetProperties();


      /// <summary>
      /// String to compare the properties with.
      /// </summary>
      public string FilterString { get; set; }

      /// <summary>
      /// List of properties that should be compared
      /// </summary>
      public PropertyList<T> PropertyList { get; set; }

      /// <summary>
      /// Returns an expression that can be used to filter a collection.
      /// </summary>
      /// <returns>An expression that can be used to filter a collection.</returns>
      public Expression<Func<T, bool>> GetFilterExpression()
      {
         // get the "ToString"-method of a object
         var toStringMethod = typeof(object).GetMethod(nameof(ToString));
         // get the "ToLower"-method of a string
         var toLowerMethod = typeof(string).GetMethod(nameof(string.ToLowerInvariant));
         // get the "Contains"-method of a string
         var containsMethod = typeof(string).GetMethod(nameof(string.Contains), new[] {typeof(string)});

         // create the parameter of the expression "t =>" 
         var parameterExpression = Expression.Parameter(typeof(T), "t");

         // create the comparing constant of the expressions "FilterString"
         var stringExpression = Expression.Constant(FilterString);
         var lowerStringExpression =
            Expression.Call(stringExpression, typeof(string).GetMethod(nameof(string.ToLowerInvariant)));

         // convert the property list to expressions to get the properties as string in lowercase
         var propertyExpressions = PropertyList
            .Select(propertyName =>
            {
               var propertyExpression = Expression.Property(parameterExpression, typeof(T).GetProperty(propertyName));
               var stringPropertyExpression = Expression.Call(propertyExpression, toStringMethod);
               return Expression.Call(stringPropertyExpression, toLowerMethod);
            });

         // create equal expressions for all "t => $propertyName == $FilterString"
         var equalExpressions = propertyExpressions
            .Select(pe => Expression.Call(pe, containsMethod, lowerStringExpression))
            .ToList();

         // if there is only one property to filter on, return "t => $propertyName == $FilterString"
         if (PropertyList.Count == 1)
            return Expression.Lambda<Func<T, bool>>(equalExpressions.First(), parameterExpression);

         // concatenate expressions
         var finalExpression = Expression.OrElse(equalExpressions[0], equalExpressions[1]);
         for (var i = 2; i < PropertyList.Count; i++)
            finalExpression = Expression.OrElse(finalExpression, equalExpressions[i]);

         // return the final expression
         return Expression.Lambda<Func<T, bool>>(finalExpression, parameterExpression);
      }
   }
}
