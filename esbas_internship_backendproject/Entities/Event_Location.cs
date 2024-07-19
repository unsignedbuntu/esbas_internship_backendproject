namespace esbas_internship_backendproject.Entities
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class Event_Location
    {
#nullable disable

        [Key]
        public int Event_LocationID { get; set; }
        
        public string Event_Location_Name { get; set; }

        public Event_Location() { }
    }
}
