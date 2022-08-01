using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TestProject.Data.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Colors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedByUser = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAtTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedByUser = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedAtTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedByUser = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeletedAtTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Colors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmailAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Cars",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ColorId = table.Column<int>(type: "int", nullable: false),
                    BrandName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedByUser = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAtTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedByUser = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedAtTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedByUser = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeletedAtTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cars", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cars_Colors_ColorId",
                        column: x => x.ColorId,
                        principalTable: "Colors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Colors",
                columns: new[] { "Id", "CreatedAtTime", "CreatedByUser", "DeletedAtTime", "DeletedByUser", "IsDeleted", "Name", "UpdatedAtTime", "UpdatedByUser" },
                values: new object[,]
                {
                    { 1, new DateTime(2022, 7, 31, 15, 33, 2, 379, DateTimeKind.Local).AddTicks(6398), null, null, null, false, "Black", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null },
                    { 2, new DateTime(2022, 7, 31, 15, 33, 2, 380, DateTimeKind.Local).AddTicks(2670), null, null, null, false, "Gray", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null },
                    { 3, new DateTime(2022, 7, 31, 15, 33, 2, 380, DateTimeKind.Local).AddTicks(2680), null, null, null, false, "Blue", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null },
                    { 4, new DateTime(2022, 7, 31, 15, 33, 2, 380, DateTimeKind.Local).AddTicks(2682), null, null, null, false, "DarkBlue", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null },
                    { 5, new DateTime(2022, 7, 31, 15, 33, 2, 380, DateTimeKind.Local).AddTicks(2683), null, null, null, false, "White", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "EmailAddress", "Password", "Role", "UserName" },
                values: new object[] { 1, "admin@mail.com", "12345", "admin", "admin" });

            migrationBuilder.InsertData(
                table: "Cars",
                columns: new[] { "Id", "BrandName", "ColorId", "CreatedAtTime", "CreatedByUser", "DeletedAtTime", "DeletedByUser", "IsDeleted", "Name", "UpdatedAtTime", "UpdatedByUser" },
                values: new object[] { 1, "Audi", 4, new DateTime(2022, 7, 31, 15, 33, 2, 381, DateTimeKind.Local).AddTicks(53), null, null, null, false, "Audi A4", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null });

            migrationBuilder.InsertData(
                table: "Cars",
                columns: new[] { "Id", "BrandName", "ColorId", "CreatedAtTime", "CreatedByUser", "DeletedAtTime", "DeletedByUser", "IsDeleted", "Name", "UpdatedAtTime", "UpdatedByUser" },
                values: new object[] { 2, "Mercedes-Benz", 5, new DateTime(2022, 7, 31, 15, 33, 2, 381, DateTimeKind.Local).AddTicks(59), null, null, null, false, "S-class", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null });

            migrationBuilder.CreateIndex(
                name: "IX_Cars_ColorId",
                table: "Cars",
                column: "ColorId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cars");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Colors");
        }
    }
}
