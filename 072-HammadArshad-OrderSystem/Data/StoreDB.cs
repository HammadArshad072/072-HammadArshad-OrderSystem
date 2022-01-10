using _072_HammadArshad_OrderSystem.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _072_HammadArshad_OrderSystem.Data
{
    public class StoreDB : DbContext
    {
        public StoreDB(DbContextOptions<StoreDB>options):base(options)
        {

        }
        public DbSet<OrderModel> OrderModels { get; set; }
    }
}
