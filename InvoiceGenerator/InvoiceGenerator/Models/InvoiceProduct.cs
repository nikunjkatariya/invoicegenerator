using System.ComponentModel.DataAnnotations;

namespace InvoiceGenerator.Models
{
    public class InvoiceProduct
    {
        public int Id { get; set; }
        public string InvoiceId { get; set; }
        [Required]
        public string? ProductName { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public int ProductQuantity { get; set; }
        [Required]
        public decimal Discount { get; set; }
        [Required]
        public decimal ProductTax { get; set; }

        public decimal Amount { get; set; }
    }
}