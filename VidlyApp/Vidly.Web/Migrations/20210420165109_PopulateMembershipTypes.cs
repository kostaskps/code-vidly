using Microsoft.EntityFrameworkCore.Migrations;

namespace Vidly.Web.Migrations
{
    public partial class PopulateMembershipTypes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "MembershipType",
                columns: new[] { "Id", "DiscountRate", "DurationInMonths", "SignupFee" },
                values: new object[,]
                {
                    { (byte)1, (byte)0, (byte)0, (short)0 },
                    { (byte)2, (byte)10, (byte)1, (short)30 },
                    { (byte)3, (byte)15, (byte)3, (short)90 },
                    { (byte)4, (byte)2, (byte)12, (short)300 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "MembershipType",
                keyColumn: "Id",
                keyValue: (byte)1);

            migrationBuilder.DeleteData(
                table: "MembershipType",
                keyColumn: "Id",
                keyValue: (byte)2);

            migrationBuilder.DeleteData(
                table: "MembershipType",
                keyColumn: "Id",
                keyValue: (byte)3);

            migrationBuilder.DeleteData(
                table: "MembershipType",
                keyColumn: "Id",
                keyValue: (byte)4);
        }
    }
}
