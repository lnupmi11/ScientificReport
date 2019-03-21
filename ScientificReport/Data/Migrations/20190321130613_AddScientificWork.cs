using Microsoft.EntityFrameworkCore.Migrations;

namespace ScientificReport.Data.Migrations
{
    public partial class AddScientificWork : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ScientificWork",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Cypher = table.Column<string>(nullable: true),
                    Category = table.Column<string>(nullable: true),
                    Theme = table.Column<string>(nullable: true),
                    Content = table.Column<string>(nullable: true),
                    DepartmentId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScientificWork", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ScientificWork_Departments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Departments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ScientificWork_DepartmentId",
                table: "ScientificWork",
                column: "DepartmentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ScientificWork");
        }
    }
}
