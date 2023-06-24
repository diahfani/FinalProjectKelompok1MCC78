using System.ComponentModel.DataAnnotations.Schema;

namespace API.Model;

[Table("tb_m_ratings")]
public class Rating : BaseEntity
{
    [Column("rating_value", TypeName ="float")]
    public float RatingValue { get; set; }
    [Column("comment", TypeName = "text")]
    public string Comment { get; set; }

    //Cardinalitas Below Here
    public Task? Task { get; set; }
}
