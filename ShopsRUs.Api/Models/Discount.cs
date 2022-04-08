using System.ComponentModel.DataAnnotations;

namespace ShopsRUs.Api.Models
{
    public class Discount :BaseModel
    {

      
        public int DiscountTypeId { get; set; }
        public bool isPercent { get; set; }
        public double DiscountPercent { get; set; }
        public double DiscountPrice { get; set; }
        public int Status { get; set; }
    }
}
