using System;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using PdfRpt.Core.Contracts;
using PdfRpt.FluentInterface;
using WineryApp.Data;
using WineryApp.Data.Entiteti;
using WineryApp.ViewModels.Izvješća.Podrumi;
using WineryApp.ViewModels.Izvješća.Spremnici;
using WineryApp.ViewModels.Izvješća.Zadaci;
using WineryApp.ViewModels.Izvješća.Zaposlenici;

namespace WineryApp.Controllers
{
    public class IzvješćaController : Controller
    {
        private readonly WineryAppDbContext _context;
        private readonly IRepository _repository;
        private readonly UserManager<IdentityUser> _userManager;
        private const string ExcelContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

        public IzvješćaController(WineryAppDbContext context, IRepository repository, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _repository = repository;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            return RedirectToAction("Zadaci");
        }

        public IActionResult Zadaci()
        {
            var zaposlenici = _repository.GetAllZaposleniciBezVlasnika();

            var kategorije = _repository.GetAllKategorijeZadataka();

            var allPodrumi = _repository.GetAllPodrumi().OrderBy(p => p.ŠifraPodruma).ToList();
            var allSpremnici = _repository.GetAllSpremnici().OrderBy(s => s.ŠifraSpremnika).ToList();

            var model = new IzvješćaZadaciIViewModel
            {
                KategorijeZadataka = kategorije,
                Zaposlenici = zaposlenici,
                Podrumi = allPodrumi,
                Spremnici = allSpremnici
            };

            return View(model);
        }

        public IActionResult Zaposlenici()
        {
            return View();
        }

        public IActionResult Podrumi()
        {
            var allBerbe = _repository.GetAllBerba();
            var allSorteVina = _repository.GetAllSorteVina();

            var model = new IzvješćaPodrumiViewModel
            {
                Berbe = allBerbe,
                SorteVina = allSorteVina
            };

            return View(model);
        }

        public IActionResult Spremnici()
        {
            var allZaposlenici = _repository.GetAllZaposleniciBezVlasnika();
            var allPodrumi = _repository.GetAllPodrumi();
            var allBerbe = _repository.GetAllBerba();
            var allSorteVina = _repository.GetAllSorteVina();
            var allVrsteSpremnika = _repository.GetAllVrsteSpremnika();

            var model = new IzvješćaSpremniciViewModel
            {
                Podrumi = allPodrumi,
                SorteVina = allSorteVina,
                Berbe = allBerbe,
                Zaposlenici = allZaposlenici,
                VrsteSpremnika = allVrsteSpremnika
            };

            return View(model);
        }

        public IActionResult Aditivi()
        {
            return View();
        }

        public IActionResult UzorciZaAnalizu()
        {
            return View();
        }

