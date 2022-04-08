using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShopsRUs.Api.Concrete;
using ShopsRUs.Api.Constants;
using ShopsRUs.Api.Models;

namespace ShopsRUs.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DiscountController : ControllerBase
    {

        private readonly ILogger<DiscountController> _logger;
        private readonly ShopsRUsDbContext _context;
        public DiscountController(ILogger<DiscountController> logger, ShopsRUsDbContext context)
        {
            _logger = logger;
            _context = context;
        }


        [HttpGet(Name = "GetCustomerDiscount")]
        public double? GetCustomerDiscount(int customerId, double bill)
        {
            var customer = _context.Customers.FirstOrDefault(x => x.Id == customerId && x.Status == (int)StatusEnum.Active);
            double discountedBill = 0;
            double discountUnit = 0;

            if (customer == null)
            {
                return bill;
            }

            var usedDiscounts = _context.UsedDiscounts.Where(x => x.CustomerId == customerId).ToList();

            if (usedDiscounts.Count == 0)
            {

                int currentDiscountTypeId = (int)DiscountTypeEnum.None;

                if (customer.isEmployee)
                    currentDiscountTypeId = (int)DiscountTypeEnum.EmployeeDiscount;

                else if (customer.isGrocery)
                    currentDiscountTypeId = (int)DiscountTypeEnum.None;

                else if (customer.isAffiliateCustomer)
                    currentDiscountTypeId = (int)DiscountTypeEnum.AffiliateDiscount;

                else if (DateTime.Now.AddYears(-2) >= customer.CreatedDate)
                    currentDiscountTypeId = (int)DiscountTypeEnum.Customer2Years;

                else if (bill > 100)
                    currentDiscountTypeId = (int)DiscountTypeEnum.BillOver100Discount;


                var discountDetail = _context.Discounts.FirstOrDefault(x => x.DiscountTypeId == currentDiscountTypeId && x.Status == (int)StatusEnum.Active);

                if (discountDetail != null)
                {
                    discountUnit = discountDetail.isPercent ? ((bill / discountDetail.DiscountPercent) * 100) : discountDetail.DiscountPrice;
                    discountedBill = bill - discountUnit;

                    AddCustomerDiscount(customerId, bill, discountedBill, discountDetail);

                }
                else
                    discountedBill = bill;

                return discountedBill;

            }

            return bill;


        }

        private void AddCustomerDiscount(int customerId, double bill, double discountedBill, Discount? discountDetail)
        {
            UsedDiscount usedDiscount = new UsedDiscount();
            usedDiscount.CustomerId = customerId;
            usedDiscount.DiscountId = discountDetail.Id;
            usedDiscount.CreatedDate = DateTime.Now;
            usedDiscount.Discount = discountedBill;
            usedDiscount.LastBill = bill - discountedBill;
            usedDiscount.Bill = bill;
            _context.UsedDiscounts.Add(usedDiscount);
        }
    }
}
