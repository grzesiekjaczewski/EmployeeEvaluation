using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EmployeeEvaluation.Logic
{
    public static class StringToValue
    {
        public static int ParseInt(string value)
        {
            int id;
            if (!int.TryParse(value, out id))
            {
                id = 0;
            }
            return id;
        }
    }
}