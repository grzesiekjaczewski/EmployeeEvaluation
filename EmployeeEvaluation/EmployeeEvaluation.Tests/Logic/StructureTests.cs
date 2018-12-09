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
            Assert.AreEqual(structure.Boss.Teams.Count(), 1);
            Assert.AreEqual(structure.Boss.Teams[0].Name, "Dyrekcja generalna");
            Assert.AreEqual(structure.Boss.Teams[0].Persons.Count(), 2);
            Assert.AreEqual(structure.Boss.Teams[0].Persons[0].Teams[0].Persons[0].LastName, "Pierwszy Bryg");
            Assert.AreEqual(structure.Boss.Teams[0].Persons[0].Teams[0].Persons[0].Teams[0].Persons[0].LastName, "Czwarty");

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
            Position position1 = new Position()
            {
                Id = 2,
                Name = "CEO",
                Employees = new List<Employee>()
            };

            Position position2 = new Position()
            {
                Id = 3,
                Name = "Menadżer",
                Employees = new List<Employee>()
            };

            Position position3 = new Position()
            {
                Id = 4,
                Name = "Pracownik",
                Employees = new List<Employee>()
            };

            Position position4 = new Position()
            {
                Id = 5,
                Name = "Brygadzista",
                Employees = new List<Employee>()
            };


            Team team1 = new Team()
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

            Employee p1 = new Employee()
            {
                Id = 3,
                FirstName = "Menadżer",
                LastName = "Pierwszy",
                PositionId = 3,
                TeamId = 2
            };

            Team team2 = new Team()
            {
                Id = 3,
                Name = "Team 1",
                ManagerId = 3,
                Employees = new List<Employee>()
            };

            Employee p3 = new Employee()
            {
                Id = 5,
                FirstName = "Pracownik",
                LastName = "Pierwszy Bryg",
                PositionId = 5,
                TeamId = 3
            };

            Team team4 = new Team()
            {
                Id = 5,
                Name = "Team 1 2",
                ManagerId = 5,
                Employees = new List<Employee>()
            };

            Employee p6 = new Employee()
            {
                Id = 8,
                FirstName = "Pracownik",
                LastName = "Czwarty",
                PositionId = 4,
                TeamId = 5
            };

            Employee p7 = new Employee()
            {
                Id = 9,
                FirstName = "Pracownik",
                LastName = "Piąty",
                PositionId = 4,
                TeamId = 5
            };



            Employee p2 = new Employee()
            {
                Id = 4,
                FirstName = "Menadżer",
                LastName = "Drugi",
                PositionId = 3,
                TeamId = 2
            };

            Team team3 = new Team()
            {
                Id = 4,
                Name = "Team 2",
                ManagerId = 4,
                Employees = new List<Employee>()
            };

            Employee p4 = new Employee()
            {
                Id = 6,
                FirstName = "Pracownik",
                LastName = "Drugi",
                PositionId = 4,
                TeamId = 4
            };

            Employee p5 = new Employee()
            {
                Id = 7,
                FirstName = "Pracownik",
                LastName = "trzeci",
                PositionId = 4,
                TeamId = 4
            };

            List<Team> t = new List<Team>() { team1, team2, team3, team4 };
            List<Position> p = new List<Position>() { position1, position2, position3, position4 };
            List<Employee> e = new List<Employee>() { ceo, p1, p2, p3, p4, p5, p6, p7 };

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
