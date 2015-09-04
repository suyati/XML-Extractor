namespace Suyati.XMLExtractor.Tests
{
    using Suyati.XmlExtractor;
    using System;
    
    public class ContentElement
    {
        [Value]
        public string Value { get; set; }
    }

    public class ContentElement<T>
    {
        [Value]
        public T Value { get; set; }
    }

}
