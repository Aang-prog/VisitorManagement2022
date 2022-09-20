using System.ComponentModel.DataAnnotations;

namespace VisitorManagement2022.ViewModels
{
    public class StaffNamesVM
    {
        public Guid Id { get; set; }
        [Display(Name = "Staff Name")]
        public string? Name { get; set; }
        public string? Department { get; set; }
        [Display(Name = "Amount of Visitors")]
        public int VisitorCount { get; set; }
    }
}
