using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClashOfClans.API.Migrations
{
    /// <inheritdoc />
    public partial class NovaTabelaMembroInativoGuerra : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "data_entrada",
                table: "membro",
                type: "datetime(0)",
                precision: 0,
                nullable: false,
                defaultValue: new DateTime(2025, 12, 10, 20, 24, 58, 969, DateTimeKind.Local).AddTicks(9167),
                oldClrType: typeof(DateTime),
                oldType: "datetime(0)",
                oldPrecision: 0,
                oldDefaultValue: new DateTime(2025, 12, 10, 19, 38, 32, 689, DateTimeKind.Local).AddTicks(7911));

            migrationBuilder.CreateTable(
                name: "membros_inativos_guerras",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    membro_id = table.Column<int>(type: "int", nullable: false),
                    clan_tag = table.Column<string>(type: "varchar(100)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    data_avaliacao = table.Column<DateTime>(type: "datetime(0)", precision: 0, nullable: false),
                    guerras_analisadas = table.Column<int>(type: "int", nullable: false),
                    guerras_participadas = table.Column<int>(type: "int", nullable: false),
                    guerras_nao_participadas = table.Column<int>(type: "int", nullable: false),
                    data_entrada_membro = table.Column<DateTime>(type: "datetime(0)", precision: 0, nullable: false),
                    motivo = table.Column<string>(type: "varchar(100)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    criado_em = table.Column<DateTime>(type: "datetime(0)", precision: 0, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_membros_inativos_guerras", x => x.id);
                    table.ForeignKey(
                        name: "fk_membros_inativos_guerras_membros_membro_id",
                        column: x => x.membro_id,
                        principalTable: "membro",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "ix_membros_inativos_guerras_membro_id",
                table: "membros_inativos_guerras",
                column: "membro_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "membros_inativos_guerras");

            migrationBuilder.AlterColumn<DateTime>(
                name: "data_entrada",
                table: "membro",
                type: "datetime(0)",
                precision: 0,
                nullable: false,
                defaultValue: new DateTime(2025, 12, 10, 19, 38, 32, 689, DateTimeKind.Local).AddTicks(7911),
                oldClrType: typeof(DateTime),
                oldType: "datetime(0)",
                oldPrecision: 0,
                oldDefaultValue: new DateTime(2025, 12, 10, 20, 24, 58, 969, DateTimeKind.Local).AddTicks(9167));
        }
    }
}
