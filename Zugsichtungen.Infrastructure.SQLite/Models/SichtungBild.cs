using System;
using System.Collections.Generic;

namespace Zugsichtungen.Infrastructure.SQLite.Models;

public partial class SichtungBild
{
    public int Id { get; set; }

    public int SichtungId { get; set; }

    public string Dateiname { get; set; } = null!;

    public byte[] Bild { get; set; } = null!;

    public byte[]? Thumbnail { get; set; }

    public virtual Sichtungen Sichtung { get; set; } = null!;
}
