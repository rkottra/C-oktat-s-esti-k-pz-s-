# Megoldás – 14-M05-L03-F03 · Bankszámla

- `BankTesztek/` – MSTest-projekt (ProjectReference a `Bank`-ra), 16 teszteset.
- `Bank/` – a **javított** osztálykönyvtár (a javított sorokat komment jelöli).

## A tesztekkel megtalált hibák

| # | Elbukó teszt(ek) | Ok | Javítás |
|---|---|---|---|
| 1 | `Kivesz_FedezetenBelul_CsokkenAzEgyenleg (1000, 0)`, `Atutal_TeljesEgyenleg_Sikeres` | `osszeg >= Egyenleg` – a teljes egyenleget nem engedi felvenni | `osszeg > Egyenleg` |
| 2 | `Atutal_NincsFedezet_KivetelEsEgyikEgyenlegSemValtozik` | előbb jóváír a célszámlán, csak utána von le; fedezethiánykor a kivétel már a jóváírás után keletkezik, így a fogadó „ingyen” pénzt kap | előbb `Kivesz`, utána `cel.Befizet` |
| 3 | `Konstruktor_NegativNyitoEgyenleg_Kivetel` | a konstruktor nem ellenőrzi a nyitóegyenleget | `if (nyitoEgyenleg < 0) throw …` |

A 2. hiba a legveszélyesebb: a program nem száll el, a kivételt el is kapja a hívó, a hiba csak abban látszik, hogy egy másik objektum állapota rossz.
Ezt csak olyan teszt találja meg, amely kivétel után **minden érintett objektum** állapotát ellenőrzi.
