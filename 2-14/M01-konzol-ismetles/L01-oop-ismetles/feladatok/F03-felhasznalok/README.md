# 14-M01-L01-F03 · Felhasználók és jogosultságok

**Nehézség:** ★★★ **Becsült idő:** 60 perc

## Feladat

Egy egyszerű beléptetőrendszer osztályait kell elkészítened (`Felhasznalok` projekt). A hangsúly az **adatrejtésen** van: a jelszót kívülről senki ne lássa, és csak ellenőrzött módon lehessen megváltoztatni.

1. **`Felhasznalo`** osztály:
   - `Felhasznalonev`: kívülről olvasható, de csak az osztályon belül írható (`private set`); kisbetűsre alakítva tárolja, legalább 3 karakter;
   - a jelszó **privát mezőben** van, nincs hozzá publikus tulajdonság;
   - konstruktor `(felhasznalonev, jelszo = "Kezdo123")` – opcionális paraméterrel;
   - jelszószabály: legalább 8 karakter és legalább egy számjegy – különben `ArgumentException`;
   - `virtual bool Ellenoriz(string jelszo)` – egyezik-e a megadott jelszó;
   - `bool JelszoCsere(string regi, string uj)` – csak helyes régi jelszóval cserél;
   - virtuális `Jogosultsag` tulajdonság (`"olvasó"`);
   - `ToString()`: felhasználónév és jogosultság – **a jelszó nem szerepelhet benne!**
2. **`Admin`** (a `Felhasznalo` leszármazottja): kezelt modulok listája (`List<string>`), jogosultsága `"rendszergazda"`, a `ToString()` a modulokat is kiírja.
3. **`Vendeg`** (lezárt, `sealed` osztály): neve mindig `vendeg`, nincs jelszava, üres jelszóval léphet be (írd felül az `Ellenoriz`-t), jogosultsága `"vendég"`.
4. Főprogram:
   - hozz létre néhány felhasználót, köztük egy admint és egy vendéget; próbálj létrehozni hibásakat is (túl rövid név, gyenge jelszó), és írd ki a hibaüzenetet;
   - listázd a felhasználókat;
   - próbálj ki egy sikertelen és egy sikeres jelszócserét;
   - bejelentkezés: kérj be felhasználónevet és jelszót, legfeljebb 3 próbálkozással. Sikeres belépéskor köszöntsd a felhasználót és írd ki a jogosultságát; adminnál a modulokat is.

## Minta kimenet

```
Nem jött létre (xy): A felhasználónév legalább 3 karakter legyen.
Nem jött létre (csaba): A jelszó legalább 8 karakter legyen, és tartalmazzon számjegyet.

Felhasználók:
  anna.k       [olvasó]
  bela         [olvasó]
  admin        [rendszergazda] modulok: felhasználók, mentés, naplók
  vendeg       [vendég]

Jelszócsere rossz régi jelszóval: nem sikerült
Jelszócsere jó régi jelszóval:   sikerült

--- Bejelentkezés ---
Felhasználónév: admin
Jelszó: rossz
Hibás felhasználónév vagy jelszó (1/3).
Felhasználónév: admin
Jelszó: Admin2026!
Üdv, admin! Jogosultság: rendszergazda
Kezelt modulok: felhasználók, mentés, naplók
```

## Tippek

- Az admin konstruktorában `params string[] modulok` paraméterrel tetszőleges számú modult adhatsz át: `new Admin("admin", "Admin2026!", "mentés", "naplók")`.
- Számjegy keresése a jelszóban: `jelszo.Any(char.IsDigit)`.
- Belépéskor nem kell tudnod, hogy vendégről vagy rendes felhasználóról van-e szó: az `Ellenoriz` hívásakor mindig a megfelelő változat fut le.
- Ellenőrizd: ha a `ToString`-et `override` nélkül írod meg, mit ír ki a `Console.WriteLine(f)` egy `Felhasznalo` típusú változóra?

Megoldás: [../../megoldasok/F03-felhasznalok/](../../megoldasok/F03-felhasznalok/)
