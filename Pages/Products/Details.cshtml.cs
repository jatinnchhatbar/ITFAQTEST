using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ITFAQTest.DBContexts;
using ITFAQTest.Models;

namespace ITFAQTest.Pages.Products
{
    public class DetailsModel : PageModel
    {
        private readonly ITFAQTest.DBContexts.MyDbContext _context;

        public DetailsModel(ITFAQTest.DBContexts.MyDbContext context)
        {
            _context = context;
        }

        public Product Product { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Product = await _context.Products.FirstOrDefaultAsync(m => m.Id == id);

            if (Product == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
