using System;
using System.Reflection;
using Microsoft.AspNetCore.Mvc;

namespace Singer.DummyDataSeeder.Extensions
{
    public static class TypeExtensions
    {
        public static string GetRoute(this Type type)
        {
            var attribute = type.GetCustomAttribute<RouteAttribute>() 
                ?? throw new ArgumentException($"The type {type.Name} does noet have the {nameof(RouteAttribute)}", nameof(type));

            return attribute.Template.Replace("[Controller]", type.Name);
        }
    }
}