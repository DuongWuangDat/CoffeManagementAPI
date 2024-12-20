﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoffeeManagementAPI.Model
{
    public class CustomerType
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CustomerTypeID { get; set; }
        public string CustomerTypeName { get; set; } = string.Empty;
        [Column(TypeName ="decimal(18,2)")]
        public decimal DiscountValue { get; set; }


        [Column(TypeName = "decimal(18,2)")]
        public decimal BoundaryRevenue { get; set; } = 0;

        public List<Customer> Customers { get; set; } = new List<Customer>();
    }
}
