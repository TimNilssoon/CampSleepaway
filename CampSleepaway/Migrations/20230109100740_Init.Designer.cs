﻿// <auto-generated />
using System;
using CampSleepaway.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CampSleepaway.Migrations
{
    [DbContext(typeof(CampSleepawayContext))]
    [Migration("20230109100740_Init")]
    partial class Init
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("CampSleepaway.Model.Cabin", b =>
                {
                    b.Property<int>("CabinId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CabinId"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CabinId");

                    b.ToTable("Cabin");
                });

            modelBuilder.Entity("CampSleepaway.Model.Camper", b =>
                {
                    b.Property<int>("CamperId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CamperId"));

                    b.Property<int>("CabinId")
                        .HasColumnType("int");

                    b.Property<DateTime>("DateOfBirth")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("EndTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.HasKey("CamperId");

                    b.HasIndex("CabinId");

                    b.ToTable("Camper");
                });

            modelBuilder.Entity("CampSleepaway.Model.CamperNextOfKin", b =>
                {
                    b.Property<int>("CamperId")
                        .HasColumnType("int");

                    b.Property<int>("NextOfKinId")
                        .HasColumnType("int");

                    b.HasKey("CamperId", "NextOfKinId");

                    b.HasIndex("NextOfKinId");

                    b.ToTable("CamperNextOfKin");
                });

            modelBuilder.Entity("CampSleepaway.Model.Councelor", b =>
                {
                    b.Property<int>("CouncelorId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CouncelorId"));

                    b.Property<int>("CabinId")
                        .HasColumnType("int");

                    b.Property<DateTime>("EndTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("MyProperty")
                        .HasColumnType("int");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.HasKey("CouncelorId");

                    b.HasIndex("CabinId")
                        .IsUnique();

                    b.ToTable("Councelor");
                });

            modelBuilder.Entity("CampSleepaway.Model.NextOfKin", b =>
                {
                    b.Property<int>("NextOfKinId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("NextOfKinId"));

                    b.Property<DateTime>("EndTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RelationType")
                        .HasColumnType("int");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.HasKey("NextOfKinId");

                    b.ToTable("NextOfKin");
                });

            modelBuilder.Entity("CampSleepaway.Model.Camper", b =>
                {
                    b.HasOne("CampSleepaway.Model.Cabin", null)
                        .WithMany("Campers")
                        .HasForeignKey("CabinId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("CampSleepaway.Model.CamperNextOfKin", b =>
                {
                    b.HasOne("CampSleepaway.Model.Camper", "Camper")
                        .WithMany()
                        .HasForeignKey("CamperId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CampSleepaway.Model.NextOfKin", "NextOfKin")
                        .WithMany()
                        .HasForeignKey("NextOfKinId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Camper");

                    b.Navigation("NextOfKin");
                });

            modelBuilder.Entity("CampSleepaway.Model.Councelor", b =>
                {
                    b.HasOne("CampSleepaway.Model.Cabin", null)
                        .WithOne("Councelor")
                        .HasForeignKey("CampSleepaway.Model.Councelor", "CabinId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("CampSleepaway.Model.Cabin", b =>
                {
                    b.Navigation("Campers");

                    b.Navigation("Councelor")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
