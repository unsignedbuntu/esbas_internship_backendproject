namespace esbas_internship_backendproject.Entities
{
    using Microsoft.Extensions.Configuration.UserSecrets;
    using System;
    using System.ComponentModel.DataAnnotations;
    public class Users
    {
#nullable disable
        [Key]
        public int UserID { get; set; }

        public string FullName { get; set; }
        public string Department { get; set; }
        public Boolean IsOfficeEmployee { get; set; }
        public int Age { get; set; }
        public string Gender { get; set; }

        public bool Status { get; set; }       
 
       public ICollection<Events_Users> Events { get; set; }
        public Users() { }
    }
}
