using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Shop.Infrastructure.Migrations
{
    public partial class _create_comments_relation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                    ALTER TABLE [dbo].Comments
                    ADD CONSTRAINT FK_Comments_Users_UserId
                    FOREIGN KEY (UserId) REFERENCES [user].Users(Id);
                ");

            migrationBuilder.Sql(@"
                    ALTER TABLE [dbo].Comments
                    ADD CONSTRAINT FK_Comments_Products_ProductId
                    FOREIGN KEY (ProductId) REFERENCES [product].Products(Id);
                ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
