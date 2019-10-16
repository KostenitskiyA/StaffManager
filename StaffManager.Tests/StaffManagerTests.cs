using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace StaffManager.Tests
{
    [TestClass]
    public class StaffManagerTests
    {
        [TestMethod]
        public void Allowance_Manager_()
        {
            // arrange
            string workGroup = "Manager";
            DateTime startDate = new DateTime(2000, 1, 1);
            DateTime selectedDate = new DateTime(2005, 1, 1);

            double expected = Math.Pow(1.05, 5);

            // act
            Model.MainModel model = new Model.MainModel();

            double actual = model.Allowance(workGroup, startDate, selectedDate);

            // assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void MultiplierCalculate()
        {
            // arrange
            string workGroup = "Manager";

            double expected = 1.05d;

            // act
            Model.MainModel model = new Model.MainModel();

            double actual = model.MultiplierCalculate(workGroup);

            // assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TimeCalculate()
        {
            // arrange
            DateTime startDate = new DateTime(2000, 1, 1);
            DateTime selectedDate = new DateTime(2010, 1, 1);

            double expected = 10;

            // act
            Model.MainModel model = new Model.MainModel();

            double actual = model.TimeCalculate(startDate, selectedDate);

            // assert
            Assert.AreEqual(expected, actual);
        }
    }
}
