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

            modelBuilder.Entity("Entities.Models.Benchmark", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("GameName")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("nvarchar(64)");

                    b.Property<int>("Resolution")
                        .HasColumnType("int");

                    b.Property<int>("Settings")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Benchmarks");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            GameName = "Cyberpunk 2077",
                            Resolution = 0,
                            Settings = 0
                        },
                        new
                        {
                            Id = 2,
                            GameName = "Cyberpunk 2077",
                            Resolution = 1,
                            Settings = 0
                        },
                        new
                        {
                            Id = 3,
                            GameName = "Cyberpunk 2077",
                            Resolution = 2,
                            Settings = 0
                        },
                        new
                        {
                            Id = 4,
                            GameName = "Horizon: Zero Dawn",
                            Resolution = 0,
                            Settings = 0
                        },
                        new
                        {
                            Id = 5,
                            GameName = "Horizon: Zero Dawn",
                            Resolution = 1,
                            Settings = 0
                        },
                        new
                        {
                            Id = 6,
                            GameName = "Horizon: Zero Dawn",
                            Resolution = 1,
                            Settings = 0
                        });
                });

            modelBuilder.Entity("Entities.Models.GraphicsCard", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

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

                    b.Property<string>("Manufacturer")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<string>("MaxClockSpeed")
                        .IsRequired()
                        .HasMaxLength(9)
                        .HasColumnType("nvarchar(9)");

                    b.Property<string>("MemoryClockSpeed")
                        .IsRequired()
                        .HasMaxLength(9)
                        .HasColumnType("nvarchar(9)");

                    b.Property<string>("Model")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<decimal>("Price")
                        .HasPrecision(10, 2)
                        .HasColumnType("decimal(10,2)");

                    b.Property<int>("StockQuantity")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("GraphicsCards");

                    b.HasData(
                        new
                        {
                            Id = new Guid("b5d628f0-d4e2-4d63-920d-9aeaae84c418"),
                            BaseClockSpeed = "1470",
                            ConnectorPins = (byte)8,
                            Distributor = "MSI",
                            IsSupportRtx = true,
                            Manufacturer = "NVIDIA",
                            MaxClockSpeed = "1650",
                            MemoryClockSpeed = "14000",
                            Model = "RTX 2060 Super",
                            Price = 262.17m,
                            StockQuantity = 17
                        },
                        new
                        {
                            Id = new Guid("5280896c-12eb-49fc-a011-1bee7b7032f0"),
                            BaseClockSpeed = "1210",
                            ConnectorPins = (byte)14,
                            Distributor = "Sapphire",
                            IsSupportRtx = false,
                            Manufacturer = "AMD",
                            MaxClockSpeed = "1411",
                            MemoryClockSpeed = "8000",
                            Model = "RX 580",
                            Price = 80.39m,
                            StockQuantity = 3
                        },
                        new
                        {
                            Id = new Guid("50ea126c-b789-4a14-bb74-153ef1cb018b"),
                            BaseClockSpeed = "2295",
                            ConnectorPins = (byte)16,
                            Distributor = "MSI",
                            IsSupportRtx = true,
                            Manufacturer = "NVIDIA",
                            MaxClockSpeed = "2595",
                            MemoryClockSpeed = "23000",
                            Model = "RTX 4080 Super",
                            Price = 1139.99m,
                            StockQuantity = 21
                        });
                });

            modelBuilder.Entity("Entities.Models.GraphicsCardBenchmark", b =>
                {
                    b.Property<Guid>("GraphicsCardId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("BenchmarkId")
                        .HasColumnType("int");

                    b.Property<decimal>("Fps")
                        .HasPrecision(4, 1)
                        .HasColumnType("decimal(4,1)");

                    b.HasKey("GraphicsCardId", "BenchmarkId");

                    b.HasIndex("BenchmarkId");

                    b.ToTable("GraphicsCardBenchmarks");

                    b.HasData(
                        new
                        {
                            GraphicsCardId = new Guid("b5d628f0-d4e2-4d63-920d-9aeaae84c418"),
                            BenchmarkId = 1,
                            Fps = 96.7m
                        },
                        new
                        {
                            GraphicsCardId = new Guid("b5d628f0-d4e2-4d63-920d-9aeaae84c418"),
                            BenchmarkId = 2,
                            Fps = 112.3m
                        },
                        new
                        {
                            GraphicsCardId = new Guid("b5d628f0-d4e2-4d63-920d-9aeaae84c418"),
                            BenchmarkId = 3,
                            Fps = 164.2m
                        },
                        new
                        {
                            GraphicsCardId = new Guid("5280896c-12eb-49fc-a011-1bee7b7032f0"),
                            BenchmarkId = 4,
                            Fps = 24.3m
                        },
                        new
                        {
                            GraphicsCardId = new Guid("5280896c-12eb-49fc-a011-1bee7b7032f0"),
                            BenchmarkId = 5,
                            Fps = 48.5m
                        },
                        new
                        {
                            GraphicsCardId = new Guid("5280896c-12eb-49fc-a011-1bee7b7032f0"),
                            BenchmarkId = 6,
                            Fps = 76.1m
                        },
                        new
                        {
                            GraphicsCardId = new Guid("5280896c-12eb-49fc-a011-1bee7b7032f0"),
                            BenchmarkId = 3,
                            Fps = 63.4m
                        });
                });

            modelBuilder.Entity("Entities.Models.GraphicsCardBenchmark", b =>
                {
                    b.HasOne("Entities.Models.Benchmark", "Benchmark")
                        .WithMany("GraphicsCardBenchmarks")
                        .HasForeignKey("BenchmarkId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Entities.Models.GraphicsCard", "GraphicsCard")
                        .WithMany("GraphicsCardBenchmarks")
                        .HasForeignKey("GraphicsCardId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Benchmark");

                    b.Navigation("GraphicsCard");
                });

            modelBuilder.Entity("Entities.Models.Benchmark", b =>
                {
                    b.Navigation("GraphicsCardBenchmarks");
                });

            modelBuilder.Entity("Entities.Models.GraphicsCard", b =>
                {
                    b.Navigation("GraphicsCardBenchmarks");
                });
#pragma warning restore 612, 618
        }
    }
}
