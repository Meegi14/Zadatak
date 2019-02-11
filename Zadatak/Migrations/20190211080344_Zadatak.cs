using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Zadatak.Migrations
{
    public partial class Zadatak : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Kancelarijas",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Ime = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kancelarijas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Osobas",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Ime = table.Column<string>(nullable: true),
                    Prezime = table.Column<string>(nullable: true),
                    KancelarijaId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Osobas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Osobas_Kancelarijas_KancelarijaId",
                        column: x => x.KancelarijaId,
                        principalTable: "Kancelarijas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Uredjajs",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Ime = table.Column<string>(nullable: true),
                    OsobaId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Uredjajs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Uredjajs_Osobas_OsobaId",
                        column: x => x.OsobaId,
                        principalTable: "Osobas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OsobaUredjajs",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    VrijemeOd = table.Column<DateTime>(nullable: false),
                    VrijemeDo = table.Column<DateTime>(nullable: true),
                    OsobaId = table.Column<long>(nullable: false),
                    UredjajId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OsobaUredjajs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OsobaUredjajs_Osobas_OsobaId",
                        column: x => x.OsobaId,
                        principalTable: "Osobas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OsobaUredjajs_Uredjajs_UredjajId",
                        column: x => x.UredjajId,
                        principalTable: "Uredjajs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Osobas_KancelarijaId",
                table: "Osobas",
                column: "KancelarijaId");

            migrationBuilder.CreateIndex(
                name: "IX_OsobaUredjajs_OsobaId",
                table: "OsobaUredjajs",
                column: "OsobaId");

            migrationBuilder.CreateIndex(
                name: "IX_OsobaUredjajs_UredjajId",
                table: "OsobaUredjajs",
                column: "UredjajId");

            migrationBuilder.CreateIndex(
                name: "IX_Uredjajs_OsobaId",
                table: "Uredjajs",
                column: "OsobaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OsobaUredjajs");

            migrationBuilder.DropTable(
                name: "Uredjajs");

            migrationBuilder.DropTable(
                name: "Osobas");

            migrationBuilder.DropTable(
                name: "Kancelarijas");
        }
    }
}
