using System;
using System.Globalization;
using static System.Console;

namespace ByteBank
{
    public class ByteBank
    {
        static void LimpaTela()
        {
            TitulosTelas("PRESSIONE QUALQUER TECLA PARA CONTINUAR...");
            ReadKey();
            Clear();

        }
        static void ConsoleWriteLine(string msg)
        {   
            Console.WriteLine($"\t\t {msg}");
        }
        static void ConsoleWrite(string msg)
        {
            Console.Write($"\t\t {msg}");
        }

        static void TitulosTelas(string titulo)
        {
            ConsoleWriteLine("");
            Console.ForegroundColor = ConsoleColor.Green;
            ConsoleWriteLine(titulo);
            ConsoleWriteLine("");
            Console.ResetColor();
        }
        static void MenuPrincipal()
        {
            TitulosTelas(" MENU PRINCIPAL ");
            ConsoleWriteLine("F1 - CAD. CLIENTE");
            ConsoleWriteLine("F2 - DEL. CLIENTE");
            ConsoleWriteLine("F3 - CLIENTES");
            ConsoleWriteLine("F4 - INFO.CLIENTE");
            ConsoleWriteLine("F5 - SALDO DA CONTA");
            ConsoleWriteLine("F6 - ADM.CONTAS");
            ConsoleWriteLine("ESC - SAIR");
            ConsoleWriteLine("");
            ConsoleWrite("Digite a opção desejada: ");

        }

        static void MenuManutencao()
        {
            TitulosTelas("MENU MANUTENCAO DE CONTA");
            ConsoleWriteLine("F1 - DEPOSITO");
            ConsoleWriteLine("F2 - SAQUE");
            ConsoleWriteLine("F3 - TRANSFERENCIA");
            ConsoleWriteLine("ESC - VOLTAR AO MENU PRINCIPAL");
            ConsoleWriteLine("");
            ConsoleWrite("Digite a opção desejada: ");
        }


        static void ShowSubmenuManutencao(List<string> cpfs, List<string> titulares, List<string> senhas, List<string> nomesSociais, List<string> numerosContas, List<double> saldos)
        {
            ConsoleKey opcao;

            do
            {
                Clear();
                MenuManutencao();
                opcao = Console.ReadKey().Key;
                ConsoleWriteLine("");
                ConsoleWriteLine("");

                switch (opcao)
                {
                    case ConsoleKey.Escape:
                        MenuPrincipal();
                        Clear();
                        break;
                    case ConsoleKey.F1:
                        Clear();
                        Deposito(cpfs, numerosContas, saldos);
                        break;
                    case ConsoleKey.F2:
                        Saque(cpfs, numerosContas, saldos); 
                        Clear();
                        break;
                    case ConsoleKey.F3:
                        Clear();
                        Transferencia(cpfs, numerosContas, saldos);
                        break;
                }

                } while (opcao != ConsoleKey.Escape);

        }
        static void Deposito(List<string> cpfs, List<string> numerosContas, List<double> saldos)
        {
            TitulosTelas($"CPF PARA DEPOSITO");
            int indiceDeposito = PesquisaIndice(cpfs, "Deposito");
            if (indiceDeposito != -1)
            {
                Clear();
                TitulosTelas($"DEPOSITO NA CONTA {numerosContas[indiceDeposito]}");
                ConsoleWriteLine($"Saldo: R$ {saldos[indiceDeposito].ToString("F2", CultureInfo.InvariantCulture)}");
                ConsoleWrite("Digite o Valor: ");
                double valorDeposito = Convert.ToDouble(Console.ReadLine(), CultureInfo.InvariantCulture);
                saldos[indiceDeposito] += valorDeposito;
                ConsoleWriteLine("");
                ConsoleWriteLine($">> >Novo Saldo: R$ {saldos[indiceDeposito].ToString("F2", CultureInfo.InvariantCulture)}");
                LimpaTela();
            }
            else
            {
                Clear();
                ConsoleWriteLine("");
                ConsoleWriteLine("CPF não encontrado!");
                LimpaTela();
            }
        }

        static void Saque(List<string> cpfs, List<string> numerosContas, List<double> saldos)
        {
            TitulosTelas($"SAQUE");
            int indiceSaque = PesquisaIndice(cpfs, "CPF PARA SAQUE");
            if (indiceSaque != -1)
            {
                Clear();
                TitulosTelas($"SAQUE NA CONTA {numerosContas[indiceSaque]}");
                ConsoleWriteLine($"Saldo: R$ {saldos[indiceSaque].ToString("F2", CultureInfo.InvariantCulture)}");
                ConsoleWrite("Digite o Valor: ");
                double valorSaque = Convert.ToDouble(Console.ReadLine(), CultureInfo.InvariantCulture);
                double calculoSaldoSaque = saldos[indiceSaque] - valorSaque;
                if (calculoSaldoSaque >= 0) {
                    saldos[indiceSaque] -= valorSaque;
                    ConsoleWriteLine("");
                    ConsoleWriteLine($">> >Novo Saldo: R$ {saldos[indiceSaque].ToString("F2", CultureInfo.InvariantCulture)}");
                    LimpaTela();
                }
                else
                {
                    ConsoleWriteLine("");
                    Console.ForegroundColor = ConsoleColor.Red;
                    ConsoleWriteLine("SALDO SUFICIENTE PARA SAQUE!");
                    Console.ResetColor();
                    LimpaTela();
                }
                
            }
            else
            {
                Clear();
                ConsoleWriteLine("");
                ConsoleWriteLine("CPF não encontrado!");
                LimpaTela();
            }
        }

