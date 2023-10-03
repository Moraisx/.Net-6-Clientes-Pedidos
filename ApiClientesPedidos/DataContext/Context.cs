using ApiClientesPedidos.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiClientesPedidos.DataContext
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options){ }

        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Endereco> Enderecos { get; set; }
        public DbSet<Produto> Produtos { get; set; }
    }
}
