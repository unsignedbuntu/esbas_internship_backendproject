namespace esbas_internship_backendproject.DTOs
{
   
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    public class EventsUsersDTO
    {
#nullable disable
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [ForeignKey("EventID")]
        public int EventID { get; set; }

        [ForeignKey("CardID")]
        public int CardID { get; set; }
        public bool Status { get; set; } = true;
        public EventDTO Event { get; set; }   
        public UserDTO User { get; set; }
    }
}
