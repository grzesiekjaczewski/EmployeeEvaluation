using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeEvaluation.Logic
{
    interface IViewBagExtendedLoader<T> : IViewBagLoader, IViewBagParameterLoader<T>
    {
    }
}
