using KaroseriApp.Application.Domain;
using KaroseriApp.Application.Features.SuratKeteranganRubahBentukFeature.ExportPDF;
using QuestPDF.Companion;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using System.Globalization;
using System.Reflection;

QuestPDF.Settings.License = LicenseType.Community;

Console.WriteLine("Hello, World!");

var skrb = new SuratKeteranganRubahBentuk()
{
    MerkKendaraan = "MITSUBISHI / COLT DIESEL FE 74 HDV (4x2) M/T",
    ModelKendaraan = "Bak Terbuka",
    NomorChasis = "MHMFE74P5KK206703",
    NomorMesin = "4D34TT54724",
    NomorSurat = " No. JS041/II/2024",
    TahunPembuatan = 2024,
    WarnaSetelahDirubah = "Kuning",
    TempatSuratDiBuat = "Sukabumi"
};

var document = ConsolePDFHandler.Handle(skrb);

document.ShowInCompanion();


public class ConsolePDFHandler()
{
    public static Document Handle(SuratKeteranganRubahBentuk skrb)
    {
        var assemblyLocation = Path.GetDirectoryName(typeof(ExportSKRBToPDFHandler).Assembly.Location);
        var path = "Images\\Logo_Jaya_Saputra.png";
        var imagePath = Path.Combine(assemblyLocation!, path);
        var document = Document.Create(container =>
        {
            container.Page(page =>
            {
                page.DefaultTextStyle(x => x.FontFamily("Times New Roman"));
                page.Margin(50);
                page.MarginTop(30);

                page.Header()
                    .Column(column =>
                    {
                        column.Spacing(0.5f);
                        column.Item()
                              .Height(120)
                              .BorderBottom(1)
                              .Row(row =>
                              {
                                  row.Spacing(2);
                                  row.ConstantItem(100).Image(imagePath);

                                  row.RelativeItem()
                                      .Text(text =>
                                      {
                                          text.AlignCenter();
                                          text.Line(skrb.NamaPerusahaanPenerbitSKRB.ToUpper()).FontSize(30);
                                          text.Line("Industri Karoseri Kendaraan Bermotor Roda Empat Atau Lebih Industri Trailer \n Dan Semi Trailer")
                                              .Italic();
                                          text.Line("Jl. Siliwangi No. 94 Sekarwangi, Cibadak, Kabupaten Sukabumi");
                                          text.Line("No.Hp. 081563555554 / 081286347313");
                                          text.Line("email : jayasaputrakaroseri@gmail.com");
                                      });
                              });

                        column.Item().BorderBottom(7).Height(5.3f);
                    });

                page.Content().PaddingTop(30)
                    .Column(column =>
                    {
                        column.Item().Text(text =>
                        {
                            text.AlignCenter();
                            text.Line("SURAT KETERANGAN PERRUBAHAN BENTUK").Bold().FontSize(14);

                            text.Line(skrb.NomorSurat).LineHeight(4);
                        });

                        column.Item().PaddingTop(-50).Text(text =>
                        {
                            text.AlignStart();
                            text.Line("Menerangkan kendaraan tersebut dibawah ini :");

                        });

                        column.Item().Text(text =>
                        {
                            text.ParagraphFirstLineIndentation(40);
                            text.Line($"No. Mesin                        : {skrb.NomorMesin}").LineHeight(2);
                            text.Line($"Merk / Type                     : {skrb.MerkKendaraan}").LineHeight(2);
                            text.Line($"Tahun Pembuatan            : {skrb.TahunPembuatan}").LineHeight(2);
                            text.Line($"No. Chasis / Landasan     : {skrb.NomorChasis}").LineHeight(2);
                            text.Line($"Pemilik                             : {skrb.NamaPemilik}").LineHeight(2);

                        });

                        column.Item().Text(text =>
                        {
                            text.Line("Telah dirubah bentuknya menjadi");
                        });

                        column.Item().Text(text =>
                        {
                            text.ParagraphFirstLineIndentation(40);
                            text.Line($"Model                              : {skrb.ModelKendaraan}").LineHeight(2);
                            text.Line($"Warna                              : {skrb.WarnaSetelahDirubah}").LineHeight(2);
                        });

                        column.Item().Text(text =>
                        {
                            text.Line("Demikian Surat Keterangan Perubahan Bentuk ini dibuat agar yang berkepentingan maklum.");
                        });

                        column.Item().PaddingTop(50).Text(text =>
                        {
                            text.AlignEnd();
                            text.Line($"{skrb.TempatSuratDiBuat}, {skrb.TanggalSuratDibuat!.Value.ToString("dd-MM-yyyy", new CultureInfo("id-ID"))}");
                            text.Line(skrb.NamaPerusahaanPenerbitSKRB).LineHeight(2);
                            text.EmptyLine();
                            text.EmptyLine();
                            text.Line(skrb.DitandatanganiOleh).LineHeight(1);
                            text.Line(skrb.JabatanPenandaTangan).LineHeight(1);
                        });

                        column.Item().Text(text =>
                        {
                            text.AlignStart();
                            text.DefaultTextStyle(x => x.LineHeight(1.5f));
                            text.Line("Dibuat rangkap 3 (tiga)");
                            text.Line("1. Untuk DLLAJ");
                            text.Line("2. Untuk Kepolisian");
                            text.Line("3. Untuk Arsip");
                        });
                    });

            });
        });


        return document;
    }
}
