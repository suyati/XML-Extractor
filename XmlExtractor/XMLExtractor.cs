namespace Suyati.XmlExtractor
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Xml;

    /// <summary>
    /// The XmlExtractor
    /// </summary>
    public static class XMLExtractor
    {
        #region Public Methods

        /// <summary>
        /// To extract the object from a XmlNode
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="item"></param>
        /// <param name="xmlNode"></param>
        public static void Extract<T>(this T item, XmlNode xmlNode)
            // The generic parameter should be a class
            where T : class
        {
            // checking whether the xml node present
            if (xmlNode != null)
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
                    ValueAttribute valueAttribute = GetCustomeAttribute<ValueAttribute>(property);

                    // If present
                    if (valueAttribute != null)
                    {
                        // Extracting value from Node
                        ExtractValueFromNode(item, xmlNode, property);

                        continue;
                    }

                    #endregion

                    #region Element Attribute

                    // Checking wheather there is any Element Attribute present
                    ElementAttribute elementAttribute = GetCustomeAttribute<ElementAttribute>(property);

                    // If present
                    if (elementAttribute != null)
                    {
                        // Getting the name of node 
                        var name = GetName(elementAttribute, property);

                        // Extracting Elemnt from the Node
                        ExtractElementFromNode(item, xmlNode, name, property);

                        continue;
                    }

                    #endregion

                    #region Property Attribute

                    // Getting the property attribute
                    PropertyAttribute propertyAttribute = GetCustomeAttribute<PropertyAttribute>(property);
                    if (propertyAttribute != null)
                    {
                        // Getting the name of property
                        var name = GetName(propertyAttribute, property);

                        // Extract propertry from Node
                        ExtractPropertyFromNode(item, xmlNode, name, property);

                        continue;
                    }

                    #endregion
                }
            }
        }

        /// <summary>
        /// To extract the object from a xml string
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="item"></param>
        /// <param name="xml"></param>
        public static void Extract<T>(this T item, string xml) where T : class
        {
            var xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(xml);
            item.Extract(xmlDocument.DocumentElement);
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// To extract a property from Node
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="item"></param>
        /// <param name="xmlNode"></param>
        /// <param name="name"></param>
        /// <param name="property"></param>
        private static void ExtractPropertyFromNode<T>(T item, XmlNode xmlNode, string name, PropertyInfo property) where T : class
        {
            var propertyType = property.PropertyType;
            if (IsStringOrValueType(propertyType))
            {
                var attribute = xmlNode.Attributes[name];
                if (attribute != null)
                {
                    SetStringOrValueTypeProperty(item, property, attribute.Value);
                }
            }
        }

        /// <summary>
        /// To Extract value from Node
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="item"></param>
        /// <param name="xmlNode"></param>
        /// <param name="property"></param>
        private static void ExtractValueFromNode<T>(T item, XmlNode xmlNode, PropertyInfo property) where T : class
        {
            var propertyType = property.PropertyType;
            // Checking whether the type is string or Value Type
            if (IsStringOrValueType(propertyType))
            {
                // If so, setting the property
                SetStringOrValueTypeProperty(item, property, xmlNode.InnerText);
            }
        }

        /// <summary>
        /// Extracting Elemnt from Node
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="item"></param>
        /// <param name="xmlNode"></param>
        /// <param name="name"></param>
        /// <param name="property"></param>
        private static void ExtractElementFromNode<T>(T item, XmlNode xmlNode, string name, PropertyInfo property) where T : class
        {
            // Getting the property Type
            var propertyType = property.PropertyType;

            // Checking wheather it is of Generic List Type (IList<T>/List<T>)
            if (IsGenericListType(propertyType))
            {
                // Getting the generic arguments
                var genericArguments = propertyType.GetGenericArguments();

                // Checking whether the generic list argument exist
                if (genericArguments != null && genericArguments.Length > 0)
                {
                    // Creating a new List of The Generic Type
                    var constructedListType = typeof(List<>).MakeGenericType(genericArguments[0]);
                    IList list = (IList)Activator.CreateInstance(constructedListType);

                    // Getting the first node with the name
                    var nestedNode = xmlNode[name];

                    // If any node with name exists
                    if (nestedNode != null)
                    {
                        // Adding items to the list
                        ExtractListOfNodes(list, nestedNode, name, genericArguments[0]);

                        // Setting the list to the object property
                        property.SetValue(item, list);
                    }
                }

            }
            else if (IsStringOrValueType(propertyType))
            {
                var nestedNode = xmlNode[name];
                if (nestedNode != null)
                {
                    SetStringOrValueTypeProperty(item, property, nestedNode.InnerText);
                }
            }
            else if (propertyType.IsClass)
            {
                var nestedNode = xmlNode[name];
                if (nestedNode != null)
                {
                    // Extracting the inner object from nested node
                    var content = GetObjectPropertyValue(item, property);
                    content.Extract(nestedNode);
                    property.SetValue(item, content);
                }
            }
        }

        /// <summary>
        /// To extract list of nodes from the Xml Node
        /// </summary>
        /// <param name="list"></param>
        /// <param name="node"></param>
        /// <param name="name"></param>
        /// <param name="type"></param>
        private static void ExtractListOfNodes(IList list, XmlNode node, string name, Type type)
        {
            // Looping through the nodes
            while (node != null)
            {
                // Matching the name of the node 
                if (node.Name == name)
                {
                    // If premitive data type
                    if (IsStringOrValueType(type))
                    {
                        // Getting the value from inner Text
                        var content = GetStringOrValueTypeSafeValue(type, node.InnerText);

                        // Adding to the List
                        list.Add(content);
                    }
                    // If class (not string)
                    else if (type.IsClass)
                    {
                        // Creating the instance of the class
                        var content = Activator.CreateInstance(type);

                        // Extracting the internal content to the object
                        content.Extract(node);

                        // Adding to the List
                        list.Add(content);
                    }
                }

                // Iterating to the next Sibling
                node = node.NextSibling;
            }
        }

        /// <summary>
        /// To check wheather it is of generic list type
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        private static bool IsGenericListType(Type type)
        {
            return type.IsGenericType && (type.GetGenericTypeDefinition() == typeof(IList<>) || type.GetGenericTypeDefinition() == typeof(List<>));
        }

        /// <summary>
        /// To get name of a attribute
        /// </summary>
        /// <param name="attribute"></param>
        /// <param name="property"></param>
        /// <returns></returns>
        private static string GetName(NamedAttribute attribute, PropertyInfo property)
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
        private static T GetCustomeAttribute<T>(PropertyInfo property) where T : Attribute
        {
            return property.GetCustomAttributes(true).FirstOrDefault(att => att.GetType() == typeof(T)) as T;
        }

        /// <summary>
        /// Is string or value type
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        private static bool IsStringOrValueType(Type type)
        {
            return type.IsValueType || type == typeof(string);
        }

        /// <summary>
        /// To set string or value type
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="item"></param>
        /// <param name="property"></param>
        /// <param name="value"></param>
        private static void SetStringOrValueTypeProperty<T>(T item, PropertyInfo property, object value) where T : class
        {
            // Getting the safe Value
            var safeValue = GetStringOrValueTypeSafeValue(property.PropertyType, value);

            // Setting The value
            property.SetValue(item, safeValue);
        }

        /// <summary>
        /// To get the safe Value fro a Nullable/Non-Nullable Value Type
        /// </summary>
        /// <param name="property"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        private static object GetStringOrValueTypeSafeValue(Type type, object value)
        {
            // Getting the underlying Type if Nullable
            type = Nullable.GetUnderlyingType(type) ?? type;

            // returning the safe value
            return (value == null) ? null : Convert.ChangeType(value, type);

        }

        /// <summary>
        /// To get Object from the entity of a class type
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="item"></param>
        /// <returns></returns>
        private static object GetObjectPropertyValue<T>(T item, PropertyInfo property) where T : class
        {
            var value = property.GetValue(item);
            if (value == null)
            {
                return Activator.CreateInstance(property.PropertyType);
            }
            return value;
        }
        #endregion
    }
}
