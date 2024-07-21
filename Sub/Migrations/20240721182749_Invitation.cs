using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sub.Migrations
{
    /// <inheritdoc />
    public partial class Invitation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Invitations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InviterId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    InviteeEmail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CompanyId = table.Column<int>(type: "int", nullable: false),
                    SendDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ResponseDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Invitations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Invitations_AspNetUsers_InviterId",
                        column: x => x.InviterId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Invitations_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5edf8313-66dd-448b-a111-e7058aaf6bd6",
                column: "ConcurrencyStamp",
                value: "da538e03-3347-45d2-aff7-3410ef308198");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c49e8523-b93c-4872-b2cb-d92f4843c98d",
                column: "ConcurrencyStamp",
                value: "0c4c9b03-a08c-40b5-8182-00892e8c154a");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1e1c54f5-7105-4582-b8a8-ab88eda4b7db",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "5b78b10a-1630-4a96-9396-b7138655352e", "AQAAAAIAAYagAAAAEGrzRqHwSkK2cHnxE237lsmqcEZ34Rd0nWDP4oMnJh66WINYeQwlYiQ5mkMkNA1zRg==", "408981cf-2879-4a54-ac7b-4c93eacec293" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "64ca7758-4f3d-47b1-87f6-3b3ebf20ffe8",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "1ba048c0-434e-4799-84ed-9c5325943bde", "AQAAAAIAAYagAAAAEMFqk/cQ4XzxyHFh51KmEv+sTrUyIUNSbqIflznKOUTBZ0D/7T5Td652cZr+t/Pk3A==", "f9eeea58-731c-42ee-a2b7-8a1bedb32199" });

            migrationBuilder.CreateIndex(
                name: "IX_Invitations_CompanyId",
                table: "Invitations",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Invitations_InviterId",
                table: "Invitations",
                column: "InviterId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Invitations");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5edf8313-66dd-448b-a111-e7058aaf6bd6",
                column: "ConcurrencyStamp",
                value: "f1abd069-3667-424a-a03d-7069f333ae83");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c49e8523-b93c-4872-b2cb-d92f4843c98d",
                column: "ConcurrencyStamp",
                value: "b093d590-4f95-4fec-b2da-6ca98e74bb34");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1e1c54f5-7105-4582-b8a8-ab88eda4b7db",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "91f97832-6e29-45be-9a92-d28eba3866ec", "AQAAAAIAAYagAAAAEIKZcDpOrbWsemlQXN00YPA2qCPpNviVaqqKd4fG2cyjAH+sSP6fDGMDRID15QURmQ==", "28e8dfd7-d32e-4edd-a934-4c9151e99e2a" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "64ca7758-4f3d-47b1-87f6-3b3ebf20ffe8",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "7df3ae2c-5bc0-4069-a3ce-0c7fcab064a1", "AQAAAAIAAYagAAAAEAsUKuZW53rfrn77zWFQwIfgWE54mMPV5xfSFU4IYGKaCFRqfcdZpqTsNbZB3kBUQw==", "c74a6eae-121d-46d2-aeb7-3745a280f87c" });
        }
    }
}
