using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TrainingApi.Migrations
{
    public partial class LocalDbAdd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    CategoryId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.CategoryId);
                });

            migrationBuilder.CreateTable(
                name: "Clients",
                columns: table => new
                {
                    ClientId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    HomeAddress = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    Mobile = table.Column<int>(nullable: false),
                    ObjectIdentifier = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clients", x => x.ClientId);
                });

            migrationBuilder.CreateTable(
                name: "VideoLibraries",
                columns: table => new
                {
                    VideoLibraryId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    VideoUrl = table.Column<string>(nullable: true),
                    AltTag = table.Column<string>(nullable: true),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: false),
                    DoNotUse = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VideoLibraries", x => x.VideoLibraryId);
                });

            migrationBuilder.CreateTable(
                name: "WorkoutPlans",
                columns: table => new
                {
                    WorkoutPlanId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    DoNotUse = table.Column<bool>(nullable: false),
                    ImageUrl = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkoutPlans", x => x.WorkoutPlanId);
                });

            migrationBuilder.CreateTable(
                name: "Exercises",
                columns: table => new
                {
                    ExerciseId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    CategoryId = table.Column<int>(nullable: false),
                    VideoLibraryId = table.Column<int>(nullable: false),
                    DoNotUse = table.Column<bool>(nullable: false),
                    ImageUrl = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Exercises", x => x.ExerciseId);
                    table.ForeignKey(
                        name: "FK_Exercises_VideoLibraries_VideoLibraryId",
                        column: x => x.VideoLibraryId,
                        principalTable: "VideoLibraries",
                        principalColumn: "VideoLibraryId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ClientWorkouts",
                columns: table => new
                {
                    ClientWorkoutId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ClientId = table.Column<int>(nullable: false),
                    ClientExerciseId = table.Column<int>(nullable: false),
                    WorkoutPlanId = table.Column<int>(nullable: false),
                    Frequency = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientWorkouts", x => x.ClientWorkoutId);
                    table.ForeignKey(
                        name: "FK_ClientWorkouts_WorkoutPlans_WorkoutPlanId",
                        column: x => x.WorkoutPlanId,
                        principalTable: "WorkoutPlans",
                        principalColumn: "WorkoutPlanId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ClientExercises",
                columns: table => new
                {
                    ClientExerciseId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ClientWorkoutId = table.Column<int>(nullable: false),
                    ExerciseId = table.Column<int>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientExercises", x => x.ClientExerciseId);
                    table.ForeignKey(
                        name: "FK_ClientExercises_Exercises_ExerciseId",
                        column: x => x.ExerciseId,
                        principalTable: "Exercises",
                        principalColumn: "ExerciseId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WorkoutExercises",
                columns: table => new
                {
                    WorkoutExerciseId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    WorkoutPlanId = table.Column<int>(nullable: false),
                    ExerciseId = table.Column<int>(nullable: false),
                    DoNotUse = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkoutExercises", x => x.WorkoutExerciseId);
                    table.ForeignKey(
                        name: "FK_WorkoutExercises_Exercises_ExerciseId",
                        column: x => x.ExerciseId,
                        principalTable: "Exercises",
                        principalColumn: "ExerciseId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "CategoryId", "Name" },
                values: new object[,]
                {
                    { 1, "Shoulders" },
                    { 2, "Back" },
                    { 3, "Legs" }
                });

            migrationBuilder.InsertData(
                table: "Clients",
                columns: new[] { "ClientId", "Email", "FirstName", "HomeAddress", "LastName", "Mobile", "ObjectIdentifier" },
                values: new object[,]
                {
                    { 1, "buzz@gmail.com", "Buzz", "2 Galaxy Way Milkyway", "Lightyear", 421055555, null },
                    { 2, "woody@gmail.com", "Woody", "5 Ranch Road Earth", "Cowboy", 421054444, null }
                });

            migrationBuilder.InsertData(
                table: "VideoLibraries",
                columns: new[] { "VideoLibraryId", "AltTag", "CreateDate", "DoNotUse", "ModifiedDate", "VideoUrl" },
                values: new object[,]
                {
                    { 1, "Lateral Raise", new DateTime(2019, 2, 26, 11, 14, 39, 264, DateTimeKind.Local), false, new DateTime(2019, 2, 26, 11, 14, 39, 267, DateTimeKind.Local), "https://www.youtube.com/embed/0z-QQPzQHRE" },
                    { 2, "Incline front Raise", new DateTime(2019, 2, 26, 11, 14, 39, 267, DateTimeKind.Local), false, new DateTime(2019, 2, 26, 11, 14, 39, 267, DateTimeKind.Local), "https://www.youtube.com/embed/2hLRHXZs15Y" },
                    { 3, "Band Overhead Press", new DateTime(2019, 2, 26, 11, 14, 39, 267, DateTimeKind.Local), false, new DateTime(2019, 2, 26, 11, 14, 39, 267, DateTimeKind.Local), "https://www.youtube.com/embed/Zli1UXH9ZeE" }
                });

            migrationBuilder.InsertData(
                table: "WorkoutPlans",
                columns: new[] { "WorkoutPlanId", "DoNotUse", "ImageUrl", "Name" },
                values: new object[,]
                {
                    { 4, false, "./images/Shoulders.jpg", "Low Impact Shoulders" },
                    { 5, false, "./images/legs.jpg", "High Impact Legs" }
                });

            migrationBuilder.InsertData(
                table: "ClientWorkouts",
                columns: new[] { "ClientWorkoutId", "ClientExerciseId", "ClientId", "Frequency", "WorkoutPlanId" },
                values: new object[,]
                {
                    { 1, 1, 1, 2, 4 },
                    { 2, 1, 2, 3, 4 }
                });

            migrationBuilder.InsertData(
                table: "Exercises",
                columns: new[] { "ExerciseId", "CategoryId", "DoNotUse", "ImageUrl", "Name", "VideoLibraryId" },
                values: new object[,]
                {
                    { 1, 1, false, null, "Lateral Raise", 1 },
                    { 2, 1, false, null, "Incline Front Raise", 2 },
                    { 3, 1, false, null, "Band Overhead Press", 3 }
                });

            migrationBuilder.InsertData(
                table: "ClientExercises",
                columns: new[] { "ClientExerciseId", "ClientWorkoutId", "ExerciseId", "IsActive" },
                values: new object[,]
                {
                    { 1, 1, 1, true },
                    { 6, 2, 1, false },
                    { 2, 1, 2, true },
                    { 4, 2, 2, true },
                    { 3, 1, 3, true },
                    { 5, 2, 3, true }
                });

            migrationBuilder.InsertData(
                table: "WorkoutExercises",
                columns: new[] { "WorkoutExerciseId", "DoNotUse", "ExerciseId", "WorkoutPlanId" },
                values: new object[,]
                {
                    { 1, false, 1, 4 },
                    { 2, false, 2, 4 },
                    { 3, false, 3, 4 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ClientExercises_ExerciseId",
                table: "ClientExercises",
                column: "ExerciseId");

            migrationBuilder.CreateIndex(
                name: "IX_ClientWorkouts_WorkoutPlanId",
                table: "ClientWorkouts",
                column: "WorkoutPlanId");

            migrationBuilder.CreateIndex(
                name: "IX_Exercises_VideoLibraryId",
                table: "Exercises",
                column: "VideoLibraryId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkoutExercises_ExerciseId",
                table: "WorkoutExercises",
                column: "ExerciseId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "ClientExercises");

            migrationBuilder.DropTable(
                name: "Clients");

            migrationBuilder.DropTable(
                name: "ClientWorkouts");

            migrationBuilder.DropTable(
                name: "WorkoutExercises");

            migrationBuilder.DropTable(
                name: "WorkoutPlans");

            migrationBuilder.DropTable(
                name: "Exercises");

            migrationBuilder.DropTable(
                name: "VideoLibraries");
        }
    }
}
