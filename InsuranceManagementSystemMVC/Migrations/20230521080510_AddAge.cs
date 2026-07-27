using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InsuranceManagementSystemMVC.Migrations
{
    /// <inheritdoc />
    public partial class AddAge : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Age",
                schema: "insurance",
                table: "customers",
                type: "int",
                unicode: false,
                maxLength: 10,
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Age",
                schema: "insurance",
                table: "customers");
        }
    }
}
