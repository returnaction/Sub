using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sub.Migrations
{
    /// <inheritdoc />
    public partial class CompanyAndEmployees : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Companies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Info = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NameLegal = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    INN = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    KPP = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OGRN = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OKPO = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BIK = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BankName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BankAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CorrAccount = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Companies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Companies_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Position = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Obligation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CompanyId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Employees_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Employees_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5edf8313-66dd-448b-a111-e7058aaf6bd6",
                column: "ConcurrencyStamp",
                value: "72bc777a-9cd0-4fa3-b371-efa6557ad4dd");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c49e8523-b93c-4872-b2cb-d92f4843c98d",
                column: "ConcurrencyStamp",
                value: "054cc88b-e8f2-4791-8d12-9842eecd5b6a");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1e1c54f5-7105-4582-b8a8-ab88eda4b7db",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "983908d2-1bd4-4830-9be5-8ed52b863bce", "AQAAAAIAAYagAAAAEHipbsK5m1FjuRVlir/5mzQlDPU4xQUxx0G1/FG95dwCp3Lo9qPtfKBieIQShJ7/EQ==", "ac6bca3f-afae-4615-a53c-68cb3bcf9840" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "64ca7758-4f3d-47b1-87f6-3b3ebf20ffe8",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "c0dc26c9-337c-495b-9342-3144dbd18440", "AQAAAAIAAYagAAAAENWgm5xPqdUTZ0ed9QZmrnGbrPzb0WrHgItDEKS1BxTAU5LbZj29QujKkjrKD4yV4w==", "8e197b65-26eb-46ad-be48-09a5a67d2507" });

            migrationBuilder.CreateIndex(
                name: "IX_Companies_UserId",
                table: "Companies",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_CompanyId",
                table: "Employees",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_UserId",
                table: "Employees",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "Companies");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5edf8313-66dd-448b-a111-e7058aaf6bd6",
                column: "ConcurrencyStamp",
                value: "f3cd7056-d9fb-4974-ada6-05bf01b07324");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c49e8523-b93c-4872-b2cb-d92f4843c98d",
                column: "ConcurrencyStamp",
                value: "00348d0d-2f75-4402-a6ef-cc8f4ee4417a");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1e1c54f5-7105-4582-b8a8-ab88eda4b7db",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "aafc86c5-c375-4f1e-97bc-8d1ae08c2129", "AQAAAAIAAYagAAAAEHVPhkSQhwS2SidaKnn+M1hdr80pGsJwzJCcD0TVTPNCn4e0MZuTzz9I1oBok5V8YA==", "0bd01584-c806-4f21-beb3-11c834a9a1fe" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "64ca7758-4f3d-47b1-87f6-3b3ebf20ffe8",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "c11c4bf6-2f2b-49bd-8578-cdf720a5bc5c", "AQAAAAIAAYagAAAAEEw4XwyECZ4OsKVzeuxzdSIdUtId6laKBiY6o7GzfQyA7V3T8CQEbPzAHc2/BfaH8Q==", "4ff11514-94dc-415b-b50c-bf8254ff060a" });
        }
    }
}
