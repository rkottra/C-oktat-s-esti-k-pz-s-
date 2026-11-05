using System;
using System.Collections.Generic;

namespace OregfakForce.Models;

public partial class Fa
{
    public int Id { get; set; }

    public string Faj { get; set; } = null!;

    /// <summary>
    /// törzskerület cm-ben
    /// </summary>
    public int Kormeret { get; set; }

    public string Telepules { get; set; } = null!;

    public int MegyeId { get; set; }

    public int? MeresEve { get; set; }

    public bool Vedett { get; set; }

    public string? Megjegyzes { get; set; }

    public virtual Megye Megye { get; set; } = null!;
}
