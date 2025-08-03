using System;
using System.Collections.Generic;

namespace Zugsichtungen.Infrastructure.SQLServer.Models;

public partial class Vehicle
{
    public int Id { get; set; }

    public string? Number { get; set; }

    public int SeriesId { get; set; }

    public string? Comment { get; set; }

    public virtual Series Series { get; set; } = null!;

    public virtual ICollection<Sighting> Sightings { get; set; } = new List<Sighting>();
}
