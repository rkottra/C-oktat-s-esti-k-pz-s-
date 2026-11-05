-- A periódusos rendszer első 36 eleme – 14-M06-L04 vizsgaszimuláció
-- Atomtömeg: IUPAC standard (kerekítve), elektronegativitás: Pauling-skála (nemesgázoknál NULL, a kripton kivételével).

DROP DATABASE IF EXISTS estis_elemek;
CREATE DATABASE estis_elemek CHARACTER SET utf8mb4 COLLATE utf8mb4_hungarian_ci;
USE estis_elemek;

CREATE TABLE kategoria (
  id INT NOT NULL AUTO_INCREMENT,
  nev VARCHAR(30) NOT NULL,
  PRIMARY KEY (id)
);

CREATE TABLE elem (
  rendszam INT NOT NULL,
  vegyjel VARCHAR(2) NOT NULL,
  nev VARCHAR(30) NOT NULL,
  kategoriaid INT NOT NULL,
  periodus INT NOT NULL,
  csoport INT NOT NULL,
  atomtomeg DOUBLE NOT NULL,
  elektronegativitas DOUBLE NULL,
  halmazallapot VARCHAR(10) NOT NULL,
  PRIMARY KEY (rendszam),
  UNIQUE (vegyjel),
  CONSTRAINT fk_elem_kategoria FOREIGN KEY (kategoriaid) REFERENCES kategoria (id)
);

INSERT INTO kategoria (id, nev) VALUES
(1, 'alkálifém'),
(2, 'alkáliföldfém'),
(3, 'átmenetifém'),
(4, 'egyéb fém'),
(5, 'félfém'),
(6, 'nemfém'),
(7, 'halogén'),
(8, 'nemesgáz');

INSERT INTO elem (rendszam, vegyjel, nev, kategoriaid, periodus, csoport, atomtomeg, elektronegativitas, halmazallapot) VALUES
(1, 'H', 'hidrogén', 6, 1, 1, 1.008, 2.20, 'gáz'),
(2, 'He', 'hélium', 8, 1, 18, 4.003, NULL, 'gáz'),
(3, 'Li', 'lítium', 1, 2, 1, 6.94, 0.98, 'szilárd'),
(4, 'Be', 'berillium', 2, 2, 2, 9.012, 1.57, 'szilárd'),
(5, 'B', 'bór', 5, 2, 13, 10.81, 2.04, 'szilárd'),
(6, 'C', 'szén', 6, 2, 14, 12.011, 2.55, 'szilárd'),
(7, 'N', 'nitrogén', 6, 2, 15, 14.007, 3.04, 'gáz'),
(8, 'O', 'oxigén', 6, 2, 16, 15.999, 3.44, 'gáz'),
(9, 'F', 'fluor', 7, 2, 17, 18.998, 3.98, 'gáz'),
(10, 'Ne', 'neon', 8, 2, 18, 20.18, NULL, 'gáz'),
(11, 'Na', 'nátrium', 1, 3, 1, 22.99, 0.93, 'szilárd'),
(12, 'Mg', 'magnézium', 2, 3, 2, 24.305, 1.31, 'szilárd'),
(13, 'Al', 'alumínium', 4, 3, 13, 26.982, 1.61, 'szilárd'),
(14, 'Si', 'szilícium', 5, 3, 14, 28.085, 1.90, 'szilárd'),
(15, 'P', 'foszfor', 6, 3, 15, 30.974, 2.19, 'szilárd'),
(16, 'S', 'kén', 6, 3, 16, 32.06, 2.58, 'szilárd'),
(17, 'Cl', 'klór', 7, 3, 17, 35.45, 3.16, 'gáz'),
(18, 'Ar', 'argon', 8, 3, 18, 39.948, NULL, 'gáz'),
(19, 'K', 'kálium', 1, 4, 1, 39.098, 0.82, 'szilárd'),
(20, 'Ca', 'kalcium', 2, 4, 2, 40.078, 1.00, 'szilárd'),
(21, 'Sc', 'szkandium', 3, 4, 3, 44.956, 1.36, 'szilárd'),
(22, 'Ti', 'titán', 3, 4, 4, 47.867, 1.54, 'szilárd'),
(23, 'V', 'vanádium', 3, 4, 5, 50.942, 1.63, 'szilárd'),
(24, 'Cr', 'króm', 3, 4, 6, 51.996, 1.66, 'szilárd'),
(25, 'Mn', 'mangán', 3, 4, 7, 54.938, 1.55, 'szilárd'),
(26, 'Fe', 'vas', 3, 4, 8, 55.845, 1.83, 'szilárd'),
(27, 'Co', 'kobalt', 3, 4, 9, 58.933, 1.88, 'szilárd'),
(28, 'Ni', 'nikkel', 3, 4, 10, 58.693, 1.91, 'szilárd'),
(29, 'Cu', 'réz', 3, 4, 11, 63.546, 1.90, 'szilárd'),
(30, 'Zn', 'cink', 3, 4, 12, 65.38, 1.65, 'szilárd'),
(31, 'Ga', 'gallium', 4, 4, 13, 69.723, 1.81, 'szilárd'),
(32, 'Ge', 'germánium', 5, 4, 14, 72.63, 2.01, 'szilárd'),
(33, 'As', 'arzén', 5, 4, 15, 74.922, 2.18, 'szilárd'),
(34, 'Se', 'szelén', 6, 4, 16, 78.971, 2.55, 'szilárd'),
(35, 'Br', 'bróm', 7, 4, 17, 79.904, 2.96, 'folyékony'),
(36, 'Kr', 'kripton', 8, 4, 18, 83.798, 3.00, 'gáz');
