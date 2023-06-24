using System.ComponentModel.DataAnnotations.Schema;

namespace API.Model
{
    [Table("tb_tr_accountrole")]
    public class AccountRole : BaseEntity
    {
        [Column("account_guid")]
        public Guid AccountGuid { get; set; }

        [Column("role_guid")]
        public Guid RoleGuid { get; set; }

        //Cardinalitas Below Here
        public Account? Account { get; set; }
        public Role? Role { get; set; }
    }
}
