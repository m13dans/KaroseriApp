using KaroseriApp.Application.Data;
using KaroseriApp.Application.Domain;
using KaroseriApp.Application.Features.SuratKeteranganRubahBentukFeature.Buat;
using KaroseriApp.Application.Features.SuratKeteranganRubahBentukFeature.ExportPDF;
using Microsoft.AspNetCore.Mvc;

namespace KaroseriApp.Web.Controllers;

public class SKRBController : Controller
{
    public async Task<SuratKeteranganRubahBentuk> ExportToPDF(
        SuratKeteranganRubahBentuk skrb,
        ExportSKRBToPDFHandler exportHandler,
        BuatSKRBHandler buatSKRBHandler
        )
    {
        throw new();
    }
}