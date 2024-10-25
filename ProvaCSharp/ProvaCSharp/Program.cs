using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bergs.AvaliacaoCSharp
{
    class Program
    {
        private static CadastroConvenio cadastroConvenio = new CadastroConvenio(@"C:\Soft\pxc\data\Pxcz02da.mdb");

        static void Main(string[] args)
        {
            var convenios = cadastroConvenio.ListarConvenios();
            int numeroDeConvenios = 0;

            void ExecutaMenu(int numeroConvenios)
            {
                Console.Clear();
                Console.WriteLine("Cadastro de convênios para crédito consignado");
                Console.WriteLine("---------------------------------------------");
                Console.WriteLine($"\nTotal de convênios: {numeroConvenios}\n");
                Console.WriteLine("1.Adicionar convênio");
                Console.WriteLine("2.Remover convênio");
                Console.WriteLine("3.Listar convênios");
                Console.WriteLine("4.Salvar convênios");
                Console.WriteLine("5.Sair");
                Console.Write("\nInforme a operação desejada:  ");
            }

            void executarOpcao()
            {
                int opcaoUsuario = 0;

                while (opcaoUsuario != 5)
                {
                    ExecutaMenu(numeroDeConvenios);
                    try
                    {
                        opcaoUsuario = Convert.ToInt32(Console.ReadLine().Trim());
                    }
                    catch
                    {
                        Console.WriteLine("\nOpção inválida. Digite qualquer tecla para tentar novamente.");
                        Console.ReadKey();
                    }

                    if (opcaoUsuario >= 1)
                    {
                        switch (opcaoUsuario)
                        {
                            case 1:
                                AdicionarConvenio();
                                FimDeOperacao();
                                break;
                            case 2:
                                RemoverConvenio();
                                FimDeOperacao();
                                break;
                            case 3:
                                ListarConvenios();
                                FimDeOperacao();
                                break;
                            case 4:
                                SalvarConvenios();
                                FimDeOperacao();
                                break;
                            case 5:
                                break;
                            default:
                                Console.WriteLine("\nOpção inválida. Digite qualquer tecla para tentar novamente.");
                                Console.ReadKey();
                                break;
                        }
                    }
                    

                }
            }

            // Lógica das opções:
            void AdicionarConvenio() {
                Console.Clear();
                Console.Write("Digite o CNPJ:  ");
                var cnpj = Console.ReadLine();
                Console.Write("\nDigite a razão social:  ");
                var razaoSocial = Console.ReadLine();
                Console.Write("\nDigite a quantidade de empregados:  ");
                var qntEmpregados = Console.ReadLine();
                Console.WriteLine("\nDigite o Status:" +
                    "\n(1) - Cadastrado;" +
                    "\n(2) - Deferido" +
                    "\n(3) - Suspenso");
                var status = Console.ReadLine();
                Console.Write("\nDigite a data de atualização do status (dd/mm/yyyy):  ");
                var data = Console.ReadLine();
                var retorno1 = cadastroConvenio.AdicionarConvenio(cnpj, razaoSocial, qntEmpregados, status, data);
                if (retorno1.Codigo == 00)
                {
                    numeroDeConvenios++;
                }
                Console.Clear();
                ExibeMensagemDeRetorno(retorno1);
            };

            void RemoverConvenio() {
                Console.Clear();
                Console.Write("Digite o CNPJ do convênio a ser removido:  ");
                var cnpj = Console.ReadLine();
                var retorno2 = cadastroConvenio.RemoverConvenio(cnpj);
                Console.Clear();
                ExibeMensagemDeRetorno(retorno2);
            };

            void ListarConvenios() {
                Console.Clear();
                var retorno3 = cadastroConvenio.ListarConvenios();
                if (retorno3.Codigo == 00)
                {
                    foreach (var convenio in retorno3.Dados)
                    {
                        Console.WriteLine($"CNPJ: {convenio.Cnpj}  Razão Social: {convenio.RazaoSocial}  " +
                            $"Quantidade de Empregados: {convenio.QtdEmpregados}  Status: {convenio.Status}  " +
                            $"Data de atualização do status: {convenio.DtAtuStatus.ToString("dd/MM/yyyy")}");
                    }
                } else
                {
                    Console.WriteLine($"{retorno3.Codigo}: {retorno3.Mensagem}");
                }
            };

            void SalvarConvenios() {
                Console.Clear();
                var retorno4 = cadastroConvenio.SalvarConvenios();
                ExibeMensagemDeRetorno(retorno4);
            };

            //Formata mensagens de retorno:
            void ExibeMensagemDeRetorno(Retorno<int> retorno)
            {
                Console.WriteLine($"{retorno.Codigo}: {retorno.Mensagem}\n");
            }

            //Fim de operação:
            void FimDeOperacao()
            {
                Console.WriteLine("Fim de operação. Digite qualquer tecla para voltar ao menu.");
                Console.ReadKey();
            }

            executarOpcao();

        }
    }
}
