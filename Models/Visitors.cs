﻿using System.ComponentModel.DataAnnotations;

namespace VisitorManagement2022.Models
{
    public class Visitors
    {
        public Guid Id { get; set; }
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        [Display(Name = "Buisness")]
        public string Buisness { get; set; }
        [Display(Name = "Visit Date In")]
        public DateTime DateIn { get; set; }
        [Display(Name = "Visit Date Out")]
        public DateTime DateOut { get; set; }

        //Navigation
        [Display(Name = "Staff Member Visited")]
        public Guid StaffNameId { get; set; }
        [Display(Name = "Staff Member Visited")]
        public StaffNames? StaffName { get; set; }
    }
}
