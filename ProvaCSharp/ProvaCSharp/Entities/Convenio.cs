using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bergs.AvaliacaoCSharp
{
    class Convenio
    {
        public string Cnpj { get; private set; }
        public string RazaoSocial { get; private set; }
        public int QtdEmpregados { get; private set; }
        public StatusConvenio Status { get; private set; }
        public DateTime DtAtuStatus { get; private set; }

        public Convenio(string cnpj, string razaoSocial, int qtdEmpregados, StatusConvenio status, DateTime dtAtuStatus) {
            Cnpj = cnpj;
            RazaoSocial = razaoSocial;
            QtdEmpregados = qtdEmpregados;
            Status = status;
            DtAtuStatus = dtAtuStatus;
        }
    }
}
