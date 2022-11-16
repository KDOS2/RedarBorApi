using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RedarborDataBase.Migrations
{
    /// <inheritdoc />
    public partial class AleterTableEmployees : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedOn",
                schema: "Redarbor",
                table: "Employees",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "(getdate())",
                oldClrType: typeof(DateTime),
                oldType: "datetime2");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedOn",
                schema: "Redarbor",
                table: "Employees",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "(getdate())");
        }
    }
}
