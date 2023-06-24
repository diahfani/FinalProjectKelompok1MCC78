using System.ComponentModel.DataAnnotations.Schema;

namespace API.Model
{
    [Table("tb_tr_tasks")]
    public class Task : BaseEntity
    {
        [Column("subject", TypeName = "nvarchar(255)")]
        public string Subject { get; set; }

        [Column("description", TypeName = "text")]
        public string Description { get; set; }

        [Column("deadline")]
        public DateTime Deadline { get; set; }

        //Foreign Key
        [Column("employee_id")]
        public Guid EmployeeGuid { get; set; }

        //Cardinalitas Below Here
        public Rating? Rating { get; set; }
        public Report? Report { get; set; }
        public Employee? Employee { get; set; }

    }
}
