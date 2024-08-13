namespace esbas_internship_backendproject.DTOs
{
  
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class EventDTO
    {
#nullable disable
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int EventID { get; set; }

        [ForeignKey("L_ID")]
        public int L_ID { get; set; }

        [ForeignKey("T_ID")]
        public int T_ID { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Location { get; set; }
        public DateTime EventDateTime { get; set; }
        public bool Status { get; set; } = true;
        public bool Event_Status { get; set; }

        public EventLocationDTO Event_Location { get; set; }

        public EventTypeDTO Event_Type { get; set; }
    }
}
