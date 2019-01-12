using EmployeeEvaluation.Controllers;
using EmployeeEvaluation.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;


namespace EmployeeEvaluation.Tests.IntegrationTests
{
    [TestClass]
    public class DBTests
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        [TestMethod]
        public void CanOpenConnection()
        {
            SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
            connection.Open();
            var state = connection.State;
            connection.Close();
            Assert.AreEqual(state.ToString(), "Open");
        }

        [TestMethod]
        public void CanListEmployee()
        {
            var employees = db.T_Employees.ToList();
            Assert.IsTrue(employees.Count() > 0);
        }

    }
}
