namespace esbas_internship_backendproject.DTOs
{
    using System.ComponentModel.DataAnnotations.Schema;
    using System.ComponentModel.DataAnnotations;
    public class DepartmentDTO
    {
 
#nullable disable
            [Key]
            [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

            public int DepartmentID { get; set; }

            [ForeignKey("CostCenterID")]
            public int CostCenterID { get; set; }

            [ForeignKey("TaskID")]
            public int TaskID { get; set; }

            public string Name { get; set; }

            public bool Status { get; set; } = true;


            public CostCentersDTO CostCenters { get; set; }

            public TasksDTO Tasks { get; set; }
        }
}
