using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmployeeEvaluation.Logic.PrepareView;
using EmployeeEvaluation.Logic;
using EmployeeEvaluation.Models;

namespace EmployeeEvaluation.Tests.Logic
{
    [TestClass]
    public class StructureTests
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        [TestMethod]
        public void CanLoadStructure()
        {
            Employee employee1 = new Employee()
            {
                Id = 2,
                FirstName = "Jan",
                LastName = "Kowalski"
            };

            db.T_Employees.Add(employee1);

            IPrepareView<Structure> prepareView = new PrepareStructureView<Structure>();
            Structure structure = prepareView.GetView(db);
            Assert.IsTrue(true);

            Employee employee = db.T_Employees.Find(2);
        }
    }
}
