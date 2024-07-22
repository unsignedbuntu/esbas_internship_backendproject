namespace esbas_internship_backendproject.Entities
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Events_Users
    {
#nullable disable 
        [Key]
        public int Events_UserID { get; set; }

        public int EventID { get; set; }
        public int UserID { get; set; }
        public bool Status { get; set; } = true;

        [ForeignKey("EventID")]
        public Events Event { get; set; }

        [ForeignKey("UserID")]
        public Users User { get; set; }

        public Events_Users() { }
    }
}
