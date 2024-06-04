﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using cd_collection.infrastructure.DataAccessLayer;

#nullable disable

namespace cd_collection.infrastructure.DataAccessLayer.Migrations
{
    [DbContext(typeof(CdCollectionDbContext))]
    [Migration("20240604042954_ChangeCdItemReleaseDateToDateOnly")]
    partial class ChangeCdItemReleaseDateToDateOnly
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.17")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("CdItemCollection", b =>
                {
                    b.Property<Guid>("CdItemsId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("CollectionsId")
                        .HasColumnType("uuid");

                    b.HasKey("CdItemsId", "CollectionsId");

                    b.HasIndex("CollectionsId");

                    b.ToTable("CdItemCollection");
                });

            modelBuilder.Entity("cd_collection.core.Entities.CdItem", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<string>("Artist")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Label")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("LastUpdate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateOnly>("ReleaseDate")
                        .HasColumnType("date");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("CdItems");
                });

            modelBuilder.Entity("cd_collection.core.Entities.Collection", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("LastUpdate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Collections");
                });

            modelBuilder.Entity("CdItemCollection", b =>
                {
                    b.HasOne("cd_collection.core.Entities.CdItem", null)
                        .WithMany()
                        .HasForeignKey("CdItemsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("cd_collection.core.Entities.Collection", null)
                        .WithMany()
                        .HasForeignKey("CollectionsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
