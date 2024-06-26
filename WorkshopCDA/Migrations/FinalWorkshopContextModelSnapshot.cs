﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WorkshopCDA.Data;

#nullable disable

namespace WorkshopCDA.Migrations
{
    [DbContext(typeof(FinalWorkshopContext))]
    partial class FinalWorkshopContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseCollation("utf8_general_ci")
                .HasAnnotation("ProductVersion", "8.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            MySqlModelBuilderExtensions.HasCharSet(modelBuilder, "utf8");
            MySqlModelBuilderExtensions.AutoIncrementColumns(modelBuilder);

            modelBuilder.Entity("WorkshopCDA.Models.Animal", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int(11)");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.Property<int>("RaceId")
                        .HasColumnType("int(11)");

                    b.HasKey("Id")
                        .HasName("PRIMARY");

                    b.HasIndex("RaceId");

                    b.ToTable("animal", (string)null);
                });

            modelBuilder.Entity("WorkshopCDA.Models.Race", b =>
                {
                    b.Property<int>("RaceId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int(11)");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("RaceId"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.HasKey("RaceId")
                        .HasName("PRIMARY");

                    b.ToTable("race", (string)null);
                });

            modelBuilder.Entity("WorkshopCDA.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int(11)");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.HasKey("Id")
                        .HasName("PRIMARY");

                    b.ToTable("users", (string)null);
                });

            modelBuilder.Entity("WorkshopCDA.Models.Animal", b =>
                {
                    b.HasOne("WorkshopCDA.Models.Race", "Race")
                        .WithMany("Animals")
                        .HasForeignKey("RaceId")
                        .IsRequired()
                        .HasConstraintName("FK_Animal_Race");

                    b.Navigation("Race");
                });

            modelBuilder.Entity("WorkshopCDA.Models.Race", b =>
                {
                    b.Navigation("Animals");
                });
#pragma warning restore 612, 618
        }
    }
}
