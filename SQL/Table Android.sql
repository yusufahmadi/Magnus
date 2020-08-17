create table TLabel([no] int primary key, 
dokumen nvarchar(50), 
tgl datetime default current_timestamp,
id_bahan int,
harga_bahan money,
lebar numeric(18,4),
tinggi numeric(18,4),
gap numeric(18,4),
pisau int,
pembulatan numeric(18,4),
qty_order int,
jual_sesuai_order int,
biaya_pisau money,
biaya_tinta money,
biaya_toyobo money,
biaya_operator money,
biaya_kirim money)


create table TRibbon([no] integer primary key, 
dokumen text, 
tgl datetime default current_timestamp,
id_bahan int,
harga_bahan real,
lebar real,
panjang real,
modal real,
qty real,
jual_roll real,
jumlah_profit_kotor real,
transport real,
komisisalesprosen real,
netprofit real)

create table TTaffeta([no] integer primary key, 
dokumen text, 
tgl datetime default current_timestamp,
id_bahan int,
harga_bahan real,
lebar real,
panjang real,
modal real,
qty real,
jual_roll real,
jumlah_profit_kotor real,
transport real,
komisisalesprosen real,
netprofit real,
kurs real)

create table TPaket([no] integer primary key, 
dokumen text, 
tgl datetime default current_timestamp,
customer_minta_bikin_jadinya_line real,
qty_order_customer_pcs real,
isi_roll real,
lebar real,
tinggi real,
pisau_yang_digunakan real,
dibulatkan real,
lebar_ribbon real,
panjang_ribbon real)