        static void Transferencia(List<string> cpfs, List<string> numerosContas, List<double> saldos)
        {
            TitulosTelas("Transferência de valor entre contas");
            ConsoleWrite("Digite o valor: ");
            double valorTransferencia = Convert.ToDouble(Console.ReadLine(), CultureInfo.InvariantCulture);
            ConsoleWriteLine("");
            int indiceOrigem = PesquisaIndice(cpfs, "Transferência Remetente");


            if (indiceOrigem != -1)
            {
                double verificaSaldo = saldos[indiceOrigem] - valorTransferencia;
                if (verificaSaldo >= 0) { 
                    
                    ConsoleWriteLine($"Saldo da Conta de origem {numerosContas[indiceOrigem]} R$ {saldos[indiceOrigem]}");

                    ConsoleWriteLine("");
                    int indiceDestino = PesquisaIndice(cpfs, "Transferência Destinatário");
                    if (indiceDestino != -1 )
                    {
                        saldos[indiceOrigem] -= valorTransferencia;
                        ConsoleWriteLine("");
                        ConsoleWriteLine($"Novo saldo conta de origem número: {numerosContas[indiceOrigem]} R$ {saldos[indiceOrigem]}");
                        ConsoleWriteLine("");
                        saldos[indiceDestino] += valorTransferencia;
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        ConsoleWriteLine($"Novo saldo conta de destino número: {numerosContas[indiceDestino]} R$ {saldos[indiceDestino]}");
                        Console.ResetColor();
                        LimpaTela();
                    }
                    else
                    {
                        ConsoleWriteLine("CPF não encontrado!");
                        LimpaTela();
                    }
                }
                else
                {
                    ConsoleWriteLine("");
                    Console.ForegroundColor = ConsoleColor.Red;
                    ConsoleWriteLine("Conta de origem SEM SALDO SUFICIENTE");
                    Console.ResetColor();
                    LimpaTela();

                }


            }
            else
            {
                ConsoleWriteLine("CPF Origem não encontrado!");
                LimpaTela();
            }
            
           

            
              
            }
            

        
        static void AdicionarNovoUsuario(List<string> cpfs, List<string> titulares, List<string> senhas, List<string> nomesSociais, List<string> numerosContas, List<double> saldos)
        {
            
            TitulosTelas("CADASTRO DE NOVO CLIENTE");
            
            ConsoleWrite("Digite o CPF: ");
            cpfs.Add(Console.ReadLine());
            ConsoleWrite("Digite o nome: ");
            titulares.Add(Console.ReadLine());
            ConsoleWrite("Digite o nome Social: ");
            nomesSociais.Add(Console.ReadLine());
            ConsoleWrite("Digite sua senha: ");
            senhas.Add(Console.ReadLine());
            int nroConta = numerosContas.Count;
            numerosContas.Add(Convert.ToString(nroConta += 1));
            saldos.Add(0);
            LimpaTela();
        }

        static void ListarClientesCadastrados(List<string> cpfs, List<string> titulares)
        {
            TitulosTelas("LISTA DE CLIENTES CADASTRADOS");
            if (cpfs.Count == 0)
            {
                ConsoleWriteLine("Não existe cliente cadastrado!");
                LimpaTela();
            }

            for (int i = 0; i < cpfs.Count; i++)
            {
                    ConsoleWriteLine($"Nome: {titulares[i]} | CPF: {cpfs[i]}");
                    
            }
            LimpaTela();

        }

        static void Saldo(List<string> cpfs, List<string> numerosContas, List<double> saldos)
        {
            TitulosTelas($"CPF PARA Saldo");
            int indiceSaldo = PesquisaIndice(cpfs, "Saldo");
            if (indiceSaldo != -1)
            {
                Clear();
                TitulosTelas($"SALDO DA CONTA {numerosContas[indiceSaldo]}");
                ConsoleWriteLine($"Saldo: R$ {saldos[indiceSaldo].ToString("F2", CultureInfo.InvariantCulture)}");
                LimpaTela();
            }
            else
            {
                Clear();
                ConsoleWriteLine("");
                ConsoleWriteLine("CPF não encontrado!");
                LimpaTela();
            }
        }
        static void DetalhesDoUsuario(List<string> cpfs, List<string> titulares, List<string> senhas, List<string> nomesSociais, List<string> numerosContas, List<double> saldos)
        {
            TitulosTelas("DETALHES CADASTRO DO CLIENTE");
            int indiceCPF = PesquisaIndice(cpfs, "Detalhes");

            if (indiceCPF == -1)
            {
                ConsoleWriteLine("CPF não encontrado!");
                LimpaTela();
            }
            else
            {
                detalhesComConta(indiceCPF, cpfs, titulares, nomesSociais, numerosContas, saldos);
            }
        }

