using System;
using System.Collections.Generic;

namespace Zugsichtungen.Infrastructure.SQLServer.Models;

public partial class Gallery
{
    public int SightingId { get; set; }

    public int PictureId { get; set; }

    public DateOnly SightingDate { get; set; }

    public string VehicleNumber { get; set; } = null!;

    public string Location { get; set; } = null!;

    public string? ContextDescription { get; set; }

    public string? Comment { get; set; }
}
