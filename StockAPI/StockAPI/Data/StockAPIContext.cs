using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using StockAPI.Model;

namespace StockAPI.Data
{
    public class StockAPIContext : DbContext
    {
        public StockAPIContext (DbContextOptions<StockAPIContext> options)
            : base(options)
        {
        }

        public DbSet<StockAPI.Model.Product> Product { get; set; } = default!;

        public DbSet<StockAPI.Model.Variant> Variant { get; set; } = default!;
    }
}
