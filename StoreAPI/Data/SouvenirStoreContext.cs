using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using StoreAPI.Models;

namespace StoreAPI.Data;

public partial class SouvenirStoreContext : DbContext
{
    public SouvenirStoreContext()
    {
    }

    public SouvenirStoreContext(DbContextOptions<SouvenirStoreContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Client> Clients { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<EmployeeType> EmployeeTypes { get; set; }

    public virtual DbSet<Item> Items { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OrderItem> OrderItems { get; set; }

    public virtual DbSet<Price> Prices { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=localhost;Database=SouvenirStore;Username=postgres;Password=postgres");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Client>(entity =>
        {
            entity.HasKey(e => e.ClientId).HasName("Client_pkey");

            entity.ToTable("Client");

            entity.Property(e => e.ClientId)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("ClientID");
            entity.Property(e => e.Email).HasColumnType("character varying");
            entity.Property(e => e.Login).HasColumnType("character varying");
            entity.Property(e => e.Name).HasColumnType("character varying");
            entity.Property(e => e.Password).HasColumnType("character varying");
            entity.Property(e => e.Patronymic).HasColumnType("character varying");
            entity.Property(e => e.Phone).HasColumnType("character varying");
            entity.Property(e => e.Surname).HasColumnType("character varying");
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.EmployeeId).HasName("Employee_pkey");

            entity.ToTable("Employee");

            entity.Property(e => e.EmployeeId)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("EmployeeID");
            entity.Property(e => e.Email).HasColumnType("character varying");
            entity.Property(e => e.Login).HasColumnType("character varying");
            entity.Property(e => e.Name).HasColumnType("character varying");
            entity.Property(e => e.Password).HasColumnType("character varying");
            entity.Property(e => e.Patronymic).HasColumnType("character varying");
            entity.Property(e => e.Phone).HasColumnType("character varying");
            entity.Property(e => e.Surname).HasColumnType("character varying");

            entity.HasOne(d => d.EmpTypeNavigation).WithMany(p => p.Employees)
                .HasForeignKey(d => d.EmpType)
                .HasConstraintName("EmployeeType");
        });

        modelBuilder.Entity<EmployeeType>(entity =>
        {
            entity.HasKey(e => e.EmpTypeId).HasName("EmployeeType_pkey");

            entity.ToTable("EmployeeType");

            entity.Property(e => e.EmpTypeId)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("EmpTypeID");
            entity.Property(e => e.Name).HasColumnType("character varying");
        });

        modelBuilder.Entity<Item>(entity =>
        {
            entity.HasKey(e => e.ItemId).HasName("Item_pkey");

            entity.ToTable("Item");

            entity.Property(e => e.ItemId)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("ItemID");
            entity.Property(e => e.Name).HasColumnType("character varying");
            entity.Property(e => e.PriceId).HasColumnName("PriceID");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.OrderId).HasName("Order_pkey");

            entity.ToTable("Order");

            entity.Property(e => e.OrderId)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("OrderID");
            entity.Property(e => e.ClientId).HasColumnName("ClientID");
            entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");
            entity.Property(e => e.OrderItemsId).HasColumnName("OrderItemsID");

            entity.HasOne(d => d.Client).WithMany(p => p.Orders)
                .HasForeignKey(d => d.ClientId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("ClientID");

            entity.HasOne(d => d.Employee).WithMany(p => p.Orders)
                .HasForeignKey(d => d.EmployeeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("EmployeeID");

            entity.HasOne(d => d.OrderItems).WithMany(p => p.Orders)
                .HasForeignKey(d => d.OrderItemsId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("OrderItemsId");
        });

        modelBuilder.Entity<OrderItem>(entity =>
        {
            entity.HasKey(e => e.OrderItemsId).HasName("OrderItems_pkey");

            entity.Property(e => e.OrderItemsId)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("OrderItemsID");
            entity.Property(e => e.OrderId).HasColumnName("OrderID");
            entity.Property(e => e.PriceId).HasColumnName("PriceID");

            entity.HasOne(d => d.Order).WithMany(p => p.OrderItemsNavigation)
                .HasForeignKey(d => d.OrderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("OrderID");

            entity.HasOne(d => d.Price).WithMany(p => p.OrderItems)
                .HasForeignKey(d => d.PriceId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("PriceID");
        });

        modelBuilder.Entity<Price>(entity =>
        {
            entity.HasKey(e => e.PriceId).HasName("Price_pkey");

            entity.ToTable("Price");

            entity.Property(e => e.PriceId)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("PriceID");
            entity.Property(e => e.ItemId).HasColumnName("ItemID");

            entity.HasOne(d => d.Item).WithMany(p => p.Prices)
                .HasForeignKey(d => d.ItemId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("ItemID");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
