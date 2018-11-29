using EmployeeEvaluation.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeEvaluation.Logic
{
    public interface IPrepareView<T> where T : class
    {
        T GetView(ApplicationDbContext db);
    }
}
