using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CASE_VMS_Backend.Migrations
{
    /// <inheritdoc />
    public partial class AttendeesAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CorporateInfoModel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Department = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ContractNumber = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CorporateInfoModel", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PrivateInfoModel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StreetName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StreetNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PostalCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BankAccount = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrivateInfoModel", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AttendeeModel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Surname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CorporateInfoId = table.Column<int>(type: "int", nullable: true),
                    PrivateInfoId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AttendeeModel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AttendeeModel_CorporateInfoModel_CorporateInfoId",
                        column: x => x.CorporateInfoId,
                        principalTable: "CorporateInfoModel",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AttendeeModel_PrivateInfoModel_PrivateInfoId",
                        column: x => x.PrivateInfoId,
                        principalTable: "PrivateInfoModel",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AttendeeModelCourseInstance",
                columns: table => new
                {
                    AttendeesId = table.Column<int>(type: "int", nullable: false),
                    CoursesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AttendeeModelCourseInstance", x => new { x.AttendeesId, x.CoursesId });
                    table.ForeignKey(
                        name: "FK_AttendeeModelCourseInstance_AttendeeModel_AttendeesId",
                        column: x => x.AttendeesId,
                        principalTable: "AttendeeModel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AttendeeModelCourseInstance_CourseInstances_CoursesId",
                        column: x => x.CoursesId,
                        principalTable: "CourseInstances",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AttendeeModel_CorporateInfoId",
                table: "AttendeeModel",
                column: "CorporateInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_AttendeeModel_PrivateInfoId",
                table: "AttendeeModel",
                column: "PrivateInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_AttendeeModelCourseInstance_CoursesId",
                table: "AttendeeModelCourseInstance",
                column: "CoursesId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AttendeeModelCourseInstance");

            migrationBuilder.DropTable(
                name: "AttendeeModel");

            migrationBuilder.DropTable(
                name: "CorporateInfoModel");

            migrationBuilder.DropTable(
                name: "PrivateInfoModel");
        }
    }
}
