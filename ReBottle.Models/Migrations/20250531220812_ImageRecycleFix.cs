using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ReBottle.Models.Migrations
{
    /// <inheritdoc />
    public partial class ImageRecycleFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ImagesStorage_RecyclingRecords_RecyclingRecordId",
                table: "ImagesStorage");

            migrationBuilder.DropIndex(
                name: "IX_ImagesStorage_RecyclingRecordId",
                table: "ImagesStorage");

            migrationBuilder.DropColumn(
                name: "RecyclingRecordId",
                table: "ImagesStorage");

            migrationBuilder.AddColumn<Guid>(
                name: "ImageId",
                table: "RecyclingRecords",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_RecyclingRecords_ImageId",
                table: "RecyclingRecords",
                column: "ImageId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_RecyclingRecords_ImagesStorage_ImageId",
                table: "RecyclingRecords",
                column: "ImageId",
                principalTable: "ImagesStorage",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RecyclingRecords_ImagesStorage_ImageId",
                table: "RecyclingRecords");

            migrationBuilder.DropIndex(
                name: "IX_RecyclingRecords_ImageId",
                table: "RecyclingRecords");

            migrationBuilder.DropColumn(
                name: "ImageId",
                table: "RecyclingRecords");

            migrationBuilder.AddColumn<Guid>(
                name: "RecyclingRecordId",
                table: "ImagesStorage",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_ImagesStorage_RecyclingRecordId",
                table: "ImagesStorage",
                column: "RecyclingRecordId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_RecyclingRecords_ImagesStorage_ImageId",
                table: "RecyclingRecords",
                column: "ImageId",
                principalTable: "ImagesStorage",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict); 

        }
    }
}
