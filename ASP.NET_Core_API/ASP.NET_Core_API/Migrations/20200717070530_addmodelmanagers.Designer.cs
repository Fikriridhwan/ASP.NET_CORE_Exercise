﻿// <auto-generated />
using System;
using ASP.NET_Core_API.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace ASP.NET_Core_API.Migrations
{
    [DbContext(typeof(apiContext))]
    [Migration("20200717070530_addmodelmanagers")]
    partial class addmodelmanagers
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.14-servicing-32113")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ASP.NET_Core_API.Models.Employee", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name");

                    b.Property<int>("Nip");

                    b.Property<int>("annualLeaveRemaining");

                    b.HasKey("Id");

                    b.ToTable("TB_M_Employee");
                });

            modelBuilder.Entity("ASP.NET_Core_API.Models.Manager", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Division");

                    b.Property<int?>("employeeId");

                    b.HasKey("Id");

                    b.HasIndex("employeeId");

                    b.ToTable("TB_R_Manager");
                });

            modelBuilder.Entity("ASP.NET_Core_API.Models.Manager", b =>
                {
                    b.HasOne("ASP.NET_Core_API.Models.Employee", "employee")
                        .WithMany()
                        .HasForeignKey("employeeId");
                });
#pragma warning restore 612, 618
        }
    }
}
