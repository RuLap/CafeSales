﻿// <auto-generated />
using System;
using CafeSales.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace CafeSales.Migrations
{
    [DbContext(typeof(CafeDbContext))]
    partial class CafeDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.13")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("CafeSales.Models.Order", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("ClientName")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("character varying(30)");

                    b.Property<Guid>("PaymentTypeId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("StatusId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("Time")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.HasIndex("PaymentTypeId");

                    b.HasIndex("StatusId");

                    b.ToTable("orders", "public");
                });

            modelBuilder.Entity("CafeSales.Models.OrderProduct", b =>
                {
                    b.Property<Guid>("OrderId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("ProductId")
                        .HasColumnType("uuid");

                    b.Property<int>("Quantity")
                        .HasColumnType("integer");

                    b.HasKey("OrderId", "ProductId");

                    b.HasIndex("ProductId");

                    b.ToTable("order_products", "public");
                });

            modelBuilder.Entity("CafeSales.Models.OrderStatus", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.ToTable("order_statuses", "public");

                    b.HasData(
                        new
                        {
                            Id = new Guid("76dfd036-cb2d-4a8a-ac50-1ed8a5908ba7")
                        },
                        new
                        {
                            Id = new Guid("51ed485a-d259-4b1e-9418-dd63147451ee")
                        },
                        new
                        {
                            Id = new Guid("1f429036-0a5e-4888-a74f-cef8275f5df8")
                        });
                });

            modelBuilder.Entity("CafeSales.Models.PaymentType", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.ToTable("payment_types", "public");

                    b.HasData(
                        new
                        {
                            Id = new Guid("9fdbe7bf-68ca-4ef9-ac4a-aa8b78584a7f")
                        },
                        new
                        {
                            Id = new Guid("16a1fec6-a9ea-40a9-b547-cb228401ccf9")
                        });
                });

            modelBuilder.Entity("CafeSales.Models.Product", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("products", "public");
                });

            modelBuilder.Entity("CafeSales.Models.Order", b =>
                {
                    b.HasOne("CafeSales.Models.PaymentType", "PaymentType")
                        .WithMany()
                        .HasForeignKey("PaymentTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CafeSales.Models.OrderStatus", "Status")
                        .WithMany()
                        .HasForeignKey("StatusId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("PaymentType");

                    b.Navigation("Status");
                });

            modelBuilder.Entity("CafeSales.Models.OrderProduct", b =>
                {
                    b.HasOne("CafeSales.Models.Order", "Order")
                        .WithMany("Products")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CafeSales.Models.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Order");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("CafeSales.Models.Order", b =>
                {
                    b.Navigation("Products");
                });
#pragma warning restore 612, 618
        }
    }
}
