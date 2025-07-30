using System;
using System.Collections.Generic;

namespace Zugsichtungen.Infrastructure.Models;

public partial class Fahrzeuge
{
    public int Id { get; set; }

    public string Nummer { get; set; } = null!;

    public int? BaureiheId { get; set; }

    public string? Bemerkung { get; set; }

    public virtual Baureihen? Baureihe { get; set; }

    public virtual ICollection<Sichtungen> Sichtungens { get; set; } = new List<Sichtungen>();
}
