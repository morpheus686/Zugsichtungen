using System;
using System.Collections.Generic;

namespace Zugsichtungen.Infrastructure.Models;

public partial class Kontexte
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Sichtungen> Sichtungens { get; set; } = new List<Sichtungen>();
}
