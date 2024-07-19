namespace esbas_internship_backendproject.Entities
{
    using System;
    using System.ComponentModel.DataAnnotations;
    public class Event_Type
    {
#nullable disable
        [Key]
        public int Event_TypeID { get; set; }
        
        public string Event_Type_Name { get; set; }

        public Event_Type() { }
    }
}
