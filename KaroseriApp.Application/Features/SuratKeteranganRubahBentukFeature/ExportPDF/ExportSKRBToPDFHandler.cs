using KaroseriApp.Application.Domain;
using QuestPDF.Companion;
using QuestPDF.Fluent;
using QuestPDF.Helpers;

namespace KaroseriApp.Application.Features.SuratKeteranganRubahBentukFeature.ExportPDF;

public class ExportSKRBToPDFHandler()
{
    public byte[] Handle(SuratKeteranganRubahBentuk skrb)
    {
        var document = Document.Create(container =>
        {
            container.Page(page =>
            {
                page.Margin(50);
                page.MarginTop(30);

                page.Header()
                    .Height(130)
                    .Background(Colors.Blue.Accent1)
                    .BorderBottom(1)
                    .BorderBottom(8)
                    .Text(text =>
                    {
                        text.DefaultTextStyle(x => x.FontFamily("Times New Roman"));
                        text.AlignCenter();
                        text.Line(skrb.NamaPerusahaanPenerbitSKRB).FontSize(30);
                        text.Line("Industri Karoseri Kendaraan Bermotor Roda Empat Atau Lebih Industri Trailer \n Dan Semi Trailer")
                            .Italic();
                        text.Line("Jl. Siliwangi No. 94 Sekarwangi, Cibadak, Kabupaten Sukabumi");
                        text.Line("No.Hp. 081563555554 / 081286347313");
                        text.Line("email : jayasaputrakaroseri@gmail.com");
                    });


                page.Content().Background(Colors.Grey.Lighten3);
                page.Footer().Height(50).Background(Colors.Grey.Lighten1);
            });
        });

        return document.GeneratePdf();

    }
}