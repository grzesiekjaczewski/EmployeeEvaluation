using EmployeeEvaluation.Logic;
using EmployeeEvaluation.Logic.PrepareView;
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
    public class PreparingDisplayDataTests
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        [TestMethod]
        public void CanPrepareServeyView()
        {
            IPrepareView<List<HRBrowseSurvey>> prepare = new PrepareHRSurveyView<List<HRBrowseSurvey>>();
            List<HRBrowseSurvey> view = prepare.GetView(db);

            Assert.IsNotNull(view);
        }

        [TestMethod]
        public void CanGetStructure()
        {
            IPrepareView<Structure> prepareView = new PrepareStructureView<Structure>();
            Structure structure = prepareView.GetView(db);
            Assert.IsNotNull(structure);
        }

    }
}
