using System;
using System.Collections.Generic;

namespace Zugsichtungen.Infrastructure.SQLServer.Models;

public partial class Manufacturer
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Model> Models { get; set; } = new List<Model>();
}
