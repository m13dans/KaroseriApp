namespace KaroseriApp.Application.Domain;

public class SuratKeteranganRubahBentuk
{
    public int Id { get; set; }
    public required string NomorSurat { get; set; } = $"No. JS000/II/{DateTime.Now.Year}";
    public required string NamaPerusahaanPenerbitSKRB { get; set; } = "JAYA SAPUTRA";
    public required string NomorMesin { get; set; }
    public required string MerkKendaraan { get; set; }
    public required string TahunPembuatan { get; set; }
    public required string NomorChasis { get; set; }
    public string? NomorPolisi { get; set; }
    public string? NamaPemilik { get; set; }
    public string? AlamatPemilik { get; set; }
    public string? WarnaSebelumnya { get; set; }
    public required string WarnaSetelahDirubah { get; set; }
    public required string ModelKendaraan { get; set; }
    public DateTime TanggalSuratDibuat { get; set; } = DateTime.Now;
    public required string TempatSuratDiBuat { get; set; } = "Sukabumi";
    public required string DiTandaTanganiOleh { get; set; } = "Cucu Abdurachman";
    public required string JabatanPenandaTangan { get; set; } = "Direktur";
}