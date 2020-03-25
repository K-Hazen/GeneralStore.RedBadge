using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GeneralStore.MVC.Models
{
    public class Transaction
    {   
        [Key]
        public int TransactionID { get; set; }

        
        [ForeignKey(nameof(Product))]
        [Display(Name = "Product Name")]
        public int ProductID { get; set; }
        public virtual Product Product { get; set; }

        
        [ForeignKey(nameof(Customer))]
        [Display(Name = "Customer Name")]
        public int CustomerID { get; set; }
        public virtual Customer Customer { get; set; }

        [Display(Name = "Date Of Transaction")]
        public DateTime DateOfTransaction { get; set; }

        [Display(Name = "Total Cost")]
        public decimal TotalCost
        {
            get
            {
                return (Product != null) ? Product.Price * ProductCount : 0;
            }
        }

        [Display(Name = "Number of Items")]
        public int ProductCount { get; set; }

    }
}