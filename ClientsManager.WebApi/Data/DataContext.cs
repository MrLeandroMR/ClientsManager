using ClientsManager.WebApi.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClientsManager.WebApi.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Endereco> Enderecos { get; set; }
        public DbSet<ClienteEndereco> ClientesEnderecos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ClienteEndereco>().HasKey(ce => new { ce.ClienteId, ce.EnderecoId });

            modelBuilder.Entity<ClienteEndereco>()
                .HasOne(cd => cd.Cliente)
                .WithMany(c => c.ClientesEnderecos)
                .HasForeignKey(ce => ce.ClienteId);

            modelBuilder.Entity<ClienteEndereco>()
                .HasOne(cd => cd.Endereco)
                .WithMany(c => c.ClientesEnderecos)
                .HasForeignKey(ce => ce.EnderecoId);
        }
    }
}
