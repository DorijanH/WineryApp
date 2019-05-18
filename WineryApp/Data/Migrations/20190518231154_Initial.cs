using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WineryApp.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    UserName = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true),
                    Email = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Berba",
                columns: table => new
                {
                    BerbaId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    GodinaBerbe = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Berba", x => x.BerbaId);
                });

            migrationBuilder.CreateTable(
                name: "KategorijaZadatka",
                columns: table => new
                {
                    KategorijaZadatkaId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ImeKategorije = table.Column<string>(unicode: false, maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KategorijaZadatka", x => x.KategorijaZadatkaId);
                });

            migrationBuilder.CreateTable(
                name: "Podrum",
                columns: table => new
                {
                    PodrumId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ŠifraPodruma = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    Popunjenost = table.Column<double>(unicode: false, maxLength: 50, nullable: false),
                    Lokacija = table.Column<string>(unicode: false, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Podrum", x => x.PodrumId);
                });

            migrationBuilder.CreateTable(
                name: "SortaVina",
                columns: table => new
                {
                    SortaVinaId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    NazivSorte = table.Column<string>(unicode: false, maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SortaVina", x => x.SortaVinaId);
                });

            migrationBuilder.CreateTable(
                name: "Uloga",
                columns: table => new
                {
                    UlogaId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    NazivUloga = table.Column<string>(unicode: false, maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Uloga", x => x.UlogaId);
                });

            migrationBuilder.CreateTable(
                name: "VrstaAditiva",
                columns: table => new
                {
                    VrstaAditivaId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    NazivVrste = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    Opis = table.Column<string>(unicode: false, maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VrstaAditiva", x => x.VrstaAditivaId);
                });

            migrationBuilder.CreateTable(
                name: "VrstaSpremnika",
                columns: table => new
                {
                    VrstaSpremnikaId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    NazivVrste = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    Opis = table.Column<string>(unicode: false, maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VrstaSpremnika", x => x.VrstaSpremnikaId);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    RoleId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(maxLength: 128, nullable: false),
                    ProviderKey = table.Column<string>(maxLength: 128, nullable: false),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    RoleId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    LoginProvider = table.Column<string>(maxLength: 128, nullable: false),
                    Name = table.Column<string>(maxLength: 128, nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Zaposlenik",
                columns: table => new
                {
                    ZaposlenikId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Ime = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    Prezime = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    Spol = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    Adresa = table.Column<string>(unicode: false, maxLength: 255, nullable: true),
                    Grad = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    Telefon = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    Email = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    Lozinka = table.Column<string>(unicode: false, maxLength: 255, nullable: true),
                    DatumZaposlenja = table.Column<DateTime>(type: "date", nullable: false),
                    KorisnickoIme = table.Column<string>(maxLength: 450, nullable: false),
                    UlogaId = table.Column<int>(nullable: false),
                    UserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Zaposlenik", x => x.ZaposlenikId);
                    table.ForeignKey(
                        name: "FK__Zaposleni__Uloga__398D8EEE",
                        column: x => x.UlogaId,
                        principalTable: "Uloga",
                        principalColumn: "UlogaId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Zaposlenik_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Aditiv",
                columns: table => new
                {
                    AditivId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ImeAditiva = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    Koncentracija = table.Column<decimal>(type: "decimal(8, 2)", nullable: true),
                    Količina = table.Column<int>(nullable: true),
                    Instrukcije = table.Column<string>(unicode: false, nullable: true),
                    VrstaAditivaId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Aditiv", x => x.AditivId);
                    table.ForeignKey(
                        name: "FK__Aditiv__VrstaAdi__534D60F1",
                        column: x => x.VrstaAditivaId,
                        principalTable: "VrstaAditiva",
                        principalColumn: "VrstaAditivaId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PovijestSpremnika",
                columns: table => new
                {
                    PovijestSpremnikaId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DatumAkcije = table.Column<DateTime>(type: "date", nullable: true),
                    Akcija = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    DetaljiAkcije = table.Column<string>(unicode: false, nullable: true),
                    Bilješka = table.Column<string>(unicode: false, nullable: true),
                    ZaposlenikId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PovijestSpremnika", x => x.PovijestSpremnikaId);
                    table.ForeignKey(
                        name: "FK__PovijestS__Zapos__4E88ABD4",
                        column: x => x.ZaposlenikId,
                        principalTable: "Zaposlenik",
                        principalColumn: "ZaposlenikId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Spremnik",
                columns: table => new
                {
                    SpremnikId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ŠifraSpremnika = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    Kapacitet = table.Column<double>(unicode: false, maxLength: 50, nullable: false),
                    Napunjenost = table.Column<double>(unicode: false, maxLength: 50, nullable: false),
                    FazaIzrade = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    VrstaSpremnikaId = table.Column<int>(nullable: false),
                    BerbaId = table.Column<int>(nullable: true),
                    PunilacId = table.Column<int>(nullable: true),
                    PodrumId = table.Column<int>(nullable: true),
                    SortaVinaId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Spremnik", x => x.SpremnikId);
                    table.ForeignKey(
                        name: "FK__Spremnik__BerbaI__44FF419A",
                        column: x => x.BerbaId,
                        principalTable: "Berba",
                        principalColumn: "BerbaId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK__Spremnik__Podrum__46E78A0C",
                        column: x => x.PodrumId,
                        principalTable: "Podrum",
                        principalColumn: "PodrumId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK__Spremnik__Punila__45F365D3",
                        column: x => x.PunilacId,
                        principalTable: "Zaposlenik",
                        principalColumn: "ZaposlenikId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK__Spremnik__SortaV__47DBAE45",
                        column: x => x.SortaVinaId,
                        principalTable: "SortaVina",
                        principalColumn: "SortaVinaId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK__Spremnik__VrstaS__440B1D61",
                        column: x => x.VrstaSpremnikaId,
                        principalTable: "VrstaSpremnika",
                        principalColumn: "VrstaSpremnikaId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PovijestAditiva",
                columns: table => new
                {
                    PovijestAditivaId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Akcija = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    Datum = table.Column<DateTime>(type: "datetime", nullable: true),
                    IskorištenaKoličina = table.Column<int>(nullable: true),
                    PreostalaKoličina = table.Column<int>(nullable: true),
                    AditivId = table.Column<int>(nullable: false),
                    PodrumId = table.Column<int>(nullable: false),
                    ZaposlenikId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PovijestAditiva", x => x.PovijestAditivaId);
                    table.ForeignKey(
                        name: "FK__PovijestA__Aditi__5629CD9C",
                        column: x => x.AditivId,
                        principalTable: "Aditiv",
                        principalColumn: "AditivId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK__PovijestA__Podru__571DF1D5",
                        column: x => x.PodrumId,
                        principalTable: "Podrum",
                        principalColumn: "PodrumId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK__PovijestA__Zapos__5812160E",
                        column: x => x.ZaposlenikId,
                        principalTable: "Zaposlenik",
                        principalColumn: "ZaposlenikId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RezultatAnalize",
                columns: table => new
                {
                    RezultatAnalizeId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ŠifraUzorka = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    DatumUzimanjaUzorka = table.Column<DateTime>(type: "date", nullable: true),
                    StatusRezultata = table.Column<byte>(nullable: true),
                    ŠifraPodruma = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    PhVrijednost = table.Column<decimal>(type: "decimal(8, 2)", nullable: true),
                    Šećer = table.Column<decimal>(type: "decimal(8, 2)", nullable: true),
                    RezidualniŠećer = table.Column<decimal>(type: "decimal(8, 2)", nullable: true),
                    SlobodniSumpor = table.Column<decimal>(type: "decimal(8, 2)", nullable: true),
                    UkupniSumpor = table.Column<decimal>(type: "decimal(8, 2)", nullable: true),
                    Kiselina = table.Column<decimal>(type: "decimal(8, 2)", nullable: true),
                    PostotakAlkohola = table.Column<decimal>(type: "decimal(8, 2)", nullable: true),
                    UzorakUzeoId = table.Column<int>(nullable: false),
                    SpremnikId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RezultatAnalize", x => x.RezultatAnalizeId);
                    table.ForeignKey(
                        name: "FK__RezultatA__Sprem__4BAC3F29",
                        column: x => x.SpremnikId,
                        principalTable: "Spremnik",
                        principalColumn: "SpremnikId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK__RezultatA__Uzora__4AB81AF0",
                        column: x => x.UzorakUzeoId,
                        principalTable: "Zaposlenik",
                        principalColumn: "ZaposlenikId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Zadatak",
                columns: table => new
                {
                    ZadatakId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ImeZadatka = table.Column<string>(unicode: false, maxLength: 50, nullable: false),
                    PočetakZadatka = table.Column<DateTime>(type: "date", nullable: false),
                    RokZadatka = table.Column<DateTime>(type: "date", nullable: false),
                    StatusZadatka = table.Column<byte>(nullable: true),
                    Bilješke = table.Column<string>(unicode: false, nullable: true),
                    PodrumId = table.Column<int>(nullable: true),
                    SpremnikId = table.Column<int>(nullable: true),
                    KategorijaZadatkaId = table.Column<int>(nullable: false),
                    ZaduženiZaposlenik = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Zadatak", x => x.ZadatakId);
                    table.ForeignKey(
                        name: "FK__Zadatak__Kategor__5EBF139D",
                        column: x => x.KategorijaZadatkaId,
                        principalTable: "KategorijaZadatka",
                        principalColumn: "KategorijaZadatkaId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK__Zadatak__PodrumI__5CD6CB2B",
                        column: x => x.PodrumId,
                        principalTable: "Podrum",
                        principalColumn: "PodrumId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK__Zadatak__Spremni__5DCAEF64",
                        column: x => x.SpremnikId,
                        principalTable: "Spremnik",
                        principalColumn: "SpremnikId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK__Zadatak__Zadužen__5FB337D6",
                        column: x => x.ZaduženiZaposlenik,
                        principalTable: "Zaposlenik",
                        principalColumn: "ZaposlenikId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "KategorijaZadatka",
                columns: new[] { "KategorijaZadatkaId", "ImeKategorije" },
                values: new object[,]
                {
                    { 1, "Muljanje" },
                    { 12, "Burna fermentacija" },
                    { 11, "Tiha fermentacija" },
                    { 9, "Pretakanje" },
                    { 8, "Bistrenje" },
                    { 7, "Doslađivanje" },
                    { 10, "Dozrijevanje" },
                    { 5, "Fermentacija" },
                    { 4, "Dodavanje aditiva" },
                    { 3, "Prešanje" },
                    { 2, "Ruljenje" },
                    { 6, "Pakiranje" }
                });

            migrationBuilder.InsertData(
                table: "SortaVina",
                columns: new[] { "SortaVinaId", "NazivSorte" },
                values: new object[,]
                {
                    { 25, "Pošip" },
                    { 20, "Trojšćina" },
                    { 21, "Vrbnička žlahtina" },
                    { 22, "Babić" },
                    { 23, "Plavina" },
                    { 24, "Debit" },
                    { 26, "Maraština" },
                    { 32, "Plavac mali" },
                    { 28, "Bijela vugava" },
                    { 29, "Crni plavac" },
                    { 30, "Plavac" },
                    { 31, "Bogdanuša" },
                    { 19, "Muškat" },
                    { 33, "Dingač" },
                    { 27, "Grk" },
                    { 18, "Refok" },
                    { 14, "Malvazija" },
                    { 16, "Cabernet sauvignon" },
                    { 1, "Graševina" },
                    { 2, "Rajnski rizling" },
                    { 3, "Chardonnay" },
                    { 4, "Moslavac" },
                    { 17, "Teran" },
                    { 6, "Kraljevina" },
                    { 7, "Bijeli pinot" },
                    { 5, "Škrlet" },
                    { 9, "Zeleni silvanac" },
                    { 10, "Traminac" },
                    { 11, "Sauvignon" },
                    { 12, "Frankovka" },
                    { 13, "Zweigelt" },
                    { 15, "Merlot" },
                    { 8, "Sivi pinot" }
                });

            migrationBuilder.InsertData(
                table: "Uloga",
                columns: new[] { "UlogaId", "NazivUloga" },
                values: new object[,]
                {
                    { 1, "Vlasnik" },
                    { 2, "Zaposlenik" }
                });

            migrationBuilder.InsertData(
                table: "VrstaSpremnika",
                columns: new[] { "VrstaSpremnikaId", "NazivVrste", "Opis" },
                values: new object[,]
                {
                    { 4, "Staklena boca srednja", "Srednja staklena boca za alkoholnu fermentaciju" },
                    { 1, "Bačva", "Drvena bačva sa željeznim obručima" },
                    { 2, "Inox bačva", "Inox bačva za držanje vina" },
                    { 3, "Staklena boca veća", "Veća staklena boca za alkoholnu fermentaciju" },
                    { 5, "Spremnik", "Inox posuda za držanje velikih količina" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Aditiv_VrstaAditivaId",
                table: "Aditiv",
                column: "VrstaAditivaId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_PovijestAditiva_AditivId",
                table: "PovijestAditiva",
                column: "AditivId");

            migrationBuilder.CreateIndex(
                name: "IX_PovijestAditiva_PodrumId",
                table: "PovijestAditiva",
                column: "PodrumId");

            migrationBuilder.CreateIndex(
                name: "IX_PovijestAditiva_ZaposlenikId",
                table: "PovijestAditiva",
                column: "ZaposlenikId");

            migrationBuilder.CreateIndex(
                name: "IX_PovijestSpremnika_ZaposlenikId",
                table: "PovijestSpremnika",
                column: "ZaposlenikId");

            migrationBuilder.CreateIndex(
                name: "IX_RezultatAnalize_SpremnikId",
                table: "RezultatAnalize",
                column: "SpremnikId");

            migrationBuilder.CreateIndex(
                name: "IX_RezultatAnalize_UzorakUzeoId",
                table: "RezultatAnalize",
                column: "UzorakUzeoId");

            migrationBuilder.CreateIndex(
                name: "IX_Spremnik_BerbaId",
                table: "Spremnik",
                column: "BerbaId");

            migrationBuilder.CreateIndex(
                name: "IX_Spremnik_PodrumId",
                table: "Spremnik",
                column: "PodrumId");

            migrationBuilder.CreateIndex(
                name: "IX_Spremnik_PunilacId",
                table: "Spremnik",
                column: "PunilacId");

            migrationBuilder.CreateIndex(
                name: "IX_Spremnik_SortaVinaId",
                table: "Spremnik",
                column: "SortaVinaId");

            migrationBuilder.CreateIndex(
                name: "IX_Spremnik_VrstaSpremnikaId",
                table: "Spremnik",
                column: "VrstaSpremnikaId");

            migrationBuilder.CreateIndex(
                name: "IX_Zadatak_KategorijaZadatkaId",
                table: "Zadatak",
                column: "KategorijaZadatkaId");

            migrationBuilder.CreateIndex(
                name: "IX_Zadatak_PodrumId",
                table: "Zadatak",
                column: "PodrumId");

            migrationBuilder.CreateIndex(
                name: "IX_Zadatak_SpremnikId",
                table: "Zadatak",
                column: "SpremnikId");

            migrationBuilder.CreateIndex(
                name: "IX_Zadatak_ZaduženiZaposlenik",
                table: "Zadatak",
                column: "ZaduženiZaposlenik");

            migrationBuilder.CreateIndex(
                name: "IX_Zaposlenik_UlogaId",
                table: "Zaposlenik",
                column: "UlogaId");

            migrationBuilder.CreateIndex(
                name: "IX_Zaposlenik_UserId",
                table: "Zaposlenik",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "PovijestAditiva");

            migrationBuilder.DropTable(
                name: "PovijestSpremnika");

            migrationBuilder.DropTable(
                name: "RezultatAnalize");

            migrationBuilder.DropTable(
                name: "Zadatak");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Aditiv");

            migrationBuilder.DropTable(
                name: "KategorijaZadatka");

            migrationBuilder.DropTable(
                name: "Spremnik");

            migrationBuilder.DropTable(
                name: "VrstaAditiva");

            migrationBuilder.DropTable(
                name: "Berba");

            migrationBuilder.DropTable(
                name: "Podrum");

            migrationBuilder.DropTable(
                name: "Zaposlenik");

            migrationBuilder.DropTable(
                name: "SortaVina");

            migrationBuilder.DropTable(
                name: "VrstaSpremnika");

            migrationBuilder.DropTable(
                name: "Uloga");

            migrationBuilder.DropTable(
                name: "AspNetUsers");
        }
    }
}
