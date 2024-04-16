﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using cd_collection.infrastructure.DataAccessLayer;

#nullable disable

namespace cd_collection.infrastructure.DataAccessLayer.Migrations
{
    [DbContext(typeof(CDCollectionDbContext))]
    partial class CDCollectionDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.17")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("cd_collection.core.Entities.CdItem", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<string>("Artist")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid?>("CollectionId")
                        .HasColumnType("uuid");

                    b.Property<string>("Label")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("LastUpdate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("ReleaseDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("CollectionId");

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

            modelBuilder.Entity("cd_collection.core.Entities.CollectionCdItem", b =>
                {
                    b.Property<Guid>("ItemId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("CollectionId")
                        .HasColumnType("uuid");

                    b.HasKey("ItemId", "CollectionId");

                    b.HasIndex("CollectionId");

                    b.ToTable("CollectionCdItems");
                });

            modelBuilder.Entity("cd_collection.core.Entities.CdItem", b =>
                {
                    b.HasOne("cd_collection.core.Entities.Collection", null)
                        .WithMany("Items")
                        .HasForeignKey("CollectionId");
                });

            modelBuilder.Entity("cd_collection.core.Entities.CollectionCdItem", b =>
                {
                    b.HasOne("cd_collection.core.Entities.Collection", "Collection")
                        .WithMany("CollectionCdItems")
                        .HasForeignKey("CollectionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("cd_collection.core.Entities.CdItem", "Item")
                        .WithMany("CollectionCdItems")
                        .HasForeignKey("ItemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Collection");

                    b.Navigation("Item");
                });

            modelBuilder.Entity("cd_collection.core.Entities.CdItem", b =>
                {
                    b.Navigation("CollectionCdItems");
                });

            modelBuilder.Entity("cd_collection.core.Entities.Collection", b =>
                {
                    b.Navigation("CollectionCdItems");

                    b.Navigation("Items");
                });
#pragma warning restore 612, 618
        }
    }
}
