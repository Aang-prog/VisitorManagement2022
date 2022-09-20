using VisitorManagement2022.Data;
using VisitorManagement2022.Models;

namespace VisitorManagement2022.Service
{
    public class DataSeeder : IDataSeeder
    {
        private readonly ApplicationDbContext _context;
        public DataSeeder(ApplicationDbContext dbContext)
        {
            _context = dbContext;
        }

        public async Task SeedAsync()
        {
            if (!_context.StaffNames.Any())
            {
                var staff = new List<StaffNames>
                {
                    new StaffNames()
                    { Id = Guid.NewGuid(), Name = "Dwain Henson", Department = "Counselling", VisitorCount = 2
                    },
                    new StaffNames()
                    { Id = Guid.NewGuid(), Name = "Ciara Head", Department = "Counselling", VisitorCount = 2},
                     new StaffNames()
                    { Id = Guid.NewGuid(), Name = "Dwain Henson", Department = "Web Design", VisitorCount = 2},
                     new StaffNames()
                    { Id = Guid.NewGuid(), Name = "Quentin Thwaite", Department = "NZ Bat", VisitorCount = 2},
                     new StaffNames()
                    { Id = Guid.NewGuid(), Name = "Madhav Benn", Department = "Web Design", VisitorCount = 2},
                     new StaffNames()
                    { Id = Guid.NewGuid(), Name = "Suniti Lood", Department = "Early Childhood", VisitorCount = 2},
                     new StaffNames()
                    { Id = Guid.NewGuid(), Name = "Susie Tyrrell", Department = "Early Childhood", VisitorCount = 2},
                     new StaffNames()
                    { Id = Guid.NewGuid(), Name = "Jie Roy", Department = "Web Design", VisitorCount = 2},
                    new StaffNames()
                    { Id = Guid.NewGuid(), Name = "Shobha Carpenter", Department = "Software", VisitorCount = 2},
                    new StaffNames()
                    { Id = Guid.NewGuid(), Name = "Merletta Winton", Department = "Ultimate", VisitorCount = 2}
                };
                _context.StaffNames.AddRange(staff);
                await _context.SaveChangesAsync();
            }
        }
    }
}
