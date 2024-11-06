// using KaroseriApp.Application.Domain;
// using QuestPDF.Fluent;
// using QuestPDF.Helpers;
// using QuestPDF.Infrastructure;

// namespace KaroseriApp.Application.Features.SuratKeteranganRubahBentukFeature.ExportPDF;

// public class SKRBDocument : IDocument
// {
//     public SuratKeteranganRubahBentuk Skrb { get; }
//     public SKRBDocument(SuratKeteranganRubahBentuk skrb)
//     {
//         Skrb = skrb;
//     }

//     public void Compose(IDocumentContainer container)
//     {
//         container.Page(page =>
//         {
//             page.Margin(50);

//             page.Header().Height(100).Background(Colors.Grey.Lighten1);
//             page.Content().Background(Colors.Grey.Lighten3);
//             page.Footer().Height(50).Background(Colors.Grey.Lighten1);

//         });
//     }
// }
