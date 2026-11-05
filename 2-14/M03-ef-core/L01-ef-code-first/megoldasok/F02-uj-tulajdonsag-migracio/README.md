# Megoldás – 14-M03-L01-F02 · Új tulajdonság és migráció

A bemutató példa bővített változata (ugyanazt az `estis_konyvtar` adatbázist használja).

```
Add-Migration KolcsonozhetoMezo
Update-Database
```

**Ellenőrző kérdések:**

- Migráció nélkül a program hibával áll le (`Unknown column 'k.Kolcsonozheto'`), mert a modell és az adatbázis eltér.
- Az `Up` metódus hozzáadja az oszlopot (`AddColumn<bool>`), a `Down` visszavonja (`DropColumn`) – így a változás visszaállítható (`Update-Database <előző migráció neve>`).
