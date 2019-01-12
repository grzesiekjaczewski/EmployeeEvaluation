using EmployeeEvaluation.Logic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeEvaluation.Tests.UnitTests
{
    [TestClass]
    public class LogicTests
    {
        [TestMethod]
        public void CanConvertToNumberFromValidString()
        {
            var number = StringToValue.ParseInt("123");
            Assert.AreEqual(number, 123);
            number = StringToValue.ParseInt("-12322223");
            Assert.AreEqual(number, -12322223);
        }

        [TestMethod]
        public void CanNotConvertToNumberFromInvalidString()
        {
            var number = StringToValue.ParseInt("123A");
            Assert.AreEqual(number, 0);
        }

        [TestMethod]
        public void CanCalculateDateFromString()
        {
            var date = CalculateDate.StringToDate("31.01.2016", ".");
            Assert.AreEqual(date.Year, 2016);
            Assert.AreEqual(date.Month, 1);
            Assert.AreEqual(date.Day, 31);

        }

        [TestMethod]
        public void CanStandariseAndCalculateDateFromString()
        {
            var date = CalculateDate.StringToDate("31-01/2016", ".", "/", "-");
            Assert.AreEqual(date.Year, 2016);
            Assert.AreEqual(date.Month, 1);
            Assert.AreEqual(date.Day, 31);
        }
    }
}
