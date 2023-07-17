using System.ComponentModel.DataAnnotations.Schema;

namespace Domain
{
    [Table("CriteriaAdvert")]
    public class CriteriaAdvert
    {
         public int Id { get; set; }
         public Criterias Criteria { get; set; } = null!;
         public Advert Advert { get; set; } = null!;
    }
}
