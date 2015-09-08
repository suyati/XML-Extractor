namespace Suyati.XMLExtractor.Tests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Suyati.XmlExtractor;
    using System;

    /// <summary>
    /// The test class for checking string or value type Value Extraction
    /// </summary>
    [TestClass]
    public class StringOrValueTypeValueConversion
    {
        /// <summary>
        /// The test class for value type extraction
        /// </summary>
        private class ValueTypesTestModel
        {
            /// <summary>
            /// The Integer
            /// </summary>
            [Element(Name = "int")]
            public ContentElement<int> Int { get; set; }

            /// <summary>
            /// The Date
            /// </summary>
            [Element(Name = "date")]
            public ContentElement<DateTime> Date { get; set; }

            /// <summary>
            /// The Double
            /// </summary>
            [Element(Name = "double")]
            public ContentElement<double> Double { get; set; }

            /// <summary>
            /// The Float
            /// </summary>
            [Element(Name = "float")]
            public ContentElement<float> Float { get; set; }

            /// <summary>
            /// The Charactor
            /// </summary>
            [Element(Name = "char")]
            public ContentElement<char> Char { get; set; }

            /// <summary>
            /// The Boolean
            /// </summary>
            [Element(Name = "bool")]
            public ContentElement<bool> Bool { get; set; }
        }

        /// <summary>
        /// The test class for StringOrNullable value type extraction
        /// </summary>
        private class StringOrNullableValueTypesTestModel
        {
            /// <summary>
            /// The Integer
            /// </summary>
            [Element(Name = "int")]
            public ContentElement<int?> Int { get; set; }

            /// <summary>
            /// The Date
            /// </summary>
            [Element(Name = "date")]
            public ContentElement<DateTime?> Date { get; set; }

            /// <summary>
            /// The Double
            /// </summary>
            [Element(Name = "double")]
            public ContentElement<double?> Double { get; set; }

            /// <summary>
            /// The Float
            /// </summary>
            [Element(Name = "float")]
            public ContentElement<float?> Float { get; set; }

            /// <summary>
            /// The Charactor
            /// </summary>
            [Element(Name = "char")]
            public ContentElement<char?> Char { get; set; }

            /// <summary>
            /// The Boolean
            /// </summary>
            [Element(Name = "bool")]
            public ContentElement<bool?> Bool { get; set; }

            /// <summary>
            /// The String
            /// </summary>
            [Element(Name = "string")]
            public ContentElement String { get; set; }
        }

        /// <summary>
        /// To convert ValueType Elements To XML Node
        /// </summary>
        [TestMethod]
        public void ValueTypeElementsToXML_SuccessResult()
        {
            // Creating the model
            var model = new ValueTypesTestModel()
            {
                Bool = new ContentElement<bool>() { Value = true },
                Char = new ContentElement<char>() { Value = 'c' },
                Date = new ContentElement<DateTime>() { Value = new DateTime(2015, 10, 10, 12, 13, 15) },
                Double = new ContentElement<double>() { Value = 123.4 },
                Float = new ContentElement<float>() { Value = (float)125.6 },
                Int = new ContentElement<int>() { Value = 1000 }
            };

            // Extracting Contents
            var xml = model.ToXml("test");

            // Checking Result
            Assert.IsNotNull(xml);
            Assert.IsNotNull(xml.DocumentElement);
            Assert.AreEqual(xml.DocumentElement["bool"].InnerText, Convert.ToString(true));
            Assert.AreEqual(xml.DocumentElement["int"].InnerText, Convert.ToString(1000));
            Assert.AreEqual(xml.DocumentElement["date"].InnerText, Convert.ToString(new DateTime(2015, 10, 10, 12, 13, 15)));
            Assert.AreEqual(xml.DocumentElement["double"].InnerText, Convert.ToString(123.4));
            Assert.AreEqual(xml.DocumentElement["float"].InnerText, Convert.ToString((float)125.6));
            Assert.AreEqual(xml.DocumentElement["char"].InnerText, Convert.ToString('c'));
        }

        /// <summary>
        /// To convert ValueType Elements To XML Node
        /// </summary>
        [TestMethod]
        public void ValueTypeElementsToXML_DefaultValueIfNoValuePresent()
        {
            // Creating the model
            var model = new ValueTypesTestModel()
            {
                Bool = new ContentElement<bool>(),
                Char = new ContentElement<char>(),
                Date = new ContentElement<DateTime>(),
                Double = new ContentElement<double>(),
                Float = new ContentElement<float>(),
                Int = new ContentElement<int>()
            };

            // Extracting Contents
            var xml = model.ToXml("test");

            // Checking Result
            Assert.IsNotNull(xml);
            Assert.IsNotNull(xml.DocumentElement);
            Assert.AreEqual(xml.DocumentElement["bool"].InnerText, Convert.ToString(default(bool)));
            Assert.AreEqual(xml.DocumentElement["int"].InnerText, Convert.ToString(default(int)));
            Assert.AreEqual(xml.DocumentElement["date"].InnerText, Convert.ToString(default(DateTime)));
            Assert.AreEqual(xml.DocumentElement["double"].InnerText, Convert.ToString(default(double)));
            Assert.AreEqual(xml.DocumentElement["float"].InnerText, Convert.ToString(default(float)));
            Assert.AreEqual(xml.DocumentElement["char"].InnerText, Convert.ToString(default(char)));
        }

        /// <summary>
        /// To convert String or Nullable ValueType Elements To XML Node
        /// </summary>
        [TestMethod]
        public void StringOrNullableValueTypeElementsToXML_SuccessResult()
        {
            // Creating the model
            var model = new StringOrNullableValueTypesTestModel()
            {
                Bool = new ContentElement<bool?>() { Value = true },
                Char = new ContentElement<char?>() { Value = 'c' },
                Date = new ContentElement<DateTime?>() { Value = new DateTime(2015, 10, 10, 12, 13, 15) },
                Double = new ContentElement<double?>() { Value = 123.4 },
                Float = new ContentElement<float?>() { Value = (float)125.6 },
                Int = new ContentElement<int?>() { Value = 1000 },
                String = new ContentElement() { Value = "string" }
            };

            // Converting To Xml
            var xml = model.ToXml("test");

            // Checking Result
            Assert.IsNotNull(xml);
            Assert.IsNotNull(xml.DocumentElement);
            Assert.AreEqual(xml.DocumentElement["bool"].InnerText, Convert.ToString(true));
            Assert.AreEqual(xml.DocumentElement["int"].InnerText, Convert.ToString(1000));
            Assert.AreEqual(xml.DocumentElement["date"].InnerText, Convert.ToString(new DateTime(2015, 10, 10, 12, 13, 15)));
            Assert.AreEqual(xml.DocumentElement["double"].InnerText, Convert.ToString(123.4));
            Assert.AreEqual(xml.DocumentElement["float"].InnerText, Convert.ToString((float)125.6));
            Assert.AreEqual(xml.DocumentElement["string"].InnerText, "string");
            Assert.AreEqual(xml.DocumentElement["char"].InnerText, Convert.ToString('c'));
        }

        /// <summary>
        /// To convert String or Nullable ValueType Elements To XML Node
        /// </summary>
        [TestMethod]
        public void StringOrNullableValueTypeElementsToXML_GetNullIfNoValuePresent()
        {
            // Creating the model
            var model = new StringOrNullableValueTypesTestModel()
            {
                Bool = new ContentElement<bool?>(),
                Char = new ContentElement<char?>(),
                Date = new ContentElement<DateTime?>(),
                Double = new ContentElement<double?>(),
                Float = new ContentElement<float?>(),
                Int = new ContentElement<int?>(),
                String = new ContentElement()
            };

            // Converting To Xml
            var xml = model.ToXml("test");

            // Checking Result
            Assert.IsNotNull(xml);
            Assert.IsNotNull(xml.DocumentElement);
            Assert.AreEqual(xml.DocumentElement["bool"].InnerText, string.Empty);
            Assert.AreEqual(xml.DocumentElement["int"].InnerText, string.Empty);
            Assert.AreEqual(xml.DocumentElement["date"].InnerText, string.Empty);
            Assert.AreEqual(xml.DocumentElement["double"].InnerText, string.Empty);
            Assert.AreEqual(xml.DocumentElement["float"].InnerText, string.Empty);
            Assert.AreEqual(xml.DocumentElement["char"].InnerText, string.Empty);
            Assert.AreEqual(xml.DocumentElement["string"].InnerText, string.Empty);
        }
    }
}
