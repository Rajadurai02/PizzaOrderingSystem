using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace PizzaOrderingApplication.Models
{
    public partial class pizzaContext : DbContext
    {
        public pizzaContext()
        {
        }

        public pizzaContext(DbContextOptions<pizzaContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<OrderDetail> OrderDetails { get; set; }
        public virtual DbSet<OrderItemDetail> OrderItemDetails { get; set; }
        public virtual DbSet<PizzaDetail> PizzaDetails { get; set; }
        public virtual DbSet<Topping> Toppings { get; set; }
        public virtual DbSet<UserLoginDetail> UserLoginDetails { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data source=KANINI-LTP-474\\KANINISQLSERVER;Integrated Security=true;Initial Catalog=pizza");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Order>(entity =>
            {
                entity.Property(e => e.Status)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__Orders__UserId__3C69FB99");
            });

            modelBuilder.Entity<OrderDetail>(entity =>
            {
                entity.HasKey(e => e.ItemNumber)
                    .HasName("PK__OrderDet__C28ACDB6EC11E389");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.OrderDetails)
                    .HasForeignKey(d => d.OrderId)
                    .HasConstraintName("FK__OrderDeta__Order__3F466844");

                entity.HasOne(d => d.PizzaNumberNavigation)
                    .WithMany(p => p.OrderDetails)
                    .HasForeignKey(d => d.PizzaNumber)
                    .HasConstraintName("FK__OrderDeta__Pizza__403A8C7D");
            });

            modelBuilder.Entity<OrderItemDetail>(entity =>
            {
                entity.HasKey(e => new { e.ItemNumber, e.ToppingNumber })
                    .HasName("PK__OrderIte__F23F4C4294D5BFFF");

                entity.HasOne(d => d.ItemNumberNavigation)
                    .WithMany(p => p.OrderItemDetails)
                    .HasForeignKey(d => d.ItemNumber)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__OrderItem__ItemN__4316F928");

                entity.HasOne(d => d.ToppingNumberNavigation)
                    .WithMany(p => p.OrderItemDetails)
                    .HasForeignKey(d => d.ToppingNumber)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__OrderItem__Toppi__440B1D61");
            });

            modelBuilder.Entity<PizzaDetail>(entity =>
            {
                entity.HasKey(e => e.PizzaNumber)
                    .HasName("PK__PizzaDet__E4FE0B3038219213");

                entity.Property(e => e.PizzaName)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.PizzaType)
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Topping>(entity =>
            {
                entity.HasKey(e => e.ToppingNumber)
                    .HasName("PK__Toppings__0B581F426950E5DA");

                entity.Property(e => e.ToppingName)
                    .HasMaxLength(30)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<UserLoginDetail>(entity =>
            {
                entity.HasKey(e => e.UserId)
                    .HasName("PK__UserLogi__1788CC4CF747A498");

                entity.Property(e => e.UserAddress)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UserMail)
                    .HasMaxLength(40)
                    .IsUnicode(false);

                entity.Property(e => e.UserName)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.UserPassword)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.UserPhone)
                    .HasMaxLength(11)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
