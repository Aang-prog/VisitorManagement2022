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

        //https://www.infoq.com/news/2021/04/Net6-Linq/ new toys in Linq
        // from[identifier] in [data source] 
        // let[expression] // where[boolean expression] 
        // order by[[expression] (ascending/descending)], [optionally repeat] 
        // select[expression] 
        // group[expression] by[expression] into[expression]
        public IEnumerable<Visitors> VisitorsLoggedIn()
        {
            return _context.Visitors.OrderByDescending(v => v.DateIn).Where(v => v.DateOut == null).ToList();
        }

        public IEnumerable<Visitors> WhereQuery()
        {
            var query = from v in _context.Visitors
                        where v.FirstName == "John"
                        select v;
            return query;
        }


        public IEnumerable<StaffNames> WhereMethodSyntax()
        {
            var query = _context.StaffNames.Where(v => v.Name!.Contains("in")).Select(a => new StaffNames { Name = a.Name, VisitorCount = a.VisitorCount });

            return query;
        }

        public IEnumerable<StaffNames> OrderBy()
        {
            var query = _context.StaffNames.OrderByDescending(s => s.VisitorCount);

            return query;
        }

     

    }
}