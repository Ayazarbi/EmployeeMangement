using Microsoft.EntityFrameworkCore.Migrations;

namespace EmployeeManagement.Migrations
{
    public partial class Inital : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "_dptlist",
                columns: table => new
                {
                    DepartmentId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Department_name = table.Column<string>(maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__dptlist", x => x.DepartmentId);
                });

            migrationBuilder.CreateTable(
                name: "_employeelist",
                columns: table => new
                {
                    EmployeeId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 20, nullable: false),
                    Surname = table.Column<string>(maxLength: 20, nullable: false),
                    Address = table.Column<string>(maxLength: 100, nullable: false),
                    Contactnumber = table.Column<string>(maxLength: 10, nullable: false),
                    DepartmentId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__employeelist", x => x.EmployeeId);
                    table.ForeignKey(
                        name: "FK__employeelist__dptlist_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "_dptlist",
                        principalColumn: "DepartmentId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX__employeelist_DepartmentId",
                table: "_employeelist",
                column: "DepartmentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "_employeelist");

            migrationBuilder.DropTable(
                name: "_dptlist");
        }
    }
}
