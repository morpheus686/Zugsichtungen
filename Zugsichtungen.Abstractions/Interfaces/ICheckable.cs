using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zugsichtungen.Abstractions.Interfaces
{
    public interface ICheckable
    {
        int Id { get; }
        bool IsChecked { get; set; }
    }
}
