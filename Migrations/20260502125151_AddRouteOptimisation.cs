using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LogiTech.Migrations
{
    /// <inheritdoc />
    public partial class AddRouteOptimisation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DeliveryStops_Route_RouteVehicleId",
                table: "DeliveryStops");

            migrationBuilder.DropIndex(
                name: "IX_DeliveryStops_RouteVehicleId",
                table: "DeliveryStops");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Route",
                table: "Route");

            migrationBuilder.DropColumn(
                name: "RouteVehicleId",
                table: "DeliveryStops");

            migrationBuilder.RenameTable(
                name: "Route",
                newName: "Routes");

            migrationBuilder.RenameColumn(
                name: "Order",
                table: "DeliveryStops",
                newName: "OptimalOrder");

            migrationBuilder.RenameColumn(
                name: "DistanceKm",
                table: "DeliveryStops",
                newName: "Longitude");

            migrationBuilder.AlterColumn<string>(
                name: "ETA",
                table: "DeliveryStops",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "DeliveryStops",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CustomerName",
                table: "DeliveryStops",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "DistanceFromPrevKm",
                table: "DeliveryStops",
                type: "decimal(10,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<bool>(
                name: "IsCompleted",
                table: "DeliveryStops",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<double>(
                name: "Latitude",
                table: "DeliveryStops",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<int>(
                name: "RouteId",
                table: "DeliveryStops",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TrackingNumber",
                table: "DeliveryStops",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<decimal>(
                name: "FuelSaved",
                table: "Routes",
                type: "decimal(10,2)",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AlterColumn<decimal>(
                name: "DistanceSaved",
                table: "Routes",
                type: "decimal(10,2)",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AlterColumn<decimal>(
                name: "CapacityPercent",
                table: "Routes",
                type: "decimal(5,2)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "VehicleId",
                table: "Routes",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Routes",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Routes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Routes",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Routes",
                table: "Routes",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_DeliveryStops_RouteId",
                table: "DeliveryStops",
                column: "RouteId");

            migrationBuilder.AddForeignKey(
                name: "FK_DeliveryStops_Routes_RouteId",
                table: "DeliveryStops",
                column: "RouteId",
                principalTable: "Routes",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DeliveryStops_Routes_RouteId",
                table: "DeliveryStops");

            migrationBuilder.DropIndex(
                name: "IX_DeliveryStops_RouteId",
                table: "DeliveryStops");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Routes",
                table: "Routes");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "DeliveryStops");

            migrationBuilder.DropColumn(
                name: "CustomerName",
                table: "DeliveryStops");

            migrationBuilder.DropColumn(
                name: "DistanceFromPrevKm",
                table: "DeliveryStops");

            migrationBuilder.DropColumn(
                name: "IsCompleted",
                table: "DeliveryStops");

            migrationBuilder.DropColumn(
                name: "Latitude",
                table: "DeliveryStops");

            migrationBuilder.DropColumn(
                name: "RouteId",
                table: "DeliveryStops");

            migrationBuilder.DropColumn(
                name: "TrackingNumber",
                table: "DeliveryStops");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Routes");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Routes");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Routes");

            migrationBuilder.RenameTable(
                name: "Routes",
                newName: "Route");

            migrationBuilder.RenameColumn(
                name: "OptimalOrder",
                table: "DeliveryStops",
                newName: "Order");

            migrationBuilder.RenameColumn(
                name: "Longitude",
                table: "DeliveryStops",
                newName: "DistanceKm");

            migrationBuilder.AlterColumn<string>(
                name: "ETA",
                table: "DeliveryStops",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RouteVehicleId",
                table: "DeliveryStops",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "VehicleId",
                table: "Route",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<double>(
                name: "FuelSaved",
                table: "Route",
                type: "float",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,2)");

            migrationBuilder.AlterColumn<double>(
                name: "DistanceSaved",
                table: "Route",
                type: "float",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,2)");

            migrationBuilder.AlterColumn<int>(
                name: "CapacityPercent",
                table: "Route",
                type: "int",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(5,2)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Route",
                table: "Route",
                column: "VehicleId");

            migrationBuilder.CreateIndex(
                name: "IX_DeliveryStops_RouteVehicleId",
                table: "DeliveryStops",
                column: "RouteVehicleId");

            migrationBuilder.AddForeignKey(
                name: "FK_DeliveryStops_Route_RouteVehicleId",
                table: "DeliveryStops",
                column: "RouteVehicleId",
                principalTable: "Route",
                principalColumn: "VehicleId");
        }
    }
}
