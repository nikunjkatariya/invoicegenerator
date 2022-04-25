using Microsoft.EntityFrameworkCore;
using InvoiceGenerator.Models;

namespace InvoiceGenerator.Data
{
    public class InvoiceContext : DbContext
    {
        public InvoiceContext(DbContextOptions<InvoiceContext> options) : base(options)
        {

        }
        public DbSet<Invoice>? Invoices { get; set; }
        public DbSet<InvoiceProduct>? InvoiceProducts { get; set; }
    }
}
