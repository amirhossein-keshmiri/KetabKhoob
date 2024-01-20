using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Shop.Infrastructure.Migrations
{
    public partial class _create_seller_relation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                    ALTER TABLE [seller].Sellers
                    ADD CONSTRAINT FK_Sellers_Users_UserId
                    FOREIGN KEY (UserId) REFERENCES [user].Users(Id);
                ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
