namespace KaroseriApp.Application.Domain;

public class SuratKeteranganRubahBentuk
{
    public int Id { get; set; }
    public required string NomorSurat { get; set; } = $"No. JS000/II/{DateTime.Now.Year}";
    public required string NamaPerusahaanPenerbitSKRB { get; set; }
    public required string NomorMesin { get; set; }
    public required string MerkKendaraan { get; set; }
    public required string TahunPembuatan { get; set; }
    public required string NomorChasis { get; set; }
    public required string NomorPolisi { get; set; }
    public required string NamaPemilik { get; set; }
    public string? AlamatPemiliki { get; set; }
    public string? WarnaSebelumnya { get; set; }
    public required string WarnaSetelahDirubah { get; set; }
    public required string ModelKendaraan { get; set; }
    public DateTime TanggalSuratDibuat { get; set; }
    public required string TempatSuratDiBuat { get; set; }
    public required string DiTandaTanganiOleh { get; set; }
    public required string JabatanPenandaTangan { get; set; }
}