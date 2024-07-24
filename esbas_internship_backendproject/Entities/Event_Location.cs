﻿namespace esbas_internship_backendproject.Entities
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class Event_Location
    {
#nullable disable

        [Key]
        public int L_ID { get; set; }
        
        public string Name { get; set; }
        public bool Status { get; set; } = true;
        
        public Event_Location() { }
    }
}
