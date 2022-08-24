using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StockAPI.Data;
using StockAPI.Model;

namespace StockAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StocksController : ControllerBase
    {
        private readonly StockAPIContext _context;

        public StocksController(StockAPIContext context)
        {
            _context = context;
        }

        // POST: api/Stock
        /// <summary>
        /// 
        /// </summary>
        /// <param name="stock"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<Variant>> PostStock(Stock stock)
        {
            if (!ProductExists(stock.ProductCode))
            {
                return Problem("Aynı product code eklenemez!!!");
            }

            var product = new Product();
            product.ProductCode = stock.ProductCode;

            var variant = new Variant();
            variant.VariantCode = stock.VariantCode;
            variant.Quantity = stock.Quantity;

            product.Variants = new List<Variant>();
            product.Variants.Add(variant);

            _context.Product.Add(product);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetVariant", new { stock.ProductCode }, stock);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="variantCode"></param>
        /// <returns></returns>
        [HttpGet("{variantCode}/variant")]
        public async Task<ActionResult<List<Variant>>> GetVariantStock(string variantCode)
        {
            if (_context.Variant == null)
            {
                return NotFound();
            }

            var allVariantList = _context.Variant.Where(e => e.VariantCode == variantCode).ToList();
            if (allVariantList == null)
            {
                return NotFound();
            }
            return allVariantList;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="productCode"></param>
        /// <returns></returns>
        [HttpGet("{storeCode}/product")]
        public async Task<ActionResult<Product>> GetProductStock(string storeCode)
        {
            if (_context.Product == null)
            {
                return NotFound();
            }

            var product = _context.Product.Where(e => e.ProductCode == storeCode).FirstOrDefault(); // tek product olmalı 
            if (product == null)
            {
                return NotFound();
            }

            var variants = _context.Variant.Where(e => e.ProductId == product.Id);
            product.Variants = new List<Variant>(variants);
            return product;
        }

        // GET: api/Stock
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Variant>>> GetVariant()
        {
            if (_context.Variant == null)
            {
                return NotFound();
            }
            return await _context.Variant.ToListAsync();
        }

        // GET: api/Stock/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Variant>> GetVariant(int id)
        {
            if (_context.Variant == null)
            {
                return NotFound();
            }
            var variant = await _context.Variant.FindAsync(id);

            if (variant == null)
            {
                return NotFound();
            }

            return variant;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private bool VariantExists(int id)
        {
            return (_context.Variant?.Any(e => e.Id == id)).GetValueOrDefault();
        }
       
        /// <summary>
        /// 
        /// </summary>
        /// <param name="productCode"></param>
        /// <returns></returns>
        private bool ProductExists(string productCode)
        {
            return (_context.Product?.Any(e => e.ProductCode == productCode)).GetValueOrDefault();
        }
    }
}
