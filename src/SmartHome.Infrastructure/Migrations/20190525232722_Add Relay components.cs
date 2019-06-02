using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SmartHome.Infrastructure.Migrations
{
    public partial class AddRelaycomponents : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ActorComponent",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    DeviceId = table.Column<int>(nullable: true),
                    Discriminator = table.Column<string>(nullable: false),
                    Delay = table.Column<int>(nullable: true),
                    PinNumber = table.Column<int>(nullable: true),
                    RelayToggle_PinNumber = table.Column<int>(nullable: true),
                    IsToggled = table.Column<bool>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActorComponent", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ActorComponent_Devices_DeviceId",
                        column: x => x.DeviceId,
                        principalTable: "Devices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ActorComponent_DeviceId",
                table: "ActorComponent",
                column: "DeviceId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ActorComponent");
        }
    }
}
