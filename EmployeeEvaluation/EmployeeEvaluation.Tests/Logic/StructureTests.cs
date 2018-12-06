using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using EmployeeEvaluation.Logic.PrepareView;
using EmployeeEvaluation.Logic;
using EmployeeEvaluation.Models;
using System.Data.Entity;
using Moq;

namespace EmployeeEvaluation.Tests.Logic
{
    [TestClass]
    public class StructureTests
    {

        private ApplicationDbContext db = new ApplicationDbContext();
        string userId = "4ccecfe3-3cc1-4a66-b660-f46a010041a2";

        [TestMethod]
        public void CanLoadStructure()
        {
            arrangeStructure();

            IPrepareView<Structure> prepareView = new PrepareStructureView<Structure>();
            Structure structure = prepareView.GetView(db);

            Assert.AreEqual(structure.Boss.FirstName, "Krzysztof");
        }

        [TestMethod]
        public void CanLoadMGEmployee()
        {
            arrangeMGEmployee();

            IPrepareExtendedView<List<EmployeeExtended>, string> prepareStartSurveyView = new PrepareManagerEmployeeView<List<EmployeeExtended>, string>();
            prepareStartSurveyView.Parameters = userId;
            List<EmployeeExtended> employees = prepareStartSurveyView.GetView(db);

            Assert.AreEqual(employees[0].FirstName, "Krzysztof");
        }

        private void arrangeMGEmployee()
        {
            Position position = new Position()
            {
                Id = 2,
                Name = "Tester",
                Employees = new List<Employee>()
            };

            Position manPos = new Position()
            {
                Id = 3,
                Name = "Kierownik",
                Employees = new List<Employee>()
            };

            Team team = new Team()
            {
                Id = 2,
                Name = "Dział testów",
                ManagerId = 3,
                Employees = new List<Employee>()
            };

            Employee manager = new Employee()
            {
                Id = 3,
                FirstName = "Kaska",
                LastName = "Jaczewska",
                UserId = userId,
                PositionId = 3
            };

            Employee emp = new Employee()
            {
                Id = 2,
                FirstName = "Krzysztof",
                LastName = "Jarzyna",
                PositionId = 2,
                TeamId = 2
            };

            List<Team> t = new List<Team>() { team };
            List<Position> p = new List<Position>() { position, manPos };
            List<Employee> e = new List<Employee>() { emp , manager };

            var mockSetT = SetupMock(t);
            var mockSetP = SetupMock(p);
            var mockSetE = SetupMock(e);

            db.T_Teams = mockSetT.Object;
            db.T_Positions = mockSetP.Object;
            db.T_Employees = mockSetE.Object;

        }

        private void arrangeStructure()
        {
            Position position = new Position()
            {
                Id = 2,
                Name = "CEO",
                Employees = new List<Employee>()
            };

            Team team = new Team()
            {
                Id = 2,
                Name = "Dyrekcja generalna",
                ManagerId = 2,
                Employees = new List<Employee>()
            };

            Employee ceo = new Employee()
            {
                Id = 2,
                FirstName = "Krzysztof",
                LastName = "Jarzyna",
                PositionId = 2,
                TeamId = 2
            };

            position.Employees.Add(ceo);
            team.Employees.Add(ceo);

            List<Team> t = new List<Team>() { team };
            List<Position> p = new List<Position>() { position };
            List<Employee> e = new List<Employee>() { ceo };

            var mockSetT = SetupMock(t);
            var mockSetP = SetupMock(p);
            var mockSetE = SetupMock(e);

            db.T_Teams = mockSetT.Object;
            db.T_Positions = mockSetP.Object;
            db.T_Employees = mockSetE.Object;
        }

        private Mock<DbSet<T>> SetupMock<T>(IList<T> entities) where T : class
        {
            var mockSet = new Mock<DbSet<T>>();
            mockSet.As<IQueryable<T>>().Setup(m => m.Provider).Returns(entities.AsQueryable().Provider);
            mockSet.As<IQueryable<T>>().Setup(m => m.Expression).Returns(entities.AsQueryable().Expression);
            mockSet.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(entities.AsQueryable().ElementType);
            mockSet.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(entities.GetEnumerator());
            mockSet.Setup(m => m.Add(It.IsAny<T>())).Callback<T>((s) => entities.Add(s));
            return mockSet;
        }
    }
}
