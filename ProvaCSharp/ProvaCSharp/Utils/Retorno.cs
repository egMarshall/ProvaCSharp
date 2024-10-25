using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bergs.AvaliacaoCSharp
{
    class Retorno<T>
    {
        public int Codigo { get; set; }
        public string Mensagem { get; set; }
        public T Dados { get; set; }

        public Retorno(int codigo, string mensagem, T dados) {
            Codigo = codigo;
            Mensagem = mensagem;
            Dados = dados;
        }
    }
}
