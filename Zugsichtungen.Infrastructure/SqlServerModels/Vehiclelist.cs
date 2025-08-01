using System;
using System.Collections.Generic;

namespace Zugsichtungen.Infrastructure.SqlServerModels;

public partial class Vehiclelist
{
    public int Id { get; set; }

    public string VehicleDesignation { get; set; } = null!;
}
