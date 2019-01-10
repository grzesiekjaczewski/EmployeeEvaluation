using EmployeeEvaluation.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace EmployeeEvaluation.Tests.Controllers
{
    [TestClass]
    public class HREmployeesControllerTest
    {
        [TestMethod]
        public void Index()
        {
            // Arrange
            HREmployeesController controller = new HREmployeesController();

            // Act
            var result = controller.Index() as ViewResult;

            //EmployeeEvaluation.Models.PersonalDetail person = (EmployeeEvaluation.Models.PersonalDetail)result.Model;
            //ViewResult result = controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }
    }
}
