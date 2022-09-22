using VisitorManagement2022.Data;
using VisitorManagement2022.Models;


namespace VisitorManagement2022.Service
{
    public class DBCalls : IDBCalls
    {

        private readonly ApplicationDbContext _context;

        public DBCalls(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Visitors> VisitorsLoggedIn()
        {
            return _context.Visitors.OrderByDescending(v => v.DateIn).Where(v => v.DateOut == null).ToList();
        }
    }
}
