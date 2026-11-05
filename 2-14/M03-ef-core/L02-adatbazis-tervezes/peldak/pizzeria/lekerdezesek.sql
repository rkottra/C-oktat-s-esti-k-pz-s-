-- =============================================================
-- Lekérdezések a pizzéria adatbázison (14-M03-L02)
-- Előbb futtasd a pizzeria.sql-t!
-- =============================================================
USE estis_pizzeria;

-- 1. Egy tábla: a pizzák ár szerint növekvő sorrendben
SELECT nev, ar
FROM pizza
ORDER BY ar;

-- 2. Két tábla összekapcsolása (1:N): melyik rendelést melyik futár vitte ki?
--    A JOIN feltétele mindig: idegen kulcs = elsődleges kulcs
SELECT r.id, r.idopont, f.nev AS futar
FROM rendeles r
JOIN futar f ON r.futar_id = f.id
ORDER BY r.idopont;

-- 3. Három tábla: ki, mikor, kinél rendelt?
SELECT r.id, v.nev AS vevo, f.nev AS futar, r.idopont
FROM rendeles r
JOIN vevo  v ON r.vevo_id  = v.id
JOIN futar f ON r.futar_id = f.id;

-- 4. N:M a kapcsolótáblán keresztül: az 1-es rendelés tételei
SELECT p.nev, t.db, p.ar * t.db AS osszeg
FROM tetel t
JOIN pizza p ON t.pizza_id = p.id
WHERE t.rendeles_id = 1;

-- 5. Csoportosítás: mennyit költött az egyes vevők? (legtöbbet költő elöl)
SELECT v.nev, SUM(t.db * p.ar) AS koltes
FROM vevo v
JOIN rendeles r ON r.vevo_id = v.id
JOIN tetel t    ON t.rendeles_id = r.id
JOIN pizza p    ON t.pizza_id = p.id
GROUP BY v.id, v.nev
ORDER BY koltes DESC;

-- 6. Hányszor szállítottak házhoz az egyes futárok?
SELECT f.nev, COUNT(*) AS szallitasok
FROM futar f
JOIN rendeles r ON r.futar_id = f.id
GROUP BY f.id, f.nev
ORDER BY szallitasok DESC;

-- 7. LEFT JOIN: van-e olyan pizza, amelyből még sosem rendeltek?
SELECT p.nev
FROM pizza p
LEFT JOIN tetel t ON t.pizza_id = p.id
WHERE t.pizza_id IS NULL;

-- 8. Hivatkozási épség (integritás) kipróbálása – ezek SZÁNDÉKOSAN hibát adnak:
-- INSERT INTO rendeles (vevo_id, futar_id, idopont) VALUES (99, 1, NOW());
--   → Cannot add or update a child row: a foreign key constraint fails
-- DELETE FROM vevo WHERE id = 1;
--   → Cannot delete or update a parent row: a foreign key constraint fails
