-- Megoldás – 14-M03-L02-F03 · Tanfolyami jelentkezések (N:M)
--
-- Egyedek: oktato, tanfolyam, resztvevo
-- Kapcsolatok: oktato 1:N tanfolyam
--              resztvevo N:M tanfolyam → kapcsolótábla: jelentkezes (saját adatai: datum, fizetve)

CREATE DATABASE IF NOT EXISTS estis_tanfolyam
  CHARACTER SET utf8mb4 COLLATE utf8mb4_hungarian_ci;
USE estis_tanfolyam;

DROP TABLE IF EXISTS jelentkezes;
DROP TABLE IF EXISTS tanfolyam;
DROP TABLE IF EXISTS resztvevo;
DROP TABLE IF EXISTS oktato;

CREATE TABLE oktato (
  id          INT NOT NULL AUTO_INCREMENT,
  nev         VARCHAR(50) NOT NULL,
  szakterulet VARCHAR(50) NOT NULL,
  PRIMARY KEY (id)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_hungarian_ci;

CREATE TABLE tanfolyam (
  id          INT NOT NULL AUTO_INCREMENT,
  nev         VARCHAR(60) NOT NULL,
  kezdes      DATE NOT NULL,
  ar          INT NOT NULL,
  max_letszam INT NOT NULL,
  oktato_id   INT NOT NULL,
  PRIMARY KEY (id),
  CONSTRAINT fk_tanfolyam_oktato FOREIGN KEY (oktato_id) REFERENCES oktato (id)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_hungarian_ci;

CREATE TABLE resztvevo (
  id    INT NOT NULL AUTO_INCREMENT,
  nev   VARCHAR(50) NOT NULL,
  email VARCHAR(80) NOT NULL,
  PRIMARY KEY (id),
  UNIQUE KEY uq_resztvevo_email (email)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_hungarian_ci;

-- Kapcsolótábla: az összetett PK miatt ugyanaz a pár csak egyszer szerepelhet
CREATE TABLE jelentkezes (
  resztvevo_id INT NOT NULL,
  tanfolyam_id INT NOT NULL,
  datum        DATE NOT NULL,
  fizetve      BOOL NOT NULL DEFAULT 0,
  PRIMARY KEY (resztvevo_id, tanfolyam_id),
  CONSTRAINT fk_jelentkezes_resztvevo FOREIGN KEY (resztvevo_id) REFERENCES resztvevo (id) ON DELETE CASCADE,
  CONSTRAINT fk_jelentkezes_tanfolyam FOREIGN KEY (tanfolyam_id) REFERENCES tanfolyam (id)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_hungarian_ci;

-- Adatok (kitalált személyek)
INSERT INTO oktato (nev, szakterulet) VALUES
('Kódoló Kornél',  'programozás'),
('Lekérdező Lili', 'adatbázisok'),
('Táblás Tivadar', 'irodai szoftverek');

INSERT INTO tanfolyam (nev, kezdes, ar, max_letszam, oktato_id) VALUES
('C# alapok',       '2026-09-07', 60000, 4, 1),
('SQL kezdőknek',   '2026-09-14', 45000, 5, 2),
('Excel haladó',    '2026-10-05', 30000, 6, 3),
('WPF felületek',   '2026-11-02', 55000, 4, 1),
('Python bevezető', '2026-11-09', 50000, 5, 2);

INSERT INTO resztvevo (nev, email) VALUES
('Arany Ábel',      'abel.arany@example.hu'),
('Bodor Bella',     'bella.bodor@example.hu'),
('Csiszár Csongor', 'csongor.csiszar@example.hu'),
('Deák Dóra',       'dora.deak@example.hu'),
('Erdős Egon',      'egon.erdos@example.hu'),
('Fodor Flóra',     'flora.fodor@example.hu'),
('Gál Gusztáv',     'gusztav.gal@example.hu'),
('Halmi Hédi',      'hedi.halmi@example.hu');

INSERT INTO jelentkezes (resztvevo_id, tanfolyam_id, datum, fizetve) VALUES
(1, 1, '2026-08-20', 1), (2, 1, '2026-08-22', 1), (3, 1, '2026-08-25', 0), (4, 1, '2026-08-30', 1),
(1, 2, '2026-08-21', 1), (5, 2, '2026-09-01', 0), (6, 2, '2026-09-02', 1),
(2, 3, '2026-09-10', 1), (7, 3, '2026-09-15', 0),
(1, 4, '2026-10-01', 0), (3, 4, '2026-10-03', 1), (8, 4, '2026-10-04', 0);

-- 4. Ezek SZÁNDÉKOSAN hibát adnak (vedd ki a megjegyzésjelet, és próbáld ki):
-- INSERT INTO jelentkezes VALUES (1, 1, '2026-09-01', 0);    -- Duplicate entry '1-1' for key 'PRIMARY'
-- INSERT INTO jelentkezes VALUES (1, 99, '2026-09-01', 0);   -- a foreign key constraint fails
-- INSERT INTO resztvevo (nev, email) VALUES ('Másik Ábel', 'abel.arany@example.hu');  -- Duplicate entry (UNIQUE)

-- 5. Lekérdezések
-- a) jelentkezők száma tanfolyamonként (a 0 is megjelenik)
SELECT t.nev, COUNT(j.resztvevo_id) AS jelentkezok, t.max_letszam
FROM tanfolyam t
LEFT JOIN jelentkezes j ON j.tanfolyam_id = t.id
GROUP BY t.id, t.nev, t.max_letszam
ORDER BY t.kezdes;

-- b) betelt tanfolyamok
SELECT t.nev
FROM tanfolyam t
JOIN jelentkezes j ON j.tanfolyam_id = t.id
GROUP BY t.id, t.nev, t.max_letszam
HAVING COUNT(*) >= t.max_letszam;

-- c) legalább két tanfolyamra jelentkezett résztvevők
SELECT r.nev, COUNT(*) AS tanfolyamok
FROM resztvevo r
JOIN jelentkezes j ON j.resztvevo_id = r.id
GROUP BY r.id, r.nev
HAVING COUNT(*) >= 2
ORDER BY tanfolyamok DESC;

-- d) ki nem fizetett díjak összege (az ár a tanfolyam táblából jön!)
SELECT SUM(t.ar) AS tartozas
FROM jelentkezes j
JOIN tanfolyam t ON j.tanfolyam_id = t.id
WHERE j.fizetve = 0;

-- e) oktatónként a jelentkezők száma (három tábla)
SELECT o.nev, COUNT(j.resztvevo_id) AS jelentkezok
FROM oktato o
JOIN tanfolyam t        ON t.oktato_id = o.id
LEFT JOIN jelentkezes j ON j.tanfolyam_id = t.id
GROUP BY o.id, o.nev;
