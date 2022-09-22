namespace VisitorManagement2022.Models
{
    public class Visitors
    {
        public Guid Id { get; set; }

        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public string? Buisness { get; set; }

        public DateTime DateIn { get; set; }

        public DateTime? DateOut { get; set; }

        //Navigation

        public Guid StaffNameId { get; set; }

        public StaffNames? StaffName { get; set; }
    }
}
