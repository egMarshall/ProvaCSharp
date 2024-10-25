using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bergs.AvaliacaoCSharp
{
     interface IRepositorio<T>
    {
        Retorno<int> Adicionar(List<T> convenios);
    }
}
