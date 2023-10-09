using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OsDsII.api.Migrations
{
    /// <inheritdoc />
    public partial class AddRelationships : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "opening_date",
                table: "service_order",
                type: "datetime(6)",
                rowVersion: true,
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(2023, 10, 8, 23, 6, 59, 467, DateTimeKind.Unspecified).AddTicks(6231), new TimeSpan(0, -3, 0, 0, 0)),
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetime(6)",
                oldRowVersion: true,
                oldDefaultValue: new DateTimeOffset(new DateTime(2023, 9, 4, 17, 8, 4, 696, DateTimeKind.Unspecified).AddTicks(901), new TimeSpan(0, -3, 0, 0, 0)))
                .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.ComputedColumn)
                .OldAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.ComputedColumn);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "opening_date",
                table: "service_order",
                type: "datetime(6)",
                rowVersion: true,
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(2023, 9, 4, 17, 8, 4, 696, DateTimeKind.Unspecified).AddTicks(901), new TimeSpan(0, -3, 0, 0, 0)),
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetime(6)",
                oldRowVersion: true,
                oldDefaultValue: new DateTimeOffset(new DateTime(2023, 10, 8, 23, 6, 59, 467, DateTimeKind.Unspecified).AddTicks(6231), new TimeSpan(0, -3, 0, 0, 0)))
                .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.ComputedColumn)
                .OldAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.ComputedColumn);
        }
    }
}
