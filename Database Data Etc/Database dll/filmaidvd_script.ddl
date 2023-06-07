#@(#) script.ddl

DROP TABLE IF EXISTS Mokejimas;
DROP TABLE IF EXISTS Uzsakyta_paslauga;
DROP TABLE IF EXISTS Saskaita;
DROP TABLE IF EXISTS priziuri;
DROP TABLE IF EXISTS Sutartis;
DROP TABLE IF EXISTS Darbuotojas;
DROP TABLE IF EXISTS Sandelis;
DROP TABLE IF EXISTS Paslaugos_kaina;
DROP TABLE IF EXISTS Parduotuve;
DROP TABLE IF EXISTS Filmo_DVD;
DROP TABLE IF EXISTS Paslauga;
DROP TABLE IF EXISTS Miestas;
DROP TABLE IF EXISTS Klientas;
DROP TABLE IF EXISTS Filmu_tipai;
DROP TABLE IF EXISTS Amziaus_apribojimai;

CREATE TABLE Amziaus_apribojimai
(
	id_Amziaus_apribojimai integer,
	name char (4) NOT NULL,
	PRIMARY KEY(id_Amziaus_apribojimai)
);
INSERT INTO Amziaus_apribojimai(id_Amziaus_apribojimai, name) VALUES(1, 'PG');
INSERT INTO Amziaus_apribojimai(id_Amziaus_apribojimai, name) VALUES(2, 'N-7');
INSERT INTO Amziaus_apribojimai(id_Amziaus_apribojimai, name) VALUES(3, 'N-13');
INSERT INTO Amziaus_apribojimai(id_Amziaus_apribojimai, name) VALUES(4, 'N-16');
INSERT INTO Amziaus_apribojimai(id_Amziaus_apribojimai, name) VALUES(5, 'N-18');

CREATE TABLE Filmu_tipai
(
	id_Filmu_tipai integer,
	name char (19) NOT NULL,
	PRIMARY KEY(id_Filmu_tipai)
);
INSERT INTO Filmu_tipai(id_Filmu_tipai, name) VALUES(1, 'detektyvinis');
INSERT INTO Filmu_tipai(id_Filmu_tipai, name) VALUES(2, 'istorinis');
INSERT INTO Filmu_tipai(id_Filmu_tipai, name) VALUES(3, 'karinis');
INSERT INTO Filmu_tipai(id_Filmu_tipai, name) VALUES(4, 'kriminalinis');
INSERT INTO Filmu_tipai(id_Filmu_tipai, name) VALUES(5, 'moksline fantastika');
INSERT INTO Filmu_tipai(id_Filmu_tipai, name) VALUES(6, 'nuotykių');
INSERT INTO Filmu_tipai(id_Filmu_tipai, name) VALUES(7, 'romantinis');
INSERT INTO Filmu_tipai(id_Filmu_tipai, name) VALUES(8, 'siaubo');
INSERT INTO Filmu_tipai(id_Filmu_tipai, name) VALUES(9, 'trileris');
INSERT INTO Filmu_tipai(id_Filmu_tipai, name) VALUES(10, 'veiksmo');
INSERT INTO Filmu_tipai(id_Filmu_tipai, name) VALUES(11, 'vesternas');
INSERT INTO Filmu_tipai(id_Filmu_tipai, name) VALUES(12, 'kultinis');
INSERT INTO Filmu_tipai(id_Filmu_tipai, name) VALUES(13, 'pornografinis');
INSERT INTO Filmu_tipai(id_Filmu_tipai, name) VALUES(14, 'noiras');
INSERT INTO Filmu_tipai(id_Filmu_tipai, name) VALUES(15, 'eksploatacinis');

CREATE TABLE Klientas
(
	vardas varchar (255) NOT NULL,
	pavarde varchar (255) NOT NULL,
	asmens_kodas varchar (255) NOT NULL,
	gimimo_data varchar (255) NOT NULL,
	telefonas varchar (255) NOT NULL,
	e_pastas varchar (255) NOT NULL,
	gyvenamoji_vieta varchar (255) NOT NULL,
	PRIMARY KEY(asmens_kodas)
);

CREATE TABLE Miestas
(
	pavadinimas varchar (255) NOT NULL,
	id_Miestas integer (11) NOT NULL,
	PRIMARY KEY(id_Miestas)
);

CREATE TABLE Paslauga
(
	pavadinimas varchar (255) NOT NULL,
	aprasymas varchar (255) NOT NULL,
	id_Paslauga integer (11) NOT NULL,
	PRIMARY KEY(id_Paslauga)
);

CREATE TABLE Filmo_DVD
(
	dvd_nr int (11) NOT NULL,
	verte float NOT NULL,
	aktoriai varchar (255) NOT NULL,
	ivertinimas varchar (255) NOT NULL,
	kurejai varchar (255) NOT NULL,
	filmo_aprasymas varchar (255) NOT NULL,
	filmo_ilgumas int (11) NOT NULL,
	filmo_kalba varchar (255) NOT NULL,
	rezoliucija varchar (255) NOT NULL,
	zanras integer (11) NOT NULL,
	amziaus_ribojimai integer (11) NOT NULL,
	PRIMARY KEY(dvd_nr),
	FOREIGN KEY(zanras) REFERENCES Filmu_tipai (id_Filmu_tipai),
	FOREIGN KEY(amziaus_ribojimai) REFERENCES Amziaus_apribojimai (id_Amziaus_apribojimai)
);

