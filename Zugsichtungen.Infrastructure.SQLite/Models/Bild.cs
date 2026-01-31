using System;
using System.Collections.Generic;

namespace Zugsichtungen.Infrastructure.SQLite.Models;

public partial class Bild
{
    public int? Id { get; set; }

    public DateOnly? Datum { get; set; }

    public string? Loknummer { get; set; }

    public string? Ort { get; set; }

    public string? Thema { get; set; }

    public string? Bemerkung { get; set; }

    public byte[]? Bild1 { get; set; }

    public byte[]? Thumbnail { get; set; }
}
