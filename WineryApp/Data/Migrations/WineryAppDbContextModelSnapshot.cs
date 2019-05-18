﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WineryApp.Data;

namespace WineryApp.Migrations
{
    [DbContext(typeof(WineryAppDbContext))]
    partial class WineryAppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.1-servicing-10028")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUser", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128);

                    b.Property<string>("ProviderKey")
                        .HasMaxLength(128);

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128);

                    b.Property<string>("Name")
                        .HasMaxLength(128);

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("WineryApp.Data.Entiteti.Aditiv", b =>
                {
                    b.Property<int>("AditivId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ImeAditiva")
                        .HasMaxLength(50)
                        .IsUnicode(false);

                    b.Property<string>("Instrukcije")
                        .IsUnicode(false);

                    b.Property<int?>("Količina");

                    b.Property<decimal?>("Koncentracija")
                        .HasColumnType("decimal(8, 2)");

                    b.Property<int>("VrstaAditivaId");

                    b.HasKey("AditivId");

                    b.HasIndex("VrstaAditivaId");

                    b.ToTable("Aditiv");
                });

            modelBuilder.Entity("WineryApp.Data.Entiteti.Berba", b =>
                {
                    b.Property<int>("BerbaId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("GodinaBerbe");

                    b.HasKey("BerbaId");

                    b.ToTable("Berba");
                });

            modelBuilder.Entity("WineryApp.Data.Entiteti.KategorijaZadatka", b =>
                {
                    b.Property<int>("KategorijaZadatkaId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ImeKategorije")
                        .HasMaxLength(50)
                        .IsUnicode(false);

                    b.HasKey("KategorijaZadatkaId");

                    b.ToTable("KategorijaZadatka");

                    b.HasData(
                        new
                        {
                            KategorijaZadatkaId = 1,
                            ImeKategorije = "Muljanje"
                        },
                        new
                        {
                            KategorijaZadatkaId = 2,
                            ImeKategorije = "Ruljenje"
                        },
                        new
                        {
                            KategorijaZadatkaId = 3,
                            ImeKategorije = "Prešanje"
                        },
                        new
                        {
                            KategorijaZadatkaId = 4,
                            ImeKategorije = "Dodavanje aditiva"
                        },
                        new
                        {
                            KategorijaZadatkaId = 5,
                            ImeKategorije = "Fermentacija"
                        },
                        new
                        {
                            KategorijaZadatkaId = 6,
                            ImeKategorije = "Pakiranje"
                        },
                        new
                        {
                            KategorijaZadatkaId = 7,
                            ImeKategorije = "Doslađivanje"
                        },
                        new
                        {
                            KategorijaZadatkaId = 8,
                            ImeKategorije = "Bistrenje"
                        },
                        new
                        {
                            KategorijaZadatkaId = 9,
                            ImeKategorije = "Pretakanje"
                        },
                        new
                        {
                            KategorijaZadatkaId = 10,
                            ImeKategorije = "Dozrijevanje"
                        },
                        new
                        {
                            KategorijaZadatkaId = 11,
                            ImeKategorije = "Tiha fermentacija"
                        },
                        new
                        {
                            KategorijaZadatkaId = 12,
                            ImeKategorije = "Burna fermentacija"
                        });
                });

            modelBuilder.Entity("WineryApp.Data.Entiteti.Podrum", b =>
                {
                    b.Property<int>("PodrumId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Lokacija")
                        .IsUnicode(false);

                    b.Property<double>("Popunjenost")
                        .HasMaxLength(50)
                        .IsUnicode(false);

                    b.Property<string>("ŠifraPodruma")
                        .HasMaxLength(50)
                        .IsUnicode(false);

                    b.HasKey("PodrumId");

                    b.ToTable("Podrum");
                });

            modelBuilder.Entity("WineryApp.Data.Entiteti.PovijestAditiva", b =>
                {
                    b.Property<int>("PovijestAditivaId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AditivId");

                    b.Property<string>("Akcija")
                        .HasMaxLength(50)
                        .IsUnicode(false);

                    b.Property<DateTime?>("Datum")
                        .HasColumnType("datetime");

                    b.Property<int?>("IskorištenaKoličina");

                    b.Property<int>("PodrumId");

                    b.Property<int?>("PreostalaKoličina");

                    b.Property<int>("ZaposlenikId");

                    b.HasKey("PovijestAditivaId");

                    b.HasIndex("AditivId");

                    b.HasIndex("PodrumId");

                    b.HasIndex("ZaposlenikId");

                    b.ToTable("PovijestAditiva");
                });

            modelBuilder.Entity("WineryApp.Data.Entiteti.PovijestSpremnika", b =>
                {
                    b.Property<int>("PovijestSpremnikaId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Akcija")
                        .HasMaxLength(50)
                        .IsUnicode(false);

                    b.Property<string>("Bilješka")
                        .IsUnicode(false);

                    b.Property<DateTime?>("DatumAkcije")
                        .HasColumnType("date");

                    b.Property<string>("DetaljiAkcije")
                        .IsUnicode(false);

                    b.Property<int>("ZaposlenikId");

                    b.HasKey("PovijestSpremnikaId");

                    b.HasIndex("ZaposlenikId");

                    b.ToTable("PovijestSpremnika");
                });

            modelBuilder.Entity("WineryApp.Data.Entiteti.RezultatAnalize", b =>
                {
                    b.Property<int>("RezultatAnalizeId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime?>("DatumUzimanjaUzorka")
                        .HasColumnType("date");

                    b.Property<decimal?>("Kiselina")
                        .HasColumnType("decimal(8, 2)");

                    b.Property<decimal?>("PhVrijednost")
                        .HasColumnType("decimal(8, 2)");

                    b.Property<decimal?>("PostotakAlkohola")
                        .HasColumnType("decimal(8, 2)");

                    b.Property<decimal?>("RezidualniŠećer")
                        .HasColumnType("decimal(8, 2)");

                    b.Property<decimal?>("SlobodniSumpor")
                        .HasColumnType("decimal(8, 2)");

                    b.Property<int>("SpremnikId");

                    b.Property<byte?>("StatusRezultata");

                    b.Property<decimal?>("UkupniSumpor")
                        .HasColumnType("decimal(8, 2)");

                    b.Property<int>("UzorakUzeoId");

                    b.Property<decimal?>("Šećer")
                        .HasColumnType("decimal(8, 2)");

                    b.Property<string>("ŠifraPodruma")
                        .HasMaxLength(50)
                        .IsUnicode(false);

                    b.Property<string>("ŠifraUzorka")
                        .HasMaxLength(50)
                        .IsUnicode(false);

                    b.HasKey("RezultatAnalizeId");

                    b.HasIndex("SpremnikId");

                    b.HasIndex("UzorakUzeoId");

                    b.ToTable("RezultatAnalize");
                });

            modelBuilder.Entity("WineryApp.Data.Entiteti.SortaVina", b =>
                {
                    b.Property<int>("SortaVinaId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("NazivSorte")
                        .HasMaxLength(50)
                        .IsUnicode(false);

                    b.HasKey("SortaVinaId");

                    b.ToTable("SortaVina");

                    b.HasData(
                        new
                        {
                            SortaVinaId = 1,
                            NazivSorte = "Graševina"
                        },
                        new
                        {
                            SortaVinaId = 2,
                            NazivSorte = "Rajnski rizling"
                        },
                        new
                        {
                            SortaVinaId = 3,
                            NazivSorte = "Chardonnay"
                        },
                        new
                        {
                            SortaVinaId = 4,
                            NazivSorte = "Moslavac"
                        },
                        new
                        {
                            SortaVinaId = 5,
                            NazivSorte = "Škrlet"
                        },
                        new
                        {
                            SortaVinaId = 6,
                            NazivSorte = "Kraljevina"
                        },
                        new
                        {
                            SortaVinaId = 7,
                            NazivSorte = "Bijeli pinot"
                        },
                        new
                        {
                            SortaVinaId = 8,
                            NazivSorte = "Sivi pinot"
                        },
                        new
                        {
                            SortaVinaId = 9,
                            NazivSorte = "Zeleni silvanac"
                        },
                        new
                        {
                            SortaVinaId = 10,
                            NazivSorte = "Traminac"
                        },
                        new
                        {
                            SortaVinaId = 11,
                            NazivSorte = "Sauvignon"
                        },
                        new
                        {
                            SortaVinaId = 12,
                            NazivSorte = "Frankovka"
                        },
                        new
                        {
                            SortaVinaId = 13,
                            NazivSorte = "Zweigelt"
                        },
                        new
                        {
                            SortaVinaId = 14,
                            NazivSorte = "Malvazija"
                        },
                        new
                        {
                            SortaVinaId = 15,
                            NazivSorte = "Merlot"
                        },
                        new
                        {
                            SortaVinaId = 16,
                            NazivSorte = "Cabernet sauvignon"
                        },
                        new
                        {
                            SortaVinaId = 17,
                            NazivSorte = "Teran"
                        },
                        new
                        {
                            SortaVinaId = 18,
                            NazivSorte = "Refok"
                        },
                        new
                        {
                            SortaVinaId = 19,
                            NazivSorte = "Muškat"
                        },
                        new
                        {
                            SortaVinaId = 20,
                            NazivSorte = "Trojšćina"
                        },
                        new
                        {
                            SortaVinaId = 21,
                            NazivSorte = "Vrbnička žlahtina"
                        },
                        new
                        {
                            SortaVinaId = 22,
                            NazivSorte = "Babić"
                        },
                        new
                        {
                            SortaVinaId = 23,
                            NazivSorte = "Plavina"
                        },
                        new
                        {
                            SortaVinaId = 24,
                            NazivSorte = "Debit"
                        },
                        new
                        {
                            SortaVinaId = 25,
                            NazivSorte = "Pošip"
                        },
                        new
                        {
                            SortaVinaId = 26,
                            NazivSorte = "Maraština"
                        },
                        new
                        {
                            SortaVinaId = 27,
                            NazivSorte = "Grk"
                        },
                        new
                        {
                            SortaVinaId = 28,
                            NazivSorte = "Bijela vugava"
                        },
                        new
                        {
                            SortaVinaId = 29,
                            NazivSorte = "Crni plavac"
                        },
                        new
                        {
                            SortaVinaId = 30,
                            NazivSorte = "Plavac"
                        },
                        new
                        {
                            SortaVinaId = 31,
                            NazivSorte = "Bogdanuša"
                        },
                        new
                        {
                            SortaVinaId = 32,
                            NazivSorte = "Plavac mali"
                        },
                        new
                        {
                            SortaVinaId = 33,
                            NazivSorte = "Dingač"
                        });
                });

            modelBuilder.Entity("WineryApp.Data.Entiteti.Spremnik", b =>
                {
                    b.Property<int>("SpremnikId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("BerbaId");

                    b.Property<string>("FazaIzrade")
                        .HasMaxLength(50)
                        .IsUnicode(false);

                    b.Property<double>("Kapacitet")
                        .HasMaxLength(50)
                        .IsUnicode(false);

                    b.Property<double>("Napunjenost")
                        .HasMaxLength(50)
                        .IsUnicode(false);

                    b.Property<int?>("PodrumId");

                    b.Property<int?>("PunilacId");

                    b.Property<int?>("SortaVinaId");

                    b.Property<int>("VrstaSpremnikaId");

                    b.Property<string>("ŠifraSpremnika")
                        .HasMaxLength(50)
                        .IsUnicode(false);

                    b.HasKey("SpremnikId");

                    b.HasIndex("BerbaId");

                    b.HasIndex("PodrumId");

                    b.HasIndex("PunilacId");

                    b.HasIndex("SortaVinaId");

                    b.HasIndex("VrstaSpremnikaId");

                    b.ToTable("Spremnik");
                });

            modelBuilder.Entity("WineryApp.Data.Entiteti.Uloga", b =>
                {
                    b.Property<int>("UlogaId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("NazivUloga")
                        .HasMaxLength(50)
                        .IsUnicode(false);

                    b.HasKey("UlogaId");

                    b.ToTable("Uloga");

                    b.HasData(
                        new
                        {
                            UlogaId = 1,
                            NazivUloga = "Vlasnik"
                        },
                        new
                        {
                            UlogaId = 2,
                            NazivUloga = "Zaposlenik"
                        });
                });

            modelBuilder.Entity("WineryApp.Data.Entiteti.VrstaAditiva", b =>
                {
                    b.Property<int>("VrstaAditivaId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("NazivVrste")
                        .HasMaxLength(50)
                        .IsUnicode(false);

                    b.Property<string>("Opis")
                        .HasMaxLength(255)
                        .IsUnicode(false);

                    b.HasKey("VrstaAditivaId");

                    b.ToTable("VrstaAditiva");
                });

            modelBuilder.Entity("WineryApp.Data.Entiteti.VrstaSpremnika", b =>
                {
                    b.Property<int>("VrstaSpremnikaId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("NazivVrste")
                        .HasMaxLength(50)
                        .IsUnicode(false);

                    b.Property<string>("Opis")
                        .HasMaxLength(50)
                        .IsUnicode(false);

                    b.HasKey("VrstaSpremnikaId");

                    b.ToTable("VrstaSpremnika");

                    b.HasData(
                        new
                        {
                            VrstaSpremnikaId = 1,
                            NazivVrste = "Bačva",
                            Opis = "Drvena bačva sa željeznim obručima"
                        },
                        new
                        {
                            VrstaSpremnikaId = 2,
                            NazivVrste = "Inox bačva",
                            Opis = "Inox bačva za držanje vina"
                        },
                        new
                        {
                            VrstaSpremnikaId = 3,
                            NazivVrste = "Staklena boca veća",
                            Opis = "Veća staklena boca za alkoholnu fermentaciju"
                        },
                        new
                        {
                            VrstaSpremnikaId = 4,
                            NazivVrste = "Staklena boca srednja",
                            Opis = "Srednja staklena boca za alkoholnu fermentaciju"
                        },
                        new
                        {
                            VrstaSpremnikaId = 5,
                            NazivVrste = "Spremnik",
                            Opis = "Inox posuda za držanje velikih količina"
                        });
                });

            modelBuilder.Entity("WineryApp.Data.Entiteti.Zadatak", b =>
                {
                    b.Property<int>("ZadatakId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Bilješke")
                        .IsUnicode(false);

                    b.Property<string>("ImeZadatka")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false);

                    b.Property<int>("KategorijaZadatkaId");

                    b.Property<int?>("PodrumId");

                    b.Property<DateTime>("PočetakZadatka")
                        .HasColumnType("date");

                    b.Property<DateTime>("RokZadatka")
                        .HasColumnType("date");

                    b.Property<int?>("SpremnikId");

                    b.Property<byte?>("StatusZadatka");

                    b.Property<int>("ZaduženiZaposlenik");

                    b.HasKey("ZadatakId");

                    b.HasIndex("KategorijaZadatkaId");

                    b.HasIndex("PodrumId");

                    b.HasIndex("SpremnikId");

                    b.HasIndex("ZaduženiZaposlenik");

                    b.ToTable("Zadatak");
                });

            modelBuilder.Entity("WineryApp.Data.Entiteti.Zaposlenik", b =>
                {
                    b.Property<int>("ZaposlenikId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Adresa")
                        .HasMaxLength(255)
                        .IsUnicode(false);

                    b.Property<DateTime>("DatumZaposlenja")
                        .HasColumnType("date");

                    b.Property<string>("Email")
                        .HasMaxLength(50)
                        .IsUnicode(false);

                    b.Property<string>("Grad")
                        .HasMaxLength(50)
                        .IsUnicode(false);

                    b.Property<string>("Ime")
                        .HasMaxLength(50)
                        .IsUnicode(false);

                    b.Property<string>("KorisnickoIme")
                        .IsRequired()
                        .HasMaxLength(450);

                    b.Property<string>("Lozinka")
                        .HasMaxLength(255)
                        .IsUnicode(false);

                    b.Property<string>("Prezime")
                        .HasMaxLength(50)
                        .IsUnicode(false);

                    b.Property<string>("Spol")
                        .HasMaxLength(50)
                        .IsUnicode(false);

                    b.Property<string>("Telefon")
                        .HasMaxLength(50)
                        .IsUnicode(false);

                    b.Property<int>("UlogaId");

                    b.Property<string>("UserId");

                    b.HasKey("ZaposlenikId");

                    b.HasIndex("UlogaId");

                    b.HasIndex("UserId");

                    b.ToTable("Zaposlenik");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("WineryApp.Data.Entiteti.Aditiv", b =>
                {
                    b.HasOne("WineryApp.Data.Entiteti.VrstaAditiva", "VrstaAditiva")
                        .WithMany("Aditiv")
                        .HasForeignKey("VrstaAditivaId")
                        .HasConstraintName("FK__Aditiv__VrstaAdi__534D60F1");
                });

            modelBuilder.Entity("WineryApp.Data.Entiteti.PovijestAditiva", b =>
                {
                    b.HasOne("WineryApp.Data.Entiteti.Aditiv", "Aditiv")
                        .WithMany("PovijestAditiva")
                        .HasForeignKey("AditivId")
                        .HasConstraintName("FK__PovijestA__Aditi__5629CD9C");

                    b.HasOne("WineryApp.Data.Entiteti.Podrum", "Podrum")
                        .WithMany("PovijestAditiva")
                        .HasForeignKey("PodrumId")
                        .HasConstraintName("FK__PovijestA__Podru__571DF1D5");

                    b.HasOne("WineryApp.Data.Entiteti.Zaposlenik", "Zaposlenik")
                        .WithMany("PovijestAditiva")
                        .HasForeignKey("ZaposlenikId")
                        .HasConstraintName("FK__PovijestA__Zapos__5812160E");
                });

            modelBuilder.Entity("WineryApp.Data.Entiteti.PovijestSpremnika", b =>
                {
                    b.HasOne("WineryApp.Data.Entiteti.Zaposlenik", "Zaposlenik")
                        .WithMany("PovijestSpremnika")
                        .HasForeignKey("ZaposlenikId")
                        .HasConstraintName("FK__PovijestS__Zapos__4E88ABD4");
                });

            modelBuilder.Entity("WineryApp.Data.Entiteti.RezultatAnalize", b =>
                {
                    b.HasOne("WineryApp.Data.Entiteti.Spremnik", "Spremnik")
                        .WithMany("RezultatAnalize")
                        .HasForeignKey("SpremnikId")
                        .HasConstraintName("FK__RezultatA__Sprem__4BAC3F29");

                    b.HasOne("WineryApp.Data.Entiteti.Zaposlenik", "UzorakUzeo")
                        .WithMany("RezultatAnalize")
                        .HasForeignKey("UzorakUzeoId")
                        .HasConstraintName("FK__RezultatA__Uzora__4AB81AF0");
                });

            modelBuilder.Entity("WineryApp.Data.Entiteti.Spremnik", b =>
                {
                    b.HasOne("WineryApp.Data.Entiteti.Berba", "Berba")
                        .WithMany("Spremnik")
                        .HasForeignKey("BerbaId")
                        .HasConstraintName("FK__Spremnik__BerbaI__44FF419A");

                    b.HasOne("WineryApp.Data.Entiteti.Podrum", "Podrum")
                        .WithMany("Spremnik")
                        .HasForeignKey("PodrumId")
                        .HasConstraintName("FK__Spremnik__Podrum__46E78A0C");

                    b.HasOne("WineryApp.Data.Entiteti.Zaposlenik", "Punilac")
                        .WithMany("Spremnik")
                        .HasForeignKey("PunilacId")
                        .HasConstraintName("FK__Spremnik__Punila__45F365D3");

                    b.HasOne("WineryApp.Data.Entiteti.SortaVina", "SortaVina")
                        .WithMany("Spremnik")
                        .HasForeignKey("SortaVinaId")
                        .HasConstraintName("FK__Spremnik__SortaV__47DBAE45");

                    b.HasOne("WineryApp.Data.Entiteti.VrstaSpremnika", "VrstaSpremnika")
                        .WithMany("Spremnik")
                        .HasForeignKey("VrstaSpremnikaId")
                        .HasConstraintName("FK__Spremnik__VrstaS__440B1D61");
                });

            modelBuilder.Entity("WineryApp.Data.Entiteti.Zadatak", b =>
                {
                    b.HasOne("WineryApp.Data.Entiteti.KategorijaZadatka", "KategorijaZadatka")
                        .WithMany("Zadatak")
                        .HasForeignKey("KategorijaZadatkaId")
                        .HasConstraintName("FK__Zadatak__Kategor__5EBF139D");

                    b.HasOne("WineryApp.Data.Entiteti.Podrum", "Podrum")
                        .WithMany("Zadatak")
                        .HasForeignKey("PodrumId")
                        .HasConstraintName("FK__Zadatak__PodrumI__5CD6CB2B");

                    b.HasOne("WineryApp.Data.Entiteti.Spremnik", "Spremnik")
                        .WithMany("Zadatak")
                        .HasForeignKey("SpremnikId")
                        .HasConstraintName("FK__Zadatak__Spremni__5DCAEF64");

                    b.HasOne("WineryApp.Data.Entiteti.Zaposlenik", "ZaduženiZaposlenikNavigation")
                        .WithMany("Zadatak")
                        .HasForeignKey("ZaduženiZaposlenik")
                        .HasConstraintName("FK__Zadatak__Zadužen__5FB337D6");
                });

            modelBuilder.Entity("WineryApp.Data.Entiteti.Zaposlenik", b =>
                {
                    b.HasOne("WineryApp.Data.Entiteti.Uloga", "Uloga")
                        .WithMany("Zaposlenik")
                        .HasForeignKey("UlogaId")
                        .HasConstraintName("FK__Zaposleni__Uloga__398D8EEE");

                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", "User")
                        .WithMany()
                        .HasForeignKey("UserId");
                });
#pragma warning restore 612, 618
        }
    }
}
