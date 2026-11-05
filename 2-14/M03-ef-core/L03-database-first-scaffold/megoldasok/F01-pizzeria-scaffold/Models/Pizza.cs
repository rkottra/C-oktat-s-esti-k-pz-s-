using System;
using System.Collections.Generic;

namespace PizzeriaScaffold.Models;

public partial class Pizza
{
    public int Id { get; set; }

    public string Nev { get; set; } = null!;

    public int Ar { get; set; }

    public virtual ICollection<Tetel> Tetel { get; set; } = new List<Tetel>();
}
