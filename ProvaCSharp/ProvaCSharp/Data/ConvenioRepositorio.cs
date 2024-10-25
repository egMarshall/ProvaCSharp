using Bergs.ProvacSharp.BD;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bergs.AvaliacaoCSharp
{
    class ConvenioRepositorio : IRepositorio<Convenio>
    {

        private readonly AcessoBancoDados _acessoBancoDados;

        public ConvenioRepositorio(string database)
        {
            _acessoBancoDados = new AcessoBancoDados(database);
        }

        public Retorno<int> Adicionar(List<Convenio> convenios)
        {
           try
            {
                // Deleta os dados antes
                _acessoBancoDados.Abrir();

                string sql = "DELETE FROM CONVENIO";

                _acessoBancoDados.ExecutarDelete(sql);
            }
            catch (Exception ex)
            {
                return new Retorno<int>(41, ex.Message, convenios.Count);
            }
            try
            {
                foreach (var convenio in convenios)
                {
                    string sql = "INSERT INTO CONVENIO (CNPJ, RAZAO_SOCIAL, QTD_EMPREGADOS, STATUS, DT_ATU_STATUS) VALUES(@Cnpj, @RazaoSocial, @QtdEmpregados, @Status, @DtAtuStatus)";

                    SqlCommand command = new SqlCommand(sql);
                    command.CommandType = CommandType.Text;

                    var cnpj = new SqlParameter("@Cnpj", DbType.String);
                    cnpj.Value = convenio.Cnpj;

                    var razaoSocial = new SqlParameter("@RazaoSocial", DbType.String);
                    razaoSocial.Value = convenio.RazaoSocial;

                    var qtdEmpregados = new SqlParameter("@QtdEmpregados", DbType.Int32);
                    qtdEmpregados.Value = convenio.QtdEmpregados;

                    var status = new SqlParameter("@Status", DbType.Byte);
                    status.Value = (int)convenio.Status;

                    var dtAtuStatus = new SqlParameter("@DtAtuStatus", DbType.DateTime);
                    dtAtuStatus.Value = convenio.DtAtuStatus;

                    command.Parameters.Add(cnpj);
                    command.Parameters.Add(razaoSocial);
                    command.Parameters.Add(qtdEmpregados);
                    command.Parameters.Add(status);
                    command.Parameters.Add(dtAtuStatus);

                    _acessoBancoDados.ExecutarInsert(command.GetGeneratedQuery());
                }
                _acessoBancoDados.EfetivarComandos();
            }
            catch (Exception ex)
            {
                return new Retorno<int>(41, ex.Message, convenios.Count);
            }
            finally
            {
                _acessoBancoDados.Dispose();
            }
            return new Retorno<int>(00, "Operação realizada com sucesso", convenios.Count);
        }
    }
}