CREATE TABLE Parduotuve
(
	pavadinimas varchar (255) NOT NULL,
	adresas varchar (255) NOT NULL,
	telefonas varchar (255) NOT NULL,
	e_pastas varchar (255) NOT NULL,
	id_Parduotuve integer (11) NOT NULL,
	fk_Miestasid_Miestas integer (11) NOT NULL,
	fk_Parduotuveid_Parduotuve integer (11) NOT NULL,
	PRIMARY KEY(id_Parduotuve),
	CONSTRAINT naudoja FOREIGN KEY(fk_Miestasid_Miestas) REFERENCES Miestas (id_Miestas),
	CONSTRAINT priklauso FOREIGN KEY(fk_Parduotuveid_Parduotuve) REFERENCES Parduotuve (id_Parduotuve)
);

CREATE TABLE Paslaugos_kaina
(
	kaina float NOT NULL,
	id_Paslaugos_kaina integer (11) NOT NULL,
	fk_Paslaugaid_Paslauga integer (11) NOT NULL,
	PRIMARY KEY(id_Paslaugos_kaina),
	CONSTRAINT duodama_uz FOREIGN KEY(fk_Paslaugaid_Paslauga) REFERENCES Paslauga (id_Paslauga)
);

CREATE TABLE Sandelis
(
	pavadinimas varchar (255) NOT NULL,
	adresas varchar (255) NOT NULL,
	id_Sandelis integer (11) NOT NULL,
	fk_Miestasid_Miestas integer (11) NOT NULL,
	PRIMARY KEY(id_Sandelis),
	CONSTRAINT turi FOREIGN KEY(fk_Miestasid_Miestas) REFERENCES Miestas (id_Miestas)
);

CREATE TABLE Darbuotojas
(
	vardas varchar (255) NOT NULL,
	pavarde varchar (255) NOT NULL,
	darbuotojo_nr varchar (255) NOT NULL,
	fk_Parduotuveid_Parduotuve integer (11) NOT NULL,
	PRIMARY KEY(darbuotojo_nr),
	CONSTRAINT dirba FOREIGN KEY(fk_Parduotuveid_Parduotuve) REFERENCES Parduotuve (id_Parduotuve)
);

CREATE TABLE Sutartis
(
	Sutarties_id int (11) NOT NULL,
	sutarties_data varchar (255) NOT NULL,
	kaina float NOT NULL,
	garantinio_galiojimas varchar (255) NOT NULL,
	laikas varchar (255) NOT NULL,
	fk_Darbuotojasdarbuotojo_nr varchar (255) NOT NULL,
	fk_Filmo_DVDdvd_nr int (11) NOT NULL,
	fk_Klientasasmens_kodas varchar (255) NOT NULL,
	PRIMARY KEY(Sutarties_id),
	CONSTRAINT patvirtina FOREIGN KEY(fk_Darbuotojasdarbuotojo_nr) REFERENCES Darbuotojas (darbuotojo_nr),
	FOREIGN KEY(fk_Filmo_DVDdvd_nr) REFERENCES Filmo_DVD (dvd_nr),
	CONSTRAINT sudaro FOREIGN KEY(fk_Klientasasmens_kodas) REFERENCES Klientas (asmens_kodas)
);

CREATE TABLE priziuri
(
	fk_Sandelisid_Sandelis integer (11) NOT NULL,
	fk_Darbuotojasdarbuotojo_nr varchar (255) NOT NULL,
	PRIMARY KEY(fk_Sandelisid_Sandelis, fk_Darbuotojasdarbuotojo_nr),
	CONSTRAINT priziuri FOREIGN KEY(fk_Sandelisid_Sandelis) REFERENCES Sandelis (id_Sandelis)
);

CREATE TABLE Saskaita
(
	nr int (11) NOT NULL,
	data varchar (255) NOT NULL,
	suma float NOT NULL,
	fk_SutartisSutarties_id int (11) NOT NULL,
	PRIMARY KEY(nr),
	CONSTRAINT israsyta FOREIGN KEY(fk_SutartisSutarties_id) REFERENCES Sutartis (Sutarties_id)
);

CREATE TABLE Uzsakyta_paslauga
(
	kiekis int (11) NOT NULL,
	kaina float NOT NULL,
	data varchar (255) NOT NULL,
	id_Uzsakyta_paslauga integer (11) NOT NULL,
	fk_SutartisSutarties_id int (11) NOT NULL,
	fk_Paslaugos_kainaid_Paslaugos_kaina integer (11) NOT NULL,
	PRIMARY KEY(id_Uzsakyta_paslauga),
	CONSTRAINT yraitraukta FOREIGN KEY(fk_SutartisSutarties_id) REFERENCES Sutartis (Sutarties_id),
	CONSTRAINT teikiama_uz FOREIGN KEY(fk_Paslaugos_kainaid_Paslaugos_kaina) REFERENCES Paslaugos_kaina (id_Paslaugos_kaina)
);

CREATE TABLE Mokejimas
(
	data varchar (255) NOT NULL,
	suma float NOT NULL,
	id_Mokejimas integer (11) NOT NULL,
	fk_Saskaitanr int (11) NOT NULL,
	fk_Klientasasmens_kodas varchar (255) NOT NULL,
	PRIMARY KEY(id_Mokejimas),
	CONSTRAINT apmoka FOREIGN KEY(fk_Saskaitanr) REFERENCES Saskaita (nr),
	CONSTRAINT sumokejo FOREIGN KEY(fk_Klientasasmens_kodas) REFERENCES Klientas (asmens_kodas)
);
