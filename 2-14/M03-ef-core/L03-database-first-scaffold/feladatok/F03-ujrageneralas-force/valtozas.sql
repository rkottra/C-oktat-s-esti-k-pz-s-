-- 14-M03-L03-F03 – az adatbázis szerkezetének módosítása (a "DBA" változtat, a programnak követnie kell)
-- Előfeltétel: az estis_oregfak adatbázis már létezik (peldak/OregfakScaffold/oregfak.sql).
USE estis_oregfak;

-- Új, kötelező logikai oszlop alapértékkel
ALTER TABLE fa ADD COLUMN vedett BOOL NOT NULL DEFAULT 0;

-- Új, nem kötelező szöveges oszlop
ALTER TABLE fa ADD COLUMN megjegyzes VARCHAR(200) NULL;

-- A 7 méternél vastagabb törzsű fák védettek
UPDATE fa SET vedett = 1 WHERE kormeret >= 700;
