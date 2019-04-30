using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace sspx.Migrations
{
    public partial class AddTestUsers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // TODO CS2:
            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] {
                    "Id",
                    "UserName",
                    "NormalizedUserName",
                    "Email",
                    "NormalizedEmail",
                    "EmailConfirmed",
                    "PasswordHash",
                    "ConcurrencyStamp",
                    "SecurityStamp",
                    "PhoneNumberConfirmed",
                    "TwoFactorEnabled",
                    "LockoutEnabled",
                    "AccessFailedCount",
                    "SSPxUserKey"
                },
                values: new object[]
                {
                    Guid.NewGuid().ToString(),
                    "admin",
                    "ADMIN",
                    "test_admin@cap.org",
                    "TEST_ADMIN@CAP.ORG",
                    true,
                    "AQAAAAEAACcQAAAAEEbg7aDuaU/YnWYNO1uHdpnAPheyCw2da4oPhC0FLTE0/5JtadVtqYXgNPLAtkg1NA==",
                    "O6NJVHGR2SOJWVQSX6GNJCYVDQNGDP4O",
                    "b708e173-fcb0-44a0-be17-96dfc230015d",
                    false,
                    false,
                    false,
                    0,
                    null
                }
            );

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] {
                    "Id",
                    "UserName",
                    "NormalizedUserName",
                    "Email",
                    "NormalizedEmail",
                    "EmailConfirmed",
                    "PasswordHash",
                    "ConcurrencyStamp",
                    "SecurityStamp",
                    "PhoneNumberConfirmed",
                    "TwoFactorEnabled",
                    "LockoutEnabled",
                    "AccessFailedCount",
                    "SSPxUserKey",
                },
                values: new object[]
                {
                    Guid.NewGuid().ToString(),
                    "test",
                    "TEST",
                    "test@cap.org",
                    "TEST@CAP.ORG",
                    true,
                    "AQAAAAEAACcQAAAAEEbg7aDuaU/YnWYNO1uHdpnAPheyCw2da4oPhC0FLTE0/5JtadVtqYXgNPLAtkg1NA==",
                    "O6NJVHGR2SOJWVQSX6GNJCYVDQNGDP4O",
                    "b708e173-fcb0-44a0-be17-96dfc230015d",
                    false,
                    false,
                    false,
                    0,
                    null
                }
            );

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] {
                    "Id",
                    "UserName",
                    "NormalizedUserName",
                    "Email",
                    "NormalizedEmail",
                    "EmailConfirmed",
                    "PasswordHash",
                    "ConcurrencyStamp",
                    "SecurityStamp",
                    "PhoneNumberConfirmed",
                    "TwoFactorEnabled",
                    "LockoutEnabled",
                    "AccessFailedCount",
                    "SSPxUserKey"
                },
                values: new object[]
                {
                    Guid.NewGuid().ToString(),
                    "staff",
                    "STAFF",
                    "test_staff@cap.org",
                    "TEST_STAFF@CAP.ORG",
                    true,
                    "AQAAAAEAACcQAAAAEEbg7aDuaU/YnWYNO1uHdpnAPheyCw2da4oPhC0FLTE0/5JtadVtqYXgNPLAtkg1NA==",
                    "O6NJVHGR2SOJWVQSX6GNJCYVDQNGDP4O",
                    "b708e173-fcb0-44a0-be17-96dfc230015d",
                    false,
                    false,
                    false,
                    0,
                    null
                }
            );
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "UserName",
                keyValues: new object[] { "admin", "test", "staff" }
            );
        }
    }
}
