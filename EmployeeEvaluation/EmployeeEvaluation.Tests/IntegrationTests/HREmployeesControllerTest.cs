using EmployeeEvaluation.Controllers;
using EmployeeEvaluation.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace EmployeeEvaluation.Tests.IntegrationTests
{
    [TestClass]
    public class ControllerTests
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        [TestMethod]
        public void CanListHREmployeesIndex()
        {
            // Arrange
            HREmployeesController controller = new HREmployeesController();

            // Act
            var result = controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void CanListHRManagersIndex()
        {
            // Arrange
            HRManagersController controller = new HRManagersController();

            // Act
            var result = controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void CanListHRPositionsIndex()
        {
            // Arrange
            HRPositionsController controller = new HRPositionsController();

            // Act
            var result = controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void CanListHRSurveyIndex()
        {
            // Arrange
            HRSurveyController controller = new HRSurveyController();

            // Act
            var result = controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result);

        }
    }
}
