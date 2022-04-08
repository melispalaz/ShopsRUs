namespace ShopsRUs.Api.Models
{
    public class UsedDiscount : BaseModel
    {
        public int CustomerId { get; set; }
        public int DiscountId { get; set; }
        public double Bill { get; set; }
        public double Discount { get; set; }
        public double LastBill { get; set; }
    }
}
