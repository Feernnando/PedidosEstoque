using Microsoft.EntityFrameworkCore;
using PedidosEstoque.Domain.Entities;

namespace PedidosEstoque.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base (options){}
        
        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Cliente> Clientes {get; set;}
        public DbSet<Pedido> Pedidos { get; set; }
        public DbSet<ItemPedido> ItemPedidos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder. Entity<Produto>()
                .Property(p => p.Preco)
                .HasPrecision(18, 2);



            modelBuilder.Entity<Cliente>(e =>
            {
                e.Property(c => c.Nome)
                    .IsRequired()
                    .HasMaxLength(150);

                e.Property(c => c.Email)
                    .IsRequired()
                    .HasMaxLength(200);

                e.HasIndex(c => c.Email).IsUnique();    
            });    


            modelBuilder.Entity<Pedido>(e =>
            {
                e.Property(p=> p.Status). HasConversion<string>().HasMaxLength(20);
                e.Ignore(p => p.ValorTotal);

                e.HasOne( p => p.Cliente)
                    .WithMany(c => c.Pedidos)
                    .HasForeignKey(p => p.ClienteId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<ItemPedido>(e =>
            {
                e.Property(i => i.PrecoUnitario).HasPrecision(18, 2);

                e.HasOne(i => i.Pedido)
                    .WithMany(p => p.Itens)
                    .HasForeignKey(i => i.PedidoId)
                    .OnDelete(DeleteBehavior.Cascade);

                    e.HasOne(i => i.Produto)
                        .WithMany()
                        .HasForeignKey(i => i.ProdutoId)
                        .OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
}