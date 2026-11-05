-- Könyvtárak (14-M03-L02-F02) – KIINDULÓ állapot: a táblákban NINCS kulcs és kapcsolat!
-- Két vármegye (Nógrád, Tolna) könyvtárai, a települések irányítószámmal.

CREATE DATABASE IF NOT EXISTS estis_konyvtarak
  CHARACTER SET utf8mb4 COLLATE utf8mb4_hungarian_ci;
USE estis_konyvtarak;

DROP TABLE IF EXISTS konyvtar;
DROP TABLE IF EXISTS telepules;
DROP TABLE IF EXISTS megye;

CREATE TABLE megye (
  id  INT NOT NULL,
  nev VARCHAR(30) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_hungarian_ci;

CREATE TABLE telepules (
  irsz     CHAR(4) NOT NULL,
  nev      VARCHAR(40) NOT NULL,
  megye_id INT NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_hungarian_ci;

CREATE TABLE konyvtar (
  id   INT NOT NULL,
  nev  VARCHAR(200) NOT NULL,
  irsz CHAR(4) NOT NULL,
  cim  VARCHAR(60) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_hungarian_ci;

INSERT INTO megye (id, nev) VALUES
(0, 'Budapest'),
(1, 'Bács-Kiskun'),
(2, 'Baranya'),
(3, 'Békés'),
(4, 'Borsod-Abaúj-Zemplén'),
(5, 'Csongrád-Csanád'),
(6, 'Fejér'),
(7, 'Győr-Moson-Sopron'),
(8, 'Hajdú-Bihar'),
(9, 'Heves'),
(10, 'Jász-Nagykun-Szolnok'),
(11, 'Komárom-Esztergom'),
(12, 'Nógrád'),
(13, 'Pest'),
(14, 'Somogy'),
(15, 'Szabolcs-Szatmár-Bereg'),
(16, 'Tolna'),
(17, 'Vas'),
(18, 'Veszprém'),
(19, 'Zala');

INSERT INTO telepules (irsz, nev, megye_id) VALUES
('2175', 'Kálló', 12),
('2176', 'Erdőkürt', 12),
('2645', 'Nagyoroszi', 12),
('2651', 'Rétság', 12),
('2653', 'Bánk', 12),
('2654', 'Romhány', 12),
('2659', 'Érsekvadkert', 12),
('2660', 'Balassagyarmat', 12),
('2685', 'Nógrádsáp', 12),
('3042', 'Palotás', 12),
('3060', 'Pásztó', 12),
('3064', 'Szurdokpüspöki', 12),
('3070', 'Bátonyterenye', 12),
('3100', 'Salgótarján', 12),
('3109', 'Salgótarján', 12),
('3121', 'Somoskőújfalu', 12),
('3132', 'Nógrádmegyer', 12),
('3141', 'Salgótarján', 12),
('3170', 'Szécsény', 12),
('7020', 'Dunaföldvár', 16),
('7027', 'Paks', 16),
('7030', 'Paks', 16),
('7064', 'Gyönk', 16),
('7081', 'Simontornya', 16),
('7090', 'Tamási', 16),
('7100', 'Szekszárd', 16),
('7130', 'Tolna', 16),
('7133', 'Fadd', 16),
('7140', 'Bátaszék', 16),
('7150', 'Bonyhád', 16),
('7200', 'Dombóvár', 16),
('7355', 'Nagymányok', 16);

INSERT INTO konyvtar (id, nev, irsz, cim) VALUES
(1, 'A Dr. Kenessey Albert Kórház-Rendelőintézet Orvosi Könyvtár', '2660', 'Rákóczi út 125-127.'),
(2, 'A Nógrádi Történeti Múzeum Könyvtára', '3100', 'Múzeum tér 2.'),
(3, 'A Palóc Múzeum Könyvtára', '2660', 'Palóc liget 1.'),
(4, 'A Tolna Megyei Önkormányzat Kórháza. Orvosi Könyvtár', '7100', 'Béri-Balogh Á. u. 5-7.'),
(5, 'Általános Iskola és Könyvtár', '2645', 'Kertész u. 45.'),
(6, 'Balassagyarmati Törvényszék Könyvtára', '2660', 'Köztársaság tér 2.'),
(7, 'Balassi Bálint Megyei Könyvtár', '3100', 'Kassai sor 2.'),
(8, 'Balassi Bálint Megyei Könyvtár. Beszterce Lakótelepi Fiókkönyvtár', '3121', 'Beszterce tér 4.'),
(9, 'Balassi Bálint Megyei Könyvtár. Forgách-i Fiókkönyvtár', '3109', 'Munkásotthon tér 1.'),
(10, 'Balassi Bálint Megyei Könyvtár. Somoskői Fiókkönyvtár', '3100', 'Vároldal út 2.'),
(11, 'Balassi Blint Megyei Könyvtár. Zagyvarónai Fiókkönyvtár', '3141', 'Zagyva út 3.'),
(12, 'Bátonyterenyei Városi Közművelődési Központ és Könyvtár', '3070', 'Molnár S. út 1-3.'),
(13, 'Béri Balogh Ádám Művelődési Ház és Könyvtár', '7133', 'Béke u. 1.'),
(14, 'Bezerédj Pál Szabadidőközpont és Könyvtár', '7130', 'Bajcsy-Zsilinszky Endre u. 73/B.'),
(15, 'Cogito Általános Művelődési Központ. Petőfi Sándor Művelődési Ház és Könyvtár', '3121', 'Somosi út 101.'),
(16, 'Dombóvár Város Könyvtára', '7200', 'Hunyadi tér 25.'),
(17, 'Dunaföldvári Művelődési Központ és Könyvtár', '7020', 'Béke tér 13.'),
(52, 'Községi Könyvtár és Közösségi Tér', '7099', 'Fő utca 12.'),
(18, 'Gyönki Művelődési Ház és Könyvtár', '7064', 'Dózsa Gy. tér 548.'),
(19, 'Keresztély Gyula Városi Könyvtár', '7140', 'Hősök tere 1.'),
(20, 'Kínálgató. Iskolai és Gyermekkönyvtár', '7030', 'Ifjúság útja 2.'),
(21, 'Kincskereső Gyermekkönyvtár', '7030', 'Dózsa György út 20.'),
(22, 'Könnyű László Városi Könyvtár és Helytörténeti Gyűjtemény', '7090', 'Szabadság u. 52.'),
(23, 'Községi Könyvtár', '2175', 'Kossuth u. 30.'),
(24, 'Községi Könyvtár', '2176', 'Kossuth út 51.'),
(25, 'Községi Könyvtár Bánk', '2653', 'Kis u. 1.'),
(26, 'Községi Könyvtár Nógrádsáp', '2685', 'Hunyadi út 2.'),
(27, 'Kubinyi Ferenc Múzeum és Krúdy Gyula Könyvtár', '3170', 'Ady E. út 12.'),
(28, 'Kubinyi Ferenc Múzeum és Krúdy Gyula Könyvtár (Múzeum könyvtára)', '3170', 'Ady Endre u. 7.'),
(29, 'Madách Imre Városi Könyvtár', '2660', 'Rákóczi fejedelem útja 50.'),
(30, 'Madách Imre Városi Könyvtár Letéti Könyvtára (Városi Idősek Otthona)', '2660', 'Markusovszky u. 1.'),
(31, 'Magyar Nemzeti Levéltár Nógrád Megyei Levéltára Zárt Forgalmú Tudományos Szakkönyvtára', '3100', 'Bem út 18.'),
(32, 'Magyar Nemzeti Levéltár Tolna Megyei Levéltára Könyvtár', '7100', 'Béla tér 1.'),
(33, 'Mihályfi Ernő Művelődési Ház és Könyvtár', '3042', 'Szabadság út 26.'),
(34, 'Mikszáth Kálmán Általános Iskola és Óvoda Tagintézménye: Községi Könyvtár', '3132', 'Petőfi út 100.'),
(35, 'Mikszáth Kálmán Könyvtár és Faluház', '3064', 'Béke út 4-6.'),
(36, 'Mikszáth Kálmán Közművelődési Intézmény Könyvtár és Ifjúsági Ház', '2659', 'Rákóczi u. 120.'),
(37, 'Nagymányoki Közművelődési Központ. Nagymányoki Városi Könyvtár - Teleház', '7355', 'Táncsics u. 2/a'),
(38, 'Paksi Közművelődési Nonprofit Kft. Könyvtára. Dunakömlődi Fiókkönyvtár', '7027', 'Béke u. 3.'),
(39, 'Paksi Közművelődési Nonprofit Korlátolt Felelősségű Társaság Könyvtára', '7030', 'Gagarin u. 2.'),
(40, 'Paksi Pákolitz István Városi Könyvtár', '7030', 'Villany u. 1.'),
(41, 'Pécsi Tudományegyetem Egyetemi Könyvtár és Tudásközpont Kultúratudományi Pedagógusképző és Vidékfejlesztési Kar Könyvtára', '7100', 'Rákóczi u. 1.'),
(42, 'Romhány Község Önkormányzata Integrált Közművelődési Könyvtára', '2654', 'Kossuth u. 68.'),
(43, 'Simontornyai Városi Könyvtár', '7081', 'Petőfi Sándor u. 67.'),
(44, 'Solymár Imre Városi Könyvtár', '7150', 'Perczel Mór u. 50.'),
(45, 'Szekszárdi Törvényszék Jogi Szakkönyvtára', '7100', 'Dózsa György u. 2.'),
(46, 'Szent Lázár Megyei Kórház Orvosi Könyvtára', '3100', 'Füleki út 54-56.'),
(47, 'Teleki László Városi Könyvtár és Művelődési Központ', '3060', 'Nagymező u. 3.'),
(48, 'Tolna Megyei Illyés Gyula Könyvtár', '7100', 'Széchenyi u. 51.'),
(49, 'Tolna Megyei Illyés Gyula Könyvtár Fiókkönyvtára', '7100', 'Dienes Valéria u. 14.'),
(50, 'Városi Művelődési Központ és Könyvtár', '2651', 'Rákóczi út 26.'),
(51, 'Wosinsky Mór Megyei Múzeum Könyvtár', '7100', 'Szent István tér 26.');
