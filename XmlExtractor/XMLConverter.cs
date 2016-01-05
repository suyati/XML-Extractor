using System;
using System.Collections;
using System.Reflection;
using System.Xml;
namespace Suyati.XmlExtractor
{
    public static class XMLConverter
    {
        public static XmlDocument ToXml<T>(this T item, string rootName = null)
            // The generic parameter should be a class
    where T : class
        {
            // Getting the object Type
            var type = item.GetType();

            if (string.IsNullOrEmpty(rootName))
            {
                rootName = item.GetType().Name;
            }

            // Creating xmlDocument
            XmlDocument xmlDocument = new XmlDocument();

            // Adding RootNode
            XmlNode rootNode = xmlDocument.CreateElement(rootName);
            xmlDocument.AppendChild(rootNode);

            item.ToXmlRecursive(rootNode, xmlDocument);
            return xmlDocument;

        }

        private static void ToXmlRecursive<T>(this T item, XmlNode node, XmlDocument document)
            // The generic parameter should be a class
            where T : class
        {
            // Getting the object Type
            var type = item.GetType();

            // Getting the properties of the object
            var properties = type.GetProperties();

            // Iterating through each properties
            foreach (var property in properties)
            {
                #region Value Attribute

                // Checking wheather the Value Attribute present
                ValueAttribute valueAttribute = ReflectionHelper.GetCustomeAttribute<ValueAttribute>(property);

                // If present
                if (valueAttribute != null)
                {
                    AddValueToNode(item, property, node);
                    continue;
                }

                #endregion

                #region Element Attribute

                // Checking wheather there is any Element Attribute present
                ElementAttribute elementAttribute = ReflectionHelper.GetCustomeAttribute<ElementAttribute>(property);

                // If present
                if (elementAttribute != null)
                {
                    // Getting the name of node 
                    var name = ReflectionHelper.GetName(elementAttribute, property);

                    AddElementToNode(item, property, node, name, document);

                    continue;
                }

                #endregion

                #region Property Attribute

                // Getting the property attribute
                PropertyAttribute propertyAttribute = ReflectionHelper.GetCustomeAttribute<PropertyAttribute>(property);
                if (propertyAttribute != null)
                {
                    var name = ReflectionHelper.GetName(propertyAttribute, property);
                    AddPropertyToNode(item, property, node, name, document);

                    continue;
                }

                #endregion
            }
        }

        private static void AddPropertyToNode<T>(T item, PropertyInfo property, XmlNode node, string name, XmlDocument document) where T : class
        {
            if (ReflectionHelper.IsStringOrValueType(property.PropertyType) && node.Attributes[name] == null)
            {
                var value = ReflectionHelper.GetPropertyValue(item, property);
                if (value != null)
                {
                    var attribute = document.CreateAttribute(name);
                    attribute.Value = value;
                    node.Attributes.Append(attribute);
                }
            }
        }

        private static void AddElementToNode<T>(T item, PropertyInfo property, XmlNode node, string name, XmlDocument document) where T : class
        {
            // Getting the property Type
            var propertyType = property.PropertyType;

            // Checking wheather it is of Generic List Type (IList<T>/List<T>)
            if (ReflectionHelper.IsGenericListType(propertyType))
            {
                IList list = (IList)property.GetValue(item);
                if (list != null && list.Count > 0)
                {
                    foreach (var element in list)
                    {
                        var childNode = document.CreateElement(name);
                        element.ToXmlRecursive(childNode, document);
                        node.AppendChild(childNode);
                    }
                }
            }
            else if (ReflectionHelper.IsStringOrValueType(propertyType))
            {
                var element = ReflectionHelper.GetPropertyValue(item, property);
                if (element != null)
                {
                    var childNode = document.CreateElement(name);
                    childNode.InnerText = element;
                    node.AppendChild(childNode);
                }
            }
            else if (propertyType.IsClass)
            {
                var element = property.GetValue(item);
                if (element != null)
                {
                    var childNode = document.CreateElement(name);
                    element.ToXmlRecursive(childNode, document);
                    node.AppendChild(childNode);
                }
            }
        }

        private static void AddValueToNode<T>(T item, PropertyInfo property, XmlNode node) where T : class
        {
            var value = ReflectionHelper.GetPropertyValue(item, property);
            if (value != null)
            {
                node.InnerText = value;
            }
        }


    }
}
