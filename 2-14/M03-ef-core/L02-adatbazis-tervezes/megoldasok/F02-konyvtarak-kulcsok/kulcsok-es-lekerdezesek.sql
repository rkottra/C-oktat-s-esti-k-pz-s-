-- Megoldás – 14-M03-L02-F02 · Könyvtárak: kulcsok és kapcsolatok utólag
-- Előbb importáld a feladat konyvtarak-kulcs-nelkul.sql szkriptjét!
USE estis_konyvtarak;

-- 2. Kapcsolatok: megye 1:N telepules, telepules 1:N konyvtar

-- 3. Elsődleges kulcsok
ALTER TABLE megye     ADD PRIMARY KEY (id);
ALTER TABLE telepules ADD PRIMARY KEY (irsz);
ALTER TABLE konyvtar  ADD PRIMARY KEY (id);

-- AUTO_INCREMENT csak kulcs oszlopon állítható be, ezért a PK után jön
ALTER TABLE megye    MODIFY id INT NOT NULL AUTO_INCREMENT;
ALTER TABLE konyvtar MODIFY id INT NOT NULL AUTO_INCREMENT;

-- 4. Idegen kulcsok
ALTER TABLE telepules
  ADD CONSTRAINT fk_telepules_megye FOREIGN KEY (megye_id) REFERENCES megye (id);

-- Ez még HIBÁT adna (Cannot add or update a child row), mert van "árva" könyvtár:
-- ALTER TABLE konyvtar ADD CONSTRAINT fk_konyvtar_telepules FOREIGN KEY (irsz) REFERENCES telepules (irsz);

-- A hibás sor megkeresése: a könyvtár irányítószáma nincs a telepules táblában
SELECT k.id, k.nev, k.irsz
FROM konyvtar k
LEFT JOIN telepules t ON k.irsz = t.irsz
WHERE t.irsz IS NULL;
-- → 52, Községi Könyvtár és Közösségi Tér, 7099

UPDATE konyvtar SET irsz = '7100' WHERE id = 52;

ALTER TABLE konyvtar
  ADD CONSTRAINT fk_konyvtar_telepules FOREIGN KEY (irsz) REFERENCES telepules (irsz);

-- 6. Lekérdezések
-- a) könyvtárak települése
SELECT t.nev AS telepules, k.nev AS konyvtar
FROM konyvtar k
JOIN telepules t ON k.irsz = t.irsz
ORDER BY t.nev, k.nev;

-- b) vármegyénként a könyvtárak száma (két JOIN: konyvtar → telepules → megye)
SELECT m.nev AS megye, COUNT(*) AS db
FROM konyvtar k
JOIN telepules t ON k.irsz = t.irsz
JOIN megye m     ON t.megye_id = m.id
GROUP BY m.id, m.nev;

-- c) az 5 legtöbb könyvtárral rendelkező település
SELECT t.nev, COUNT(*) AS db
FROM konyvtar k
JOIN telepules t ON k.irsz = t.irsz
GROUP BY t.irsz, t.nev
ORDER BY db DESC
LIMIT 5;
