using System;
using System.Collections.Generic;

namespace PizzeriaScaffold.Models;

public partial class Vevo
{
    public int Id { get; set; }

    public string Nev { get; set; } = null!;

    public string Cim { get; set; } = null!;

    public virtual ICollection<Rendeles> Rendeles { get; set; } = new List<Rendeles>();
}
