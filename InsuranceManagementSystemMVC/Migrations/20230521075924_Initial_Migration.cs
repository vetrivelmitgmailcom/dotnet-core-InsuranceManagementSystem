using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InsuranceManagementSystemMVC.Migrations
{
    /// <inheritdoc />
    public partial class Initial_Migration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "insurance");

            migrationBuilder.CreateTable(
                name: "admin",
                schema: "insurance",
                columns: table => new
                {
                    adminId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    email = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    password = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__admin__AD0500A6C7ABCA31", x => x.adminId);
                });

            migrationBuilder.CreateTable(
                name: "country_master",
                schema: "insurance",
                columns: table => new
                {
                    Country_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Country = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__country___8037C7D643E9E2B0", x => x.Country_id);
                });

            migrationBuilder.CreateTable(
                name: "customers",
                schema: "insurance",
                columns: table => new
                {
                    Customer_id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    First_name = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    Last_name = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__customer__8CB382B19F2D5FD5", x => x.Customer_id);
                });

            migrationBuilder.CreateTable(
                name: "gender_master",
                schema: "insurance",
                columns: table => new
                {
                    Gender_id = table.Column<int>(type: "int", nullable: false),
                    Gender = table.Column<string>(type: "varchar(15)", unicode: false, maxLength: 15, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__gender_m__AF740A3C4D92378F", x => x.Gender_id);
                });

            migrationBuilder.CreateTable(
                name: "insurance_type_master",
                schema: "insurance",
                columns: table => new
                {
                    Insurance_id = table.Column<int>(type: "int", nullable: false),
                    Insurance_type = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__insuranc__FFF1644B9C333A9C", x => x.Insurance_id);
                });

            migrationBuilder.CreateTable(
                name: "marital_status_master",
                schema: "insurance",
                columns: table => new
                {
                    Marital_status_id = table.Column<int>(type: "int", nullable: false),
                    Marital_status = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__marital___EB830C2D851EA3C0", x => x.Marital_status_id);
                });

            migrationBuilder.CreateTable(
                name: "mode_of_premium_master",
                schema: "insurance",
                columns: table => new
                {
                    mode_of_premium_id = table.Column<int>(type: "int", nullable: false),
                    mode_of_premium = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__mode_of___61535EFC29C4C980", x => x.mode_of_premium_id);
                });

            migrationBuilder.CreateTable(
                name: "payment_type_master",
                schema: "insurance",
                columns: table => new
                {
                    Payment_type_id = table.Column<int>(type: "int", nullable: false),
                    Payment_type = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__payment___19563F1C73524910", x => x.Payment_type_id);
                });

            migrationBuilder.CreateTable(
                name: "relationship_master",
                schema: "insurance",
                columns: table => new
                {
                    Relationship_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Relationship = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__relation__1D4D88B87EC455F9", x => x.Relationship_id);
                });

            migrationBuilder.CreateTable(
                name: "status_master",
                schema: "insurance",
                columns: table => new
                {
                    Status_id = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__status_m__5191052418AFB1B4", x => x.Status_id);
                });

            migrationBuilder.CreateTable(
                name: "state_master",
                schema: "insurance",
                columns: table => new
                {
                    State_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    State = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: false),
                    Country_Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__state_ma__AF9444CFB582AA42", x => x.State_id);
                    table.ForeignKey(
                        name: "fk_countryF_id",
                        column: x => x.Country_Id,
                        principalSchema: "insurance",
                        principalTable: "country_master",
                        principalColumn: "Country_id");
                });

            migrationBuilder.CreateTable(
                name: "policy_details",
                schema: "insurance",
                columns: table => new
                {
                    Policy_id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Customer_id = table.Column<long>(type: "bigint", nullable: false),
                    Insurance_id = table.Column<int>(type: "int", nullable: false),
                    Date_of_issue = table.Column<DateTime>(type: "date", nullable: false),
                    Date_of_expire = table.Column<DateTime>(type: "date", nullable: true),
                    Status_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__policy_d__4569BF19CA8DD8D5", x => x.Policy_id);
                    table.ForeignKey(
                        name: "fk_customer_id",
                        column: x => x.Customer_id,
                        principalSchema: "insurance",
                        principalTable: "customers",
                        principalColumn: "Customer_id");
                    table.ForeignKey(
                        name: "fk_insurance_id",
                        column: x => x.Insurance_id,
                        principalSchema: "insurance",
                        principalTable: "insurance_type_master",
                        principalColumn: "Insurance_id");
                    table.ForeignKey(
                        name: "fk_status_id",
                        column: x => x.Status_id,
                        principalSchema: "insurance",
                        principalTable: "status_master",
                        principalColumn: "Status_id");
                });

            migrationBuilder.CreateTable(
                name: "city_master",
                schema: "insurance",
                columns: table => new
                {
                    City_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    City = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: false),
                    State_Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__city_mas__DE9CEC38771DD4B4", x => x.City_id);
                    table.ForeignKey(
                        name: "fk_stateF_id",
                        column: x => x.State_Id,
                        principalSchema: "insurance",
                        principalTable: "state_master",
                        principalColumn: "State_id");
                });

            migrationBuilder.CreateTable(
                name: "nominee_details",
                schema: "insurance",
                columns: table => new
                {
                    Nominee_id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nominee_name = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    Policy_id = table.Column<long>(type: "bigint", nullable: false),
                    Dob = table.Column<DateTime>(type: "date", nullable: true),
                    Gender_id = table.Column<int>(type: "int", nullable: false),
                    Mobile_number = table.Column<long>(type: "bigint", nullable: false),
                    Aadhar_number = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    PAN_number = table.Column<string>(type: "varchar(15)", unicode: false, maxLength: 15, nullable: false),
                    Relationship_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__nominee___917234FC2B1A1421", x => x.Nominee_id);
                    table.ForeignKey(
                        name: "fk_gender_id",
                        column: x => x.Gender_id,
                        principalSchema: "insurance",
                        principalTable: "gender_master",
                        principalColumn: "Gender_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_policy2_id",
                        column: x => x.Policy_id,
                        principalSchema: "insurance",
                        principalTable: "policy_details",
                        principalColumn: "Policy_id");
                    table.ForeignKey(
                        name: "fk_relationship_id",
                        column: x => x.Relationship_id,
                        principalSchema: "insurance",
                        principalTable: "relationship_master",
                        principalColumn: "Relationship_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "policy_value",
                schema: "insurance",
                columns: table => new
                {
                    Premium_id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    policy_id = table.Column<long>(type: "bigint", nullable: false),
                    Amount_of_period = table.Column<int>(type: "int", nullable: false),
                    Insured_Declared_value = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    premium_to_be_paid = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    mode_of_premium_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__policy_v__368DE5EA17999DD8", x => x.Premium_id);
                    table.ForeignKey(
                        name: "fk_mode_of_premium_id",
                        column: x => x.mode_of_premium_id,
                        principalSchema: "insurance",
                        principalTable: "mode_of_premium_master",
                        principalColumn: "mode_of_premium_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_policy3_id",
                        column: x => x.policy_id,
                        principalSchema: "insurance",
                        principalTable: "policy_details",
                        principalColumn: "Policy_id");
                });

            migrationBuilder.CreateTable(
                name: "personal_details",
                schema: "insurance",
                columns: table => new
                {
                    Personal_id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    customer_id = table.Column<long>(type: "bigint", nullable: false),
                    Dob = table.Column<DateTime>(type: "date", nullable: false),
                    Gender_id = table.Column<int>(type: "int", nullable: false),
                    Marital_status_id = table.Column<int>(type: "int", nullable: false),
                    Mobile_number = table.Column<long>(type: "bigint", nullable: false),
                    Email = table.Column<string>(type: "varchar(40)", unicode: false, maxLength: 40, nullable: false),
                    Aadhar_number = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    PAN_number = table.Column<string>(type: "varchar(15)", unicode: false, maxLength: 15, nullable: false),
                    Street = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: false),
                    City_id = table.Column<int>(type: "int", nullable: false),
                    State_id = table.Column<int>(type: "int", nullable: false),
                    Country_id = table.Column<int>(type: "int", nullable: false),
                    Postal_code = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__personal__732C802264EA0558", x => x.Personal_id);
                    table.ForeignKey(
                        name: "fk_city2_id",
                        column: x => x.City_id,
                        principalSchema: "insurance",
                        principalTable: "city_master",
                        principalColumn: "City_id");
                    table.ForeignKey(
                        name: "fk_country2_id",
                        column: x => x.Country_id,
                        principalSchema: "insurance",
                        principalTable: "country_master",
                        principalColumn: "Country_id");
                    table.ForeignKey(
                        name: "fk_customer2_id",
                        column: x => x.customer_id,
                        principalSchema: "insurance",
                        principalTable: "customers",
                        principalColumn: "Customer_id");
                    table.ForeignKey(
                        name: "fk_gender2_id",
                        column: x => x.Gender_id,
                        principalSchema: "insurance",
                        principalTable: "gender_master",
                        principalColumn: "Gender_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_marital_status2_id",
                        column: x => x.Marital_status_id,
                        principalSchema: "insurance",
                        principalTable: "marital_status_master",
                        principalColumn: "Marital_status_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_state2_id",
                        column: x => x.State_id,
                        principalSchema: "insurance",
                        principalTable: "state_master",
                        principalColumn: "State_id");
                });

            migrationBuilder.CreateTable(
                name: "payments",
                schema: "insurance",
                columns: table => new
                {
                    Payment_id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Premium_Id = table.Column<long>(type: "bigint", nullable: false),
                    Payment_type_id = table.Column<int>(type: "int", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    Payment_Date = table.Column<DateTime>(type: "date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__payments__DA638B199F3EDA5F", x => x.Payment_id);
                    table.ForeignKey(
                        name: "fk_payment_type_id",
                        column: x => x.Payment_type_id,
                        principalSchema: "insurance",
                        principalTable: "payment_type_master",
                        principalColumn: "Payment_type_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_primium_id",
                        column: x => x.Premium_Id,
                        principalSchema: "insurance",
                        principalTable: "policy_value",
                        principalColumn: "Premium_id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_city_master_State_Id",
                schema: "insurance",
                table: "city_master",
                column: "State_Id");

            migrationBuilder.CreateIndex(
                name: "IX_nominee_details_Gender_id",
                schema: "insurance",
                table: "nominee_details",
                column: "Gender_id");

            migrationBuilder.CreateIndex(
                name: "IX_nominee_details_Policy_id",
                schema: "insurance",
                table: "nominee_details",
                column: "Policy_id");

            migrationBuilder.CreateIndex(
                name: "IX_nominee_details_Relationship_id",
                schema: "insurance",
                table: "nominee_details",
                column: "Relationship_id");

            migrationBuilder.CreateIndex(
                name: "UQ__nominee___097AF695B810004D",
                schema: "insurance",
                table: "nominee_details",
                column: "Aadhar_number",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ__nominee___0CB01CC46680DD9B",
                schema: "insurance",
                table: "nominee_details",
                column: "PAN_number",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ__nominee___9E090FFF6CB3CDBF",
                schema: "insurance",
                table: "nominee_details",
                column: "Mobile_number",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_payments_Payment_type_id",
                schema: "insurance",
                table: "payments",
                column: "Payment_type_id");

            migrationBuilder.CreateIndex(
                name: "IX_payments_Premium_Id",
                schema: "insurance",
                table: "payments",
                column: "Premium_Id");

            migrationBuilder.CreateIndex(
                name: "IX_personal_details_City_id",
                schema: "insurance",
                table: "personal_details",
                column: "City_id");

            migrationBuilder.CreateIndex(
                name: "IX_personal_details_Country_id",
                schema: "insurance",
                table: "personal_details",
                column: "Country_id");

            migrationBuilder.CreateIndex(
                name: "IX_personal_details_customer_id",
                schema: "insurance",
                table: "personal_details",
                column: "customer_id");

            migrationBuilder.CreateIndex(
                name: "IX_personal_details_Gender_id",
                schema: "insurance",
                table: "personal_details",
                column: "Gender_id");

            migrationBuilder.CreateIndex(
                name: "IX_personal_details_Marital_status_id",
                schema: "insurance",
                table: "personal_details",
                column: "Marital_status_id");

            migrationBuilder.CreateIndex(
                name: "IX_personal_details_State_id",
                schema: "insurance",
                table: "personal_details",
                column: "State_id");

            migrationBuilder.CreateIndex(
                name: "UQ__personal__097AF695983FC34F",
                schema: "insurance",
                table: "personal_details",
                column: "Aadhar_number",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ__personal__0CB01CC4E84EC167",
                schema: "insurance",
                table: "personal_details",
                column: "PAN_number",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ__personal__9E090FFF73DA8EAF",
                schema: "insurance",
                table: "personal_details",
                column: "Mobile_number",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ__personal__A9D1053460EF173E",
                schema: "insurance",
                table: "personal_details",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_policy_details_Customer_id",
                schema: "insurance",
                table: "policy_details",
                column: "Customer_id");

            migrationBuilder.CreateIndex(
                name: "IX_policy_details_Insurance_id",
                schema: "insurance",
                table: "policy_details",
                column: "Insurance_id");

            migrationBuilder.CreateIndex(
                name: "IX_policy_details_Status_id",
                schema: "insurance",
                table: "policy_details",
                column: "Status_id");

            migrationBuilder.CreateIndex(
                name: "IX_policy_value_mode_of_premium_id",
                schema: "insurance",
                table: "policy_value",
                column: "mode_of_premium_id");

            migrationBuilder.CreateIndex(
                name: "UQ__policy_v__47DA3F02C1715153",
                schema: "insurance",
                table: "policy_value",
                column: "policy_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_state_master_Country_Id",
                schema: "insurance",
                table: "state_master",
                column: "Country_Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "admin",
                schema: "insurance");

            migrationBuilder.DropTable(
                name: "nominee_details",
                schema: "insurance");

            migrationBuilder.DropTable(
                name: "payments",
                schema: "insurance");

            migrationBuilder.DropTable(
                name: "personal_details",
                schema: "insurance");

            migrationBuilder.DropTable(
                name: "relationship_master",
                schema: "insurance");

            migrationBuilder.DropTable(
                name: "payment_type_master",
                schema: "insurance");

            migrationBuilder.DropTable(
                name: "policy_value",
                schema: "insurance");

            migrationBuilder.DropTable(
                name: "city_master",
                schema: "insurance");

            migrationBuilder.DropTable(
                name: "gender_master",
                schema: "insurance");

            migrationBuilder.DropTable(
                name: "marital_status_master",
                schema: "insurance");

            migrationBuilder.DropTable(
                name: "mode_of_premium_master",
                schema: "insurance");

            migrationBuilder.DropTable(
                name: "policy_details",
                schema: "insurance");

            migrationBuilder.DropTable(
                name: "state_master",
                schema: "insurance");

            migrationBuilder.DropTable(
                name: "customers",
                schema: "insurance");

            migrationBuilder.DropTable(
                name: "insurance_type_master",
                schema: "insurance");

            migrationBuilder.DropTable(
                name: "status_master",
                schema: "insurance");

            migrationBuilder.DropTable(
                name: "country_master",
                schema: "insurance");
        }
    }
}
