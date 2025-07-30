using System;
using System.Collections.Generic;

namespace Zugsichtungen.Infrastructure.Models;

public partial class Hersteller
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<Modelle> Modelles { get; set; } = new List<Modelle>();
}
