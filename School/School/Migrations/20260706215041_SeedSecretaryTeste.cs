using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace School.Migrations
{
    /// <inheritdoc />
    public partial class SeedSecretaryTeste : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AuditLogs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    EntityName = table.Column<string>(type: "TEXT", nullable: false),
                    Actions = table.Column<int>(type: "INTEGER", nullable: false),
                    PerformedBySecretaryId = table.Column<Guid>(type: "TEXT", nullable: false),
                    TimeStamp = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Details = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuditLogs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AuditLogs_Secretaries_PerformedBySecretaryId",
                        column: x => x.PerformedBySecretaryId,
                        principalTable: "Secretaries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Secretaries",
                columns: new[] { "Id", "Cpf", "Email", "Name", "PasswordHash" },
                values: new object[] { new Guid("11111111-1111-1111-1111-111111111111"), "00000000000", "secretario@escola.com", "Secretário Teste", "$2a$11$J0sLkJlofV8PhWE7GgrbI.STAymSuhVwskjEH1Uh5KZdRrWd.1rle" });

            migrationBuilder.CreateIndex(
                name: "IX_AuditLogs_PerformedBySecretaryId",
                table: "AuditLogs",
                column: "PerformedBySecretaryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AuditLogs");

            migrationBuilder.DeleteData(
                table: "Secretaries",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"));
        }
    }
}