        [HttpPost]
        public IActionResult GenerirajIzvješćeZadaci(ZadatakFilterIzvješće input)
        {
            string naslov = "Popis zadataka";

            var zadaci = _repository.GetAllZadaci()
                .OrderBy(z => z.PočetakZadatka).AsQueryable();

            if (input.ZaposlenikId != -1)
            {
                zadaci = zadaci.Where(z => z.ZaduženiZaposlenik == input.ZaposlenikId);
            }

            if (input.StatusId != -1)
            {
                zadaci = zadaci.Where(z => z.StatusZadatka == input.StatusId);
            }

            if (input.KategorijaZadatkaId != -1)
            {
                zadaci = zadaci.Where(z => z.KategorijaZadatkaId == input.KategorijaZadatkaId);
            }

            if (input.PodrumId != -1)
            {
                zadaci = zadaci.Where(z => z.PodrumId == input.PodrumId);
            }

            if (input.SpremnikId != -1)
            {
                zadaci = zadaci.Where(z => z.SpremnikId == input.SpremnikId);
            }

            if (input.PočetakZadatka != null)
            {
                zadaci = zadaci.Where(z => z.PočetakZadatka == input.PočetakZadatka);
            }

            if (input.RokZadatka != null)
            {
                zadaci = zadaci.Where(z => z.RokZadatka == input.RokZadatka);
            }

            var zadaciLista = zadaci
                .Select(z => new ZadatakPrikazIzvješće
                {
                    Naziv = z.ImeZadatka,
                    Kategorija = z.KategorijaZadatka.ImeKategorije,
                    ŠifraPodruma = z.PodrumId.HasValue ? z.Podrum.ŠifraPodruma : "Općenito",
                    ŠifraSpremnika = z.SpremnikId.HasValue ? z.Spremnik.ŠifraSpremnika : "Općenito",
                    PočetakZadatka = z.PočetakZadatka.ToString("dd.MM.yyyy"),
                    RokZadatka = z.RokZadatka.ToString("dd.MM.yyyy"),
                    ZaduženiZaposlenik = $"{z.ZaduženiZaposlenikNavigation.Ime} {z.ZaduženiZaposlenikNavigation.Prezime}"
                })
                .ToList();

            if (input.Format == "1")
            {
                #region PDFgeneriranje

                PdfReport izvješće = InicijalnePostavke(naslov, false);
                izvješće.PagesFooter(podnožje =>
                {
                    podnožje.DefaultFooter(DateTime.Now.ToString("dd.MM.yyyy."));
                }).PagesHeader(zaglavlje =>
                {
                    zaglavlje.CacheHeader(true);
                    zaglavlje.DefaultHeader(defaultZaglavlje =>
                    {
                        defaultZaglavlje.RunDirection(PdfRunDirection.LeftToRight);
                        defaultZaglavlje.Message(naslov);
                    });
                });

                izvješće.MainTableDataSource(izvor => izvor.StronglyTypedList(zadaciLista));

                izvješće.MainTableColumns(stupci =>
                {
                    stupci.AddColumn(stupac =>
                    {
                        stupac.IsRowNumber(true);
                        stupac.CellsHorizontalAlignment(HorizontalAlignment.Right);
                        stupac.IsVisible(true);
                        stupac.Order(0);
                        stupac.Width(1);
                        stupac.HeaderCell("#", horizontalAlignment: HorizontalAlignment.Right);
                    });

                    stupci.AddColumn(stupac =>
                    {
                        stupac.PropertyName(nameof(ZadatakPrikazIzvješće.Kategorija));
                        stupac.CellsHorizontalAlignment(HorizontalAlignment.Center);
                        stupac.IsVisible(true);
                        stupac.Order(1);
                        stupac.Width(2);
                        stupac.HeaderCell("Kategorija zadatka", horizontalAlignment: HorizontalAlignment.Center);
                    });

                    stupci.AddColumn(stupac =>
                    {
                        stupac.PropertyName(nameof(ZadatakPrikazIzvješće.Naziv));
                        stupac.CellsHorizontalAlignment(HorizontalAlignment.Center);
                        stupac.IsVisible(true);
                        stupac.Order(2);
                        stupac.Width(3);
                        stupac.HeaderCell("Ime zadatka", horizontalAlignment: HorizontalAlignment.Center);
                    });

                    stupci.AddColumn(stupac =>
                    {
                        stupac.PropertyName(nameof(ZadatakPrikazIzvješće.ŠifraPodruma));
                        stupac.CellsHorizontalAlignment(HorizontalAlignment.Center);
                        stupac.IsVisible(true);
                        stupac.Order(3);
                        stupac.Width(2);
                        stupac.HeaderCell("Šifra podruma", horizontalAlignment: HorizontalAlignment.Center);
                    });

                    stupci.AddColumn(stupac =>
                    {
                        stupac.PropertyName(nameof(ZadatakPrikazIzvješće.ŠifraSpremnika));
                        stupac.CellsHorizontalAlignment(HorizontalAlignment.Center);
                        stupac.IsVisible(true);
                        stupac.Order(4);
                        stupac.Width(2);
                        stupac.HeaderCell("Šifra spremnika", horizontalAlignment: HorizontalAlignment.Center);
                    });

                    stupci.AddColumn(stupac =>
                    {
                        stupac.PropertyName(nameof(ZadatakPrikazIzvješće.PočetakZadatka));
                        stupac.CellsHorizontalAlignment(HorizontalAlignment.Center);
                        stupac.IsVisible(true);
                        stupac.Order(5);
                        stupac.Width(2);
                        stupac.HeaderCell("Početak zadatka", horizontalAlignment: HorizontalAlignment.Center);
                    });

                    stupci.AddColumn(stupac =>
                    {
                        stupac.PropertyName(nameof(ZadatakPrikazIzvješće.RokZadatka));
                        stupac.CellsHorizontalAlignment(HorizontalAlignment.Center);
                        stupac.IsVisible(true);
                        stupac.Order(6);
                        stupac.Width(2);
                        stupac.HeaderCell("Rok zadatka", horizontalAlignment: HorizontalAlignment.Center);
                    });

                    stupci.AddColumn(stupac =>
                    {
                        stupac.PropertyName(nameof(ZadatakPrikazIzvješće.ZaduženiZaposlenik));
                        stupac.CellsHorizontalAlignment(HorizontalAlignment.Center);
                        stupac.IsVisible(true);
                        stupac.Order(7);
                        stupac.Width(2);
                        stupac.HeaderCell("Zaduženi zaposlenik", horizontalAlignment: HorizontalAlignment.Center);
                    });
                });

                byte[] pdf = izvješće.GenerateAsByteArray();

                if (pdf != null)
                {
                    Response.Headers.Add("content-disposition", "inline; filename=zadaci.pdf");
                    return File(pdf, "application/pdf");
                }
                else
                {
                    return NotFound();
                }
                #endregion 
            }
            else if (input.Format == "2")
            {
                #region Excelgeneriranje

                var userName = _userManager.GetUserName(User);
                var korisnik = _repository.GetZaposlenik(userName);

                byte[] sadržaj;
                using (ExcelPackage excel = new ExcelPackage())
                {
                    excel.Workbook.Properties.Title = naslov;
                    excel.Workbook.Properties.Author = $"{korisnik.Ime} {korisnik.Prezime}";

                    var list = excel.Workbook.Worksheets.Add("Zadaci");

                    //Zaglavlja
                    list.Cells[1, 1].Value = nameof(ZadatakPrikazIzvješće.Kategorija);
                    list.Cells[1, 2].Value = nameof(ZadatakPrikazIzvješće.Naziv);
                    list.Cells[1, 3].Value = nameof(ZadatakPrikazIzvješće.ŠifraPodruma);
                    list.Cells[1, 4].Value = nameof(ZadatakPrikazIzvješće.ŠifraSpremnika);
                    list.Cells[1, 5].Value = nameof(ZadatakPrikazIzvješće.PočetakZadatka);
                    list.Cells[1, 6].Value = nameof(ZadatakPrikazIzvješće.RokZadatka);
                    list.Cells[1, 7].Value = nameof(ZadatakPrikazIzvješće.ZaduženiZaposlenik);

                    for (int i = 0; i < zadaciLista.Count; i++)
                    {
                        list.Cells[i + 2, 1].Value = zadaciLista[i].Kategorija;
                        list.Cells[i + 2, 2].Value = zadaciLista[i].Naziv;
                        list.Cells[i + 2, 3].Value = zadaciLista[i].ŠifraPodruma;
                        list.Cells[i + 2, 4].Value = zadaciLista[i].ŠifraSpremnika;
                        list.Cells[i + 2, 5].Value = zadaciLista[i].PočetakZadatka;
                        list.Cells[i + 2, 6].Value = zadaciLista[i].RokZadatka;
                        list.Cells[i + 2, 7].Value = zadaciLista[i].ZaduženiZaposlenik;
                    }

                    list.Cells[1, 1, zadaciLista.Count + 1, 7].AutoFitColumns();

                    sadržaj = excel.GetAsByteArray();
                }

                return File(sadržaj, ExcelContentType, "zadaci.xlsx");

                #endregion
            }

            return RedirectToAction("Zadaci");
        }

