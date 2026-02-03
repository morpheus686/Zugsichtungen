using System;
using System.Collections.Generic;

namespace Zugsichtungen.Infrastructure.SQLite.Models;

public partial class Bildergalerie
{
    public int? SichtungId { get; set; }

    public int? SichtungBildId { get; set; }

    public DateOnly? Datum { get; set; }

    public string? Loknummer { get; set; }

    public string? Ort { get; set; }

    public string? Thema { get; set; }

    public string? Bemerkung { get; set; }
}
