using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CMS.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "CMS");

            migrationBuilder.CreateTable(
                name: "Permission",
                schema: "CMS",
                columns: table => new
                {
                    PermissionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PermissionType = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Permission", x => x.PermissionId);
                });

            migrationBuilder.CreateTable(
                name: "Role",
                schema: "CMS",
                columns: table => new
                {
                    RoleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.RoleId);
                });

            migrationBuilder.CreateTable(
                name: "PermissionRole",
                schema: "CMS",
                columns: table => new
                {
                    PermissionsPermissionId = table.Column<int>(type: "int", nullable: false),
                    RolesRoleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PermissionRole", x => new { x.PermissionsPermissionId, x.RolesRoleId });
                    table.ForeignKey(
                        name: "FK_PermissionRole_Permission_PermissionsPermissionId",
                        column: x => x.PermissionsPermissionId,
                        principalSchema: "CMS",
                        principalTable: "Permission",
                        principalColumn: "PermissionId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PermissionRole_Role_RolesRoleId",
                        column: x => x.RolesRoleId,
                        principalSchema: "CMS",
                        principalTable: "Role",
                        principalColumn: "RoleId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "User",
                schema: "CMS",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RoleId1 = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_User_Role_RoleId1",
                        column: x => x.RoleId1,
                        principalSchema: "CMS",
                        principalTable: "Role",
                        principalColumn: "RoleId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_PermissionRole_RolesRoleId",
                schema: "CMS",
                table: "PermissionRole",
                column: "RolesRoleId");

            migrationBuilder.CreateIndex(
                name: "IX_User_RoleId1",
                schema: "CMS",
                table: "User",
                column: "RoleId1");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PermissionRole",
                schema: "CMS");

            migrationBuilder.DropTable(
                name: "User",
                schema: "CMS");

            migrationBuilder.DropTable(
                name: "Permission",
                schema: "CMS");

            migrationBuilder.DropTable(
                name: "Role",
                schema: "CMS");
        }
    }
}
