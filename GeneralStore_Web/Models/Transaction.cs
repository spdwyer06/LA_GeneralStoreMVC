using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GeneralStore_Web.Models
{
    public class Transaction
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey(nameof(Customer))]
        [Display(Name = "Customer Id")]
        public int CustomerId { get; set; }
        public virtual Customer Customer { get; set; }

        public decimal TotalCost
        {
            get
            {
                var totalCost = 0m;

                foreach (var item in TransactionItems)
                    totalCost += (item.Product.Price * item.Quantity);

                return totalCost;
            }
        }

        public virtual ICollection<TransactionItem> TransactionItems { get; set; } = new List<TransactionItem>();

        //[ForeignKey(nameof(Product))]
        //[Display(Name = "Product Id")]
        //public int ProductId { get; set; }
        //public virtual Product Product { get; set; }

        //[Required]
        //public int Quantity { get; set; }
    }
}