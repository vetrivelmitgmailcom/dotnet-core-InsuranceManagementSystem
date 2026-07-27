using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InsuranceManagementSystemMVC.Migrations
{
    /// <inheritdoc />
    public partial class Add_Status_Column_In_Customer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Age",
                schema: "insurance",
                table: "customers",
                newName: "Status");

            migrationBuilder.AlterColumn<string>(
                name: "password",
                schema: "insurance",
                table: "admin",
                type: "varchar(100)",
                unicode: false,
                maxLength: 100,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "varchar(100)",
                oldUnicode: false,
                oldMaxLength: 100,
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Status",
                schema: "insurance",
                table: "customers",
                newName: "Age");

            migrationBuilder.AlterColumn<string>(
                name: "password",
                schema: "insurance",
                table: "admin",
                type: "varchar(100)",
                unicode: false,
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(100)",
                oldUnicode: false,
                oldMaxLength: 100);
        }
    }
}
