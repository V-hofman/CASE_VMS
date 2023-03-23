using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CASE_VMS_Backend.Migrations
{
    /// <inheritdoc />
    public partial class AttendeeMistakesFixed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AttendeeModel_CorporateInfoModel_CorporateInfoId",
                table: "AttendeeModel");

            migrationBuilder.DropForeignKey(
                name: "FK_AttendeeModel_PrivateInfoModel_PrivateInfoId",
                table: "AttendeeModel");

            migrationBuilder.DropForeignKey(
                name: "FK_AttendeeModelCourseInstance_AttendeeModel_AttendeesId",
                table: "AttendeeModelCourseInstance");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AttendeeModel",
                table: "AttendeeModel");

            migrationBuilder.RenameTable(
                name: "AttendeeModel",
                newName: "Attendees");

            migrationBuilder.RenameIndex(
                name: "IX_AttendeeModel_PrivateInfoId",
                table: "Attendees",
                newName: "IX_Attendees_PrivateInfoId");

            migrationBuilder.RenameIndex(
                name: "IX_AttendeeModel_CorporateInfoId",
                table: "Attendees",
                newName: "IX_Attendees_CorporateInfoId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Attendees",
                table: "Attendees",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AttendeeModelCourseInstance_Attendees_AttendeesId",
                table: "AttendeeModelCourseInstance",
                column: "AttendeesId",
                principalTable: "Attendees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Attendees_CorporateInfoModel_CorporateInfoId",
                table: "Attendees",
                column: "CorporateInfoId",
                principalTable: "CorporateInfoModel",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Attendees_PrivateInfoModel_PrivateInfoId",
                table: "Attendees",
                column: "PrivateInfoId",
                principalTable: "PrivateInfoModel",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AttendeeModelCourseInstance_Attendees_AttendeesId",
                table: "AttendeeModelCourseInstance");

            migrationBuilder.DropForeignKey(
                name: "FK_Attendees_CorporateInfoModel_CorporateInfoId",
                table: "Attendees");

            migrationBuilder.DropForeignKey(
                name: "FK_Attendees_PrivateInfoModel_PrivateInfoId",
                table: "Attendees");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Attendees",
                table: "Attendees");

            migrationBuilder.RenameTable(
                name: "Attendees",
                newName: "AttendeeModel");

            migrationBuilder.RenameIndex(
                name: "IX_Attendees_PrivateInfoId",
                table: "AttendeeModel",
                newName: "IX_AttendeeModel_PrivateInfoId");

            migrationBuilder.RenameIndex(
                name: "IX_Attendees_CorporateInfoId",
                table: "AttendeeModel",
                newName: "IX_AttendeeModel_CorporateInfoId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AttendeeModel",
                table: "AttendeeModel",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AttendeeModel_CorporateInfoModel_CorporateInfoId",
                table: "AttendeeModel",
                column: "CorporateInfoId",
                principalTable: "CorporateInfoModel",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AttendeeModel_PrivateInfoModel_PrivateInfoId",
                table: "AttendeeModel",
                column: "PrivateInfoId",
                principalTable: "PrivateInfoModel",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AttendeeModelCourseInstance_AttendeeModel_AttendeesId",
                table: "AttendeeModelCourseInstance",
                column: "AttendeesId",
                principalTable: "AttendeeModel",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
