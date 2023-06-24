using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Model
{
    [Table("tb_m_accounts")]
    public class Account : BaseEntity
    {
        [Column("password")]
        public string Password { get; set; }

        //Cardinalitas Below Here
        public Employee? Employee { get; set; }
        public ICollection<AccountRole>? AccountRole { get; set; }
    }
}
