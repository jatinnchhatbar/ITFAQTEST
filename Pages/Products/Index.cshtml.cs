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
    public class IndexModel : PageModel
    {
        private readonly ITFAQTest.DBContexts.MyDbContext _context;

        public IndexModel(ITFAQTest.DBContexts.MyDbContext context)
        {
            _context = context;
        }

        public IList<Product> Product { get;set; }

        public async Task OnGetAsync()
        {
            Product = await _context.Products.ToListAsync();
        }
    }
}
