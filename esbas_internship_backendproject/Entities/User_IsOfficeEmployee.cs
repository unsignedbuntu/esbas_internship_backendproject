namespace esbas_internship_backendproject.Entities
{
    using System;
    using System.ComponentModel.DataAnnotations;
    public class User_IsOfficeEmployee
    {
#nullable disable
        [Key]
        public int I_ID { get; set; }

        public string Name { get; set; }
        public bool Status { get; set; } = true;

        public User_IsOfficeEmployee() { }
    }
}
