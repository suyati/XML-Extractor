namespace Suyati.XmlExtractor
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using System.Linq;

    /// <summary>
    /// The Property Reflection class
    /// </summary>
    internal static class ReflectionHelper
    {
        /// <summary>
        /// To check wheather it is of generic list type
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        internal static bool IsGenericListType(Type type)
        {
            return type.IsGenericType && (type.GetGenericTypeDefinition() == typeof(IList<>) || type.GetGenericTypeDefinition() == typeof(List<>));
        }

        /// <summary>
        /// To get name of a attribute
        /// </summary>
        /// <param name="attribute"></param>
        /// <param name="property"></param>
        /// <returns></returns>
        internal static string GetName(NamedAttribute attribute, PropertyInfo property)
        {
            var name = attribute.Name;
            if (string.IsNullOrEmpty(name))
            {
                name = property.Name;
            }
            return name;
        }

        /// <summary>
        /// To get a custom attribute
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="property"></param>
        /// <returns></returns>
        internal static T GetCustomeAttribute<T>(PropertyInfo property) where T : Attribute
        {
            return property.GetCustomAttributes(true).FirstOrDefault(att => att.GetType() == typeof(T)) as T;
        }

        /// <summary>
        /// Is string or value type
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        internal static bool IsStringOrValueType(Type type)
        {
            return type.IsValueType || type == typeof(string);
        }

        /// <summary>
        /// To get the value as string
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="item"></param>
        /// <param name="property"></param>
        /// <returns></returns>
        internal static string GetPropertyValue<T>(T item, PropertyInfo property) where T : class
        {
            if (IsStringOrValueType(property.PropertyType))
            {
                object value = property.GetValue(item);
                if (value != null)
                {
                    return Convert.ToString(value);
                }
            }
            return null;
        }
    }
}
