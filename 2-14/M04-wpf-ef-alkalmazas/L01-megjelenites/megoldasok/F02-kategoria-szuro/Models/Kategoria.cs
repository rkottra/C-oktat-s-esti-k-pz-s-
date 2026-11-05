using System.ComponentModel.DataAnnotations;

namespace RaktarSzuro.Models;

public class Kategoria
{
    public int Id { get; set; }

    [MaxLength(50)]
    public string Nev { get; set; } = "";

    public List<Termek> Termekek { get; set; } = new();
}
