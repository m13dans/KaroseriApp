IF NOT EXISTS (
    SELECT * FROM sys.objects
    WHERE object_id = OBJECT_ID(N'[dbo].Tabel_Surat_Keterangan_Rubah_Bentuk') AND type in (N'U')
)
BEGIN
    create table Biodata (
        Id INT PRIMARY KEY IDENTITY(1, 1),
        Nomor_Surat VARCHAR(255),
        Nama_Perusahaan_Penerbit_SKRB VARCHAR(255),
        Nomor_Mesin VARCHAR(255),
        Merk_Kendaraan VARCHAR(255),
        Tahun_Pembuatan VARCHAR(255),
        Nomor_Chasis VARCHAR(255),
        Nomor_Polisi VARCHAR(255),
        Nama_Pemilik VARCHAR(255),
        Alamat_Pemilik VARCHAR(255),
        Warna_Sebelumnya VARCHAR(255),
        Warna_Setelah_Dirubah VARCHAR(255),
        Model_Kendaraan VARCHAR(255),
        Tanggal_Surat_Dibuat DATE(255),
        Tempat_Surat_Dibuat VARCHAR(255),
        DiTanda_Tangani_Oleh VARCHAR(255),
        Jabatan_Pendanda_Tangan VARCHAR(255)
    )
END