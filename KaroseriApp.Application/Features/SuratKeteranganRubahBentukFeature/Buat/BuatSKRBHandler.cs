using Dapper;
using KaroseriApp.Application.Data;
using KaroseriApp.Application.Domain;

namespace KaroseriApp.Application.Features.SuratKeteranganRubahBentukFeature.Buat;

public class BuatSKRBHandler(SqlConnectionFactory connection)
{
    public async Task<int> Handle(SuratKeteranganRubahBentuk skrb)
    {
        var sqlCommand = @"
            INSERT INTO Tabel_Surat_Keterangan_Rubah_Bentuk (
                Nomor_Surat,
                Nama_Perusahaan_Penerbit_SKRB,
                Nomor_Mesin,
                Merk_Kendaraan,
                Tahun_Pembuatan,
                Nomor_Chasis,
                Nomor_Polisi,
                Nama_Pemilik,
                Alamat_Pemilik,
                Warna_Sebelumnya,
                Warna_Setelah_Dirubah,
                Model_Kendaraan,
                Tanggal_Surat_Dibuat,
                Tempat_Surat_Dibuat,
                DiTanda_Tangani_Oleh,
                Jabatan_Pendanda_Tangan) 
            OUTPUT INSERTED.Id
            VALUES (
                NomorSurat,
                NamaPerusahaanPenerbitSKRB,
                NomorMesin,
                MerkKendaraan,
                TahunPembuatan,
                NomorChasis,
                NomorPolisi,
                NamaPemilik,
                AlamatPemilik,
                WarnaSebelumnya,
                WarnaSetelahDirubah,
                ModelKendaraan,
                TanggalSuratDibuat,
                TempatSuratDibuat,
                DiTandaTanganiOleh,
                JabatanPendandaTangan)";
        using var conn = connection.Create();

        var result = await conn.QuerySingleOrDefaultAsync<int>(sqlCommand, new { SKRB = skrb });

        return result;
    }
}