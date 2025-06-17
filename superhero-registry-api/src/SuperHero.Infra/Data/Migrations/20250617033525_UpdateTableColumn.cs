using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SuperHero.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateTableColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "AtualizadoEm",
                schema: "SuperHerois",
                table: "Herois",
                type: "timestamp",
                nullable: false,
                defaultValueSql: "CURRENT_TIMESTAMP");

            migrationBuilder.AddColumn<DateTime>(
                name: "CriadoEm",
                schema: "SuperHerois",
                table: "Herois",
                type: "timestamp",
                nullable: false,
                defaultValueSql: "CURRENT_TIMESTAMP");

            migrationBuilder.AddColumn<bool>(
                name: "Desativado",
                schema: "SuperHerois",
                table: "Herois",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AtualizadoEm",
                schema: "SuperHerois",
                table: "Herois");

            migrationBuilder.DropColumn(
                name: "CriadoEm",
                schema: "SuperHerois",
                table: "Herois");

            migrationBuilder.DropColumn(
                name: "Desativado",
                schema: "SuperHerois",
                table: "Herois");
        }
    }
}
