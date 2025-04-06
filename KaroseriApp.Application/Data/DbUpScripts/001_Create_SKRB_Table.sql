IF NOT EXISTS (
    SELECT * FROM sys.objects
    WHERE object_id = OBJECT_ID(N'[dbo].trans_skrb') AND type in (N'U')
)
BEGIN
    create table trans_skrb (
        id_trans_skrb int primary key identity(1, 1),
        nomor_surat varchar(100),
        nama_perusahaan_penerbit_skrb varchar(100),
        nomor_mesin varchar(100),
        merk_kendaraan varchar(100),
        tahun_pembuatan varchar(4),
        nomor_chasis varchar(100),
        nomor_polisi varchar(100),
        nama_pemilik varchar(100),
        alamat_pemilik varchar(100),
        warna_sebelumnya varchar(100),
        warna_setelah_dirubah varchar(100),
        model_kendaraan varchar(100),
        tanggal_surat_dibuat date,
        tempat_surat_dibuat varchar(100),
        ditandatangani varchar(100),
        jabatan_pendandatangan varchar(100)
    )
END