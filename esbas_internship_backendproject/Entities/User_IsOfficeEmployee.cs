namespace esbas_internship_backendproject.Entities
{
    using System;
    using System.ComponentModel.DataAnnotations;
    public class User_IsOfficeEmployee
    {
#nullable disable
        [Key]
        public int User_IsOfficeEmployeeID { get; set; }

        public string User_IsOfficeEmployee_Name { get; set; }
        public bool Status { get; set; } = true;

        public User_IsOfficeEmployee() { }
    }
}
