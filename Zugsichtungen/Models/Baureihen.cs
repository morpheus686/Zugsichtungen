using System;
using System.Collections.Generic;

namespace Zugsichtungen.Models;

public partial class Baureihen
{
    public int Id { get; set; }

    public string Nummer { get; set; } = null!;

    public string? Bemerkung { get; set; }

    public int? ModellId { get; set; }

    public virtual ICollection<Fahrzeuge> Fahrzeuges { get; set; } = new List<Fahrzeuge>();

    public virtual Modelle? Modell { get; set; }
}
