using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GeneralStore_Web.Models
{
    public class TransactionItem
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey(nameof(Transaction))]
        [Display(Name = "Transaction Id")]
        public int TransactionId { get; set; }
        public virtual Transaction Transaction { get; set; }

        [ForeignKey(nameof(Product))]
        [Display(Name = "Product Id")]
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }

        [Required]
        public int Quantity { get; set; } = 1;
    }
}