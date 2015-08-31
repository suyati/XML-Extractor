namespace Suyati.XmlExtractor
{
    using System;

    /// <summary>
    /// The Named Attribute
    /// </summary>
    public abstract class NamedAttribute : Attribute
    {
        /// <summary>
        /// The Name
        /// </summary>
        public string Name { get; set; }
    }

    /// <summary>
    /// The Element Attribute
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, Inherited = false)]
    public class ElementAttribute : NamedAttribute
    {
    }

    /// <summary>
    /// The property Attribute
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, Inherited = false)]
    public class PropertyAttribute : NamedAttribute
    {
    }

    /// <summary>
    /// The Value Attribute
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, Inherited = false)]
    public class ValueAttribute : Attribute
    {
    }
}
