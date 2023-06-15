using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProductApi.Models
{
    [Table("product", Schema = "dbo")]
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int ItemsInStock { get; set; }
        public int ItemsReserved { get; set; }
    }
}
