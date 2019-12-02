﻿// <auto-generated />
using System;
using GoViatic.Web.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace GoViatic.Web.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.11-servicing-32099")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("GoViatic.Web.Data.Entities.Traveler", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Company")
                        .HasMaxLength(50);

                    b.Property<string>("Name")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("Travelers");
                });

            modelBuilder.Entity("GoViatic.Web.Data.Entities.Trip", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("City");

                    b.Property<DateTime>("Date");

                    b.Property<int?>("TravelerId");

                    b.HasKey("Id");

                    b.HasIndex("TravelerId");

                    b.ToTable("Trips");
                });

            modelBuilder.Entity("GoViatic.Web.Data.Entities.Viatic", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<string>("ImageUrl");

                    b.Property<DateTime>("InvoiceDate");

                    b.Property<int?>("TravelerId");

                    b.Property<int?>("TripId");

                    b.Property<string>("ViaticName");

                    b.Property<int?>("ViaticTypeId");

                    b.HasKey("Id");

                    b.HasIndex("TravelerId");

                    b.HasIndex("TripId");

                    b.HasIndex("ViaticTypeId");

                    b.ToTable("Viatics");
                });

            modelBuilder.Entity("GoViatic.Web.Data.Entities.ViaticType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Concept")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("ViaticTypes");
                });

            modelBuilder.Entity("GoViatic.Web.Data.Entities.Trip", b =>
                {
                    b.HasOne("GoViatic.Web.Data.Entities.Traveler", "Traveler")
                        .WithMany("Trips")
                        .HasForeignKey("TravelerId");
                });

            modelBuilder.Entity("GoViatic.Web.Data.Entities.Viatic", b =>
                {
                    b.HasOne("GoViatic.Web.Data.Entities.Traveler", "Traveler")
                        .WithMany("Viatics")
                        .HasForeignKey("TravelerId");

                    b.HasOne("GoViatic.Web.Data.Entities.Trip", "Trip")
                        .WithMany("Viatics")
                        .HasForeignKey("TripId");

                    b.HasOne("GoViatic.Web.Data.Entities.ViaticType", "ViaticType")
                        .WithMany("Viatics")
                        .HasForeignKey("ViaticTypeId");
                });
#pragma warning restore 612, 618
        }
    }
}
