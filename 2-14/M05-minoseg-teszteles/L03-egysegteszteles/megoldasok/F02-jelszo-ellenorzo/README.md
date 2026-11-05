# Megoldás – 14-M05-L03-F02 · Jelszóerősség-ellenőrző

- `JelszoEllenorzoTesztek/` – MSTest-projekt (ProjectReference a `JelszoEllenorzo`-ra).
- `JelszoEllenorzo/` – a **javított** osztálykönyvtár (a javított sorokat komment jelöli).

## A tesztekkel megtalált hibák

| # | Elbukó teszt | Ok | Javítás |
|---|---|---|---|
| 1 | `Eros_MindenSzabalyTeljesul_Igaz (Alma1234)` | `Length <= 8`: a pontosan 8 karakteres jelszót rövidnek tartja | `Length < MinimalisHossz` |
| 2 | `Hibak_NincsSzamjegy_CsakEzAHiba` (az „Almafaliget” jelszóra üres listát ad) | `char.IsLetterOrDigit` betűre is igaz, így a szabály soha nem jelez, ha van betű a jelszóban | `char.IsDigit` |
| 3 | `Hibak_Null_ArgumentNullException` | nincs `null`-ellenőrzés, `NullReferenceException` keletkezik | `ArgumentNullException.ThrowIfNull(jelszo)` |

Az 1. hiba miatt a többi 8 karakteres jelszót használó teszt is piros (`Hibak_ErosJelszo_UresLista`, `Hibak_NincsNagybetu_CsakEzAHiba`) – egy hiba több tesztet is elbuktathat, ezért mindig az üzenetet olvasd el.

Figyeld meg: az üres szöveges teszt a hibás kóddal is zöld (üres szövegben betű sincs), ezért kell a „csak egy szabályt sért” típusú teszt is.
