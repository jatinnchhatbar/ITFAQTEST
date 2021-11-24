using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ITFAQTest.DBContexts;
using ITFAQTest.Models;
using ITFAQTest.ViewModels;

namespace ITFAQTest.Pages.Cart
{
    public class IndexModel : PageModel
    {
        private readonly ITFAQTest.DBContexts.MyDbContext _context;

        public IndexModel(ITFAQTest.DBContexts.MyDbContext context)
        {
            _context = context;
        }

        public IList<CartView> CartItem { get;set; }

        public async Task OnGetAsync()
        {
            CartItem = (from c in _context.CartItems
                       join p in _context.Products
                       on c.ProductId equals p.Id
                       select new CartView() 
                       { 
                           p = p,
                           c= c
                       }).ToList();

            //CartItem = await _context.CartItems.ToListAsync();
        }
    }
}
