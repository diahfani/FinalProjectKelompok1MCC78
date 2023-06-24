using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Model;

[Table("tb_m_role")]
public class Role : BaseEntity
{
    [Column("name", TypeName ="nvarchar(100)")]
    public string Name { get; set; }

    //cardinalitas below here
    public ICollection<AccountRole>? AccountRole { get; set; }
    /*public ICollection<Employee> Employee { get; set; }*/
}
