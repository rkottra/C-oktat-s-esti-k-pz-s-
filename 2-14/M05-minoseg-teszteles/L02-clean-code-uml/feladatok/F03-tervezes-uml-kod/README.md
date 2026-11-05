# 14-M05-L02-F03 · Edzőterem – tervezés UML-lel, majd megvalósítás

**Nehézség:** ★★★ **Becsült idő:** 75 perc

## Feladat

Egy edzőterem beléptetőrendszerének egyszerűsített modelljét kell elkészítened. **Először tervezz, utána kódolj!**

### A leírás

- Az edzőteremnek **tagjai** vannak. Minden tagnak van neve és egyedi tagszáma, és legfeljebb **egy** érvényes bérlete lehet (lehet, hogy nincs neki).
- Kétféle **bérlet** van, mindkettőnek van ára:
  - **alkalmas bérlet**: 10 belépésre jogosít; addig érvényes, amíg van maradék alkalom. Minden belépés egy alkalmat levon.
  - **időszakos bérlet**: kezdő és záró napja van; azokon a napokon (a határnapokat is beleértve) érvényes. A belépés nem változtat rajta.
- A rendszer minden sikeres **belépést** rögzít: ki lépett be (tagszám) és melyik napon.
- Az edzőterem tudja:
  - új tagot felvenni;
  - beléptetni egy tagot tagszám és nap alapján – a belépés sikertelen, ha nincs ilyen tag, nincs bérlete, vagy a bérlete aznap nem érvényes (ilyenkor nem is rögzít semmit);
  - megmondani egy adott nap belépéseinek számát.

### Lépések

1. Gyűjtsd ki a leírásból a főneveket (osztályjelöltek, tulajdonságok) és az igéket (metódusok).
2. Rajzold meg az osztálydiagramot **Mermaid**-kóddal (` ```mermaid ` blokk egy `README.md`-ben – a GitHub ki is rajzolja). Szerepeljen benne:
   absztrakt osztály és öröklődés, láthatóságok, legalább egy multiplicitással ellátott kapcsolat.
3. Valósítsd meg a diagramot egy `Edzoterem` nevű konzolos projektben.
4. A `Program.cs` mutassa be: egy alkalmas bérletes tag 11. belépése már sikertelen; egy időszakos bérletes tag a záró napon még, utána már nem léphet be;
   bérlet nélküli és nem létező tag nem léphet be; végül írja ki egy nap belépéseinek számát.

## Minta kimenet

```
Kovács Anna – 10 alkalom: 10 sikeres belépés, a 11. sikertelen
Tóth Bence – 2026.11.30.: beléphet
Tóth Bence – 2026.12.01.: nem léphet be
Bérlet nélküli tag: nem léphet be
Nem létező tagszám: nem léphet be
Belépések 2026.11.30-án: 2
```

## Tippek

- A bérlet közös részét (ár, `Ervenyes(nap)`, `Belepes(nap)`) tedd absztrakt ősosztályba; a két bérlettípus ebből származik.
- A „legfeljebb egy bérlet” a kódban `Berlet?` típusú tulajdonság, a diagramon `"0..1"` multiplicitás.
- Napokhoz használd a `DateOnly` típust: `new DateOnly(2026, 11, 30)`.
- A tervezés nem egyszeri: ha kódolás közben kiderül, hogy kell még egy metódus, rajzold be a diagramba is.

Megoldás: [../../megoldasok/F03-tervezes-uml-kod/](../../megoldasok/F03-tervezes-uml-kod/)
