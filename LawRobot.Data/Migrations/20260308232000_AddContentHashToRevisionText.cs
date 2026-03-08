using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LawRobot.Data.Migrations;

public partial class AddContentHashToRevisionText : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AddColumn<string>(
            name: "ContentHash",
            table: "Revision_Text",
            type: "character varying(64)",
            maxLength: 64,
            nullable: false,
            defaultValue: string.Empty);

        migrationBuilder.AddColumn<DateTimeOffset>(
            name: "ExtractedAt",
            table: "Revision_Text",
            type: "timestamp with time zone",
            nullable: false,
            defaultValueSql: "now()");
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropColumn(
            name: "ContentHash",
            table: "Revision_Text");

        migrationBuilder.DropColumn(
            name: "ExtractedAt",
            table: "Revision_Text");
    }
}
