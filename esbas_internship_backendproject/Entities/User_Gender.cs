namespace esbas_internship_backendproject.Entities
{
    using System;
    using System.ComponentModel.DataAnnotations;
    public class User_Gender
    {
#nullable disable
        [Key]
        public int G_ID { get; set; }

        public string Name { get; set; }

        public bool Status { get; set; } = true;

        public User_Gender() { }
    }
}
