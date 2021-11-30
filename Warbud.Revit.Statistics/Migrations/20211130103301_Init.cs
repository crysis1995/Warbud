using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Warbud.Revit.Statistics.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "warbud.revit.statistics");

            migrationBuilder.CreateTable(
                name: "Statistics",
                schema: "warbud.revit.statistics",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserName = table.Column<string>(type: "varchar(50)", nullable: true),
                    DomainName = table.Column<string>(type: "varchar(50)", nullable: true),
                    ComputerName = table.Column<string>(type: "varchar(50)", nullable: true),
                    AppName = table.Column<string>(type: "varchar(100)", nullable: true),
                    OperationName = table.Column<string>(type: "text", nullable: true),
                    OperationTimeMs = table.Column<long>(type: "bigint", nullable: false),
                    OperationAmount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Statistics", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Statistics",
                schema: "warbud.revit.statistics");
        }
    }
}
