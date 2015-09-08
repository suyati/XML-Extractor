using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Suyati.XmlExtractor;

namespace Suyati.XMLExtractor.Tests
{
    [TestClass]
    public class DateTimeFormatTests
    {
        /// <summary>
        /// The DateTiem Format Test
        /// </summary>
        public class DateTimeFormatTestModel
        {
            /// <summary>
            /// The Date
            /// </summary>
            [Element(Name = "date")]
            public DateTime Date { get; set; }

            /// <summary>
            /// The Nullable Date
            /// </summary>
            [Element(Name = "date")]
            public DateTime? NullableDate { get; set; }
        }

        /// <summary>
        /// DateTime Ends with Timezone Abbreviations - IST
        /// </summary>
        [TestMethod]
        public void DateTimeEndsWithTimeZoneAbbreviationsIST_Success()
        {
            // Creating the xml
            string xml = @"<test>
                                <date>Monday, 7 September 2015 20:00:00 IST</date>
                             </test>";

            // Creating the model
            var model = new DateTimeFormatTestModel();

            // Extracting Contents
            model.Extract(xml);

            // Checking Result
            Assert.AreEqual(model.Date, new DateTime(2015, 9, 7, 20, 00, 00));
            Assert.IsTrue(model.NullableDate.HasValue);
            Assert.AreEqual(model.NullableDate.Value, new DateTime(2015, 9, 7, 20, 00, 00));
        }


        /// <summary>
        /// DateTime Ends with Timezone Abbreviations - PDT
        /// </summary>
        [TestMethod]
        public void DateTimeEndsWithTimeZoneAbbreviationsPDT_Success()
        {
            // Creating the xml
            string xml = @"<test>
                                <date>Monday, 7 September 2015 20:00:00 PDT</date>
                             </test>";

            // Creating the model
            var model = new DateTimeFormatTestModel();

            // Extracting Contents
            model.Extract(xml);

            // Checking Result
            Assert.AreEqual(model.Date, new DateTime(2015, 9, 8, 8, 30, 00));
            Assert.IsTrue(model.NullableDate.HasValue);
            Assert.AreEqual(model.NullableDate.Value, new DateTime(2015, 9, 8, 8, 30, 00));
        }

        /// <summary>
        /// DateTime Ends with HHMM TimeZone Format - PDT
        /// </summary>
        [TestMethod]
        public void DateTimeEndsWith_HHMM_Format_TimeZone_PDT_Success()
        {
            // Creating the xml
            string xml = @"<test>
                                <date>Monday, 7 September 2015 20:00:00 -0700</date>
                             </test>";

            // Creating the model
            var model = new DateTimeFormatTestModel();

            // Extracting Contents
            model.Extract(xml);

            // Checking Result
            Assert.AreEqual(model.Date, new DateTime(2015, 9, 8, 8, 30, 00));
            Assert.IsTrue(model.NullableDate.HasValue);
            Assert.AreEqual(model.NullableDate.Value, new DateTime(2015, 9, 8, 8, 30, 00));
        }

        /// <summary>
        /// Format Exception Throws on Invalid Date Format
        /// </summary>
        [ExpectedException(typeof(FormatException))]
        [TestMethod]
        public void ThrowsFormatExceptionOnInvalidDateFormat()
        {
            // Creating the xml
            string xml = @"<test>
                                <date>njsk</date>
                             </test>";

            // Creating the model
            var model = new DateTimeFormatTestModel();

            // Extracting Contents
            model.Extract(xml);
        }

    }
}
