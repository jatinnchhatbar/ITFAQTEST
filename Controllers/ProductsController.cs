using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ITFAQTest.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ITFAQTest.Controllers
{
    [ApiController]

    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly ILogger<ProductsController> _logger;
        private readonly ITFAQTest.DBContexts.MyDbContext _context;

        public ProductsController(ILogger<ProductsController> logger, ITFAQTest.DBContexts.MyDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet]
        public IEnumerable<Product> Get()
        {
            var products = _context.Products.ToListAsync();
            products.Wait();
            return products.Result;
        }

        [HttpPost]
        [Route("AddToCart")]
        public ActionResult AddToCart([FromForm] Product p)
        {
            
            var product = (from pr in _context.Products
                           where pr.Id == p.Id
                           select pr).FirstOrDefault();

            var cartitem = (from ci in _context.CartItems
                            where ci.ProductId == p.Id
                            select ci).FirstOrDefault();

            if (cartitem != null)
            {
                cartitem.Quantity += 1;
                cartitem.Amount = cartitem.Quantity * product.Price;
                _context.Attach(cartitem).State = EntityState.Modified;
            }
            else
            {
                var ci = new CartItem()
                {
                    ProductId = p.Id,
                    Quantity = 1,
                    Amount = product.Price,
                };
                _context.CartItems.Add(ci);
            }
            _context.SaveChanges();
            return Ok();
        }
    }
}
