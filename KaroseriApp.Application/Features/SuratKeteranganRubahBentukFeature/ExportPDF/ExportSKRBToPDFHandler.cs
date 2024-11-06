using KaroseriApp.Application.Domain;
using KaroseriApp.Application.Features.SuratKeteranganRubahBentukFeature.Buat;
using QuestPDF.Fluent;
using QuestPDF.Helpers;

namespace KaroseriApp.Application.Features.SuratKeteranganRubahBentukFeature.ExportPDF;

public class ExportSKRBToPDFHandler()
{
    public async Task<int> Handle(SuratKeteranganRubahBentuk skrb)
    {
        Document.Create(container =>
        {
            container.Page(page =>
            {
                page.Margin(50);

                page.Header().Height(100).Background(Colors.Grey.Lighten1);
                page.Content().Background(Colors.Grey.Lighten3);
                page.Footer().Height(50).Background(Colors.Grey.Lighten1);
            });
        }).GeneratePdfAndShow();

        // var result = await buatSKRB.Handle(skrb);
        // return result;
        return await Task.FromResult(1);
    }
}