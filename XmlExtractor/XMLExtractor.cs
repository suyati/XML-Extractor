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
                    ValueAttribute valueAttribute = ReflectionHelper.GetCustomeAttribute<ValueAttribute>(property);

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
                    ElementAttribute elementAttribute = ReflectionHelper.GetCustomeAttribute<ElementAttribute>(property);

                    // If present
                    if (elementAttribute != null)
                    {
                        // Getting the name of node 
                        var name = ReflectionHelper.GetName(elementAttribute, property);

                        // Extracting Elemnt from the Node
                        ExtractElementFromNode(item, xmlNode, name, property);

                        continue;
                    }

                    #endregion

                    #region Property Attribute

                    // Getting the property attribute
                    PropertyAttribute propertyAttribute = ReflectionHelper.GetCustomeAttribute<PropertyAttribute>(property);
                    if (propertyAttribute != null)
                    {
                        // Getting the name of property
                        var name = ReflectionHelper.GetName(propertyAttribute, property);

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
            if (ReflectionHelper.IsStringOrValueType(propertyType))
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
            if (ReflectionHelper.IsStringOrValueType(propertyType))
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
            if (ReflectionHelper.IsGenericListType(propertyType))
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
            else if (ReflectionHelper.IsStringOrValueType(propertyType))
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
                    if (ReflectionHelper.IsStringOrValueType(type))
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
        /// To set string or value type
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="item"></param>
        /// <param name="property"></param>
        /// <param name="value"></param>
        private static void SetStringOrValueTypeProperty<T>(T item, PropertyInfo property, string value) where T : class
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
        private static object GetStringOrValueTypeSafeValue(Type type, string value)
        {
            // returning default value if string is empty or null
            if (string.IsNullOrEmpty(value))
            {
                return GetDefault(type);
            }

            // Getting the underlying Type if Nullable
            type = Nullable.GetUnderlyingType(type) ?? type;

            // parsing dateTime
            if (type == typeof(DateTime) && value is string)
            {
                try
                {
                    return Convert.ChangeType(value, type);
                }
                // Exception may arise if the date format ends with Timezone abbreviations (like EST, IST, etc..)
                catch (FormatException)
                {
                    bool success = false;

                    // Replacing the timezone off set in defautl format
                    value = TimezoneHelper.ReplaceOffSetToDefaultFormat(value, out success);

                    if (!success)
                    {
                        // Replacing the abbreviation with corresponding Timezone Offset
                        value = TimezoneHelper.ReplaceTimezoneAbbreviation((string)value);
                    }
                }
            }
            return Convert.ChangeType(value, type);

        }

        /// <summary>
        /// To get the default value of a type
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static object GetDefault(Type type)
        {
            // if Value Type
            if (type.IsValueType)
            {
                // If not nullable
                if (Nullable.GetUnderlyingType(type) == null)
                {
                    return Activator.CreateInstance(type);
                }
            }
            return null;
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
                // Checking for default constructor
                var constructor = property.PropertyType.GetConstructor(Type.EmptyTypes);
                if (constructor == null)
                {
                    throw new MissingMethodException("No parameterless constructor found for type " + property.PropertyType.Name);
                }
                return Activator.CreateInstance(property.PropertyType);
            }
            return value;
        }
        #endregion
    }
}