        static void detalhesComConta(int i, List<string> cpfs, List<string> titulares, List<string> nomesSociais, List<string> numerosContas, List<double> saldos)
        {
            ConsoleWriteLine($"Nome: {titulares[i]} | CPF: {cpfs[i]} | Nome Social: {nomesSociais[i]} | Numero Conta: {numerosContas[i]} | Saldo: R$ {saldos[i].ToString("F2")}");
            LimpaTela();
        }

        static void DeletarCliente(List<string> cpfs, List<string> titulares, List<string> senhas, List<string> nomesSociais, List<string> numerosContas, List<double> saldos)
        {
            TitulosTelas("DELETAR CLIENTE");
            int indiceParaDeletar = PesquisaIndice(cpfs, "Deletar");

            if (indiceParaDeletar == -1)
            {
                ConsoleWriteLine("CPF não encontrado!");
                LimpaTela();
            }
            else
            {
                string nomeDeletado = titulares[indiceParaDeletar];
                cpfs.RemoveAt(indiceParaDeletar);
                titulares.RemoveAt(indiceParaDeletar);
                senhas.RemoveAt(indiceParaDeletar);
                nomesSociais.RemoveAt(indiceParaDeletar);
                numerosContas.RemoveAt(indiceParaDeletar);
                saldos.RemoveAt(indiceParaDeletar);
                ConsoleWriteLine("");
                ConsoleWriteLine($"Cliente {nomeDeletado} deletado com sucesso!");
                LimpaTela();
            }

            
        }

        static int PesquisaIndice(List<string> cpfs, string motivo)
        {
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            ConsoleWriteLine($"{motivo}");
            Console.ResetColor();
            ConsoleWrite("Digite o CPF: ");
            string cpfParaListar = Console.ReadLine();
            return cpfs.FindIndex(cpf => cpf == cpfParaListar);
        }

        static double Soma(List<double> saldos)
        {
            return saldos.Sum();
        }


        public static void Main(string[] args)
        {
            
            Console.Title = "Byte Bank";

            // ConsoleWriteLine("Digite a quantidade de usuários");
            // int quantidaDeUsuarios = int.Parse(Console.ReadLine());

            //Console.BackgroundColor = ConsoleColor.Gray;
            //Console.ForegroundColor = ConsoleColor.Black;
            //Console.Clear();

            // listas ref. clientes
            List<string> cpfs = new List<string>();
            List<string> titulares = new List<string>();
            List<string> senhas = new List<string>();
            List<string> nomesSociais = new List<string>();

            //listas ref. contas
            List<string> numerosContas = new List<string>();
            List<double> saldos = new List<double>();



            ConsoleKey tecla;

            do
            {
                Clear();
                MenuPrincipal();
                
                tecla = Console.ReadKey().Key;
                ConsoleWriteLine("");
                ConsoleWriteLine("");

                switch (tecla)
                {
                    case ConsoleKey.Escape:
                        ConsoleWriteLine("Encerrando o sistema...");
                        break;
                    case ConsoleKey.F1:
                        Clear();
                        AdicionarNovoUsuario(cpfs, titulares, senhas, nomesSociais, numerosContas, saldos);
                        break;
                    case ConsoleKey.F2:
                        Clear();
                        DeletarCliente(cpfs, titulares, senhas, nomesSociais, numerosContas, saldos);
                        break;
                    case ConsoleKey.F3:
                        Clear();
                        ListarClientesCadastrados(cpfs, titulares);
                        break;
                    case ConsoleKey.F4:
                        Clear();
                        DetalhesDoUsuario(cpfs, titulares, senhas, nomesSociais, numerosContas, saldos);
                        break;
                    case ConsoleKey.F5:
                        Saldo(cpfs, titulares, saldos);
                        Clear();
                        break;
                    case ConsoleKey.F6:
                        Clear();
                        ShowSubmenuManutencao(cpfs, titulares, senhas, nomesSociais, numerosContas, saldos);
                        break;
                }



                if (tecla != ConsoleKey.Escape)
                {
                    
                    ConsoleWriteLine("");
                    ConsoleWriteLine("--------");
                    ConsoleWriteLine("");
                }


            } while (tecla != ConsoleKey.Escape);


        }


    }
}