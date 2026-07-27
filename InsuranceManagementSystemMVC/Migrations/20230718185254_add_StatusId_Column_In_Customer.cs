using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InsuranceManagementSystemMVC.Migrations
{
    /// <inheritdoc />
    public partial class add_StatusId_Column_In_Customer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Status_id",
                schema: "insurance",
                table: "customers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_customers_Status_id",
                schema: "insurance",
                table: "customers",
                column: "Status_id");

            migrationBuilder.AddForeignKey(
                name: "fk_status2_id",
                schema: "insurance",
                table: "customers",
                column: "Status_id",
                principalSchema: "insurance",
                principalTable: "status_master",
                principalColumn: "Status_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_status2_id",
                schema: "insurance",
                table: "customers");

            migrationBuilder.DropIndex(
                name: "IX_customers_Status_id",
                schema: "insurance",
                table: "customers");

            migrationBuilder.DropColumn(
                name: "Status_id",
                schema: "insurance",
                table: "customers");
        }
    }
}
