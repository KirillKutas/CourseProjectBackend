using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Migrations
{
    public partial class addEntities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Comments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Games",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GameName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<int>(type: "int", nullable: false),
                    ReleaseDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Developer = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Publisher = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MinSysReqOc = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MinSysReqProcessor = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MinSysReqRAM = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MinSysReqVideoCard = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RecSysReqOc = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RecSysReqProcessor = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RecSysReqRAM = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RecSysReqVideoCard = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DiskSpace = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Games", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Genres",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genres", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PeoplesComments",
                columns: table => new
                {
                    CommentsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UsersId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PeoplesComments", x => new { x.CommentsId, x.UsersId });
                    table.ForeignKey(
                        name: "FK_PeoplesComments_Comments_CommentsId",
                        column: x => x.CommentsId,
                        principalTable: "Comments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PeoplesComments_Users_UsersId",
                        column: x => x.UsersId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CommentsOnGames",
                columns: table => new
                {
                    CommentsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GamesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommentsOnGames", x => new { x.CommentsId, x.GamesId });
                    table.ForeignKey(
                        name: "FK_CommentsOnGames_Comments_CommentsId",
                        column: x => x.CommentsId,
                        principalTable: "Comments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CommentsOnGames_Games_GamesId",
                        column: x => x.GamesId,
                        principalTable: "Games",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "GamesByCategories",
                columns: table => new
                {
                    CategoriesId = table.Column<int>(type: "int", nullable: false),
                    GamesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GamesByCategories", x => new { x.CategoriesId, x.GamesId });
                    table.ForeignKey(
                        name: "FK_GamesByCategories_Categories_CategoriesId",
                        column: x => x.CategoriesId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_GamesByCategories_Games_GamesId",
                        column: x => x.GamesId,
                        principalTable: "Games",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "GamesForPeople",
                columns: table => new
                {
                    GamesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UsersId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GamesForPeople", x => new { x.GamesId, x.UsersId });
                    table.ForeignKey(
                        name: "FK_GamesForPeople_Games_GamesId",
                        column: x => x.GamesId,
                        principalTable: "Games",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_GamesForPeople_Users_UsersId",
                        column: x => x.UsersId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "GamesByGenres",
                columns: table => new
                {
                    GamesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GenresId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GamesByGenres", x => new { x.GamesId, x.GenresId });
                    table.ForeignKey(
                        name: "FK_GamesByGenres_Games_GamesId",
                        column: x => x.GamesId,
                        principalTable: "Games",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_GamesByGenres_Genres_GenresId",
                        column: x => x.GenresId,
                        principalTable: "Genres",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Specials" },
                    { 2, "Virtual reality" }
                });

            migrationBuilder.InsertData(
                table: "Genres",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Free to play" },
                    { 2, "Action" },
                    { 3, "Racing" },
                    { 4, "Strategy" },
                    { 5, "Sports" },
                    { 6, "Simulation" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_CommentsOnGames_GamesId",
                table: "CommentsOnGames",
                column: "GamesId");

            migrationBuilder.CreateIndex(
                name: "IX_GamesByCategories_GamesId",
                table: "GamesByCategories",
                column: "GamesId");

            migrationBuilder.CreateIndex(
                name: "IX_GamesByGenres_GenresId",
                table: "GamesByGenres",
                column: "GenresId");

            migrationBuilder.CreateIndex(
                name: "IX_GamesForPeople_UsersId",
                table: "GamesForPeople",
                column: "UsersId");

            migrationBuilder.CreateIndex(
                name: "IX_PeoplesComments_UsersId",
                table: "PeoplesComments",
                column: "UsersId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CommentsOnGames");

            migrationBuilder.DropTable(
                name: "GamesByCategories");

            migrationBuilder.DropTable(
                name: "GamesByGenres");

            migrationBuilder.DropTable(
                name: "GamesForPeople");

            migrationBuilder.DropTable(
                name: "PeoplesComments");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Genres");

            migrationBuilder.DropTable(
                name: "Games");

            migrationBuilder.DropTable(
                name: "Comments");
        }
    }
}
