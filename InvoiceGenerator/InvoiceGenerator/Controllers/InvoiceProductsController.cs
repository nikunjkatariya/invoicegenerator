#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using InvoiceGenerator.Data;
using InvoiceGenerator.Models;

namespace InvoiceGenerator.Controllers
{
    public class InvoiceProductsController : Controller
    {
        private readonly InvoiceContext _context;
        private static int pindex=0;
        public InvoiceProductsController(InvoiceContext context)
        {
            _context = context;
        }

        // GET: InvoiceProducts
        public async Task<IActionResult> Index(int id)
        {
            Console.WriteLine(pindex);
            var invoicevalue = await _context.Invoices.FindAsync(pindex);
            var invoiceContext = _context.InvoiceProducts.Where(i => i.InvoiceId == invoicevalue.InvoiceId);
            return View(await invoiceContext.ToListAsync());
        }

        // GET: InvoiceProducts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var invoiceProduct = await _context.InvoiceProducts
                .FirstOrDefaultAsync(m => m.Id == id);
            if (invoiceProduct == null)
            {
                return NotFound();
            }

            return View(invoiceProduct);
        }
        public async Task<IActionResult> Indexvalue(int id)
        {
            pindex = id;
            return RedirectToAction(nameof(Index), pindex);
        }
        public async Task<IActionResult> Add()
        {
            Console.WriteLine(pindex);
            return RedirectToAction(nameof(Create),pindex);
        }
        public async Task<IActionResult> Update()
        {
            Console.WriteLine(pindex);
            return RedirectToAction(nameof(Index),pindex);
        }
        public async Task<IActionResult> Goback()
        {
            return RedirectToAction("Index","Invoices");
        }
        // GET: InvoiceProducts/Create
        public async Task<IActionResult> Create(int id)
        {
            Console.WriteLine(pindex);
            var invoice = await _context.Invoices.FindAsync(pindex);
            InvoiceProduct invoiceProduct = new InvoiceProduct();
            invoiceProduct.InvoiceId = invoice.InvoiceId;
            return View(invoiceProduct);
        }

        // POST: InvoiceProducts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("InvoiceId,ProductName,Price,ProductQuantity,Discount,ProductTax,Amount")] InvoiceProduct invoiceProduct)
        {
            if (ModelState.IsValid)
            {
                invoiceProduct.Amount = CalculateAmount(invoiceProduct);
                _context.Add(invoiceProduct);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Update));
            }
            return View(invoiceProduct);
        }
        public decimal CalculateAmount(InvoiceProduct invoiceProduct)
        {
            decimal sum = 0;
            /*long price = 0, quantity = 0, discount = 0, tax = 0, sum = 0;*/
            decimal totalPrice = invoiceProduct.Price * invoiceProduct.ProductQuantity;
            decimal discountPrice = totalPrice - (totalPrice * (invoiceProduct.Discount / 100));
            sum =discountPrice+(discountPrice*(invoiceProduct.ProductTax/100));
            return sum;
        }
        // GET: InvoiceProducts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var invoiceProduct = await _context.InvoiceProducts.FindAsync(id);
            if (invoiceProduct == null)
            {
                return NotFound();
            }
            return View(invoiceProduct);
        }

        // POST: InvoiceProducts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,InvoiceId,ProductName,Price,ProductQuantity,Discount,ProductTax,Amount")] InvoiceProduct invoiceProduct)
        {
            if (id != invoiceProduct.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    invoiceProduct.Amount = CalculateAmount(invoiceProduct);
                    _context.Update(invoiceProduct);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InvoiceProductExists(invoiceProduct.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Update));
            }
            return View(invoiceProduct);
        }

        // GET: InvoiceProducts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var invoiceProduct = await _context.InvoiceProducts
                .FirstOrDefaultAsync(m => m.Id == id);
            if (invoiceProduct == null)
            {
                return NotFound();
            }

            return View(invoiceProduct);
        }

        // POST: InvoiceProducts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var invoiceProduct = await _context.InvoiceProducts.FindAsync(id);
            _context.InvoiceProducts.Remove(invoiceProduct);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Update));
        }

        private bool InvoiceProductExists(int id)
        {
            return _context.InvoiceProducts.Any(e => e.Id == id);
        }
    }
}
