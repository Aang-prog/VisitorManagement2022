namespace VisitorManagement2022.Models
{
    public class StaffNames
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Department { get; set; }

        public int VisitorCount { get; set; }
    }
}