        [HttpPost]
        public IActionResult GenerirajIzvješćeZaposlenici(ZaposlenikFilterIzvješće input)
        {
            string naslov = "Popis zaposlenika";

            var zaposlenici = _repository.GetAllZaposleniciBezVlasnika()
                .AsQueryable();

            if (input.Spol != "-1")
            {
                zaposlenici = zaposlenici.Where(z => z.Spol == input.Spol);
            }

            if (input.DatumOd != null)
            {
                zaposlenici = zaposlenici.Where(z => z.DatumZaposlenja >= input.DatumOd);
            }

            if (input.DatumDo != null)
            {
                zaposlenici = zaposlenici.Where(z => z.DatumZaposlenja <= input.DatumDo);
            }

            var zaposleniciLista = zaposlenici
                .Select(z => new ZaposlenikPrikazIzvješće
                {
                    Ime = z.Ime,
                    Prezime = z.Prezime,
                    Email = z.Email,
                    KorisnickoIme = z.KorisnickoIme,
                    Spol = z.Spol,
                    DatumZaposlenja = z.DatumZaposlenja.ToString("dd.MM.yyyy"),
                    Telefon = z.Telefon,
                    Adresa = z.Adresa,
                    Grad = z.Grad
                })
                .ToList();

            if (input.Format == "1")
            {
                #region PDFgeneriranje

                PdfReport izvješće = InicijalnePostavke(naslov, false);
                izvješće.PagesFooter(podnožje =>
                {
                    podnožje.DefaultFooter(DateTime.Now.ToString("dd.MM.yyyy."));
                }).PagesHeader(zaglavlje =>
                {
                    zaglavlje.CacheHeader(true);
                    zaglavlje.DefaultHeader(defaultZaglavlje =>
                    {
                        defaultZaglavlje.RunDirection(PdfRunDirection.LeftToRight);
                        defaultZaglavlje.Message(naslov);
                    });
                });

                izvješće.MainTableDataSource(izvor => izvor.StronglyTypedList(zaposleniciLista));

                izvješće.MainTableColumns(stupci =>
                {
                    stupci.AddColumn(stupac =>
                    {
                        stupac.IsRowNumber(true);
                        stupac.CellsHorizontalAlignment(HorizontalAlignment.Right);
                        stupac.IsVisible(true);
                        stupac.Order(0);
                        stupac.Width(1);
                        stupac.HeaderCell("#", horizontalAlignment: HorizontalAlignment.Right);
                    });

                    stupci.AddColumn(stupac =>
                    {
                        stupac.PropertyName(nameof(ZaposlenikPrikazIzvješće.Ime));
                        stupac.CellsHorizontalAlignment(HorizontalAlignment.Center);
                        stupac.IsVisible(true);
                        stupac.Order(1);
                        stupac.Width(1);
                        stupac.HeaderCell("Ime", horizontalAlignment: HorizontalAlignment.Center);
                    });

                    stupci.AddColumn(stupac =>
                    {
                        stupac.PropertyName(nameof(ZaposlenikPrikazIzvješće.Prezime));
                        stupac.CellsHorizontalAlignment(HorizontalAlignment.Center);
                        stupac.IsVisible(true);
                        stupac.Order(2);
                        stupac.Width(1);
                        stupac.HeaderCell("Prezime", horizontalAlignment: HorizontalAlignment.Center);
                    });

                    stupci.AddColumn(stupac =>
                    {
                        stupac.PropertyName(nameof(ZaposlenikPrikazIzvješće.Spol));
                        stupac.CellsHorizontalAlignment(HorizontalAlignment.Center);
                        stupac.IsVisible(true);
                        stupac.Order(3);
                        stupac.Width(1);
                        stupac.HeaderCell("Spol", horizontalAlignment: HorizontalAlignment.Center);
                    });

                    stupci.AddColumn(stupac =>
                    {
                        stupac.PropertyName(nameof(ZaposlenikPrikazIzvješće.Adresa));
                        stupac.CellsHorizontalAlignment(HorizontalAlignment.Center);
                        stupac.IsVisible(true);
                        stupac.Order(4);
                        stupac.Width(2);
                        stupac.HeaderCell("Adresa", horizontalAlignment: HorizontalAlignment.Center);
                    });

                    stupci.AddColumn(stupac =>
                    {
                        stupac.PropertyName(nameof(ZaposlenikPrikazIzvješće.Grad));
                        stupac.CellsHorizontalAlignment(HorizontalAlignment.Center);
                        stupac.IsVisible(true);
                        stupac.Order(5);
                        stupac.Width(2);
                        stupac.HeaderCell("Grad", horizontalAlignment: HorizontalAlignment.Center);
                    });

                    stupci.AddColumn(stupac =>
                    {
                        stupac.PropertyName(nameof(ZaposlenikPrikazIzvješće.Telefon));
                        stupac.CellsHorizontalAlignment(HorizontalAlignment.Center);
                        stupac.IsVisible(true);
                        stupac.Order(6);
                        stupac.Width(2);
                        stupac.HeaderCell("Telefon", horizontalAlignment: HorizontalAlignment.Center);
                    });

                    stupci.AddColumn(stupac =>
                    {
                        stupac.PropertyName(nameof(ZaposlenikPrikazIzvješće.Email));
                        stupac.CellsHorizontalAlignment(HorizontalAlignment.Center);
                        stupac.IsVisible(true);
                        stupac.Order(7);
                        stupac.Width(2);
                        stupac.HeaderCell("Email", horizontalAlignment: HorizontalAlignment.Center);
                    });

                    stupci.AddColumn(stupac =>
                    {
                        stupac.PropertyName(nameof(ZaposlenikPrikazIzvješće.DatumZaposlenja));
                        stupac.CellsHorizontalAlignment(HorizontalAlignment.Center);
                        stupac.IsVisible(true);
                        stupac.Order(8);
                        stupac.Width(2);
                        stupac.HeaderCell("Datum zaposlenja", horizontalAlignment: HorizontalAlignment.Center);
                    });
                });

                byte[] pdf = izvješće.GenerateAsByteArray();

                if (pdf != null)
                {
                    Response.Headers.Add("content-disposition", "inline; filename=zaposlenici.pdf");
                    return File(pdf, "application/pdf");
                }
                else
                {
                    return NotFound();
                }
                #endregion 
            }
            else if (input.Format == "2")
            {
                #region Excelgeneriranje

                var userName = _userManager.GetUserName(User);
                var korisnik = _repository.GetZaposlenik(userName);

                byte[] sadržaj;
                using (ExcelPackage excel = new ExcelPackage())
                {
                    excel.Workbook.Properties.Title = naslov;
                    excel.Workbook.Properties.Author = $"{korisnik.Ime} {korisnik.Prezime}";

                    var list = excel.Workbook.Worksheets.Add("Zaposlenici");

                    //Zaglavlja
                    list.Cells[1, 1].Value = nameof(ZaposlenikPrikazIzvješće.Ime);
                    list.Cells[1, 2].Value = nameof(ZaposlenikPrikazIzvješće.Prezime);
                    list.Cells[1, 3].Value = nameof(ZaposlenikPrikazIzvješće.Spol);
                    list.Cells[1, 4].Value = nameof(ZaposlenikPrikazIzvješće.Adresa);
                    list.Cells[1, 5].Value = nameof(ZaposlenikPrikazIzvješće.Grad);
                    list.Cells[1, 6].Value = nameof(ZaposlenikPrikazIzvješće.Telefon);
                    list.Cells[1, 7].Value = nameof(ZaposlenikPrikazIzvješće.Email);
                    list.Cells[1, 8].Value = nameof(ZaposlenikPrikazIzvješće.DatumZaposlenja);

                    for (int i = 0; i < zaposleniciLista.Count; i++)
                    {
                        list.Cells[i + 2, 1].Value = zaposleniciLista[i].Ime;
                        list.Cells[i + 2, 2].Value = zaposleniciLista[i].Prezime;
                        list.Cells[i + 2, 3].Value = zaposleniciLista[i].Spol;
                        list.Cells[i + 2, 4].Value = zaposleniciLista[i].Adresa;
                        list.Cells[i + 2, 5].Value = zaposleniciLista[i].Grad;
                        list.Cells[i + 2, 6].Value = zaposleniciLista[i].Telefon;
                        list.Cells[i + 2, 7].Value = zaposleniciLista[i].Email;
                        list.Cells[i + 2, 8].Value = zaposleniciLista[i].DatumZaposlenja;
                    }

                    list.Cells[1, 1, zaposleniciLista.Count + 1, 8].AutoFitColumns();

                    sadržaj = excel.GetAsByteArray();
                }

                return File(sadržaj, ExcelContentType, "zaposlenici.xlsx");

                #endregion
            }

            return RedirectToAction("Zaposlenici");
        }

