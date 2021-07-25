using Microsoft.EntityFrameworkCore.Migrations;

namespace MakeMagic.Migrations
{
    public partial class create_table_character : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Character",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Role = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    School = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    House = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false),
                    Patronus = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Character", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Character",
                columns: new[] { "Id", "House", "Name", "Patronus", "Role", "School" },
                values: new object[] { 1, "1760529f-6d51-4cb1-bcb1-25087fce5bde", "Harry Potter", "stag", "student", "Hogwarts School of Witchcraft and Wizardry" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Character");
        }
    }
}
