﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using SuperHero.Infra.Context;

#nullable disable

namespace SuperHero.Infra.Data.Migrations
{
    [DbContext(typeof(SuperHeroDbContext))]
    [Migration("20250617033525_UpdateTableColumn")]
    partial class UpdateTableColumn
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("SuperHerois")
                .HasAnnotation("ProductVersion", "9.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("SuperHero.Domain.Entities.Hero.Heroi", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<float>("Altura")
                        .HasColumnType("real");

                    b.Property<DateTime>("AtualizadoEm")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");

                    b.Property<DateTime>("CriadoEm")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");

                    b.Property<DateTime?>("DataNascimento")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool>("Desativado")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("boolean")
                        .HasDefaultValue(false);

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("NomeHeroi")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<float>("Peso")
                        .HasColumnType("real");

                    b.HasKey("Id");

                    b.ToTable("Herois", "SuperHerois");
                });

            modelBuilder.Entity("SuperHero.Domain.Entities.Hero.HeroiSuperPoder", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("HeroiId")
                        .HasColumnType("integer");

                    b.Property<int>("SuperPoderId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("HeroiId");

                    b.HasIndex("SuperPoderId");

                    b.ToTable("HeroisSuperPoderes", "SuperHerois");
                });

            modelBuilder.Entity("SuperHero.Domain.Entities.Hero.SuperPoder", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Descricao")
                        .HasColumnType("text");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("SuperPoderes", "SuperHerois");
                });

            modelBuilder.Entity("SuperHero.Domain.Entities.Hero.HeroiSuperPoder", b =>
                {
                    b.HasOne("SuperHero.Domain.Entities.Hero.Heroi", "Heroi")
                        .WithMany("HeroisSuperPoderes")
                        .HasForeignKey("HeroiId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SuperHero.Domain.Entities.Hero.SuperPoder", "SuperPoder")
                        .WithMany("HeroisSuperPoderes")
                        .HasForeignKey("SuperPoderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Heroi");

                    b.Navigation("SuperPoder");
                });

            modelBuilder.Entity("SuperHero.Domain.Entities.Hero.Heroi", b =>
                {
                    b.Navigation("HeroisSuperPoderes");
                });

            modelBuilder.Entity("SuperHero.Domain.Entities.Hero.SuperPoder", b =>
                {
                    b.Navigation("HeroisSuperPoderes");
                });
#pragma warning restore 612, 618
        }
    }
}
