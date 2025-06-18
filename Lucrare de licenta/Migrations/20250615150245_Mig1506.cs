using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lucrare_de_licenta.Migrations
{
    /// <inheritdoc />
    public partial class Mig1506 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "dbo");

            migrationBuilder.AddColumn<string>(
                name: "email_contact",
                table: "Rezervari",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "suma_totala",
                table: "Rezervari",
                type: "decimal(10,2)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "tel_contact",
                table: "Rezervari",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "email_contact",
                table: "Rezervari");

            migrationBuilder.DropColumn(
                name: "suma_totala",
                table: "Rezervari");

            migrationBuilder.DropColumn(
                name: "tel_contact",
                table: "Rezervari");

            migrationBuilder.DropSequence(
                name: "seq_beneficiari",
                schema: "dbo");

            migrationBuilder.DropSequence(
                name: "seq_camere",
                schema: "dbo");

            migrationBuilder.DropSequence(
                name: "seq_categorii",
                schema: "dbo");

            migrationBuilder.DropSequence(
                name: "seq_cazari",
                schema: "dbo");

            migrationBuilder.DropSequence(
                name: "seq_destinatii",
                schema: "dbo");

            migrationBuilder.DropSequence(
                name: "seq_destinatii_itinerarii",
                schema: "dbo");

            migrationBuilder.DropSequence(
                name: "seq_facturi",
                schema: "dbo");

            migrationBuilder.DropSequence(
                name: "seq_itinerarii",
                schema: "dbo");

            migrationBuilder.DropSequence(
                name: "seq_oferte",
                schema: "dbo");

            migrationBuilder.DropSequence(
                name: "seq_plati",
                schema: "dbo");

            migrationBuilder.DropSequence(
                name: "seq_puncte_plecare",
                schema: "dbo");

            migrationBuilder.DropSequence(
                name: "seq_rezervari",
                schema: "dbo");

            migrationBuilder.DropSequence(
                name: "seq_tari",
                schema: "dbo");

            migrationBuilder.DropSequence(
                name: "seq_tururi",
                schema: "dbo");

            migrationBuilder.DropSequence(
                name: "seq_tururi_categorii",
                schema: "dbo");

            migrationBuilder.DropSequence(
                name: "seq_utilizatori",
                schema: "dbo");
        }
    }
}
