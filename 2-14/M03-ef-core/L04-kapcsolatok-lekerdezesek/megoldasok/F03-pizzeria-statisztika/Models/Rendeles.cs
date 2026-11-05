using System;
using System.Collections.Generic;

namespace PizzeriaStatisztika.Models;

public partial class Rendeles
{
    public int Id { get; set; }

    public int VevoId { get; set; }

    public int FutarId { get; set; }

    public DateTime Idopont { get; set; }

    public virtual Futar Futar { get; set; } = null!;

    public virtual ICollection<Tetel> Tetel { get; set; } = new List<Tetel>();

    public virtual Vevo Vevo { get; set; } = null!;
}
