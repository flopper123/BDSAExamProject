﻿// <auto-generated />
using LitExplore.Entity;
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

            modelBuilder.Entity("LitExplore.Entity.Publication", b =>
                {
                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Title");

                    b.HasIndex("Title")
                        .IsUnique();

                    b.ToTable("Publications");
                });

            modelBuilder.Entity("LitExplore.Entity.Reference", b =>
                {
                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Title");

                    b.HasIndex("Title")
                        .IsUnique();

                    b.ToTable("References");
                });

            modelBuilder.Entity("PublicationReference", b =>
                {
                    b.Property<string>("PublicationsTitle")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ReferencesTitle")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("PublicationsTitle", "ReferencesTitle");

                    b.HasIndex("ReferencesTitle");

                    b.ToTable("PublicationReference");
                });

            modelBuilder.Entity("PublicationReference", b =>
                {
                    b.HasOne("LitExplore.Entity.Publication", null)
                        .WithMany()
                        .HasForeignKey("PublicationsTitle")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LitExplore.Entity.Reference", null)
                        .WithMany()
                        .HasForeignKey("ReferencesTitle")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
