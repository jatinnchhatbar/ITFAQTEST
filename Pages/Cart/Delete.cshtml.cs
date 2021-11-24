using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ITFAQTest.DBContexts;
using ITFAQTest.Models;

namespace ITFAQTest.Pages.Cart
{
    public class DeleteModel : PageModel
    {
        private readonly ITFAQTest.DBContexts.MyDbContext _context;

        public DeleteModel(ITFAQTest.DBContexts.MyDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public CartItem CartItem { get; set; }

        [BindProperty]
        public  Product Product  { get; set; }


        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            CartItem = await _context.CartItems.FirstOrDefaultAsync(m => m.Id == id);
            Product = await _context.Products.FirstOrDefaultAsync(m => m.Id == CartItem.ProductId);

            if (CartItem == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            CartItem = await _context.CartItems.FindAsync(id);

            if (CartItem != null)
            {
                _context.CartItems.Remove(CartItem);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
