using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KitapService.Migrations
{
    /// <inheritdoc />
    public partial class flexa : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Students",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    SurName = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    StudentNo = table.Column<int>(type: "int", nullable: false),
                    Birtday = table.Column<string>(type: "nvarchar(15)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Students", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Students");
        }
    }
}
