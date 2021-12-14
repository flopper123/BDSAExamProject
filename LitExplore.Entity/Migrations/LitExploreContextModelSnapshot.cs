﻿// <auto-generated />
using System;
using LitExplore.Entity.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace LitExplore.Entity.Migrations
{
    [DbContext(typeof(LitExploreContext))]
    partial class LitExploreContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("LitExplore.Entity.Entities.KeyWord", b =>
                {
                    b.Property<string>("Keyword")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("PublicationTitle")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Keyword");

                    b.HasIndex("PublicationTitle");

                    b.ToTable("KeyWords", (string)null);
                });

            modelBuilder.Entity("LitExplore.Entity.Entities.Publication", b =>
                {
                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Abstract")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Author")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Time")
                        .HasColumnType("datetime2");

                    b.HasKey("Title");

                    b.HasIndex("Title")
                        .IsUnique();

                    b.ToTable("Publications");
                });

            modelBuilder.Entity("LitExplore.Entity.Entities.PublicationTitle", b =>
                {
                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("PublicationTitle")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Title");

                    b.HasIndex("PublicationTitle");

                    b.ToTable("PublicationTitle");
                });

            modelBuilder.Entity("LitExplore.Entity.Entities.UserFilter", b =>
                {
                    b.Property<decimal>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("decimal(20,0)");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<decimal>("UserId"), 1L, 1);

                    b.Property<string>("Serialization")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId");

                    b.ToTable("History");
                });

            modelBuilder.Entity("LitExplore.Entity.Entities.KeyWord", b =>
                {
                    b.HasOne("LitExplore.Entity.Entities.Publication", null)
                        .WithMany("Keywords")
                        .HasForeignKey("PublicationTitle");
                });

            modelBuilder.Entity("LitExplore.Entity.Entities.PublicationTitle", b =>
                {
                    b.HasOne("LitExplore.Entity.Entities.Publication", null)
                        .WithMany("References")
                        .HasForeignKey("PublicationTitle");
                });

            modelBuilder.Entity("LitExplore.Entity.Entities.Publication", b =>
                {
                    b.Navigation("Keywords");

                    b.Navigation("References");
                });
#pragma warning restore 612, 618
        }
    }
}
