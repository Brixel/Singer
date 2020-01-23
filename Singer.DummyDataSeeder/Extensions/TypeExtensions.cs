using System;
using System.Linq;
using System.Reflection;
using Microsoft.AspNetCore.Mvc;

namespace Singer.DummyDataSeeder.Extensions
{
    public static class TypeExtensions
    {
        public static string GetRoute(this Type type)
        {
            var attribute = type.GetCustomAttributes<RouteAttribute>()?.FirstOrDefault()
                ?? throw new ArgumentException($"The type {type.Name} does noet have the {nameof(RouteAttribute)}", nameof(type));

            var typeName = type.Name;
            var controllerName = typeName.Substring(0, typeName.Length - "Controller".Length);
            return attribute.Template.ToLower().Replace("[controller]", controllerName);
        }
    }
}
