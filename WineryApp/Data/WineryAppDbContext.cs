using System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using WineryApp.Data.Entiteti;

namespace WineryApp.Data
{
    public partial class WineryAppDbContext : IdentityDbContext
    {
        public WineryAppDbContext(DbContextOptions<WineryAppDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Aditiv> Aditiv { get; set; }
        public virtual DbSet<Berba> Berba { get; set; }
        public virtual DbSet<KategorijaZadatka> KategorijaZadatka { get; set; }
        public virtual DbSet<Narudžba> Narudžba { get; set; }
        public virtual DbSet<Partner> Partner { get; set; }
        public virtual DbSet<Podrum> Podrum { get; set; }
        public virtual DbSet<PovijestAditiva> PovijestAditiva { get; set; }
        public virtual DbSet<PovijestSpremnika> PovijestSpremnika { get; set; }
        public virtual DbSet<RezultatAnalize> RezultatAnalize { get; set; }
        public virtual DbSet<SortaVina> SortaVina { get; set; }
        public virtual DbSet<Spremnik> Spremnik { get; set; }
        public virtual DbSet<Uloga> Uloga { get; set; }
        public virtual DbSet<VrstaAditiva> VrstaAditiva { get; set; }
        public virtual DbSet<VrstaSpremnika> VrstaSpremnika { get; set; }
        public virtual DbSet<Zadatak> Zadatak { get; set; }
        public virtual DbSet<Zaposlenik> Zaposlenik { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.HasAnnotation("ProductVersion", "2.2.1-servicing-10028");

            modelBuilder.Entity<Aditiv>(entity =>
            {
                entity.Property(e => e.ImeAditiva)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Instrukcije).IsUnicode(false);

                entity.Property(e => e.Količina).HasColumnType("decimal(8, 2)");

                entity.Property(e => e.Koncentracija).HasColumnType("decimal(8, 2)");

                entity.HasOne(d => d.VrstaAditiva)
                    .WithMany(p => p.Aditiv)
                    .HasForeignKey(d => d.VrstaAditivaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Aditiv__VrstaAdi__5441852A");
            });

            modelBuilder.Entity<KategorijaZadatka>(entity =>
            {
                entity.Property(e => e.ImeKategorije)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Narudžba>(entity =>
            {
                entity.HasKey(e => e.NarudzbaId)
                    .HasName("PK__Narudžba__FBEC1377C0AA31C4");

                entity.Property(e => e.AdresaKupca)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.DatumIsporuke).HasColumnType("date");

                entity.Property(e => e.DatumNaplate).HasColumnType("date");

                entity.Property(e => e.DatumNarudzbe).HasColumnType("date");

                entity.Property(e => e.ImeKupca)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Količina).HasColumnType("decimal(8, 2)");

                entity.Property(e => e.KonacnaCijena).HasColumnType("decimal(10, 2)");

                entity.Property(e => e.PrezimeKupca)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Partner)
                    .WithMany(p => p.Narudžba)
                    .HasForeignKey(d => d.PartnerId)
                    .HasConstraintName("FK__Narudžba__Partne__6754599E");

                entity.HasOne(d => d.Spremnik)
                    .WithMany(p => p.Narudžba)
                    .HasForeignKey(d => d.SpremnikId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Narudžba__Spremn__66603565");
            });

            modelBuilder.Entity<Partner>(entity =>
            {
                entity.Property(e => e.Adresa)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ImePartnera)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.KontaktBroj)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Oib)
                    .HasColumnName("OIB")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Podrum>(entity =>
            {
                entity.Property(e => e.Lokacija).IsUnicode(false);

                entity.Property(e => e.ŠifraPodruma)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<PovijestAditiva>(entity =>
            {
                entity.Property(e => e.Datum).HasColumnType("datetime");

                entity.Property(e => e.ImeZadatka)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.IskorištenaKoličina).HasColumnType("decimal(8, 2)");

                entity.Property(e => e.PreostalaKoličina).HasColumnType("decimal(8, 2)");

                entity.HasOne(d => d.Aditiv)
                    .WithMany(p => p.PovijestAditiva)
                    .HasForeignKey(d => d.AditivId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__PovijestA__Aditi__571DF1D5");

                entity.HasOne(d => d.Podrum)
                    .WithMany(p => p.PovijestAditiva)
                    .HasForeignKey(d => d.PodrumId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__PovijestA__Podru__5812160E");

                entity.HasOne(d => d.Zaposlenik)
                    .WithMany(p => p.PovijestAditiva)
                    .HasForeignKey(d => d.ZaposlenikId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__PovijestA__Zapos__59063A47");
            });

            modelBuilder.Entity<PovijestSpremnika>(entity =>
            {
                entity.Property(e => e.Bilješka).IsUnicode(false);

                entity.Property(e => e.Datum).HasColumnType("date");

                entity.Property(e => e.ImeZadatka).IsUnicode(false);

                entity.Property(e => e.KategorijaZadatka)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Spremnik)
                    .WithMany(p => p.PovijestSpremnika)
                    .HasForeignKey(d => d.SpremnikId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__PovijestS__Sprem__4E88ABD4");

                entity.HasOne(d => d.Zaposlenik)
                    .WithMany(p => p.PovijestSpremnika)
                    .HasForeignKey(d => d.ZaposlenikId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__PovijestS__Zapos__4F7CD00D");
            });

            modelBuilder.Entity<RezultatAnalize>(entity =>
            {
                entity.Property(e => e.DatumUzimanjaUzorka).HasColumnType("date");

                entity.Property(e => e.Kiselina).HasColumnType("decimal(8, 2)");

                entity.Property(e => e.PhVrijednost).HasColumnType("decimal(8, 2)");

                entity.Property(e => e.PostotakAlkohola).HasColumnType("decimal(8, 2)");

                entity.Property(e => e.RezidualniŠećer).HasColumnType("decimal(8, 2)");

                entity.Property(e => e.SlobodniSumpor).HasColumnType("decimal(8, 2)");

                entity.Property(e => e.UkupniSumpor).HasColumnType("decimal(8, 2)");

                entity.Property(e => e.Šećer).HasColumnType("decimal(8, 2)");

                entity.Property(e => e.ŠifraPodruma)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ŠifraUzorka)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Spremnik)
                    .WithMany(p => p.RezultatAnalize)
                    .HasForeignKey(d => d.SpremnikId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__RezultatA__Sprem__4BAC3F29");

                entity.HasOne(d => d.UzorakUzeo)
                    .WithMany(p => p.RezultatAnalize)
                    .HasForeignKey(d => d.UzorakUzeoId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__RezultatA__Uzora__4AB81AF0");
            });

            modelBuilder.Entity<SortaVina>(entity =>
            {
                entity.Property(e => e.NazivSorte)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Spremnik>(entity =>
            {
                entity.Property(e => e.CijenaLitre).HasColumnType("decimal(10, 2)");

                entity.Property(e => e.FazaIzrade)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ŠifraSpremnika)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Berba)
                    .WithMany(p => p.Spremnik)
                    .HasForeignKey(d => d.BerbaId)
                    .HasConstraintName("FK__Spremnik__BerbaI__44FF419A");

                entity.HasOne(d => d.Podrum)
                    .WithMany(p => p.Spremnik)
                    .HasForeignKey(d => d.PodrumId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Spremnik__Podrum__46E78A0C");

                entity.HasOne(d => d.Punilac)
                    .WithMany(p => p.Spremnik)
                    .HasForeignKey(d => d.PunilacId)
                    .HasConstraintName("FK__Spremnik__Punila__45F365D3");

                entity.HasOne(d => d.SortaVina)
                    .WithMany(p => p.Spremnik)
                    .HasForeignKey(d => d.SortaVinaId)
                    .HasConstraintName("FK__Spremnik__SortaV__47DBAE45");

                entity.HasOne(d => d.VrstaSpremnika)
                    .WithMany(p => p.Spremnik)
                    .HasForeignKey(d => d.VrstaSpremnikaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Spremnik__VrstaS__440B1D61");
            });

            modelBuilder.Entity<Uloga>(entity =>
            {
                entity.Property(e => e.NazivUloga)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<VrstaAditiva>(entity =>
            {
                entity.Property(e => e.NazivVrste)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<VrstaSpremnika>(entity =>
            {
                entity.Property(e => e.NazivVrste)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Opis)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Zadatak>(entity =>
            {
                entity.Property(e => e.Bilješke).IsUnicode(false);

                entity.Property(e => e.ImeZadatka)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PočetakZadatka).HasColumnType("date");

                entity.Property(e => e.RokZadatka).HasColumnType("date");

                entity.HasOne(d => d.Aditiv)
                    .WithMany(p => p.Zadatak)
                    .HasForeignKey(d => d.AditivId)
                    .HasConstraintName("FK__Zadatak__AditivI__5DCAEF64");

                entity.HasOne(d => d.KategorijaZadatka)
                    .WithMany(p => p.Zadatak)
                    .HasForeignKey(d => d.KategorijaZadatkaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Zadatak__Kategor__60A75C0F");

                entity.HasOne(d => d.Podrum)
                    .WithMany(p => p.Zadatak)
                    .HasForeignKey(d => d.PodrumId)
                    .HasConstraintName("FK__Zadatak__PodrumI__5EBF139D");

                entity.HasOne(d => d.Spremnik)
                    .WithMany(p => p.Zadatak)
                    .HasForeignKey(d => d.SpremnikId)
                    .HasConstraintName("FK__Zadatak__Spremni__5FB337D6");

                entity.HasOne(d => d.ZaduženiZaposlenikNavigation)
                    .WithMany(p => p.Zadatak)
                    .HasForeignKey(d => d.ZaduženiZaposlenik)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Zadatak__Zadužen__619B8048");
            });

            modelBuilder.Entity<Zaposlenik>(entity =>
            {
                entity.Property(e => e.Adresa)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.DatumZaposlenja).HasColumnType("date");

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Grad)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Ime)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.KorisnickoIme)
                    .IsRequired()
                    .HasMaxLength(450);

                entity.Property(e => e.Lozinka)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Prezime)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Spol)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Telefon)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Uloga)
                    .WithMany(p => p.Zaposlenik)
                    .HasForeignKey(d => d.UlogaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Zaposleni__Uloga__398D8EEE");
            });

            modelBuilder.Entity<Uloga>().HasData(
               new Uloga { UlogaId = 1, NazivUloga = "Vlasnik" },
               new Uloga { UlogaId = 2, NazivUloga = "Zaposlenik" }
           );

            modelBuilder.Entity<KategorijaZadatka>().HasData(
                new KategorijaZadatka { KategorijaZadatkaId = 1, ImeKategorije = "Muljanje" },
                new KategorijaZadatka { KategorijaZadatkaId = 2, ImeKategorije = "Ruljenje" },
                new KategorijaZadatka { KategorijaZadatkaId = 3, ImeKategorije = "Prešanje" },
                new KategorijaZadatka { KategorijaZadatkaId = 4, ImeKategorije = "Dodavanje aditiva" },
                new KategorijaZadatka { KategorijaZadatkaId = 5, ImeKategorije = "Fermentacija" },
                new KategorijaZadatka { KategorijaZadatkaId = 6, ImeKategorije = "Pakiranje" },
                new KategorijaZadatka { KategorijaZadatkaId = 7, ImeKategorije = "Doslađivanje" },
                new KategorijaZadatka { KategorijaZadatkaId = 8, ImeKategorije = "Bistrenje" },
                new KategorijaZadatka { KategorijaZadatkaId = 9, ImeKategorije = "Pretakanje" },
                new KategorijaZadatka { KategorijaZadatkaId = 10, ImeKategorije = "Dozrijevanje" },
                new KategorijaZadatka { KategorijaZadatkaId = 11, ImeKategorije = "Tiha fermentacija" },
                new KategorijaZadatka { KategorijaZadatkaId = 12, ImeKategorije = "Burna fermentacija" },
                new KategorijaZadatka { KategorijaZadatkaId = 13, ImeKategorije = "Uzorak za analizu" }
            );

            modelBuilder.Entity<VrstaSpremnika>().HasData(
                new VrstaSpremnika { VrstaSpremnikaId = 1, NazivVrste = "Bačva", Opis = "Drvena bačva sa željeznim obručima" },
                new VrstaSpremnika { VrstaSpremnikaId = 2, NazivVrste = "Inox bačva", Opis = "Inox bačva za držanje vina" },
                new VrstaSpremnika { VrstaSpremnikaId = 3, NazivVrste = "Staklena boca veća", Opis = "Veća staklena boca za alkoholnu fermentaciju" },
                new VrstaSpremnika { VrstaSpremnikaId = 4, NazivVrste = "Staklena boca srednja", Opis = "Srednja staklena boca za alkoholnu fermentaciju" },
                new VrstaSpremnika { VrstaSpremnikaId = 5, NazivVrste = "Spremnik", Opis = "Inox posuda za držanje velikih količina" }
            );

            modelBuilder.Entity<SortaVina>().HasData(
                new SortaVina { SortaVinaId = 1, NazivSorte = "Graševina" },
                new SortaVina { SortaVinaId = 2, NazivSorte = "Rajnski rizling" },
                new SortaVina { SortaVinaId = 3, NazivSorte = "Chardonnay" },
                new SortaVina { SortaVinaId = 4, NazivSorte = "Moslavac" },
                new SortaVina { SortaVinaId = 5, NazivSorte = "Škrlet" },
                new SortaVina { SortaVinaId = 6, NazivSorte = "Kraljevina" },
                new SortaVina { SortaVinaId = 7, NazivSorte = "Bijeli pinot" },
                new SortaVina { SortaVinaId = 8, NazivSorte = "Sivi pinot" },
                new SortaVina { SortaVinaId = 9, NazivSorte = "Zeleni silvanac" },
                new SortaVina { SortaVinaId = 10, NazivSorte = "Traminac" },
                new SortaVina { SortaVinaId = 11, NazivSorte = "Sauvignon" },
                new SortaVina { SortaVinaId = 12, NazivSorte = "Frankovka" },
                new SortaVina { SortaVinaId = 13, NazivSorte = "Zweigelt" },
                new SortaVina { SortaVinaId = 14, NazivSorte = "Malvazija" },
                new SortaVina { SortaVinaId = 15, NazivSorte = "Merlot" },
                new SortaVina { SortaVinaId = 16, NazivSorte = "Cabernet sauvignon" },
                new SortaVina { SortaVinaId = 17, NazivSorte = "Teran" },
                new SortaVina { SortaVinaId = 18, NazivSorte = "Refok" },
                new SortaVina { SortaVinaId = 19, NazivSorte = "Muškat" },
                new SortaVina { SortaVinaId = 20, NazivSorte = "Trojšćina" },
                new SortaVina { SortaVinaId = 21, NazivSorte = "Vrbnička žlahtina" },
                new SortaVina { SortaVinaId = 22, NazivSorte = "Babić" },
                new SortaVina { SortaVinaId = 23, NazivSorte = "Plavina" },
                new SortaVina { SortaVinaId = 24, NazivSorte = "Debit" },
                new SortaVina { SortaVinaId = 25, NazivSorte = "Pošip" },
                new SortaVina { SortaVinaId = 26, NazivSorte = "Maraština" },
                new SortaVina { SortaVinaId = 27, NazivSorte = "Grk" },
                new SortaVina { SortaVinaId = 28, NazivSorte = "Bijela vugava" },
                new SortaVina { SortaVinaId = 29, NazivSorte = "Crni plavac" },
                new SortaVina { SortaVinaId = 30, NazivSorte = "Plavac" },
                new SortaVina { SortaVinaId = 31, NazivSorte = "Bogdanuša" },
                new SortaVina { SortaVinaId = 32, NazivSorte = "Plavac mali" },
                new SortaVina { SortaVinaId = 33, NazivSorte = "Dingač" }
            );

            modelBuilder.Entity<VrstaAditiva>().HasData(
                new VrstaAditiva { VrstaAditivaId = 1, NazivVrste = "Bojilo" },
                new VrstaAditiva { VrstaAditivaId = 2, NazivVrste = "Konzervans" },
                new VrstaAditiva { VrstaAditivaId = 3, NazivVrste = "Regulator kiselosti" },
                new VrstaAditiva { VrstaAditivaId = 4, NazivVrste = "Potisni plin" },
                new VrstaAditiva { VrstaAditivaId = 5, NazivVrste = "Antioksidans" },
                new VrstaAditiva { VrstaAditivaId = 6, NazivVrste = "Emulgator" },
                new VrstaAditiva { VrstaAditivaId = 7, NazivVrste = "Stabilizator" },
                new VrstaAditiva { VrstaAditivaId = 8, NazivVrste = "Zgušnjivač" },
                new VrstaAditiva { VrstaAditivaId = 9, NazivVrste = "Tvar za zaslađivanje" },
                new VrstaAditiva { VrstaAditivaId = 10, NazivVrste = "Kvasac" },
                new VrstaAditiva { VrstaAditivaId = 11, NazivVrste = "Tvar za želiranje" },
                new VrstaAditiva { VrstaAditivaId = 12, NazivVrste = "Učvršćivač" },
                new VrstaAditiva { VrstaAditivaId = 13, NazivVrste = "Tvar za sprečavanje zgrudnjavanja" },
                new VrstaAditiva { VrstaAditivaId = 14, NazivVrste = "Pojačivač okusa/arome" },
                new VrstaAditiva { VrstaAditivaId = 15, NazivVrste = "Sladilo" },
                new VrstaAditiva { VrstaAditivaId = 16, NazivVrste = "Modificirani škrob" }
            );

            modelBuilder.Entity<Aditiv>().HasData(
                new Aditiv { AditivId = 1, ImeAditiva = "Vinska kiselina", VrstaAditivaId = 3 },
                new Aditiv { AditivId = 2, ImeAditiva = "6% S02 rješenje", VrstaAditivaId = 2, Koncentracija = 6, Instrukcije = "Dodaj direktno u spremnik. Promiješaj" },
                new Aditiv { AditivId = 3, ImeAditiva = "Kalijev metabisulfit", VrstaAditivaId = 2 },
                new Aditiv { AditivId = 4, ImeAditiva = "Kalcijev sulfit", VrstaAditivaId = 2 },
                new Aditiv { AditivId = 5, ImeAditiva = "Natrijevi tartarati", VrstaAditivaId = 7 },
                new Aditiv { AditivId = 6, ImeAditiva = "Ester vinske kiseline mono", VrstaAditivaId = 6 }
            );
        }
    }
}
