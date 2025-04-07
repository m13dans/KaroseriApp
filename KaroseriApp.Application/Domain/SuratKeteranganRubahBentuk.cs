using System.ComponentModel.DataAnnotations;

namespace KaroseriApp.Application.Domain;

public class SuratKeteranganRubahBentuk
{
    public int Id { get; set; }
    public string NomorSurat { get; set; } = $"No. JS000/II/{DateTime.Now.Year}";
    public string NamaPerusahaanPenerbitSKRB { get; set; } = "Jaya Saputra";
    [Required(ErrorMessage = "Nomor Mesin Harus Diisi.")]
    public required string NomorMesin { get; set; }
    [Required(ErrorMessage = "Merk Kendaraan Harus Diisi.")]
    public required string MerkKendaraan { get; set; }
    [Required(ErrorMessage = "Tahun Pembuatan Harus Diisi.")]
    public int TahunPembuatan { get; set; }
    [Required(ErrorMessage = "Nomor Chasis Harus Diisi.")]
    public required string NomorChasis { get; set; }
    public string? NomorPolisi { get; set; }
    public string? NamaPemilik { get; set; }
    public string? AlamatPemilik { get; set; }
    public string? WarnaSebelumnya { get; set; }
    [Required(ErrorMessage = "Warna Setelah Dirubah Harus Diisi.")]
    public required string WarnaSetelahDirubah { get; set; }
    [Required(ErrorMessage = "Model Kendaraan Harus Diisi.")]
    public required string ModelKendaraan { get; set; }
    public DateTime? TanggalSuratDibuat { get ; set; } 
    [Required(ErrorMessage = "Tempat Surat Dibuat Harus Diisi.")]
    public required string TempatSuratDiBuat { get; set; }
    public string? DitandatanganiOleh { get; set; } = "Cucu Abdurachman";
    public string? JabatanPenandaTangan { get; set; } = "Direktur";
}