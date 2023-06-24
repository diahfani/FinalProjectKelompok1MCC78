using API.Utilities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Model;

[Table("tb_m_employee")]
public class Employee : BaseEntity
{
    [Column("nik", TypeName = "char(6)")]
    public string NIK { get; set; }

    [Column("fullname", TypeName ="nvarchar(255)")]
    public string Fullname { get; set; }

    [Column("gender")]
    public GenderLevel Gender { get; set; }

    [Column("email", TypeName ="nvarchar(100)")]
    public string Email { get; set; }

    [Column("phone_number", TypeName ="nvarchar(20)")]
    public string PhoneNumber { get; set; }

    [Column("hiring_date")]
    public DateTime HiringDate { get; set; }

    // Foreign Key
    [Column("manager_id")]
    public Guid? ManagerID { get; set; }

    //Cardinalitas Below Here
    public ICollection<Task>? Task { get; set; }
    public Employee? Manager { get; set; }  
    public ICollection<Employee>? Subordinates { get; set; }
    public Account? Account { get; set; }
}