        [HttpPost]
        public IActionResult GenerirajIzvješćePodrumi(PodrumFilterIzvješće input)
        {
            string naslov = "Popis podruma";

            var podrumi = _repository.GetAllPodrumi()
                .OrderBy(p => p.ŠifraPodruma)
                .AsQueryable();

            if (input.BerbaId != -1)
            {
                var spremniciSGodinom = _repository.GetAllSpremnikWithVintage(input.BerbaId);
                var podrumiSTimSpremnicima = spremniciSGodinom.Select(s => s.Podrum).Distinct().ToList();

                podrumi = podrumi.Where(p => podrumiSTimSpremnicima.Contains(p));
            }

            if (input.SortaVinaId != -1)
            {
                podrumi = podrumi.Where(p => p.Spremnik.Any(s => s.SortaVinaId == input.SortaVinaId));
            }

            var podrumiLista = podrumi
                .Select(p => new PodrumPrikazIzvješće
                {
                    ŠifraPodruma = p.ŠifraPodruma,
                    Lokacija = p.Lokacija,
                    Popunjenost = _repository.GetBasementFill(p.PodrumId),
                    BrojSpremnika = p.Spremnik.Count.ToString(),
                    Berbe = _repository.GetAllVingatesFormatted(p),
                    SorteVina = _repository.GetAllVarientalsFormatted(p)
                })
                .ToList();

            if (input.Format == "1")
            {
                #region PDFgeneriranje

                PdfReport izvješće = InicijalnePostavke(naslov, false);
                izvješće.PagesFooter(podnožje =>
                {
                    podnožje.DefaultFooter(DateTime.Now.ToString("dd.MM.yyyy."));
                }).PagesHeader(zaglavlje =>
                {
                    zaglavlje.CacheHeader(true);
                    zaglavlje.DefaultHeader(defaultZaglavlje =>
                    {
                        defaultZaglavlje.RunDirection(PdfRunDirection.LeftToRight);
                        defaultZaglavlje.Message(naslov);
                    });
                });

                izvješće.MainTableDataSource(izvor => izvor.StronglyTypedList(podrumiLista));

                izvješće.MainTableColumns(stupci =>
                {
                    stupci.AddColumn(stupac =>
                    {
                        stupac.IsRowNumber(true);
                        stupac.CellsHorizontalAlignment(HorizontalAlignment.Right);
                        stupac.IsVisible(true);
                        stupac.Order(0);
                        stupac.Width(1);
                        stupac.HeaderCell("#", horizontalAlignment: HorizontalAlignment.Right);
                    });

                    stupci.AddColumn(stupac =>
                    {
                        stupac.PropertyName(nameof(PodrumPrikazIzvješće.ŠifraPodruma));
                        stupac.CellsHorizontalAlignment(HorizontalAlignment.Center);
                        stupac.IsVisible(true);
                        stupac.Order(1);
                        stupac.Width(2);
                        stupac.HeaderCell("Šifra podruma", horizontalAlignment: HorizontalAlignment.Center);
                    });

                    stupci.AddColumn(stupac =>
                    {
                        stupac.PropertyName(nameof(PodrumPrikazIzvješće.Popunjenost));
                        stupac.CellsHorizontalAlignment(HorizontalAlignment.Center);
                        stupac.IsVisible(true);
                        stupac.Order(2);
                        stupac.Width(1);
                        stupac.HeaderCell("Popunjenost", horizontalAlignment: HorizontalAlignment.Center);
                    });

                    stupci.AddColumn(stupac =>
                    {
                        stupac.PropertyName(nameof(PodrumPrikazIzvješće.BrojSpremnika));
                        stupac.CellsHorizontalAlignment(HorizontalAlignment.Center);
                        stupac.IsVisible(true);
                        stupac.Order(3);
                        stupac.Width(1);
                        stupac.HeaderCell("Broj spremnika", horizontalAlignment: HorizontalAlignment.Center);
                    });

                    stupci.AddColumn(stupac =>
                    {
                        stupac.PropertyName(nameof(PodrumPrikazIzvješće.Lokacija));
                        stupac.CellsHorizontalAlignment(HorizontalAlignment.Center);
                        stupac.IsVisible(true);
                        stupac.Order(4);
                        stupac.Width(2);
                        stupac.HeaderCell("Lokacija", horizontalAlignment: HorizontalAlignment.Center);
                    });

                    stupci.AddColumn(stupac =>
                    {
                        stupac.PropertyName(nameof(PodrumPrikazIzvješće.Berbe));
                        stupac.CellsHorizontalAlignment(HorizontalAlignment.Center);
                        stupac.IsVisible(true);
                        stupac.Order(5);
                        stupac.Width(1);
                        stupac.HeaderCell("Berbe", horizontalAlignment: HorizontalAlignment.Center);
                    });

                    stupci.AddColumn(stupac =>
                    {
                        stupac.PropertyName(nameof(PodrumPrikazIzvješće.SorteVina));
                        stupac.CellsHorizontalAlignment(HorizontalAlignment.Center);
                        stupac.IsVisible(true);
                        stupac.Order(6);
                        stupac.Width(2);
                        stupac.HeaderCell("Sorte vina", horizontalAlignment: HorizontalAlignment.Center);
                    });
                });

                byte[] pdf = izvješće.GenerateAsByteArray();

                if (pdf != null)
                {
                    Response.Headers.Add("content-disposition", "inline; filename=podrumi.pdf");
                    return File(pdf, "application/pdf");
                }
                else
                {
                    return NotFound();
                }
                #endregion 
            }
            else if (input.Format == "2")
            {
                #region Excelgeneriranje

                var userName = _userManager.GetUserName(User);
                var korisnik = _repository.GetZaposlenik(userName);

                byte[] sadržaj;
                using (ExcelPackage excel = new ExcelPackage())
                {
                    excel.Workbook.Properties.Title = naslov;
                    excel.Workbook.Properties.Author = $"{korisnik.Ime} {korisnik.Prezime}";

                    var list = excel.Workbook.Worksheets.Add("Podrumi");

                    //Zaglavlja
                    list.Cells[1, 1].Value = nameof(PodrumPrikazIzvješće.ŠifraPodruma);
                    list.Cells[1, 2].Value = nameof(PodrumPrikazIzvješće.Popunjenost);
                    list.Cells[1, 3].Value = nameof(PodrumPrikazIzvješće.BrojSpremnika);
                    list.Cells[1, 4].Value = nameof(PodrumPrikazIzvješće.Lokacija);
                    list.Cells[1, 5].Value = nameof(PodrumPrikazIzvješće.Berbe);
                    list.Cells[1, 6].Value = nameof(PodrumPrikazIzvješće.SorteVina);

                    for (int i = 0; i < podrumiLista.Count; i++)
                    {
                        list.Cells[i + 2, 1].Value = podrumiLista[i].ŠifraPodruma;
                        list.Cells[i + 2, 2].Value = podrumiLista[i].Popunjenost;
                        list.Cells[i + 2, 3].Value = podrumiLista[i].BrojSpremnika;
                        list.Cells[i + 2, 4].Value = podrumiLista[i].Lokacija;
                        list.Cells[i + 2, 5].Value = podrumiLista[i].Berbe;
                        list.Cells[i + 2, 6].Value = podrumiLista[i].SorteVina;
                    }

                    list.Cells[1, 1, podrumiLista.Count + 1, 8].AutoFitColumns();

                    sadržaj = excel.GetAsByteArray();
                }

                return File(sadržaj, ExcelContentType, "podrumi.xlsx");

                #endregion
            }

            return RedirectToAction("Podrumi");
        }

