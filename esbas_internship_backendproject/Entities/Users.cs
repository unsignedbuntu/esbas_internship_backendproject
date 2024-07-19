namespace esbas_internship_backendproject.Entities
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

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

        [ForeignKey("User_Department")]
        public int User_DepartmentID { get; set; }


        [ForeignKey("User_Gender")]
        public int User_GenderID { get; set; }


        [ForeignKey("User_IsOfficeEmployee")]
        public int User_IsOfficeEmployeeID { get; set; }
        // public ICollection<Events_Users> Events { get; set; }
        public Users() { }
    }
}
