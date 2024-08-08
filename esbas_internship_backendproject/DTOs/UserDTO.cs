namespace esbas_internship_backendproject.DTOs
{
    using esbas_internship_backendproject.Entities;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    public class UserDTO
    {
#nullable disable

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserID { get; set; }
        public int CardID { get; set; }
        public string FullName { get; set; }
        public string Department { get; set; }
        public string IsOfficeEmployee { get; set; }
        public string Gender { get; set; }
        public bool Status { get; set; } = true;

        public UserDepartmentDTO User_Department { get; set; }
        public UserGenderDTO User_Gender { get; set; }
        public UserIsOfficeEmployeeDTO User_IsOfficeEmployee { get; set; }
    }
}
