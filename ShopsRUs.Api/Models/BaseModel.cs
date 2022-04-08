using System.ComponentModel.DataAnnotations;

namespace ShopsRUs.Api.Models
{
    public class BaseModel
    {
        [Key]
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
