using System;
using System.Collections.Generic;

namespace Zugsichtungen.Infrastructure.SQLServer.Models;

public partial class SightingPicture
{
    public int Id { get; set; }

    public int SightingId { get; set; }

    public byte[] Image { get; set; } = null!;

    public string Filename { get; set; } = null!;

    public virtual Sighting Sighting { get; set; } = null!;
}
