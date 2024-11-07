using KaroseriApp.Application.Data;
using KaroseriApp.Application.Domain;
using KaroseriApp.Application.Features.SuratKeteranganRubahBentukFeature.Buat;
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
    public ActionResult GeneratePDF(
        SuratKeteranganRubahBentuk skrb,
        ExportSKRBToPDFHandler exportHandler
        )
    {
        byte[] pdf = exportHandler.Handle(skrb);
        var base64StringPdf = Convert.ToBase64String(pdf);
        return Json(new { base64StringPdf });
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