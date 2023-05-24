using Microsoft.EntityFrameworkCore;
using System.Xml;
using Backend.Models;
using MySql.Data.MySqlClient;

namespace Backend.Data
{
    public class SoundSenseiContext : DbContext
    {
        public SoundSenseiContext(DbContextOptions<SoundSenseiContext> options)
            : base(options)
        {
        }

        public DbSet<Brand> Brands { get; set; }
        public DbSet<Product> Products { get; set; }

        public DbSet<User> Users { get; set; }
        public DbSet<HasProduct> HasProducts { get; set; }
        public DbSet<WantProduct> WantProducts { get; set; }
    }
}
