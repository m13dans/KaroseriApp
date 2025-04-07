using Dapper;
using KaroseriApp.Application.Data;

namespace KaroseriApp.Application.Features.SuratKeteranganRubahBentukFeature.Shared;

public class SharedSKRBFeature(SqlConnectionFactory sqlConnectionFactory)
{
    private readonly SqlConnectionFactory _sqlConnectionFactory = sqlConnectionFactory;

    public async Task<string?> GetLastDocNoAsync(string tempatSuratDibuat, string namaPerusahaanPenerbit)
    {
        string sql = @"
            SELECT TOP 1 nomor_surat FROM trans_skrb
            WHERE tempat_surat_dibuat = @TempatSuratDibuat
            AND nama_perusahaan_penerbit_skrb = @NamaPerusahaanPenerbitSKRB
            ORDER BY id_trans_skrb DESC";

        try
        {
            using var conn = _sqlConnectionFactory.Create();
            var result = await conn.QueryFirstOrDefaultAsync<string>(
                sql,
                new { TempatSuratDibuat = tempatSuratDibuat, NamaPerusahaanPenerbitSKRB = namaPerusahaanPenerbit });

            return result;
        }

        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}
