using System;
using System.Collections.Generic;

namespace Zugsichtungen.Infrastructure.SQLServer.Models;

public partial class Context
{
    public int Id { get; set; }

    public string? Description { get; set; }

    public virtual ICollection<Sighting> Sightings { get; set; } = new List<Sighting>();
}
