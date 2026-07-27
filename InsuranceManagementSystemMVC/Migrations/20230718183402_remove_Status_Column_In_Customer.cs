using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InsuranceManagementSystemMVC.Migrations
{
    /// <inheritdoc />
    public partial class remove_Status_Column_In_Customer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                schema: "insurance",
                table: "customers");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Status",
                schema: "insurance",
                table: "customers",
                type: "int",
                unicode: false,
                maxLength: 10,
                nullable: false,
                defaultValue: 0);
        }
    }
}