        [HttpPost]
        public IActionResult GenerirajIzvješćeSpremnici(SpremnikFilterIzvješće input)
        {
            string naslov = "Popis spremnika";

            var spremnici = _repository.GetAllSpremnici()
                .OrderBy(s => s.ŠifraSpremnika)
                .AsQueryable();

            if (input.VrstaSpremnikaId != -1)
            {
                spremnici = spremnici.Where(s => s.VrstaSpremnikaId == input.VrstaSpremnikaId);
            }

            if (input.PodrumId != -1)
            {
                spremnici = spremnici.Where(s => s.PodrumId == input.PodrumId);
            }

            if (input.BerbaId != -1)
            {
                spremnici = spremnici.Where(s => s.BerbaId == input.BerbaId);
            }

            if (input.PunilacId != -1)
            {
                spremnici = spremnici.Where(s => s.PunilacId == input.PunilacId);
            }

            if (input.SortaVinaId != -1)
            {
                spremnici = spremnici.Where(s => s.SortaVinaId == input.SortaVinaId);
            }

            var spremniciLista = spremnici
                .Select(s => new SpremnikPrikazIzvješće
                {
                    ŠifraSpremnika = s.ŠifraSpremnika,
                    ŠifraPodruma = s.Podrum.ŠifraPodruma,
                    GodinaBerbe = s.Berba.GodinaBerbe.ToString(),
                    SortaVina = s.SortaVina.NazivSorte,
                    Napunjenost = _repository.GetSpremnikFill(s.SpremnikId),
                    Punilac = s.Punilac.Ime + " " + s.Punilac.Prezime,
                    VrstaSpremnika = s.VrstaSpremnika.NazivVrste,
                    FazaProcesa = s.FazaIzrade
                })
                .ToList();

            if (input.Format == "1")
            {
                #region PDFgeneriranje

                PdfReport izvješće = InicijalnePostavke(naslov, false);
                izvješće.PagesFooter(podnožje =>
                {
                    podnožje.DefaultFooter(DateTime.Now.ToString("dd.MM.yyyy."));
                }).PagesHeader(zaglavlje =>
                {
                    zaglavlje.CacheHeader(true);
                    zaglavlje.DefaultHeader(defaultZaglavlje =>
                    {
                        defaultZaglavlje.RunDirection(PdfRunDirection.LeftToRight);
                        defaultZaglavlje.Message(naslov);
                    });
                });

                izvješće.MainTableDataSource(izvor => izvor.StronglyTypedList(spremniciLista));

                izvješće.MainTableColumns(stupci =>
                {
                    stupci.AddColumn(stupac =>
                    {
                        stupac.IsRowNumber(true);
                        stupac.CellsHorizontalAlignment(HorizontalAlignment.Right);
                        stupac.IsVisible(true);
                        stupac.Order(0);
                        stupac.Width(1);
                        stupac.HeaderCell("#", horizontalAlignment: HorizontalAlignment.Right);
                    });

                    stupci.AddColumn(stupac =>
                    {
                        stupac.PropertyName(nameof(SpremnikPrikazIzvješće.ŠifraSpremnika));
                        stupac.CellsHorizontalAlignment(HorizontalAlignment.Center);
                        stupac.IsVisible(true);
                        stupac.Order(1);
                        stupac.Width(2);
                        stupac.HeaderCell("Šifra spremnika", horizontalAlignment: HorizontalAlignment.Center);
                    });

                    stupci.AddColumn(stupac =>
                    {
                        stupac.PropertyName(nameof(SpremnikPrikazIzvješće.Napunjenost));
                        stupac.CellsHorizontalAlignment(HorizontalAlignment.Center);
                        stupac.IsVisible(true);
                        stupac.Order(2);
                        stupac.Width(1);
                        stupac.HeaderCell("Napunjenost", horizontalAlignment: HorizontalAlignment.Center);
                    });

                    stupci.AddColumn(stupac =>
                    {
                        stupac.PropertyName(nameof(SpremnikPrikazIzvješće.FazaProcesa));
                        stupac.CellsHorizontalAlignment(HorizontalAlignment.Center);
                        stupac.IsVisible(true);
                        stupac.Order(3);
                        stupac.Width(1);
                        stupac.HeaderCell("Faza procesa", horizontalAlignment: HorizontalAlignment.Center);
                    });

                    stupci.AddColumn(stupac =>
                    {
                        stupac.PropertyName(nameof(SpremnikPrikazIzvješće.VrstaSpremnika));
                        stupac.CellsHorizontalAlignment(HorizontalAlignment.Center);
                        stupac.IsVisible(true);
                        stupac.Order(4);
                        stupac.Width(1);
                        stupac.HeaderCell("Vrsta spremnika", horizontalAlignment: HorizontalAlignment.Center);
                    });

                    stupci.AddColumn(stupac =>
                    {
                        stupac.PropertyName(nameof(SpremnikPrikazIzvješće.GodinaBerbe));
                        stupac.CellsHorizontalAlignment(HorizontalAlignment.Center);
                        stupac.IsVisible(true);
                        stupac.Order(5);
                        stupac.Width(1);
                        stupac.HeaderCell("Godina berbe", horizontalAlignment: HorizontalAlignment.Center);
                    });

                    stupci.AddColumn(stupac =>
                    {
                        stupac.PropertyName(nameof(SpremnikPrikazIzvješće.Punilac));
                        stupac.CellsHorizontalAlignment(HorizontalAlignment.Center);
                        stupac.IsVisible(true);
                        stupac.Order(6);
                        stupac.Width(2);
                        stupac.HeaderCell("Punilac", horizontalAlignment: HorizontalAlignment.Center);
                    });

                    stupci.AddColumn(stupac =>
                    {
                        stupac.PropertyName(nameof(SpremnikPrikazIzvješće.ŠifraPodruma));
                        stupac.CellsHorizontalAlignment(HorizontalAlignment.Center);
                        stupac.IsVisible(true);
                        stupac.Order(7);
                        stupac.Width(1);
                        stupac.HeaderCell("Šifra podruma", horizontalAlignment: HorizontalAlignment.Center);
                    });

                    stupci.AddColumn(stupac =>
                    {
                        stupac.PropertyName(nameof(SpremnikPrikazIzvješće.SortaVina));
                        stupac.CellsHorizontalAlignment(HorizontalAlignment.Center);
                        stupac.IsVisible(true);
                        stupac.Order(8);
                        stupac.Width(1);
                        stupac.HeaderCell("Sorta vina", horizontalAlignment: HorizontalAlignment.Center);
                    });
                });

                byte[] pdf = izvješće.GenerateAsByteArray();

                if (pdf != null)
                {
                    Response.Headers.Add("content-disposition", "inline; filename=spremnici.pdf");
                    return File(pdf, "application/pdf");
                }
                else
                {
                    return NotFound();
                }
                #endregion 
            }
            else if (input.Format == "2")
            {
                #region Excelgeneriranje

                var userName = _userManager.GetUserName(User);
                var korisnik = _repository.GetZaposlenik(userName);

                byte[] sadržaj;
                using (ExcelPackage excel = new ExcelPackage())
                {
                    excel.Workbook.Properties.Title = naslov;
                    excel.Workbook.Properties.Author = $"{korisnik.Ime} {korisnik.Prezime}";

                    var list = excel.Workbook.Worksheets.Add("Spremnici");

                    //Zaglavlja
                    list.Cells[1, 1].Value = nameof(SpremnikPrikazIzvješće.ŠifraSpremnika);
                    list.Cells[1, 2].Value = nameof(SpremnikPrikazIzvješće.Napunjenost);
                    list.Cells[1, 3].Value = nameof(SpremnikPrikazIzvješće.FazaProcesa);
                    list.Cells[1, 4].Value = nameof(SpremnikPrikazIzvješće.VrstaSpremnika);
                    list.Cells[1, 5].Value = nameof(SpremnikPrikazIzvješće.GodinaBerbe);
                    list.Cells[1, 6].Value = nameof(SpremnikPrikazIzvješće.Punilac);
                    list.Cells[1, 7].Value = nameof(SpremnikPrikazIzvješće.ŠifraPodruma);
                    list.Cells[1, 8].Value = nameof(SpremnikPrikazIzvješće.SortaVina);

                    for (int i = 0; i < spremniciLista.Count; i++)
                    {
                        list.Cells[i + 2, 1].Value = spremniciLista[i].ŠifraSpremnika;
                        list.Cells[i + 2, 2].Value = spremniciLista[i].Napunjenost;
                        list.Cells[i + 2, 3].Value = spremniciLista[i].FazaProcesa;
                        list.Cells[i + 2, 4].Value = spremniciLista[i].VrstaSpremnika;
                        list.Cells[i + 2, 5].Value = spremniciLista[i].GodinaBerbe;
                        list.Cells[i + 2, 6].Value = spremniciLista[i].Punilac;
                        list.Cells[i + 2, 7].Value = spremniciLista[i].ŠifraPodruma;
                        list.Cells[i + 2, 8].Value = spremniciLista[i].SortaVina;
                    }

                    list.Cells[1, 1, spremniciLista.Count + 1, 8].AutoFitColumns();

                    sadržaj = excel.GetAsByteArray();
                }

                return File(sadržaj, ExcelContentType, "spremnici.xlsx");

                #endregion
            }

            return RedirectToAction("Spremnici");
        }

