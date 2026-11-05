-- Megoldás – 14-M03-L02-F01 · Zenetár normalizálása
--
-- 1. Redundancia: az előadó neve és országa, valamint a műfaj minden albumnál ismétlődik.
--    Módosítási anomália: ha az "Omega" nevét javítjuk, több sort kell átírni.
--    Törlési anomália: a "Kind of Blue" törlésével elveszne, hogy Miles Davis amerikai.
--    Bővítési anomália: új előadót nem lehet felvenni, amíg nincs albuma.
-- 2. A "dalok" mező több értéket tartalmaz → sérti az 1NF-et.
--    Az "eloado_orszag" az előadótól függ, nem az albumtól (tranzitív függés) → sérti a 3NF-et.
-- 3. Kapcsolatok: eloado 1:N album, mufaj 1:N album, album 1:N dal.

CREATE DATABASE IF NOT EXISTS estis_zenetar
  CHARACTER SET utf8mb4 COLLATE utf8mb4_hungarian_ci;
USE estis_zenetar;

DROP TABLE IF EXISTS dal;
DROP TABLE IF EXISTS album;
DROP TABLE IF EXISTS eloado;
DROP TABLE IF EXISTS mufaj;

CREATE TABLE mufaj (
  id  INT NOT NULL AUTO_INCREMENT,
  nev VARCHAR(30) NOT NULL,
  PRIMARY KEY (id),
  UNIQUE KEY uq_mufaj_nev (nev)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_hungarian_ci;

CREATE TABLE eloado (
  id     INT NOT NULL AUTO_INCREMENT,
  nev    VARCHAR(60) NOT NULL,
  orszag VARCHAR(40) NOT NULL,
  PRIMARY KEY (id)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_hungarian_ci;

CREATE TABLE album (
  id        INT NOT NULL AUTO_INCREMENT,
  cim       VARCHAR(80) NOT NULL,
  ev        INT NOT NULL,
  eloado_id INT NOT NULL,
  mufaj_id  INT NOT NULL,
  PRIMARY KEY (id),
  CONSTRAINT fk_album_eloado FOREIGN KEY (eloado_id) REFERENCES eloado (id),
  CONSTRAINT fk_album_mufaj  FOREIGN KEY (mufaj_id)  REFERENCES mufaj (id)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_hungarian_ci;

CREATE TABLE dal (
  id       INT NOT NULL AUTO_INCREMENT,
  cim      VARCHAR(80) NOT NULL,
  album_id INT NOT NULL,
  PRIMARY KEY (id),
  CONSTRAINT fk_dal_album FOREIGN KEY (album_id) REFERENCES album (id) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_hungarian_ci;

INSERT INTO mufaj (nev) VALUES ('rock'), ('jazz');

INSERT INTO eloado (nev, orszag) VALUES
('Omega', 'Magyarország'),
('Queen',        'Egyesült Királyság'),
('Miles Davis',  'USA');

INSERT INTO album (cim, ev, eloado_id, mufaj_id) VALUES
('10000 lépés',        1969, 1, 1),
('Időrabló',           1977, 1, 1),
('Hungarian Rhapsody', 2012, 2, 1),
('Jazz',               1978, 2, 1),
('Kind of Blue',       1959, 3, 2);

INSERT INTO dal (cim, album_id) VALUES
('Gyöngyhajú lány', 1), ('10000 lépés', 1),
('Időrabló', 2),
('Bohemian Rhapsody', 3), ('Somebody to Love', 3),
('Mustapha', 4), ('Bicycle Race', 4),
('So What', 5), ('Blue in Green', 5);

-- 6. Lekérdezések
SELECT a.cim AS album_cim, a.ev, e.nev AS eloado, m.nev AS mufaj
FROM album a
JOIN eloado e ON a.eloado_id = e.id
JOIN mufaj  m ON a.mufaj_id  = m.id
ORDER BY a.ev;

SELECT a.cim, COUNT(d.id) AS dalok_szama
FROM album a
LEFT JOIN dal d ON d.album_id = a.id
GROUP BY a.id, a.cim;
