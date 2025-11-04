using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClashOfClans.API.Migrations
{
    /// <inheritdoc />
    public partial class novatabelaMembroGuerraResumo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "membros_guerras_resumo",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    clan_tag = table.Column<string>(type: "varchar(100)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    tag = table.Column<string>(type: "varchar(100)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    nome = table.Column<string>(type: "varchar(100)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    guerras_participadas_seq = table.Column<int>(type: "int", nullable: false),
                    quantidade_ataques = table.Column<int>(type: "int", nullable: false),
                    ultima_guerra_id = table.Column<int>(type: "int", nullable: false),
                    data_alteracao = table.Column<DateTime>(type: "datetime(0)", precision: 0, nullable: false),
                    data_criacao = table.Column<DateTime>(type: "datetime(0)", precision: 0, nullable: false),
                    foi_removido = table.Column<bool>(type: "tinyint(1)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_membros_guerras_resumo", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "membros_guerras_resumo");
        }
    }
}
