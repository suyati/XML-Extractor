namespace Suyati.XmlExtractor
{
    using System;

    /// <summary>
    /// The Named Attribute
    /// </summary>
    public abstract class NamedAttribute : Attribute
    {
        /// <summary>
        /// The Name of the propety
        /// </summary>
        public string Name { get; set; }
    }

    /// <summary>
    /// The Element Attribute
    /// To map the sub nodes
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, Inherited = false)]
    public class ElementAttribute : NamedAttribute
    {
    }

    /// <summary>
    /// The property Attribute
    /// To map the attributes
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, Inherited = false)]
    public class PropertyAttribute : NamedAttribute
    {
    }

    /// <summary>
    /// The Value Attribute
    /// To map the inner value
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, Inherited = false)]
    public class ValueAttribute : Attribute
    {
    }
}
