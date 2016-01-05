namespace Suyati.XMLExtractor.Tests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Suyati.XmlExtractor;
    using System;

    /// <summary>
    /// The test class for checking string or value type Value Extraction
    /// </summary>
    [TestClass]
    public class StringOrValueTypeValueExtraction
    {
        /// <summary>
        /// The test class for String or value type extraction
        /// </summary>
        private class StringOrValueTypesTestModel
        {
            /// <summary>
            /// The Integer
            /// </summary>
            [Element(Name = "int")]
            public ContentElement<int> Int { get; set; }

            /// <summary>
            /// The String
            /// </summary>
            [Element(Name = "string")]
            public ContentElement String { get; set; }

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
        /// The test class for Nullable value type extraction
        /// </summary>
        private class NullableValueTypesTestModel
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
        }

        /// <summary>
        /// The test method to check the success result of extracting String or Value Type
        /// </summary>
        [TestMethod]
        public void ExtractStringOrValueTypeValue_SuccessResult()
        {
            // Creating the xml
            string xml = @"<test>
                                <int>10</int>
                                <string>test</string>
                                <char>c</char>
                                <date>10/10/2015</date>
                                <double>123.5</double>
                                <float>123.4</float>
                                <bool>true</bool>
                             </test>";

            // Creating the model
            var model = new StringOrValueTypesTestModel();

            // Extracting Contents
            model.Extract(xml);

            // Checking Result
            Assert.AreEqual(model.Int.Value, 10);
            Assert.AreEqual(model.String.Value, "test");
            Assert.AreEqual(model.Char.Value, 'c');
            Assert.AreEqual(model.Date.Value, new DateTime(2015, 10, 10));
            Assert.AreEqual(model.Double.Value, (double)123.5);
            Assert.AreEqual(model.Float.Value, (float)123.4);
            Assert.AreEqual(model.Bool.Value, true);
        }

        /// <summary>
        /// The test method to check the default value of extracting String or Value Type if no value exists
        /// </summary>
        [TestMethod]
        public void ExtractStringOrValueTypeValue_GetDefaultValueIfNoValuePresent()
        {
            // Creating the xml
            string xml = @"<test>
                                <int></int>
                                <string></string>
                                <char></char>
                                <date></date>
                                <double></double>
                                <float></float>
                                <bool></bool>
                            </test>";

            // Creating the model
            var model = new StringOrValueTypesTestModel();

            // Extracting Contents
            model.Extract(xml);

            // Checking Result
            Assert.AreEqual(model.Int.Value, default(int));
            Assert.AreEqual(model.String.Value, default(string));
            Assert.AreEqual(model.Char.Value, default(char));
            Assert.AreEqual(model.Date.Value, default(DateTime));
            Assert.AreEqual(model.Double.Value, default(double));
            Assert.AreEqual(model.Float.Value, default(float));
            Assert.AreEqual(model.Bool.Value, default(bool));

        }

        /// <summary>
        /// The test method to check the success result of extracting Nullable Value Type
        /// </summary>
        [TestMethod]
        public void ExtractNullableValueTypeValue_SuccessResult()
        {
            // Creating the xml
            string xml = @"<test>
                                <int>10</int>
                                <string>test</string>
                                <char>c</char>
                                <date>10/10/2015</date>
                                <double>123.5</double>
                                <float>123.4</float>
                                <bool>true</bool>
                             </test>";
 
            // Creating the model
            var model = new NullableValueTypesTestModel();

            // Extracting Contents
            model.Extract(xml);

            // Checking Result
            Assert.AreEqual(model.Int.Value, 10);
            Assert.AreEqual(model.Char.Value, 'c');
            Assert.AreEqual(model.Date.Value, new DateTime(2015, 10, 10));
            Assert.AreEqual(model.Double.Value, (double)123.5);
            Assert.AreEqual(model.Float.Value, (float)123.4);
            Assert.AreEqual(model.Bool.Value, true);

        }

        /// <summary>
        /// The test method to check the default value of extracting Nullable Value Type if no value exists
        /// </summary>
        [TestMethod]
        public void ExtractNullableValueTypeValue_GetNullIfNoValuePresent()
        {
            // Creating the xml
            string xml = @"<test>
                                <int></int>
                                <string></string>
                                <char></char>
                                <date></date>
                                <double></double>
                                <float></float>
                                <bool></bool>
                        </test>";

            // Creating the model
            var model = new NullableValueTypesTestModel();

            // Extracting Contents
            model.Extract(xml);

            // Checking Result
            Assert.AreEqual(model.Int.Value, null);
            Assert.AreEqual(model.Char.Value, null);
            Assert.AreEqual(model.Date.Value, null);
            Assert.AreEqual(model.Double.Value, null);
            Assert.AreEqual(model.Float.Value, null);
            Assert.AreEqual(model.Bool.Value, null);

        }
    }
}
