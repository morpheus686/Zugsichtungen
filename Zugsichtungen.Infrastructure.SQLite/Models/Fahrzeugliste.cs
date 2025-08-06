using System;
using System.Collections.Generic;

namespace Zugsichtungen.Infrastructure.SQLite.Models;

public partial class Fahrzeugliste
{
    public int? Id { get; set; }

    public string? Fahrzeug { get; set; }

    public int? BaureiheId { get; set; }
}
