# Megoldás – 14-M03-L03-F02 · Öreg fák: scaffold kapcsolókkal és lekérdezések

A generált fájlok (`Data/OregfakContext.cs`, `Models/Fa.cs`, `Models/Megye.cs`) ezzel a paranccsal készültek:

```
Scaffold-DbContext "server=localhost;database=estis_oregfak;user=root;password=" Pomelo.EntityFrameworkCore.MySql -OutputDir Models -ContextDir Data -Context OregfakContext -NoPluralize -Force
```

- A context a `OregfakLekerdezesek.Data`, az entitások a `OregfakLekerdezesek.Models` névtérbe kerültek
  (a névtér a mappából képződik), ezért a `Program.cs` a `Data` névteret használja.
- `-NoPluralize` miatt: `db.Fa`, `db.Megye`, és a `Megye.Fa` gyűjtemény (nem `Fas`).
- A saját kód a `Kiegeszitesek` mappában van – egy újabb `-Force` scaffold után is megmarad.
