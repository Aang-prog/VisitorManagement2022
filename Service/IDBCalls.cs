using VisitorManagement2022.Models;

namespace VisitorManagement2022.Service
{
    public interface IDBCalls
    {
        IEnumerable<Visitors> VisitorsLoggedIn();
        IEnumerable<Visitors> WhereQuery();
        IEnumerable<StaffNames> OrderBy();
        IEnumerable<StaffNames> WhereMethodSyntax();


    }
}