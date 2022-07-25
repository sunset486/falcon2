using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace falcon2.Data.Migrations
{
    public partial class SeedHeroesAndPowers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //Tabela cu supereroi
            migrationBuilder
                .Sql("INSERT INTO SuperHeroes (Name) Values ('Spider-Man')");
            migrationBuilder
                .Sql("INSERT INTO SuperHeroes (Name) Values ('Batman')");
            migrationBuilder
                .Sql("INSERT INTO SuperHeroes (Name) Values ('Daredevil')");
            migrationBuilder
                .Sql("INSERT INTO SuperHeroes (Name) Values ('Superman')");
            migrationBuilder
                .Sql("INSERT INTO SuperHeroes (Name) Values ('The Ghost')");

            //Tabela cu superputeri
            migrationBuilder
                .Sql("INSERT INTO SuperPowers (Name, SuperHeroId) Values ('Web slinging', (SELECT Id FROM SuperHeroes WHERE Name = 'Spider-Man'))");
            migrationBuilder
                .Sql("INSERT INTO SuperPowers (Name, SuperHeroId) Values ('Heart of gold', (SELECT Id FROM SuperHeroes WHERE Name = 'The Ghost'))");
            migrationBuilder
                .Sql("INSERT INTO SuperPowers (Name, SuperHeroId) Values ('Radar vision', (SELECT Id FROM SuperHeroes WHERE Name = 'Daredevil'))");
            migrationBuilder
                .Sql("INSERT INTO SuperPowers (Name, SuperHeroId) Values ('Supersonic flight', (SELECT Id FROM SuperHeroes WHERE Name = 'Superman'))");
            migrationBuilder
                .Sql("INSERT INTO SuperPowers (Name, SuperHeroId) Values ('Money', (SELECT Id FROM SuperHeroes WHERE Name = 'Batman'))");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder
                .Sql("DELETE FROM SuperPowers");

            migrationBuilder
                .Sql("DELETE FROM SuperHeroes");

        }
    }
}
