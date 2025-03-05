using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PersonRegistry.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class PersonRegistry_Tables_Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "City");

            migrationBuilder.EnsureSchema(
                name: "Person");

            migrationBuilder.CreateTable(
                name: "Cities",
                schema: "City",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PersonRelationTypes",
                schema: "Person",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonRelationTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PhoneNumberTypes",
                schema: "Person",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhoneNumberTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Persons",
                schema: "Person",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PersonalNumber = table.Column<string>(type: "nchar(11)", fixedLength: true, maxLength: 11, nullable: false),
                    BirthDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Photo = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    Gender = table.Column<int>(type: "int", nullable: false),
                    CityId = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Persons", x => x.Id);
                    table.CheckConstraint("CK_Person_BirthDate_MinLength", "DATEDIFF(hour, BirthDate, GETDATE()) / 8766.0 >= 18");
                    table.CheckConstraint("CK_Person_LastName_MinLength", "LEN(LastName) >= 2");
                    table.CheckConstraint("CK_Person_Name_MinLength", "LEN(Name) >= 2");
                    table.ForeignKey(
                        name: "FK_Persons_Cities_CityId",
                        column: x => x.CityId,
                        principalSchema: "City",
                        principalTable: "Cities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PersonPhoneNumbers",
                schema: "Person",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PhoneNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PersonId = table.Column<int>(type: "int", nullable: false),
                    PhoneNumberTypeId = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonPhoneNumbers", x => x.Id);
                    table.CheckConstraint("CK_PersonPhoneNumber_PhoneNumber_MinLength", "LEN(PhoneNumber) >= 4");
                    table.ForeignKey(
                        name: "FK_PersonPhoneNumbers_Persons_PersonId",
                        column: x => x.PersonId,
                        principalSchema: "Person",
                        principalTable: "Persons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PersonPhoneNumbers_PhoneNumberTypes_PhoneNumberTypeId",
                        column: x => x.PhoneNumberTypeId,
                        principalSchema: "Person",
                        principalTable: "PhoneNumberTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PersonRelations",
                schema: "Person",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PersonId = table.Column<int>(type: "int", nullable: false),
                    RelatedPersonId = table.Column<int>(type: "int", nullable: false),
                    PersonRelationTypeId = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonRelations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PersonRelations_PersonRelationTypes_PersonRelationTypeId",
                        column: x => x.PersonRelationTypeId,
                        principalSchema: "Person",
                        principalTable: "PersonRelationTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PersonRelations_Persons_PersonId",
                        column: x => x.PersonId,
                        principalSchema: "Person",
                        principalTable: "Persons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PersonRelations_Persons_RelatedPersonId",
                        column: x => x.RelatedPersonId,
                        principalSchema: "Person",
                        principalTable: "Persons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PersonPhoneNumbers_PersonId",
                schema: "Person",
                table: "PersonPhoneNumbers",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonPhoneNumbers_PhoneNumberTypeId",
                schema: "Person",
                table: "PersonPhoneNumbers",
                column: "PhoneNumberTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonRelations_PersonId",
                schema: "Person",
                table: "PersonRelations",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonRelations_PersonRelationTypeId",
                schema: "Person",
                table: "PersonRelations",
                column: "PersonRelationTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonRelations_RelatedPersonId",
                schema: "Person",
                table: "PersonRelations",
                column: "RelatedPersonId");

            migrationBuilder.CreateIndex(
                name: "IX_Persons_CityId",
                schema: "Person",
                table: "Persons",
                column: "CityId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PersonPhoneNumbers",
                schema: "Person");

            migrationBuilder.DropTable(
                name: "PersonRelations",
                schema: "Person");

            migrationBuilder.DropTable(
                name: "PhoneNumberTypes",
                schema: "Person");

            migrationBuilder.DropTable(
                name: "PersonRelationTypes",
                schema: "Person");

            migrationBuilder.DropTable(
                name: "Persons",
                schema: "Person");

            migrationBuilder.DropTable(
                name: "Cities",
                schema: "City");
        }
    }
}
