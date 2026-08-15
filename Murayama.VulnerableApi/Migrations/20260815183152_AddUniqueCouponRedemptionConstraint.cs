using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Murayama.VulnerableApi.Migrations
{
    /// <inheritdoc />
    public partial class AddUniqueCouponRedemptionConstraint : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_CouponRedemptions_UserId",
                table: "CouponRedemptions");

            migrationBuilder.CreateIndex(
                name: "IX_CouponRedemptions_UserId_CouponCode",
                table: "CouponRedemptions",
                columns: new[] { "UserId", "CouponCode" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_CouponRedemptions_UserId_CouponCode",
                table: "CouponRedemptions");

            migrationBuilder.CreateIndex(
                name: "IX_CouponRedemptions_UserId",
                table: "CouponRedemptions",
                column: "UserId");
        }
    }
}
