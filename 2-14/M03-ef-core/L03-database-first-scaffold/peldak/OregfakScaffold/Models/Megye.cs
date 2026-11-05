using System;
using System.Collections.Generic;

namespace OregfakScaffold.Models;

public partial class Megye
{
    public int Id { get; set; }

    public string Nev { get; set; } = null!;

    public virtual ICollection<Fa> Fas { get; set; } = new List<Fa>();
}
