using System;
using System.Collections.Generic;

namespace OregfakLekerdezesek.Models;

public partial class Megye
{
    public int Id { get; set; }

    public string Nev { get; set; } = null!;

    public virtual ICollection<Fa> Fa { get; set; } = new List<Fa>();
}
