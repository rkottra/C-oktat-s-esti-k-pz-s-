using System;
using System.Collections.Generic;

namespace OregfakScaffold.Models;

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

    public virtual Megye Megye { get; set; } = null!;
}
