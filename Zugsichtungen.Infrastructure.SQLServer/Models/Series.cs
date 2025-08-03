using System;
using System.Collections.Generic;

namespace Zugsichtungen.Infrastructure.SQLServer.Models;

public partial class Series
{
    public int Id { get; set; }

    public string? Number { get; set; }

    public string? Comment { get; set; }

    public int ModelId { get; set; }

    public virtual Model Model { get; set; } = null!;

    public virtual ICollection<Vehicle> Vehicles { get; set; } = new List<Vehicle>();
}
