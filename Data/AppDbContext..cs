using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }

        public DbSet<Cliente> tblCliente { get; set; }
        public DbSet<Tienda> tblTienda { get; set; }
        public DbSet<Articulo> tblArticulo { get; set; }
    }
}
