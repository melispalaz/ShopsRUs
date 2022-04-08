namespace ShopsRUs.Api.Models
{
    public class Customer : BaseModel
    {

        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool isEmployee { get; set; }
        public bool isGrocery { get; set; }
        public bool isAffiliateCustomer { get; set; }
        public int Status { get; set; }


    }
}
