using System;
using System.Collections.Generic;

namespace Zugsichtungen.Infrastructure.SQLServer.Models;

public partial class Sighting
{
    public int Id { get; set; }

    public int VehicleId { get; set; }

    public DateOnly Date { get; set; }

    public string Location { get; set; } = null!;

    public int ContextId { get; set; }

    public string? Comment { get; set; }

    public virtual Context Context { get; set; } = null!;

    public virtual Vehicle Vehicle { get; set; } = null!;
}
