using KaroseriApp.Application.Data;
using KaroseriApp.Application.Features.SuratKeteranganRubahBentukFeature.Buat;
using KaroseriApp.Application.Features.SuratKeteranganRubahBentukFeature.ExportPDF;
using KaroseriApp.Application.Features.SuratKeteranganRubahBentukFeature.Shared;
using QuestPDF.Infrastructure;

QuestPDF.Settings.License = LicenseType.Community;

var builder = WebApplication.CreateBuilder(args);

if (builder.Environment.IsDevelopment())
{
    builder.Configuration.AddUserSecrets<Program>();
}

string connectionString = builder.Configuration.GetConnectionString("SqlExpress")!;

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddSingleton<SqlConnectionFactory>();

builder.Services.AddScoped<BuatSKRBHandler>();
builder.Services.AddScoped<ExportSKRBToPDFHandler>();
builder.Services.AddScoped<SharedSKRBFeature>();


var app = builder.Build();

DbInitializer.Initialize(connectionString);

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
