using Microsoft.EntityFrameworkCore.Migrations;

namespace Vidly.Web.Migrations
{
    public partial class UpdateMembershipTypeRecords : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "MembershipType",
                keyColumn: "Id",
                keyValue: (byte)1,
                column: "Name",
                value: "Pay as You Go");

            migrationBuilder.UpdateData(
                table: "MembershipType",
                keyColumn: "Id",
                keyValue: (byte)2,
                column: "Name",
                value: "Monthly");

            migrationBuilder.UpdateData(
                table: "MembershipType",
                keyColumn: "Id",
                keyValue: (byte)3,
                column: "Name",
                value: "Quarterly");

            migrationBuilder.UpdateData(
                table: "MembershipType",
                keyColumn: "Id",
                keyValue: (byte)4,
                column: "Name",
                value: "Yearly");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "MembershipType",
                keyColumn: "Id",
                keyValue: (byte)1,
                column: "Name",
                value: null);

            migrationBuilder.UpdateData(
                table: "MembershipType",
                keyColumn: "Id",
                keyValue: (byte)2,
                column: "Name",
                value: null);

            migrationBuilder.UpdateData(
                table: "MembershipType",
                keyColumn: "Id",
                keyValue: (byte)3,
                column: "Name",
                value: null);

            migrationBuilder.UpdateData(
                table: "MembershipType",
                keyColumn: "Id",
                keyValue: (byte)4,
                column: "Name",
                value: null);
        }
    }
}
