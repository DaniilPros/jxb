﻿// <auto-generated />
using System;
using JXB.Api.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace JXB.Api.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20191026214328_ModelMigration2")]
    partial class ModelMigration2
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("JXB.Api.Data.Model.Activity", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("MaxUsersCount")
                        .HasColumnType("int");

                    b.Property<int>("MinUsersCount")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<float>("Rating")
                        .HasColumnType("real");

                    b.HasKey("Id");

                    b.ToTable("Activity");
                });

            modelBuilder.Entity("JXB.Api.Data.Model.DActivity", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ActivityId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTimeOffset?>("End")
                        .HasColumnType("datetimeoffset");

                    b.Property<DateTimeOffset>("Start")
                        .HasColumnType("datetimeoffset");

                    b.HasKey("Id");

                    b.HasIndex("ActivityId");

                    b.ToTable("DActivity");
                });

            modelBuilder.Entity("JXB.Api.Data.Model.DInterest", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("DUserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("InterestId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("DUserId");

                    b.HasIndex("InterestId");

                    b.HasIndex("UserId");

                    b.ToTable("DInterest");
                });

            modelBuilder.Entity("JXB.Api.Data.Model.DUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("DActivityId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("Rating")
                        .HasColumnType("int");

                    b.Property<string>("ResponsibilityId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("DActivityId");

                    b.HasIndex("ResponsibilityId");

                    b.HasIndex("UserId");

                    b.ToTable("DUser");
                });

            modelBuilder.Entity("JXB.Api.Data.Model.Interest", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Interest");
                });

            modelBuilder.Entity("JXB.Api.Data.Model.Responsibility", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ActivityId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ActivityId");

                    b.ToTable("Responsibility");
                });

            modelBuilder.Entity("JXB.Api.Data.Model.User", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("JXB.Api.Data.Model.DActivity", b =>
                {
                    b.HasOne("JXB.Api.Data.Model.Activity", "Activity")
                        .WithMany("DActivities")
                        .HasForeignKey("ActivityId");
                });

            modelBuilder.Entity("JXB.Api.Data.Model.DInterest", b =>
                {
                    b.HasOne("JXB.Api.Data.Model.DUser", null)
                        .WithMany("DInterests")
                        .HasForeignKey("DUserId");

                    b.HasOne("JXB.Api.Data.Model.Interest", "Interest")
                        .WithMany("DInterests")
                        .HasForeignKey("InterestId");

                    b.HasOne("JXB.Api.Data.Model.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("JXB.Api.Data.Model.DUser", b =>
                {
                    b.HasOne("JXB.Api.Data.Model.DActivity", "DActivity")
                        .WithMany("DUsers")
                        .HasForeignKey("DActivityId");

                    b.HasOne("JXB.Api.Data.Model.Responsibility", "Responsibility")
                        .WithMany("DUsers")
                        .HasForeignKey("ResponsibilityId");

                    b.HasOne("JXB.Api.Data.Model.User", "User")
                        .WithMany("DUsers")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("JXB.Api.Data.Model.Responsibility", b =>
                {
                    b.HasOne("JXB.Api.Data.Model.Activity", "Activity")
                        .WithMany("Responsibilities")
                        .HasForeignKey("ActivityId");
                });
#pragma warning restore 612, 618
        }
    }
}
