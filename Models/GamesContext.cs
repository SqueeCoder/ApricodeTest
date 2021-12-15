using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApricodeTest.Models
{
    public class GamesContext : DbContext
    {
        public DbSet<Game> Games { get; set; }
        public GamesContext(DbContextOptions<GamesContext> options)
            : base(options)
        {
            Database.EnsureDeleted();
            Database.EnsureCreated();
        }
    }
}
