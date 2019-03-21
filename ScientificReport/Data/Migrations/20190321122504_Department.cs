using Microsoft.EntityFrameworkCore.Migrations;

namespace ScientificReport.Data.Migrations
{
  public partial class AddedDepartment : Migration
  {
    protected override void Up(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.CreateTable(
        name: "Departments",
        columns: table => new
        {
          Id = table.Column<int>(nullable: false)
            .Annotation("Sqlite:Autoincrement", true),
          Title = table.Column<string>(maxLength: 100, nullable: false)
        },
        constraints: table =>
        {
          table.PrimaryKey("PK_Departments", x => x.Id);
        });
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.DropTable(
        name: "Departments");
    }
  }
}
