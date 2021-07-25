using MakeMagic.Data.Mapping;
using MakeMagic.Models;
using Microsoft.EntityFrameworkCore;

namespace MakeMagic.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

 
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            _ = new CharacterMapping(modelBuilder.Entity<Character>());
        }

        public DbSet<Character> Character { get; set; }

    }
}
