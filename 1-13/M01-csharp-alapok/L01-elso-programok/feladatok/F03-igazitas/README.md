# 13-M01-L01-F03 · Jobbra és középre igazítás

**Nehézség:** ★★★ **Becsült idő:** 40 perc

## Feladat

Szövegszerkesztőben megszokott, hogy egy sort jobbra vagy középre igazíthatunk. Készíts konzolos programot `Igazitas` néven, amely ezt a konzolon valósítja meg.

1. A program először kérdezze meg, milyen igazítást kér a felhasználó: `J` (jobbra) vagy `K` (középre). A választást **egy billentyűleütéssel** lehessen megadni (Enter nélkül).
2. Ezután kérjen be szavakat (vagy rövid mondatokat) soronként, egészen addig, amíg a felhasználó üres sort nem ad.
3. Minden bekért sort írjon ki újra a választott igazítással:
   - **jobbra**: a sor utolsó karaktere mindig a 40. oszlopban legyen;
   - **középre**: a sor közepe a 40. oszlopra essen.
4. A kiírt (igazított) sorok legyenek cián színűek, a bekérés fehér.

Készítsd el kétféleképpen: először szóközök kiírásával (ciklussal), aztán a `Console.SetCursorPosition` használatával. Melyik a rövidebb?

## Minta kimenet (jobbra igazítás)

```
Igazítás (J = jobbra, K = középre): J
alma
                                    alma
körte
                                   körte
Visual Studio
                           Visual Studio

```

## Tippek

- Billentyűleütés beolvasása: `char valasz = Console.ReadKey().KeyChar;` – utána egy `Console.WriteLine()` kell, hogy új sorba kerüljünk.
- A kis- és nagybetűt is fogadd el: `char.ToUpper(valasz)`.
- Egy szöveg hossza: `sor.Length`. Jobbra igazításnál a szükséges szóközök száma `40 - sor.Length`.
- A kurzor aktuális sora: `Console.CursorTop`. Így a sor elejére ugrás nélkül pozícionálhatsz: `Console.SetCursorPosition(40 - sor.Length, Console.CursorTop);`
- Ha a sor 40 karakternél hosszabb, a számolt oszlop negatív lenne – ilyenkor a 0. oszlopból indulj (`Math.Max(0, …)`).
- „Amíg nem üres sort ad” → a Pythonból ismert `while` ciklus; C#-ban: `while (sor != "") { … }`.

Megoldás: [../../megoldasok/F03-igazitas/](../../megoldasok/F03-igazitas/)
