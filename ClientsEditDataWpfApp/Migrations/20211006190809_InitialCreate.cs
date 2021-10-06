using Microsoft.EntityFrameworkCore.Migrations;

namespace ClientsEditDataWpfApp.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Clients",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CardCode = table.Column<double>(type: "REAL", nullable: false),
                    StartDate = table.Column<string>(type: "TEXT", nullable: true),
                    FinishDate = table.Column<string>(type: "TEXT", nullable: true),
                    Lastname = table.Column<string>(type: "TEXT", nullable: true),
                    Firstname = table.Column<string>(type: "TEXT", nullable: true),
                    Surname = table.Column<string>(type: "TEXT", nullable: true),
                    FullName = table.Column<string>(type: "TEXT", nullable: true),
                    GenderId = table.Column<string>(type: "TEXT", nullable: true),
                    Birthday = table.Column<string>(type: "TEXT", nullable: true),
                    PhoneHome = table.Column<string>(type: "TEXT", nullable: true),
                    PhoneMobil = table.Column<string>(type: "TEXT", nullable: true),
                    Email = table.Column<string>(type: "TEXT", nullable: true),
                    City = table.Column<string>(type: "TEXT", nullable: true),
                    Street = table.Column<string>(type: "TEXT", nullable: true),
                    House = table.Column<string>(type: "TEXT", nullable: true),
                    Apartment = table.Column<string>(type: "TEXT", nullable: true),
                    AltAddress = table.Column<string>(type: "TEXT", nullable: true),
                    CardType = table.Column<string>(type: "TEXT", nullable: true),
                    Ownerguid = table.Column<string>(type: "TEXT", nullable: true),
                    Cardper = table.Column<string>(type: "TEXT", nullable: true),
                    Turnover = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clients", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Clients");
        }
    }
}
