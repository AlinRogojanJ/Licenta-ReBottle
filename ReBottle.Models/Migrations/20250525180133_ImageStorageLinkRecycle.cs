using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ReBottle.Models.Migrations
{
    /// <inheritdoc />
    public partial class ImageStorageLinkRecycle : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // 1) make it nullable
            migrationBuilder.AddColumn<Guid>(
                name: "RecyclingRecordId",
                table: "ImagesStorage",
                type: "uniqueidentifier",
                nullable: true);

            // 2) filtered unique index
            migrationBuilder.CreateIndex(
                name: "IX_ImagesStorage_RecyclingRecordId",
                table: "ImagesStorage",
                column: "RecyclingRecordId",
                unique: true,
                filter: "[RecyclingRecordId] IS NOT NULL");

            // 3) add FK
            migrationBuilder.AddForeignKey(
                name: "FK_ImagesStorage_RecyclingRecords_RecyclingRecordId",
            table: "ImagesStorage",
            column: "RecyclingRecordId",
            principalTable: "RecyclingRecords",
            principalColumn: "RecyclingRecordId",
            onDelete: ReferentialAction.Restrict);
        }


        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
        }
    }
}
