using Microsoft.EntityFrameworkCore.Migrations;

namespace IdentityServer.Data.Migrations.Users
{
    public partial class AddTenantIds : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AuthorizedTenantIds",
                table: "AspNetUsers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AuthorizedTenantIds",
                table: "AspNetUsers");
        }
    }
}
