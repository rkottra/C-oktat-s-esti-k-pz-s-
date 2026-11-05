using System;
using System.Collections.Generic;

namespace PizzeriaStatisztika.Models;

public partial class Tetel
{
    public int RendelesId { get; set; }

    public int PizzaId { get; set; }

    public int Db { get; set; }

    public virtual Pizza Pizza { get; set; } = null!;

    public virtual Rendeles Rendeles { get; set; } = null!;
}
