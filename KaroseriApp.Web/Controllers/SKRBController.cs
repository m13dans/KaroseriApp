using KaroseriApp.Application.Domain;
using KaroseriApp.Application.Features.SuratKeteranganRubahBentukFeature.ExportPDF;
using Microsoft.AspNetCore.Mvc;

namespace KaroseriApp.Web.Controllers;

public class SKRBController : Controller
{
    public ActionResult Buat()
    {
        return View();
    }

    [HttpPost]
    public async Task<ActionResult> GeneratePDFAsync(
        SuratKeteranganRubahBentuk skrb,
        [FromServices]ExportSKRBToPDFHandler exportHandler
        )
    {
        (byte[], string) pdfByteAndDocNo = await exportHandler.Handle(skrb);
        string base64StringPdf = Convert.ToBase64String(pdfByteAndDocNo.Item1);
        string docno = pdfByteAndDocNo.Item2;

        return Json(new
        {
            pdfData = base64StringPdf,
            nomorSurat = docno
        });
    }

    [HttpGet]
    public IActionResult PreviewPDF()
    {
        if (TempData["PDFData"] is string base64PdfData)
        {
            byte[] pdfData = Convert.FromBase64String(base64PdfData);
            return File(pdfData, "application/pdf");
        }
        return NotFound();
    }

}