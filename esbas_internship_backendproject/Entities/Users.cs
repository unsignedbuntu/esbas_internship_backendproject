namespace esbas_internship_backendproject.Entities
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Users
    {
#nullable disable
        [Key]
        public int ID { get; set; }
        public int UserID { get; set; }
        public string FullName { get; set; }
        public string Department { get; set; }
        public bool IsOfficeEmployee { get; set; }
        public string Gender { get; set; }
        public bool Status { get; set; }

        [ForeignKey("User_DepartmentID")]
        public int User_DepartmentID { get; set; }

        [ForeignKey("User_GenderID")]
        public int User_GenderID { get; set; }

        [ForeignKey("User_IsOfficeEmployeeID")]
        public int User_IsOfficeEmployeeID { get; set; }

        //public User_Department User_Department { get; set; }
        //public User_Gender User_Gender { get; set; }
        //public User_IsOfficeEmployee User_IsOfficeEmployee { get; set; }
        

        public Users() { }
    }
}
