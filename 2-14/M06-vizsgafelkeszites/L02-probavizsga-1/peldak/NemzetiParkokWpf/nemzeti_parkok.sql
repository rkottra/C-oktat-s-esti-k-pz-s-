-- Magyarország nemzeti parkjai – bemutató adatbázis (14-M06-L02)
-- Importálás: phpMyAdmin → Importálás → fájl kiválasztása → Indítás

DROP DATABASE IF EXISTS estis_nemzetiparkok;
CREATE DATABASE estis_nemzetiparkok CHARACTER SET utf8mb4 COLLATE utf8mb4_hungarian_ci;
USE estis_nemzetiparkok;

CREATE TABLE nemzeti_park (
  id INT NOT NULL AUTO_INCREMENT,
  nev VARCHAR(60) NOT NULL,
  szekhely VARCHAR(40) NOT NULL,
  alapitas_eve INT NOT NULL,
  PRIMARY KEY (id)
);

INSERT INTO nemzeti_park (nev, szekhely, alapitas_eve) VALUES
('Hortobágyi Nemzeti Park', 'Debrecen', 1973),
('Kiskunsági Nemzeti Park', 'Kecskemét', 1975),
('Bükki Nemzeti Park', 'Eger', 1977),
('Aggteleki Nemzeti Park', 'Jósvafő', 1985),
('Fertő-Hanság Nemzeti Park', 'Sarród', 1991),
('Duna-Dráva Nemzeti Park', 'Pécs', 1996),
('Körös-Maros Nemzeti Park', 'Szarvas', 1997),
('Balaton-felvidéki Nemzeti Park', 'Csopak', 1997),
('Duna-Ipoly Nemzeti Park', 'Budapest', 1997),
('Őrségi Nemzeti Park', 'Őriszentpéter', 2002);
