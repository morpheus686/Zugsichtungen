using System;
using System.Collections.Generic;

namespace Zugsichtungen.Infrastructure.SQLServer.Models;

public partial class SightingList
{
    public int Id { get; set; }

    public DateOnly SightingDate { get; set; }

    public string VehicleNumber { get; set; } = null!;

    public string Location { get; set; } = null!;

    public string? Description { get; set; }

    public string? Comment { get; set; }

    public byte[]? Image { get; set; }
}
