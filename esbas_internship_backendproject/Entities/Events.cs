namespace esbas_internship_backendproject.Entities
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Events
    {
#nullable disable
        [Key]
        public int EventID { get; set; }
        public string EventName { get; set; }
        public string EventType { get; set; }
        public string Location { get; set; }
        public DateTime EventDateTime { get; set; }
        public bool Status { get; set; } = true;
        public bool Event_Status { get; set; } = true;

        [ForeignKey("Event_LocationID")]
        public int Event_LocationID { get; set; }

        [ForeignKey("Event_TypeID")]

        public int Event_TypeID { get; set; }
        
        //public Event_Location Event_Location { get; set; }

         //public Event_Type Event_Type { get; set; }

        public Events() { }
    }
}

