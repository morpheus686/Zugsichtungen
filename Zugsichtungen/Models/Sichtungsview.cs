using System;
using System.Collections.Generic;

namespace Zugsichtungen.Models;

public partial class Sichtungsview
{
    public int? Id { get; set; }

    public DateOnly? Datum { get; set; }

    public string? Loknummer { get; set; }

    public string? Ort { get; set; }

    public string? Thema { get; set; }

    public string? Bemerkung { get; set; }
}
