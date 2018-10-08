using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeEvaluation.Logic
{
    interface IPrepareExtendedView<T1, T2>: IPrepareView<T1>, IParameterLoader<T2> where T1 : class
    {
    }
}
