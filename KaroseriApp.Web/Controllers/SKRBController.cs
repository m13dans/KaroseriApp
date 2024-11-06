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
    public async Task<ActionResult> ExportToPDF(
        SuratKeteranganRubahBentuk skrb,
        ExportSKRBToPDFHandler exportHandler
        )
    {
        await exportHandler.Handle(skrb);
        return Ok();
    }
}