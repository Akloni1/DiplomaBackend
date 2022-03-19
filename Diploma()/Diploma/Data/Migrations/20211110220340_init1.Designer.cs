﻿// <auto-generated />
using System;
using Diploma;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Diploma.Data.Migrations
{
    [DbContext(typeof(BoxContext))]
    [Migration("20211110220340_init1")]
    partial class init1
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.21")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Diploma.Boxers", b =>
                {
                    b.Property<int>("BoxerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("BoxingClubId")
                        .HasColumnType("int");

                    b.Property<int?>("CoachId")
                        .HasColumnName("CoachID")
                        .HasColumnType("int");

                    b.Property<DateTime?>("DateOfBirth")
                        .HasColumnType("datetime");

                    b.Property<string>("Discharge")
                        .HasColumnType("varchar(50)")
                        .HasMaxLength(50)
                        .IsUnicode(false);

                    b.Property<string>("FirstName")
                        .HasColumnType("varchar(50)")
                        .HasMaxLength(50)
                        .IsUnicode(false);

                    b.Property<string>("LastName")
                        .HasColumnType("varchar(50)")
                        .HasMaxLength(50)
                        .IsUnicode(false);

                    b.Property<string>("MiddleName")
                        .HasColumnType("varchar(50)")
                        .HasMaxLength(50)
                        .IsUnicode(false);

                    b.Property<int?>("NumberOfFightsHeld")
                        .HasColumnType("int");

                    b.Property<int?>("NumberOfWins")
                        .HasColumnType("int");

                    b.Property<double?>("TrainingExperience")
                        .HasColumnType("float");

                    b.HasKey("BoxerId");

                    b.HasIndex("BoxingClubId");

                    b.HasIndex("CoachId");

                    b.ToTable("Boxers");
                });

            modelBuilder.Entity("Diploma.BoxingClubs", b =>
                {
                    b.Property<int>("BoxingClubId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClubAddress")
                        .HasColumnType("varchar(100)")
                        .HasMaxLength(100)
                        .IsUnicode(false);

                    b.Property<string>("ClubName")
                        .HasColumnType("varchar(50)")
                        .HasMaxLength(50)
                        .IsUnicode(false);

                    b.HasKey("BoxingClubId");

                    b.ToTable("BoxingClubs");
                });

            modelBuilder.Entity("Diploma.Coaches", b =>
                {
                    b.Property<int>("CoachId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("CoachID")
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("BoxingClubId")
                        .HasColumnType("int");

                    b.Property<string>("FirstName")
                        .HasColumnType("varchar(50)")
                        .HasMaxLength(50)
                        .IsUnicode(false);

                    b.Property<string>("LastName")
                        .HasColumnType("varchar(50)")
                        .HasMaxLength(50)
                        .IsUnicode(false);

                    b.Property<string>("MiddleName")
                        .HasColumnType("varchar(50)")
                        .HasMaxLength(50)
                        .IsUnicode(false);

                    b.Property<string>("SportsTitle")
                        .HasColumnType("varchar(50)")
                        .HasMaxLength(50)
                        .IsUnicode(false);

                    b.HasKey("CoachId");

                    b.HasIndex("BoxingClubId");

                    b.ToTable("Coaches");
                });

            modelBuilder.Entity("Diploma.EmployeesClub", b =>
                {
                    b.Property<int>("EmployeeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("BoxingClubId")
                        .HasColumnType("int");

                    b.Property<string>("FirstName")
                        .HasColumnType("varchar(50)")
                        .HasMaxLength(50)
                        .IsUnicode(false);

                    b.Property<string>("LastName")
                        .HasColumnType("varchar(50)")
                        .HasMaxLength(50)
                        .IsUnicode(false);

                    b.Property<string>("MiddleName")
                        .HasColumnType("varchar(50)")
                        .HasMaxLength(50)
                        .IsUnicode(false);

                    b.Property<string>("Post")
                        .HasColumnType("varchar(50)")
                        .HasMaxLength(50)
                        .IsUnicode(false);

                    b.HasKey("EmployeeId");

                    b.HasIndex("BoxingClubId");

                    b.ToTable("EmployeesClub");
                });

            modelBuilder.Entity("Diploma.Boxers", b =>
                {
                    b.HasOne("Diploma.BoxingClubs", "BoxingClub")
                        .WithMany("Boxers")
                        .HasForeignKey("BoxingClubId")
                        .HasConstraintName("FK_Boxers_BoxingClubs");

                    b.HasOne("Diploma.Coaches", "Coach")
                        .WithMany("Boxers")
                        .HasForeignKey("CoachId")
                        .HasConstraintName("FK_Boxers_Coaches");
                });

            modelBuilder.Entity("Diploma.Coaches", b =>
                {
                    b.HasOne("Diploma.BoxingClubs", "BoxingClub")
                        .WithMany("Coaches")
                        .HasForeignKey("BoxingClubId")
                        .HasConstraintName("FK_Coaches_BoxingClubs");
                });

            modelBuilder.Entity("Diploma.EmployeesClub", b =>
                {
                    b.HasOne("Diploma.BoxingClubs", "BoxingClub")
                        .WithMany("EmployeesClub")
                        .HasForeignKey("BoxingClubId")
                        .HasConstraintName("FK_EmployeesClub_BoxingClubs");
                });
#pragma warning restore 612, 618
        }
    }
}
