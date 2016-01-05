namespace Suyati.XMLExtractor.Tests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Suyati.XmlExtractor;
    using System;

    /// <summary>
    /// The test class for checking string or value type Property Extraction
    /// </summary>
    [TestClass]
    public class StringOrValueTypePropertyConversion
    {
        /// <summary>
        /// The test class for value type property extraction
        /// </summary>
        private class ValueTypesTestModel
        {
            /// <summary>
            /// The Integer
            /// </summary>
            [Property(Name = "int")]
            public int Int { get; set; }

            /// <summary>
            /// The Date
            /// </summary>
            [Property(Name = "date")]
            public DateTime Date { get; set; }

            /// <summary>
            /// The Double
            /// </summary>
            [Property(Name = "double")]
            public double Double { get; set; }

            /// <summary>
            /// The Float
            /// </summary>
            [Property(Name = "float")]
            public float Float { get; set; }

            /// <summary>
            /// The Charactor
            /// </summary>
            [Property(Name = "char")]
            public char Char { get; set; }

            /// <summary>
            /// The Boolean
            /// </summary>
            [Property(Name = "bool")]
            public bool Bool { get; set; }
        }

        /// <summary>
        /// The test class for String or Nullable value type property extraction
        /// </summary>
        private class StringOrNullableValueTypesTestModel
        {
            /// <summary>
            /// The nullable integer
            /// </summary>
            [Property(Name = "int")]
            public int? Int { get; set; }

            /// <summary>
            /// The nullable date
            /// </summary>
            [Property(Name = "date")]
            public DateTime? Date { get; set; }

            /// <summary>
            /// The nullable double
            /// </summary>
            [Property(Name = "double")]
            public double? Double { get; set; }

            /// <summary>
            /// The nullable Float
            /// </summary>
            [Property(Name = "float")]
            public float? Float { get; set; }

            /// <summary>
            /// The nullable char
            /// </summary>
            [Property(Name = "char")]
            public char? Char { get; set; }

            /// <summary>
            /// The nullable bool
            /// </summary>
            [Property(Name = "bool")]
            public bool? Bool { get; set; }

            /// <summary>
            /// The String
            /// </summary>
            [Property(Name = "string")]
            public string String { get; set; }
        }

        /// <summary>
        /// To convert ValueType Properties To XML Node
        /// </summary>
        [TestMethod]
        public void ValueTypePropertiesToXML_SuccessResult()
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
            var xml = model.ToXml();

            // Checking Result
            Assert.IsNotNull(xml);
            Assert.IsNotNull(xml.DocumentElement);
            Assert.AreEqual(xml.DocumentElement.Attributes["bool"].Value, Convert.ToString(true));
            Assert.AreEqual(xml.DocumentElement.Attributes["int"].Value, Convert.ToString(1000));
            Assert.AreEqual(xml.DocumentElement.Attributes["date"].Value, Convert.ToString(new DateTime(2015, 10, 10, 12, 13, 15)));
            Assert.AreEqual(xml.DocumentElement.Attributes["double"].Value, Convert.ToString(123.4));
            Assert.AreEqual(xml.DocumentElement.Attributes["float"].Value, Convert.ToString((float)125.6));
            Assert.AreEqual(xml.DocumentElement.Attributes["char"].Value, Convert.ToString('c'));
        }

        /// <summary>
        /// To convert ValueType Properties To XML Node
        /// </summary>
        [TestMethod]
        public void ValueTypePropertiesToXML_DefaultValueIfNoValuePresent()
        {
            // Creating the model
            var model = new ValueTypesTestModel() { };

            // Extracting Contents
            var xml = model.ToXml();

            // Checking Result
            Assert.IsNotNull(xml);
            Assert.IsNotNull(xml.DocumentElement);
            Assert.AreEqual(xml.DocumentElement.Attributes["bool"].Value, Convert.ToString(default(bool)));
            Assert.AreEqual(xml.DocumentElement.Attributes["int"].Value, Convert.ToString(default(int)));
            Assert.AreEqual(xml.DocumentElement.Attributes["date"].Value, Convert.ToString(default(DateTime)));
            Assert.AreEqual(xml.DocumentElement.Attributes["double"].Value, Convert.ToString(default(double)));
            Assert.AreEqual(xml.DocumentElement.Attributes["float"].Value, Convert.ToString(default(float)));
            Assert.AreEqual(xml.DocumentElement.Attributes["char"].Value, Convert.ToString(default(char)));
        }

        /// <summary>
        /// To convert String or Nullable ValueType Properties To XML Node
        /// </summary>
        [TestMethod]
        public void StringOrNullableValueTypePropertiesToXML_SuccessResult()
        {
            // Creating the model
            var model = new StringOrNullableValueTypesTestModel()
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
            var xml = model.ToXml();

            // Checking Result
            Assert.IsNotNull(xml);
            Assert.IsNotNull(xml.DocumentElement);
            Assert.AreEqual(xml.DocumentElement.Attributes["bool"].Value, Convert.ToString(true));
            Assert.AreEqual(xml.DocumentElement.Attributes["int"].Value, Convert.ToString(1000));
            Assert.AreEqual(xml.DocumentElement.Attributes["date"].Value, Convert.ToString(new DateTime(2015, 10, 10, 12, 13, 15)));
            Assert.AreEqual(xml.DocumentElement.Attributes["double"].Value, Convert.ToString(123.4));
            Assert.AreEqual(xml.DocumentElement.Attributes["float"].Value, Convert.ToString((float)125.6));
            Assert.AreEqual(xml.DocumentElement.Attributes["string"].Value, "string");
            Assert.AreEqual(xml.DocumentElement.Attributes["char"].Value, Convert.ToString('c'));
        }

        /// <summary>
        /// To convert String or Nullable ValueType Properties To XML Node
        /// </summary>
        [TestMethod]
        public void StringOrNullableValueTypePropertiesToXML_GetNullIfNoValuePresent()
        {
            // Creating the model
            var model = new StringOrNullableValueTypesTestModel();

            // Converting To Xml
            var xml = model.ToXml();

            // Checking Result
            Assert.IsNotNull(xml);
            Assert.IsNotNull(xml.DocumentElement);
            Assert.IsNull(xml.DocumentElement.Attributes["bool"]);
            Assert.IsNull(xml.DocumentElement.Attributes["int"]);
            Assert.IsNull(xml.DocumentElement.Attributes["date"]);
            Assert.IsNull(xml.DocumentElement.Attributes["double"]);
            Assert.IsNull(xml.DocumentElement.Attributes["float"]);
            Assert.IsNull(xml.DocumentElement.Attributes["char"]);
            Assert.IsNull(xml.DocumentElement.Attributes["string"]);
        }
    }
}
