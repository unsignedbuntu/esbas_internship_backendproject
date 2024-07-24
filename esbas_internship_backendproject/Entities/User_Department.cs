namespace esbas_internship_backendproject.Entities
{
    using System;
    using System.ComponentModel.DataAnnotations;
    public class User_Department
    {
#nullable disable
        [Key]
        public int D_ID {  get; set; }
        public string Name { get; set; }
        public bool Status { get; set; } = true;

        public User_Department() { }
    }
}
