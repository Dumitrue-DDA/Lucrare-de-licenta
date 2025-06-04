using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lucrare_de_licenta.Migrations
{
    /// <inheritdoc />
    public partial class Mg1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "categorii",
                columns: table => new
                {
                    cod_categ = table.Column<byte>(type: "tinyint", nullable: false),
                    den_categ = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    desc_categ = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    img_categ = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_categorii", x => x.cod_categ);
                });

            migrationBuilder.CreateTable(
                name: "puncte_plecare",
                columns: table => new
                {
                    cod_punct = table.Column<byte>(type: "tinyint", nullable: false),
                    tip_transport = table.Column<bool>(type: "bit", nullable: false),
                    localitate = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    judet = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    adresa = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_puncte_plecare", x => x.cod_punct);
                });

            migrationBuilder.CreateTable(
                name: "Roluri",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roluri", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tari",
                columns: table => new
                {
                    cod_tara = table.Column<byte>(type: "tinyint", nullable: false),
                    den_tara = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    cod_continent = table.Column<int>(type: "int", nullable: false),
                    img_tara = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    desc_tara = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    pass = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tari", x => x.cod_tara);
                });

            migrationBuilder.CreateTable(
                name: "tururi",
                columns: table => new
                {
                    cod_tur = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    den_tur = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    desc_tur = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    solicitare_fizica = table.Column<byte>(type: "tinyint", nullable: false),
                    grup_minim = table.Column<byte>(type: "tinyint", nullable: false),
                    grup_maxim = table.Column<byte>(type: "tinyint", nullable: false),
                    img_tur = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tururi", x => x.cod_tur);
                });

            migrationBuilder.CreateTable(
                name: "utilizatori",
                columns: table => new
                {
                    nr_utilizator = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nume = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    nume_normalizat = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    email_normalizat = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    email_confirmat = table.Column<bool>(type: "bit", nullable: false),
                    parola = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    token_securitate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    token_concurenta = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    nr_telefon = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    telefon_confirmat = table.Column<bool>(type: "bit", nullable: false),
                    auth_doi_factori = table.Column<bool>(type: "bit", nullable: false),
                    blocare_pana_la = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    blocare_activata = table.Column<bool>(type: "bit", nullable: false),
                    incercari_esuate = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_utilizatori", x => x.nr_utilizator);
                });

            migrationBuilder.CreateTable(
                name: "CereriRol",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CereriRol", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CereriRol_Roluri_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roluri",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "destinatii",
                columns: table => new
                {
                    cod_destinatie = table.Column<short>(type: "smallint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    den_destinatie = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    img_destinatie = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    judet = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    desc_destinatie = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    cod_tara = table.Column<byte>(type: "tinyint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_destinatii", x => x.cod_destinatie);
                    table.ForeignKey(
                        name: "FK_destinatii_tari_cod_tara",
                        column: x => x.cod_tara,
                        principalTable: "tari",
                        principalColumn: "cod_tara",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "oferte",
                columns: table => new
                {
                    cod_oferta = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    tip_transport = table.Column<bool>(type: "bit", nullable: false),
                    pret_adult = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    pret_copil = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    data_plecare = table.Column<DateOnly>(type: "date", nullable: false),
                    data_intoarcere = table.Column<DateOnly>(type: "date", nullable: false),
                    loc_libere = table.Column<byte>(type: "tinyint", nullable: false),
                    cod_tur = table.Column<int>(type: "int", nullable: false),
                    cod_punct = table.Column<byte>(type: "tinyint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_oferte", x => x.cod_oferta);
                    table.ForeignKey(
                        name: "FK_oferte_puncte_plecare_cod_punct",
                        column: x => x.cod_punct,
                        principalTable: "puncte_plecare",
                        principalColumn: "cod_punct",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_oferte_tururi_cod_tur",
                        column: x => x.cod_tur,
                        principalTable: "tururi",
                        principalColumn: "cod_tur",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tururi_categorii",
                columns: table => new
                {
                    cod_tur_categ = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    cod_tur = table.Column<int>(type: "int", nullable: false),
                    cod_categ = table.Column<byte>(type: "tinyint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tururi_categorii", x => x.cod_tur_categ);
                    table.ForeignKey(
                        name: "FK_tururi_categorii_categorii_cod_categ",
                        column: x => x.cod_categ,
                        principalTable: "categorii",
                        principalColumn: "cod_categ",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tururi_categorii_tururi_cod_tur",
                        column: x => x.cod_tur,
                        principalTable: "tururi",
                        principalColumn: "cod_tur",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CereriUtilizator",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CereriUtilizator", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CereriUtilizator_utilizatori_UserId",
                        column: x => x.UserId,
                        principalTable: "utilizatori",
                        principalColumn: "nr_utilizator",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LoginuriUtilizator",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoginuriUtilizator", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_LoginuriUtilizator_utilizatori_UserId",
                        column: x => x.UserId,
                        principalTable: "utilizatori",
                        principalColumn: "nr_utilizator",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TokenuriUtilizator",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TokenuriUtilizator", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_TokenuriUtilizator_utilizatori_UserId",
                        column: x => x.UserId,
                        principalTable: "utilizatori",
                        principalColumn: "nr_utilizator",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UtilizatorRoluri",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UtilizatorRoluri", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_UtilizatorRoluri_Roluri_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roluri",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UtilizatorRoluri_utilizatori_UserId",
                        column: x => x.UserId,
                        principalTable: "utilizatori",
                        principalColumn: "nr_utilizator",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "cazari",
                columns: table => new
                {
                    cod_cazare = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    den_cazare = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    desc_cazare = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    nr_stele = table.Column<byte>(type: "tinyint", nullable: false),
                    tip_cazare = table.Column<byte>(type: "tinyint", nullable: false),
                    adresa_cazare = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    check_in_inceput = table.Column<TimeSpan>(type: "time", nullable: false),
                    check_in_final = table.Column<TimeSpan>(type: "time", nullable: true),
                    check_out = table.Column<TimeSpan>(type: "time", nullable: false),
                    cod_destinatie = table.Column<short>(type: "smallint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cazari", x => x.cod_cazare);
                    table.ForeignKey(
                        name: "FK_cazari_destinatii_cod_destinatie",
                        column: x => x.cod_destinatie,
                        principalTable: "destinatii",
                        principalColumn: "cod_destinatie",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Rezervari",
                columns: table => new
                {
                    cod_rezervare = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    data_rezervare = table.Column<DateOnly>(type: "date", nullable: false),
                    status_rezervare = table.Column<byte>(type: "tinyint", nullable: false),
                    cod_oferta = table.Column<int>(type: "int", nullable: false),
                    nr_utilizator = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rezervari", x => x.cod_rezervare);
                    table.ForeignKey(
                        name: "FK_Rezervari_oferte_cod_oferta",
                        column: x => x.cod_oferta,
                        principalTable: "oferte",
                        principalColumn: "cod_oferta",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Rezervari_utilizatori_nr_utilizator",
                        column: x => x.nr_utilizator,
                        principalTable: "utilizatori",
                        principalColumn: "nr_utilizator");
                });

            migrationBuilder.CreateTable(
                name: "itinerarii",
                columns: table => new
                {
                    cod_itinerariu = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    titlu_itin = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    desc_itin = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    img_itin = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    zi_activitate = table.Column<byte>(type: "tinyint", nullable: false),
                    cod_tur = table.Column<int>(type: "int", nullable: false),
                    cod_cazare = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_itinerarii", x => x.cod_itinerariu);
                    table.ForeignKey(
                        name: "FK_itinerarii_cazari_cod_cazare",
                        column: x => x.cod_cazare,
                        principalTable: "cazari",
                        principalColumn: "cod_cazare");
                    table.ForeignKey(
                        name: "FK_itinerarii_tururi_cod_tur",
                        column: x => x.cod_tur,
                        principalTable: "tururi",
                        principalColumn: "cod_tur",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Camere",
                columns: table => new
                {
                    cod_camera = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    cod_rezervare = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Camere", x => x.cod_camera);
                    table.ForeignKey(
                        name: "FK_Camere_Rezervari_cod_rezervare",
                        column: x => x.cod_rezervare,
                        principalTable: "Rezervari",
                        principalColumn: "cod_rezervare",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Plati",
                columns: table => new
                {
                    cod_plata = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    status_plata = table.Column<byte>(type: "tinyint", nullable: false),
                    suma_plata = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    tip_plata = table.Column<bool>(type: "bit", nullable: false),
                    termen_plata = table.Column<DateOnly>(type: "date", nullable: false),
                    nr_rate = table.Column<byte>(type: "tinyint", nullable: true),
                    creat_la = table.Column<DateTime>(type: "datetime", nullable: false),
                    modificat_la = table.Column<DateTime>(type: "datetime", nullable: true),
                    stripe_charge_id = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    stripe_currency = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: true),
                    cod_rezervare = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Plati", x => x.cod_plata);
                    table.ForeignKey(
                        name: "FK_Plati_Rezervari_cod_rezervare",
                        column: x => x.cod_rezervare,
                        principalTable: "Rezervari",
                        principalColumn: "cod_rezervare",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "destinatii_itinerarii",
                columns: table => new
                {
                    cod_dest_itin = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    cod_itinerariu = table.Column<int>(type: "int", nullable: false),
                    cod_destinatie = table.Column<short>(type: "smallint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_destinatii_itinerarii", x => x.cod_dest_itin);
                    table.ForeignKey(
                        name: "FK_destinatii_itinerarii_destinatii_cod_destinatie",
                        column: x => x.cod_destinatie,
                        principalTable: "destinatii",
                        principalColumn: "cod_destinatie",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_destinatii_itinerarii_itinerarii_cod_itinerariu",
                        column: x => x.cod_itinerariu,
                        principalTable: "itinerarii",
                        principalColumn: "cod_itinerariu",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "beneficiari",
                columns: table => new
                {
                    cod_beneficiar = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nume_beneficiar = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    prenume_beneficiar = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    data_nastere = table.Column<DateOnly>(type: "date", nullable: false),
                    cod_camera = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_beneficiari", x => x.cod_beneficiar);
                    table.ForeignKey(
                        name: "FK_beneficiari_Camere_cod_camera",
                        column: x => x.cod_camera,
                        principalTable: "Camere",
                        principalColumn: "cod_camera",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "facturi",
                columns: table => new
                {
                    cod_factura = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    status_factura = table.Column<byte>(type: "tinyint", nullable: false),
                    suma_factura = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    data_achitarii = table.Column<DateOnly>(type: "date", nullable: false),
                    stripe_charge_id = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    stripe_currency = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: false),
                    nr_rata = table.Column<byte>(type: "tinyint", nullable: true),
                    cod_plata = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_facturi", x => x.cod_factura);
                    table.ForeignKey(
                        name: "FK_facturi_Plati_cod_plata",
                        column: x => x.cod_plata,
                        principalTable: "Plati",
                        principalColumn: "cod_plata",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_beneficiari_cod_camera",
                table: "beneficiari",
                column: "cod_camera");

            migrationBuilder.CreateIndex(
                name: "IX_Camere_cod_rezervare",
                table: "Camere",
                column: "cod_rezervare");

            migrationBuilder.CreateIndex(
                name: "IX_cazari_cod_destinatie",
                table: "cazari",
                column: "cod_destinatie");

            migrationBuilder.CreateIndex(
                name: "IX_CereriRol_RoleId",
                table: "CereriRol",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_CereriUtilizator_UserId",
                table: "CereriUtilizator",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_destinatii_cod_tara",
                table: "destinatii",
                column: "cod_tara");

            migrationBuilder.CreateIndex(
                name: "IX_destinatii_itinerarii_cod_destinatie",
                table: "destinatii_itinerarii",
                column: "cod_destinatie");

            migrationBuilder.CreateIndex(
                name: "IX_destinatii_itinerarii_cod_itinerariu",
                table: "destinatii_itinerarii",
                column: "cod_itinerariu");

            migrationBuilder.CreateIndex(
                name: "IX_facturi_cod_plata",
                table: "facturi",
                column: "cod_plata");

            migrationBuilder.CreateIndex(
                name: "IX_itinerarii_cod_cazare",
                table: "itinerarii",
                column: "cod_cazare");

            migrationBuilder.CreateIndex(
                name: "IX_itinerarii_cod_tur",
                table: "itinerarii",
                column: "cod_tur");

            migrationBuilder.CreateIndex(
                name: "IX_LoginuriUtilizator_UserId",
                table: "LoginuriUtilizator",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_oferte_cod_punct",
                table: "oferte",
                column: "cod_punct");

            migrationBuilder.CreateIndex(
                name: "IX_oferte_cod_tur",
                table: "oferte",
                column: "cod_tur");

            migrationBuilder.CreateIndex(
                name: "IX_Plati_cod_rezervare",
                table: "Plati",
                column: "cod_rezervare");

            migrationBuilder.CreateIndex(
                name: "IX_Rezervari_cod_oferta",
                table: "Rezervari",
                column: "cod_oferta");

            migrationBuilder.CreateIndex(
                name: "IX_Rezervari_nr_utilizator",
                table: "Rezervari",
                column: "nr_utilizator");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "Roluri",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_tururi_categorii_cod_categ",
                table: "tururi_categorii",
                column: "cod_categ");

            migrationBuilder.CreateIndex(
                name: "IX_tururi_categorii_cod_tur",
                table: "tururi_categorii",
                column: "cod_tur");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "utilizatori",
                column: "email_normalizat");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "utilizatori",
                column: "nume_normalizat",
                unique: true,
                filter: "[nume_normalizat] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_UtilizatorRoluri_RoleId",
                table: "UtilizatorRoluri",
                column: "RoleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "beneficiari");

            migrationBuilder.DropTable(
                name: "CereriRol");

            migrationBuilder.DropTable(
                name: "CereriUtilizator");

            migrationBuilder.DropTable(
                name: "destinatii_itinerarii");

            migrationBuilder.DropTable(
                name: "facturi");

            migrationBuilder.DropTable(
                name: "LoginuriUtilizator");

            migrationBuilder.DropTable(
                name: "TokenuriUtilizator");

            migrationBuilder.DropTable(
                name: "tururi_categorii");

            migrationBuilder.DropTable(
                name: "UtilizatorRoluri");

            migrationBuilder.DropTable(
                name: "Camere");

            migrationBuilder.DropTable(
                name: "itinerarii");

            migrationBuilder.DropTable(
                name: "Plati");

            migrationBuilder.DropTable(
                name: "categorii");

            migrationBuilder.DropTable(
                name: "Roluri");

            migrationBuilder.DropTable(
                name: "cazari");

            migrationBuilder.DropTable(
                name: "Rezervari");

            migrationBuilder.DropTable(
                name: "destinatii");

            migrationBuilder.DropTable(
                name: "oferte");

            migrationBuilder.DropTable(
                name: "utilizatori");

            migrationBuilder.DropTable(
                name: "tari");

            migrationBuilder.DropTable(
                name: "puncte_plecare");

            migrationBuilder.DropTable(
                name: "tururi");
        }
    }
}
