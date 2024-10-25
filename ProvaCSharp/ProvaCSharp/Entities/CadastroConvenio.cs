using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bergs.AvaliacaoCSharp
{
    class CadastroConvenio
    {
        private string CaminhoBancoDados {get; set;}
        private List<Convenio> ListaConvenios { get; set; }

        public CadastroConvenio(string caminhoBancoDados)
        {
            CaminhoBancoDados = caminhoBancoDados;
            ListaConvenios = new List<Convenio>();
        }

        public Retorno<int> AdicionarConvenio(string cnpj, string razaoSocial, string qtdEmpregados, string status, string dtAtuStatus)
        {
            // Validar CNPJ
            var cnpjFormatado = cnpj.TrimStart(new char[] { '0' });
            if (ValidaCnpj(cnpjFormatado))
            {
                return new Retorno<int>(10, "O CNPJ informado é inválido", 1);
            }

            // Verifica se já existe
            var convenioExiste = ListaConvenios.FirstOrDefault(c => c.Cnpj == cnpjFormatado);
            if (convenioExiste != null)
            {
                return new Retorno<int>(11, "Já existe um convênio com esse CNPJ", 1);
            }

            // Valida razão social
            if (razaoSocial.Length < 3)
            {
                return new Retorno<int>(12, "A razão social deve ter pelo menos 3 caracteres", 1);
            }

            // Valida se quantidade de empregados é inteiro
            if (!int.TryParse(qtdEmpregados, out int qtdEmpregadosFormatado))
            {
                return new Retorno<int>(13, "A quantidade de empregados informada é inválida", 1);
            }

            // Valida se a quantidade de empregados é maior que 0
            if (qtdEmpregadosFormatado <= 0)
            {
                return new Retorno<int>(14, "A quantidade de empregados deve ser maior que 0", 1);
            }

            // Valida status
            StatusConvenio statusFormatado;
            try
            {
                statusFormatado = (StatusConvenio)Enum.Parse(typeof(StatusConvenio), status);
                if (!Enum.IsDefined(typeof(StatusConvenio), statusFormatado))
                {
                    return new Retorno<int>(15, "O status é inválido", 1);
                }
            }
            catch
            {
                return new Retorno<int>(15, "O status é inválido", 1);
            }

            // Validar se a data de atualização do status é uma data válida
            if (!DateTime.TryParse(dtAtuStatus, out var data)){
                return new Retorno<int>(16, "A data de atualização do status é inválida", 1);
            }

            // Validar se a data não é data futura
            if (data > DateTime.Now)
            {
                return new Retorno<int>(17, "A data de atualização do status não pode ser uma data futura", 1);
            }

            // Adiciona na lista de convenios
            var convenio = new Convenio(cnpjFormatado, razaoSocial, qtdEmpregadosFormatado, statusFormatado, data);
            ListaConvenios.Add(convenio);

            return new Retorno<int>(00, "Operação realizada com sucesso", 1);
        }

        public Retorno<int> RemoverConvenio(string cnpj)
        {
            // Validar CNPJ
            if (ValidaCnpj(cnpj))
            {
                return new Retorno<int>(20, "O CNPJ informado é inválido", 1);
            }

            // Verifica se já existe
            var convenioExiste = ListaConvenios.FirstOrDefault(c => c.Cnpj == cnpj);
            if (convenioExiste == null)
            {
                return new Retorno<int>(21, "Não foi encontrado nenhum convênio com o CNPJ informado", 1);
            }

            // Remove convenio da lista
            ListaConvenios.Remove(convenioExiste);

            return new Retorno<int>(00, "Operação realizada com sucesso", 1);
        }

        public Retorno<List<Convenio>> ListarConvenios()
        {
            if (ListaConvenios.Count == 0)
            {
                return new Retorno<List<Convenio>>(30, "Nenhum registro encontrado", ListaConvenios);
            }

            return new Retorno<List<Convenio>>(00, "Operação realizada com sucesso", ListaConvenios);
        }

        public Retorno<int> SalvarConvenios()
        {

            if (ListaConvenios.Count == 0)
            {
                return new Retorno<int>(40, "Nenhum registro encontrado", 0);
            }

            try
            {
                var repositorioConvenio = new ConvenioRepositorio(CaminhoBancoDados);
                return repositorioConvenio.Adicionar(ListaConvenios);
            }
            catch (Exception ex)
            {
                return new Retorno<int>(41, $"Erro ao conectar com o banco de dados: {ex.Message}", 1);
            }
              
        }

        private bool ValidaCnpj(string cnpj)
        {

            if (System.Text.RegularExpressions.Regex.IsMatch(cnpj, "^[1-9]\\d{2,13}$"))
            {
                return false;
            }
            return true;
        }

    }
}
