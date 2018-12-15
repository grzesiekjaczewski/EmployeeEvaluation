using EmployeeEvaluation.Models;

namespace EmployeeEvaluation.Logic.Remove
{
    interface IRemoveItem
    {
        string Save(int? id, ApplicationDbContext db);
    }
}
