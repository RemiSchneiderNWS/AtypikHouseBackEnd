using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AtypikHouseBackEnd.Migrations
{
    /// <inheritdoc />
    public partial class firstmigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Activity",
                columns: table => new
                {
                    act_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    act_name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    act_adv_id = table.Column<int>(type: "int", nullable: false),
                    act_adress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    act_city = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    act_postal = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    act_describe = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    act_usr_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Activity", x => x.act_id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Activity");
        }
    }
}
