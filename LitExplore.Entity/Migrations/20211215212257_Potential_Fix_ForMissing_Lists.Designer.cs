﻿// <auto-generated />
using System;
using LitExplore.Entity.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace LitExplore.Entity.Migrations
{
    [DbContext(typeof(LitExploreContext))]
    [Migration("20211215212257_Potential_Fix_ForMissing_Lists")]
    partial class Potential_Fix_ForMissing_Lists
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
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

                    b.Property<string>("PublicationId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Keyword");

                    b.HasIndex("PublicationId");

                    b.ToTable("KeyWords");
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

                    b.Property<string>("PublicationId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Title");

                    b.HasIndex("PublicationId");

                    b.ToTable("References");
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
                    b.HasOne("LitExplore.Entity.Entities.Publication", "Publication")
                        .WithMany("Keywords")
                        .HasForeignKey("PublicationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Publication");
                });

            modelBuilder.Entity("LitExplore.Entity.Entities.PublicationTitle", b =>
                {
                    b.HasOne("LitExplore.Entity.Entities.Publication", "Publication")
                        .WithMany("References")
                        .HasForeignKey("PublicationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Publication");
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
