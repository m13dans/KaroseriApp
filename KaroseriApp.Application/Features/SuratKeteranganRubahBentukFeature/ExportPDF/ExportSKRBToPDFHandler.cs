using KaroseriApp.Application.Domain;
using QuestPDF.Fluent;
using QuestPDF.Helpers;

namespace KaroseriApp.Application.Features.SuratKeteranganRubahBentukFeature.ExportPDF;

public class ExportSKRBToPDFHandler()
{
	public static byte[] Handle(SuratKeteranganRubahBentuk skrb)
	{
		var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "../KaroseriApp.Application/Images", "Logo_Jaya_Saputra.png");
		var document = Document.Create(container =>
		{
			container.Page(page =>
			{
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
											text.DefaultTextStyle(x => x.FontFamily("Times New Roman"));
											text.AlignCenter();
											text.Line(skrb.NamaPerusahaanPenerbitSKRB).FontSize(30);
											text.Line("Industri Karoseri Kendaraan Bermotor Roda Empat Atau Lebih Industri Trailer \n Dan Semi Trailer")
												.Italic();
											text.Line("Jl. Siliwangi No. 94 Sekarwangi, Cibadak, Kabupaten Sukabumi");
											text.Line("No.Hp. 081563555554 / 081286347313");
											text.Line("email : jayasaputrakaroseri@gmail.com");
										});
								});

						column.Item().BorderBottom(7).Height(5.3f);
					});


				page.Content().Background(Colors.Grey.Lighten3);
				page.Footer().Height(50).Background(Colors.Grey.Lighten1);
			});
		});

		return document.GeneratePdf();
	}
}