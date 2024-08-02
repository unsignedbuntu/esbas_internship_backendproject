namespace esbas_internship_backendproject.Entities
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Events_Users
    {
#nullable disable 
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [ForeignKey("EventID")]
        public int EventID { get; set; }

        [ForeignKey("UserID")]
        public int UserID { get; set; }
        public bool Status { get; set; } = true;

        public Events Event { get; set; }
        public Users User { get; set; }

        //public ICollection<Events> Events { get; set; }
        // public ICollection<Users> Users { get; set; }

        public Events_Users() { }
    }
}

