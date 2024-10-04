﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Repository;

#nullable disable

namespace ComputerHardwareStore.Migrations
{
    [DbContext(typeof(RepositoryContext))]
    partial class RepositoryContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Entities.Models.Category", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.HasKey("Id");

                    b.ToTable("Categories");

                    b.HasData(
                        new
                        {
                            Id = new Guid("9e768b71-f27d-426c-ac3d-41e7a7c26729"),
                            Description = "A graphics card (also called a video card)is a computer expansion card that generates a feed of graphics output to a display device such as a monitor. Essential part for gaming.",
                            Name = "GPU"
                        });
                });

            modelBuilder.Entity("Entities.Models.Product", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CategoryId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasMaxLength(13)
                        .HasColumnType("nvarchar(13)");

                    b.Property<string>("Manufacturer")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<string>("Model")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("StockQuantity")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.ToTable("Products");

                    b.HasDiscriminator().HasValue("Product");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("Entities.Models.GraphicsCard", b =>
                {
                    b.HasBaseType("Entities.Models.Product");

                    b.Property<string>("BaseClockSpeed")
                        .IsRequired()
                        .HasMaxLength(9)
                        .HasColumnType("nvarchar(9)");

                    b.Property<byte>("ConnectorPins")
                        .HasColumnType("tinyint");

                    b.Property<string>("Distributor")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<bool>("IsSupportRtx")
                        .HasColumnType("bit");

                    b.Property<string>("MaxClockSpeed")
                        .IsRequired()
                        .HasMaxLength(9)
                        .HasColumnType("nvarchar(9)");

                    b.Property<string>("MemoryClockSpeed")
                        .IsRequired()
                        .HasMaxLength(9)
                        .HasColumnType("nvarchar(9)");

                    b.HasDiscriminator().HasValue("GraphicsCard");

                    b.HasData(
                        new
                        {
                            Id = new Guid("b5d628f0-d4e2-4d63-920d-9aeaae84c418"),
                            CategoryId = new Guid("9e768b71-f27d-426c-ac3d-41e7a7c26729"),
                            Manufacturer = "NVIDIA",
                            Model = "RTX 2060 Super",
                            Price = 262.17m,
                            StockQuantity = 17,
                            BaseClockSpeed = "1470",
                            ConnectorPins = (byte)8,
                            Distributor = "MSI",
                            IsSupportRtx = true,
                            MaxClockSpeed = "1650",
                            MemoryClockSpeed = "14000"
                        },
                        new
                        {
                            Id = new Guid("5280896c-12eb-49fc-a011-1bee7b7032f0"),
                            CategoryId = new Guid("9e768b71-f27d-426c-ac3d-41e7a7c26729"),
                            Manufacturer = "AMD",
                            Model = "RX 580",
                            Price = 80.39m,
                            StockQuantity = 3,
                            BaseClockSpeed = "1210",
                            ConnectorPins = (byte)14,
                            Distributor = "Sapphire",
                            IsSupportRtx = false,
                            MaxClockSpeed = "1411",
                            MemoryClockSpeed = "8000"
                        },
                        new
                        {
                            Id = new Guid("50ea126c-b789-4a14-bb74-153ef1cb018b"),
                            CategoryId = new Guid("9e768b71-f27d-426c-ac3d-41e7a7c26729"),
                            Manufacturer = "NVIDIA",
                            Model = "RTX 4080 Super",
                            Price = 1139.99m,
                            StockQuantity = 21,
                            BaseClockSpeed = "2295",
                            ConnectorPins = (byte)16,
                            Distributor = "MSI",
                            IsSupportRtx = true,
                            MaxClockSpeed = "2595",
                            MemoryClockSpeed = "23000"
                        });
                });

            modelBuilder.Entity("Entities.Models.Product", b =>
                {
                    b.HasOne("Entities.Models.Category", "Category")
                        .WithMany("Products")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");
                });

            modelBuilder.Entity("Entities.Models.Category", b =>
                {
                    b.Navigation("Products");
                });
#pragma warning restore 612, 618
        }
    }
}
