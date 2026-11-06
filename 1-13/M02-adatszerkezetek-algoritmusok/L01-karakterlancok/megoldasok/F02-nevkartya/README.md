# Megoldás – 13-M02-L01-F02 · Névkártya

- A `tagok[1..]` tartomány a tömböknél is működik: az 1. indextől a végéig tartó új tömböt ad.
- A `tag[0] + ". "` egy `char` és egy `string` összefűzése – az eredmény `string`.
- A `new string('*', 32)` 32 csillagból álló szöveget hoz létre (Pythonban: `"*" * 32`).
- Ha a név hosszabb 30 karakternél, a kártya „kilóg” – egy `if (nev.Length > Szelesseg)` ellenőrzéssel ez is kezelhető.
