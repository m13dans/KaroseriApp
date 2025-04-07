using Dapper;
using KaroseriApp.Application.Data;
using KaroseriApp.Application.Domain;
using KaroseriApp.Application.Features.SuratKeteranganRubahBentukFeature.Shared;
using QuestPDF.Fluent;
using System.Globalization;
using System.Transactions;

namespace KaroseriApp.Application.Features.SuratKeteranganRubahBentukFeature.ExportPDF;

public class ExportSKRBToPDFHandler(SharedSKRBFeature sharedSKRBFeature, SqlConnectionFactory sqlConnectionFactory)
{
    public async Task<(byte[], string)> Handle(SuratKeteranganRubahBentuk skrb)
    {
        try
        {
            using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
            using var conn = sqlConnectionFactory.Create();

            var kodeArea = skrb.TempatSuratDiBuat.ToUpper() switch
            {
                "SUKABUMI" => "SMI",
                _ => "SMI"
            };

            var kodePerusahaan = skrb.NamaPerusahaanPenerbitSKRB.ToUpper() switch
            {
                "JAYA SAPUTRA" => "JS",
                _ => "JS"
            };

            var dateFormated = DateTime.UtcNow.ToString("ddMMyyyy");

            var lastDocNo = await sharedSKRBFeature.GetLastDocNoAsync(skrb.TempatSuratDiBuat, skrb.NamaPerusahaanPenerbitSKRB);
            var docNo = "";

            if (string.IsNullOrEmpty(lastDocNo))
            {
                docNo = $"SKRB/{kodeArea}/{kodePerusahaan}/{dateFormated}/1";
            }
            else
            {
                var lastNo = Convert.ToInt32(lastDocNo.Substring(lastDocNo.LastIndexOf('/') + 1));
                docNo = $"SKRB/{kodeArea}/{kodePerusahaan}/{dateFormated}/{lastNo + 1}";
            }

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

                                text.Line(docNo).LineHeight(4);
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

                                if (skrb.AlamatPemilik?.Length < 50)
                                {
                                    text.Line($"Alamat                              : {skrb.AlamatPemilik}").LineHeight(2);
                                }
                                else
                                {
                                    var subString = skrb.AlamatPemilik?.Substring(0, 50);
                                    var lastIndexOfSpaceInSubString = subString?.LastIndexOf(' ') ?? 50;

                                    text.Line($"Alamat                              : {skrb.AlamatPemilik?.Substring(0, lastIndexOfSpaceInSubString)}").LineHeight(2);
                                    text.Line($"                                           {skrb.AlamatPemilik?.Substring(lastIndexOfSpaceInSubString)}");
                                }
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

                            column.Item().PaddingTop(20).Text(text =>
                            {
                                text.AlignEnd();
                                text.Line($"{skrb.TempatSuratDiBuat}, {(skrb.TanggalSuratDibuat ??= DateTime.UtcNow).ToString("dd-MM-yyyy", new CultureInfo("id-ID"))}");
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

            var result = await conn.ExecuteAsync(@"INSERT INTO trans_skrb 
                (
                    nomor_surat, nama_perusahaan_penerbit_skrb, nomor_mesin, merk_kendaraan, tahun_pembuatan, nomor_chasis, nomor_polisi, 
                    nama_pemilik, alamat_pemilik, warna_sebelumnya, warna_setelah_dirubah, model_kendaraan, tanggal_surat_dibuat, tempat_surat_dibuat,
                    ditandatangani_oleh, jabatan_penandatangan
                )
                VALUES 
                (
                    @NomorSurat, @NamaPerusahaanPenerbitSKRB, @NomorMesin, @MerkKendaraan, @TahunPembuatan, @NomorChasis, @NomorPolisi,
                    @NamaPemilik, @AlamatPemilik, @WarnaSebelumnya, @WarnaSetelahDirubah, @ModelKendaraan, @TanggalSuratDibuat, @TempatSuratDibuat,
                    @DitandatanganiOleh, @JabatanPenandatangan
                )", 
                new
                {
                    NomorSurat = docNo,
                    skrb.NamaPerusahaanPenerbitSKRB,
                    skrb.NomorMesin,
                    skrb.MerkKendaraan,
                    skrb.TahunPembuatan,
                    skrb.NomorChasis,
                    skrb.NomorPolisi,
                    skrb.NamaPemilik,
                    skrb.AlamatPemilik,
                    skrb.WarnaSebelumnya,
                    skrb.WarnaSetelahDirubah,
                    skrb.ModelKendaraan,
                    TanggalSuratDibuat = skrb.TanggalSuratDibuat ?? DateTime.UtcNow,
                    skrb.TempatSuratDiBuat,
                    skrb.DitandatanganiOleh,
                    skrb.JabatanPenandaTangan
                });

            scope.Complete();

            return (document.GeneratePdf(), docNo);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }

    }
}