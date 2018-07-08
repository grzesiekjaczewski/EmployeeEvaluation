using EmployeeEvaluation.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace EmployeeEvaluation.Logic
{
    interface IViewBagLoader
    {
        void Load(Controller controller, ApplicationDbContext db);
    }
}
