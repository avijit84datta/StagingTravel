using CoreApiFirst.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace CoreApiFirst.DBContext
{
    public class TravelDbContext : DbContext
    {
        public TravelDbContext(DbContextOptions<TravelDbContext> options)
            : base(options)
        {
        }

        public DbSet<Destination> Destinations { get; set; }
    }
}
