using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sub.Migrations
{
    /// <inheritdoc />
    public partial class AddProfilePicturePath : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ProfilePicturePath",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

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
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "ProfilePicturePath", "SecurityStamp" },
                values: new object[] { "91f97832-6e29-45be-9a92-d28eba3866ec", "AQAAAAIAAYagAAAAEIKZcDpOrbWsemlQXN00YPA2qCPpNviVaqqKd4fG2cyjAH+sSP6fDGMDRID15QURmQ==", null, "28e8dfd7-d32e-4edd-a934-4c9151e99e2a" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "64ca7758-4f3d-47b1-87f6-3b3ebf20ffe8",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "ProfilePicturePath", "SecurityStamp" },
                values: new object[] { "7df3ae2c-5bc0-4069-a3ce-0c7fcab064a1", "AQAAAAIAAYagAAAAEAsUKuZW53rfrn77zWFQwIfgWE54mMPV5xfSFU4IYGKaCFRqfcdZpqTsNbZB3kBUQw==", null, "c74a6eae-121d-46d2-aeb7-3745a280f87c" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProfilePicturePath",
                table: "AspNetUsers");

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
        }
    }
}
