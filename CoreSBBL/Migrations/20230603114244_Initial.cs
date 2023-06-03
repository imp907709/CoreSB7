using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CoreSBBL.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LoggingLabel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedById = table.Column<int>(type: "int", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("LoggingLabel_Id", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LoggingTags",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedById = table.Column<int>(type: "int", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    index = table.Column<int>(type: "int", nullable: false),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("LoggingTags_Id", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Logging",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LabelId = table.Column<int>(type: "int", nullable: true),
                    CreatedById = table.Column<int>(type: "int", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Logging_Id", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Logging_LoggingLabel_LabelId",
                        column: x => x.LabelId,
                        principalTable: "LoggingLabel",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "LoggingToTags",
                columns: table => new
                {
                    LogId = table.Column<int>(type: "int", nullable: false),
                    TagsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoggingToTags", x => new { x.LogId, x.TagsId });
                    table.ForeignKey(
                        name: "FK_LoggingToTags_LoggingTags_TagsId",
                        column: x => x.TagsId,
                        principalTable: "LoggingTags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LoggingToTags_Logging_LogId",
                        column: x => x.LogId,
                        principalTable: "Logging",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Logging_LabelId",
                table: "Logging",
                column: "LabelId");

            migrationBuilder.CreateIndex(
                name: "IX_LoggingToTags_TagsId",
                table: "LoggingToTags",
                column: "TagsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LoggingToTags");

            migrationBuilder.DropTable(
                name: "LoggingTags");

            migrationBuilder.DropTable(
                name: "Logging");

            migrationBuilder.DropTable(
                name: "LoggingLabel");
        }
    }
}
