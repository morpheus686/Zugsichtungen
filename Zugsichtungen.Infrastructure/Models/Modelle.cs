using System;
using System.Collections.Generic;

namespace Zugsichtungen.Infrastructure.Models;

public partial class Modelle
{
    public int Id { get; set; }

    public string? Modell { get; set; }

    public int HerstellerId { get; set; }

    public virtual ICollection<Baureihen> Baureihens { get; set; } = new List<Baureihen>();

    public virtual Hersteller Hersteller { get; set; } = null!;
}
