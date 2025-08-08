using System;
using System.Collections.Generic;

namespace Zugsichtungen.Infrastructure.SQLite.Models;

public partial class Sichtungen
{
    public int Id { get; set; }

    public int? FahrzeugId { get; set; }

    public DateOnly? Datum { get; set; }

    public string? Ort { get; set; }

    public int? KontextId { get; set; }

    public string? Bemerkung { get; set; }

    public virtual Fahrzeuge? Fahrzeug { get; set; }

    public virtual Kontexte? Kontext { get; set; }

    public virtual ICollection<SichtungBild> SichtungBilds { get; set; } = new List<SichtungBild>();
}
