﻿namespace esbas_internship_backendproject.Entities
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Events_Users
    {
#nullable disable 
        [Key]
        public int Events_UserID { get; set; }

        [ForeignKey("EventId")]
        public int EventID { get; set; }

        [ForeignKey("UserId")]
        public int UserID { get; set; }
        public bool Status { get; set; } = true;
        
        public ICollection<Events_Users> Users { get; set; }

        public ICollection<Events_Users> Events { get; set; }

        public Events Event { get; set; }

        public Users User   { get; set; }
        public Events_Users() { }
    }
}
