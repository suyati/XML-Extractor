﻿namespace Suyati.XMLExtractor.Tests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Suyati.XmlExtractor;
    using System;

    /// <summary>
    /// The test class for checking string or value type Element Extraction
    /// </summary>
    [TestClass]
    public class StringOrValueTypeElementExtraction
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
            public int Int { get; set; }

            /// <summary>
            /// The String
            /// </summary>
            [Element(Name = "string")]
            public string String { get; set; }

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
        }

        /// <summary>
        /// The test method to check the success result of extracting String or Value Type Elements
        /// </summary>
        [TestMethod]
        public void ExtractStringOrValueTypeElements_SuccessResult()
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
            Assert.AreEqual(model.Int, 10);
            Assert.AreEqual(model.String, "test");
            Assert.AreEqual(model.Char, 'c');
            Assert.AreEqual(model.Date, new DateTime(2015, 10, 10));
            Assert.AreEqual(model.Double, (double)123.5);
            Assert.AreEqual(model.Float, (float)123.4);
            Assert.AreEqual(model.Bool, true);
        }

        /// <summary>
        /// The test method to check the default value of extracting String or Value Type Elements if no value exists
        /// </summary>
        [TestMethod]
        public void ExtractStringOrValueTypeElements_GetDefaultValueIfNoValuePresent()
        {
            // Creating the xml
            string xml = @"<test></test>";

            // Creating the model
            var model = new StringOrValueTypesTestModel();

            // Extracting Contents
            model.Extract(xml);

            // Checking Result
            Assert.AreEqual(model.Int, default(int));
            Assert.AreEqual(model.String, default(string));
            Assert.AreEqual(model.Char, default(char));
            Assert.AreEqual(model.Date, default(DateTime));
            Assert.AreEqual(model.Double, default(double));
            Assert.AreEqual(model.Float, default(float));
            Assert.AreEqual(model.Bool, default(bool));

        }

        /// <summary>
        /// The test method to check the success result of extracting Nullable Value Type Elements
        /// </summary>
        [TestMethod]
        public void ExtractNullableValueTypeElements_SuccessResult()
        {
            // Creating the xml
            string xml = @"<test>
                                <int>10</int>
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
            Assert.AreEqual(model.Int, 10);
            Assert.AreEqual(model.Char, 'c');
            Assert.AreEqual(model.Date, new DateTime(2015, 10, 10));
            Assert.AreEqual(model.Double, (double)123.5);
            Assert.AreEqual(model.Float, (float)123.4);
            Assert.AreEqual(model.Bool, true);

        }

        /// <summary>
        /// The test method to check the default value of extracting Nullable Value Type Elements if no value exists
        /// </summary>
        [TestMethod]
        public void ExtractNullableValueTypeElements_GetNullIfNoValuePresent()
        {
            // Creating the xml
            string xml = @"<test></test>";

            // Creating the model
            var model = new NullableValueTypesTestModel();

            // Extracting Contents
            model.Extract(xml);

            // Checking Result
            Assert.AreEqual(model.Int, null);
            Assert.AreEqual(model.Char, null);
            Assert.AreEqual(model.Date, null);
            Assert.AreEqual(model.Double, null);
            Assert.AreEqual(model.Float, null);
            Assert.AreEqual(model.Bool, null);

        }
    }
}
