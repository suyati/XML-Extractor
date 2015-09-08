namespace Suyati.XMLExtractor.Tests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Suyati.XmlExtractor;
    using System;

    /// <summary>
    /// The test class for checking string or value type Element Extraction
    /// </summary>
    [TestClass]
    public class StringOrValueTypeElementXmlConversion
    {
        /// <summary>
        /// The test class for String or value type extraction
        /// </summary>
        private class ValueTypesTestModel
        {
            /// <summary>
            /// The Integer
            /// </summary>
            [Element(Name = "int")]
            public int Int { get; set; }

            /// <summary>
            /// The Date
            /// </summary>
            [Element(Name = "date")]
            public DateTime Date { get; set; }

            /// <summary>
            /// The Double
            /// </summary>
            [Element(Name = "double")]
            public double Double { get; set; }

            /// <summary>
            /// The Float
            /// </summary>
            [Element(Name = "float")]
            public float Float { get; set; }

            /// <summary>
            /// The Charactor
            /// </summary>
            [Element(Name = "char")]
            public char Char { get; set; }

            /// <summary>
            /// The Boolean
            /// </summary>
            [Element(Name = "bool")]
            public bool Bool { get; set; }
        }

        /// <summary>
        /// The test class for Nullable value type extraction
        /// </summary>
        private class NullableValueTypesTestModel
        {
            /// <summary>
            /// The nullable integer
            /// </summary>
            [Element(Name = "int")]
            public int? Int { get; set; }

            /// <summary>
            /// The nullable date
            /// </summary>
            [Element(Name = "date")]
            public DateTime? Date { get; set; }

            /// <summary>
            /// The nullable double
            /// </summary>
            [Element(Name = "double")]
            public double? Double { get; set; }

            /// <summary>
            /// The nullable Float
            /// </summary>
            [Element(Name = "float")]
            public float? Float { get; set; }

            /// <summary>
            /// The nullable char
            /// </summary>
            [Element(Name = "char")]
            public char? Char { get; set; }

            /// <summary>
            /// The nullable bool
            /// </summary>
            [Element(Name = "bool")]
            public bool? Bool { get; set; }

            /// <summary>
            /// The String
            /// </summary>
            [Element(Name = "string")]
            public string String { get; set; }
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
                Bool = true,
                Char = 'c',
                Date = new DateTime(2015, 10, 10, 12, 13, 15),
                Double = 123.4,
                Float = (float)125.6,
                Int = 1000
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
            var model = new ValueTypesTestModel() { };

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
            var model = new NullableValueTypesTestModel()
            {
                Bool = true,
                Char = 'c',
                Date = new DateTime(2015, 10, 10, 12, 13, 15),
                Double = 123.4,
                Float = (float)125.6,
                Int = 1000,
                String = "string"
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
            var model = new NullableValueTypesTestModel();

            // Converting To Xml
            var xml = model.ToXml("test");

            // Checking Result
            Assert.IsNotNull(xml);
            Assert.IsNotNull(xml.DocumentElement);
            Assert.IsNull(xml.DocumentElement["bool"]);
            Assert.IsNull(xml.DocumentElement["int"]);
            Assert.IsNull(xml.DocumentElement["date"]);
            Assert.IsNull(xml.DocumentElement["double"]);
            Assert.IsNull(xml.DocumentElement["float"]);
            Assert.IsNull(xml.DocumentElement["char"]);
            Assert.IsNull(xml.DocumentElement["string"]);
        }
    }
}
