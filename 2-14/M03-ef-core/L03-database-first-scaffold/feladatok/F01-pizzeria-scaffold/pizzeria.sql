-- =============================================================
-- Pizzéria adatbázis – bemutató példa (14-M03-L02)
-- Kapcsolatok:
--   vevo  1 ── N  rendeles      (egy vevő többször rendelhet)
--   futar 1 ── N  rendeles      (egy futár sok rendelést visz ki)
--   rendeles N ── M pizza       (kapcsolótábla: tetel, plusz adat: db)
-- Importálás: phpMyAdmin → Importálás, vagy MySQL Workbench → File → Run SQL Script.
-- Újrafuttatható: a táblákat törli és újra létrehozza.
-- =============================================================

CREATE DATABASE IF NOT EXISTS estis_pizzeria
  CHARACTER SET utf8mb4 COLLATE utf8mb4_hungarian_ci;
USE estis_pizzeria;

-- Törlés fordított sorrendben: előbb a hivatkozó (gyerek) táblák!
DROP TABLE IF EXISTS tetel;
DROP TABLE IF EXISTS rendeles;
DROP TABLE IF EXISTS pizza;
DROP TABLE IF EXISTS futar;
DROP TABLE IF EXISTS vevo;

-- ---------- "1" oldali (szülő) táblák ----------

CREATE TABLE vevo (
  id  INT NOT NULL AUTO_INCREMENT,      -- mesterséges elsődleges kulcs
  nev VARCHAR(40) NOT NULL,
  cim VARCHAR(60) NOT NULL,
  PRIMARY KEY (id)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_hungarian_ci;

CREATE TABLE futar (
  id      INT NOT NULL AUTO_INCREMENT,
  nev     VARCHAR(40) NOT NULL,
  telefon VARCHAR(15) NOT NULL,
  PRIMARY KEY (id),
  UNIQUE KEY uq_futar_telefon (telefon)  -- alternatív kulcs: nem lehet két egyforma szám
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_hungarian_ci;

CREATE TABLE pizza (
  id  INT NOT NULL AUTO_INCREMENT,
  nev VARCHAR(30) NOT NULL,
  ar  INT NOT NULL,
  PRIMARY KEY (id)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_hungarian_ci;

-- ---------- "N" oldali (gyerek) tábla ----------

CREATE TABLE rendeles (
  id       INT NOT NULL AUTO_INCREMENT,
  vevo_id  INT NOT NULL,                 -- idegen kulcs → vevo.id
  futar_id INT NOT NULL,                 -- idegen kulcs → futar.id
  idopont  DATETIME NOT NULL,
  PRIMARY KEY (id),
  CONSTRAINT fk_rendeles_vevo  FOREIGN KEY (vevo_id)  REFERENCES vevo (id),
  CONSTRAINT fk_rendeles_futar FOREIGN KEY (futar_id) REFERENCES futar (id)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_hungarian_ci;

-- ---------- N:M kapcsolótábla ----------
-- Összetett elsődleges kulcs: egy rendelésben egy pizzafajta csak egyszer szerepel,
-- a darabszámot a db oszlop tárolja. Mindkét kulcsrész egyben idegen kulcs is.
CREATE TABLE tetel (
  rendeles_id INT NOT NULL,
  pizza_id    INT NOT NULL,
  db          INT NOT NULL DEFAULT 1,
  PRIMARY KEY (rendeles_id, pizza_id),
  CONSTRAINT fk_tetel_rendeles FOREIGN KEY (rendeles_id) REFERENCES rendeles (id)
    ON DELETE CASCADE,                   -- rendelés törlésekor a tételei is törlődnek
  CONSTRAINT fk_tetel_pizza FOREIGN KEY (pizza_id) REFERENCES pizza (id)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_hungarian_ci;

-- ---------- Adatok (kitalált szereplők) ----------
-- Feltöltés a kapcsolatok irányában: előbb a szülők, aztán a gyerekek.

INSERT INTO vevo (nev, cim) VALUES
('Éhes Elemér',     'Szeged, Kossuth u. 3.'),
('Falatka Fanni',   'Szeged, Tisza krt. 41.'),
('Nasi Nándor',     'Szeged, Petőfi tér 7.'),
('Sajtos Sára',     'Szeged, Dugonics tér 2.'),
('Pepperoni Péter', 'Szeged, Mars tér 15.'),
('Morzsa Márta',    'Szeged, Rózsa u. 9.');

INSERT INTO futar (nev, telefon) VALUES
('Gyors Gergő',     '+36301112233'),
('Robogó Rita',     '+36302223344'),
('Turbó Tamás',     '+36303334455'),
('Szélvész Szilvi', '+36304445566');

INSERT INTO pizza (nev, ar) VALUES
('Margherita', 2490),
('Sonkás',     2790),
('Hawaii',     2890),
('Négysajtos', 3190),
('Szalámis',   2890),
('Gombás',     2690);

INSERT INTO rendeles (vevo_id, futar_id, idopont) VALUES
(1, 1, '2026-10-01 11:45'),
(2, 2, '2026-10-01 18:20'),
(3, 1, '2026-10-02 12:10'),
(1, 3, '2026-10-02 19:05'),
(4, 2, '2026-10-03 13:30'),
(5, 4, '2026-10-03 20:15'),
(2, 1, '2026-10-04 10:50'),
(6, 3, '2026-10-04 18:40'),
(3, 2, '2026-10-04 21:00'),
(1, 1, '2026-10-05 12:25'),
(4, 4, '2026-10-05 19:30'),
(5, 2, '2026-10-06 11:15'),
(2, 3, '2026-10-06 17:55'),
(1, 2, '2026-10-06 20:30'),
(3, 4, '2026-10-07 12:00'),
(4, 1, '2026-10-07 18:10'),
(5, 3, '2026-10-07 19:45'),
(1, 2, '2026-10-07 21:20');

INSERT INTO tetel (rendeles_id, pizza_id, db) VALUES
(1, 1, 2),
(2, 3, 1), (2, 4, 1),
(3, 2, 1),
(4, 5, 2), (4, 1, 1),
(5, 3, 3),
(6, 4, 1),
(7, 2, 2), (7, 3, 1),
(8, 1, 1),
(9, 5, 1), (9, 4, 2),
(10, 3, 1),
(11, 1, 2), (11, 2, 2),
(12, 5, 1),
(13, 4, 1), (13, 3, 2),
(14, 2, 1),
(15, 1, 1), (15, 5, 1),
(16, 3, 2),
(17, 4, 3),
(18, 1, 1), (18, 2, 1), (18, 3, 1);
