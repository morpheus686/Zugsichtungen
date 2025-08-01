using System;
using System.Collections.Generic;

namespace Zugsichtungen.Infrastructure.SqlServerModels;

public partial class Model
{
    public int Id { get; set; }

    public string Model1 { get; set; } = null!;

    public int ManufactorerId { get; set; }

    public virtual Manufacturer Manufactorer { get; set; } = null!;

    public virtual ICollection<Series> Series { get; set; } = new List<Series>();
}
