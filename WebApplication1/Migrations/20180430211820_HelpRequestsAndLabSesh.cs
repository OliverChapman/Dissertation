using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace WebApplication1.Migrations
{
    public partial class HelpRequestsAndLabSesh : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LabId",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Location",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UserType",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "HelpRequest",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    HelpDesc = table.Column<string>(nullable: true),
                    Status = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HelpRequest", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LabSession",
                columns: table => new
                {
                    LabId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ModuleName = table.Column<string>(nullable: true),
                    ModuleNo = table.Column<string>(nullable: true),
                    RoomName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LabSession", x => x.LabId);
                });

            migrationBuilder.CreateTable(
                name: "UserToRequest",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    HelpRequestId = table.Column<int>(nullable: true),
                    UserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserToRequest", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserToRequest_HelpRequest_HelpRequestId",
                        column: x => x.HelpRequestId,
                        principalTable: "HelpRequest",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserToRequest_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_LabId",
                table: "AspNetUsers",
                column: "LabId");

            migrationBuilder.CreateIndex(
                name: "IX_UserToRequest_HelpRequestId",
                table: "UserToRequest",
                column: "HelpRequestId");

            migrationBuilder.CreateIndex(
                name: "IX_UserToRequest_UserId",
                table: "UserToRequest",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_LabSession_LabId",
                table: "AspNetUsers",
                column: "LabId",
                principalTable: "LabSession",
                principalColumn: "LabId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_LabSession_LabId",
                table: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "LabSession");

            migrationBuilder.DropTable(
                name: "UserToRequest");

            migrationBuilder.DropTable(
                name: "HelpRequest");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_LabId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "LabId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Location",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "UserType",
                table: "AspNetUsers");
        }
    }
}
