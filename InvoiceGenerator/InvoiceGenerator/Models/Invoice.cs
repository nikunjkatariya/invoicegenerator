using System.ComponentModel.DataAnnotations;

namespace InvoiceGenerator.Models
{
    public class Invoice
    {
        public int Id { get; set; }
        [Required]
        public string? InvoiceId { get; set; }
        [Required]
        public string? ClientName { get; set; }
        [Required]
        public string? ClientGST { get; set; }
        [Required]
        public string? Address { get; set; }
        [Required]
        public string? City { get; set; }
        [Required]
        public string? PinCode { get; set; }
        [Required]
        public string? PANNumber { get; set; }
        [Required]
        public string? ContactNumber { get; set; }
        [Required]
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        [Required]
        public DateTime LastDate { get; set; } = DateTime.Now;
    }
}