        [HttpPost]
        public IActionResult GenerirajIzvješćeAditivi()
        {
            return View();
        }

        [HttpPost]
        public IActionResult GenerirajIzvješćeUzorciZaAnalizu()
        {
            return View();
        }

        private PdfReport InicijalnePostavke(string naslov, bool portrait = true)
        {
            var userName = _userManager.GetUserName(User);
            var korisnik = _repository.GetZaposlenik(userName);

            var pdf = new PdfReport();

            pdf.DocumentPreferences(doc =>
                {
                    doc.Orientation(portrait ? PageOrientation.Portrait : PageOrientation.Landscape);

                    doc.PageSize(PdfPageSize.A4);
                    doc.DocumentMetadata(new DocumentMetadata
                    {
                        Author = $"{korisnik.Ime} {korisnik.Prezime}",
                        Application = "WineryApp",
                        Title = naslov
                    });
                    doc.Compression(new CompressionSettings
                    {
                        EnableCompression = true,
                        EnableFullCompression = true
                    });
                }).MainTableTemplate(template => { template.BasicTemplate(BasicTemplate.ProfessionalTemplate); })
                .MainTablePreferences(table =>
                {
                    table.ColumnsWidthsType(TableColumnWidthType.Relative);
                    table.GroupsPreferences(new GroupsPreferences
                    {
                        GroupType = GroupType.HideGroupingColumns,
                        RepeatHeaderRowPerGroup = true,
                        ShowOneGroupPerPage = true,
                        SpacingBeforeAllGroupsSummary = 5f,
                        NewGroupAvailableSpacingThreshold = 150,
                        SpacingAfterAllGroupsSummary = 5f
                    });
                    table.SpacingAfter(4f);
                });
            return pdf;
        }
    }
}