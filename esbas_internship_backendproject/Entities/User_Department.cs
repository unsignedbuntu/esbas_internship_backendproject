namespace esbas_internship_backendproject.Entities
{
    using System;
    using System.ComponentModel.DataAnnotations;
    public class User_Department
    {
#nullable disable
        [Key]
        public int User_DepartmentID {  get; set; }
        public string User_Department_Name { get; set; }
        public bool Status { get; set; } = true;

        public User_Department() { }
    }
}
