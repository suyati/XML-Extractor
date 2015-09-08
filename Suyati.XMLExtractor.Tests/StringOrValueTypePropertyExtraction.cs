namespace Suyati.XMLExtractor.Tests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Suyati.XmlExtractor;
    using System;

    /// <summary>
    /// The test class for checking string or value type Property Extraction
    /// </summary>
    [TestClass]
    public class StringOrValueTypePropertyExtraction
    {
        /// <summary>
        /// The test class for String or value type property extraction
        /// </summary>
        private class StringOrValueTypesTestModel
        {
            /// <summary>
            /// The Integer
            /// </summary>
            [Property(Name = "int")]
            public int Int { get; set; }

            /// <summary>
            /// The String
            /// </summary>
            [Property(Name = "string")]
            public string String { get; set; }

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
        /// The test class for Nullable value type property extraction
        /// </summary>
        private class NullableValueTypesTestModel
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
        }

        /// <summary>
        /// The test method to check the success result of extracting String or Value Type Property
        /// </summary>
        [TestMethod]
        public void ExtractStringOrValueTypeProperty_SuccessResult()
        {
            // Creating the xml
            string xml = @"<test
                                int='10'
                                string='test'
                                char='c'
                                date='10/10/2015'
                                double='123.5'
                                float='123.4'
                                bool='true'
                             ></test>";

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
        /// The test method to check the default value of extracting String or Value Type Property if no value exists
        /// </summary>
        [TestMethod]
        public void ExtractStringOrValueTypeProperty_GetDefaultValueIfNoValuePresent()
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
        /// The test method to check the success result of extracting Nullable Value Type Property
        /// </summary>
        [TestMethod]
        public void ExtractNullableValueTypeProperty_SuccessResult()
        {
            // Creating the xml
            string xml = @"<test
                                int='10'
                                string='test'
                                char='c'
                                date='10/10/2015'
                                double='123.5'
                                float='123.4'
                                bool='true'
                             ></test>";
 
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
        /// The test method to check the default value of extracting Nullable Value Type Property if no value exists
        /// </summary>
        [TestMethod]
        public void ExtractNullableValueTypeProperty_GetNullIfNoValuePresent()
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
