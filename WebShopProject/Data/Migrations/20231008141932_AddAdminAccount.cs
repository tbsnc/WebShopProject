using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Identity.Client;
using System.Text;

#nullable disable

namespace WebShopProject.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddAdminAccount : Migration
    {
        const string ADMIN_USER_GUID= "b1298492-ebe3-42a9-8568-9e82397fd71d";
        const string ADMIN_ROLE_GUID = "0e7e16cc-8b5c-468e-a02a-241174e97edc";
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var hasher = new PasswordHasher<ApplicationUser>();

            var passwordHash = hasher.HashPassword(null, "Admin123!");

            StringBuilder sb = new StringBuilder();

            sb.AppendLine("INSERT INTO AspNetUsers (Id, " +
                "UserName, " +
                "NormalizedUserName, " +
                "Email, " +
                "NormalizedEmail, " +
                "EmailConfirmed, " +
                "PasswordHash, " +
                "SecurityStamp, " +
                "PhoneNumber, " +
                "PhoneNumberConfirmed, " +
                "TwoFactorEnabled, " +
                "AccessFailedCount, " +
                "LockoutEnabled," +
                "Address, City, Country, FirstName, LastName, PostalCode)");

            sb.AppendLine("VALUES(");
            sb.AppendLine($"'{ADMIN_USER_GUID}', " +
                $"'admin@admin.com', " +
                $"'admin@admin.com', ".ToUpper() +
                $"'admin@admin.com', " +
                $"'admin@admin.com', ".ToUpper() +
                $"0, " +
                $"'{passwordHash}', " +
                $"'Admin', " +
                $"'+385935953992', " +
                $"1, " +
                $"0, " +
                $"0, " +
                $"0," +
                $"'Osijecka 2', " +
                $"'Osijek', " +
                $"'Hrvatska', " +
                $"'Mirko', " +
                $"'Miric', " +
                $"'31000'"
                );
            sb.AppendLine(")");

            migrationBuilder.Sql(sb.ToString());

            migrationBuilder.Sql($"INSERT INTO AspNetRoles (Id,Name,NormalizedName) VALUES ('{ADMIN_ROLE_GUID}', 'Admin', 'ADMIN')");

            migrationBuilder.Sql($"INSERT INTO AspNetUserRoles (UserId, RoleId) VALUES ('{ADMIN_USER_GUID}','{ADMIN_ROLE_GUID}')");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql($"delete from AspNetUserRoles where UserId = '{ADMIN_USER_GUID}' and RoleId='{ADMIN_ROLE_GUID}'");

            migrationBuilder.Sql($"delete from AspNetRoles where Id='{ADMIN_ROLE_GUID}'");

            migrationBuilder.Sql($"delete from AspNetUsers where Id='{ADMIN_USER_GUID}'");
        }
    }
}
