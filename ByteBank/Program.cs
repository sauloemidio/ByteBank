using System;
using System.Globalization;
using System.Data;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Collections.Generic;
using System.Text;
using static System.Console;
using System.Runtime.Intrinsics.Arm;


namespace ByteBank
{
    public class ByteBank
    {
        static void LimpaTela()
        {
            Titulos("PRESSIONE QUALQUER TECLA PARA CONTINUAR...");
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

        static void Titulos(string titulo)
        {
            ConsoleWriteLine("");
            Console.ForegroundColor = ConsoleColor.Green;
            ConsoleWriteLine(titulo);
            ConsoleWriteLine("");
            Console.ResetColor();
        }
        static void MenuPrincipal()
        {
            Titulos("MENU PRINCIPAL");
            ConsoleWriteLine("F1 - CAD. CLIENTE");
            ConsoleWriteLine("F2 - DEL. CLIENTE");
            ConsoleWriteLine("F3 - CLIENTES");
            ConsoleWriteLine("F4 - INFO.CLIENTE");
            ConsoleWriteLine("F5 - SALDO DA CONTA");
            ConsoleWriteLine("F6 - ADM.CONTAS");
            ConsoleWriteLine("0 - SAIR");
            ConsoleWriteLine("");
            ConsoleWrite("Digite a opção desejada: ");

        }

        static void MenuManutencao()
        {
            Titulos("MENU MANUTENCAO DE CONTA");
            ConsoleWriteLine("F1 - DEPOSITO");
            ConsoleWriteLine("F2 - SAQUE");
            ConsoleWriteLine("F3 - TRANSFERENCIA");
            ConsoleWriteLine("0 - VOLTAR AO MENU PRINCIPAL");
            ConsoleWriteLine("");
            ConsoleWrite("Digite a opção desejada: ");
        }


        static void ShowSubmenuManutencao(List<string> cpfs, List<string> titulares, List<string> senhas, List<string> nomesSociais, List<string> numerosContas, List<double> saldos)
        {
            int opcao;

            do
            {
                MenuManutencao();
                opcao = int.Parse(Console.ReadLine());
                ConsoleWriteLine("");
                ConsoleWriteLine("");

                switch (opcao)
                {
                    case 0:
                        Clear();
                        break;
                    case 1:
                        Clear();
                        Deposito(cpfs, titulares, senhas, nomesSociais, numerosContas, saldos);
                        break;

                }

                } while (opcao != 0);

        }
        static void Deposito(List<string> cpfs, List<string> titulares, List<string> senhas, List<string> nomesSociais, List<string> numerosContas, List<double> saldos)
        {
            Titulos($"CPF PARA DEPOSITO");
            int indiceDeposito = PesquisaIndice(cpfs, "Deposito");
            if (indiceDeposito != -1)
            {
                Clear();
                Titulos($"DEPOSITO NA CONTA {numerosContas[indiceDeposito]}");
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

        static void AdicionarNovoUsuario(List<string> cpfs, List<string> titulares, List<string> senhas, List<string> nomesSociais, List<string> numerosContas, List<double> saldos)
        {
            
            Titulos("CADASTRO DE NOVO CLIENTE");
            
            ConsoleWrite("Digite o CPF: ");
            cpfs.Add(Console.ReadLine());
            ConsoleWrite("Digite o nome: ");
            titulares.Add(Console.ReadLine());
            ConsoleWrite("Digite o nome Social: ");
            nomesSociais.Add(Console.ReadLine());
            ConsoleWrite("Digite sua senha: ");
            senhas.Add(Console.ReadLine());
            numerosContas.Add("0");
            saldos.Add(0);
            LimpaTela();
        }

        static void ListarClientesCadastrados(List<string> cpfs, List<string> titulares)
        {
            Titulos("LISTA DE CLIENTES CADASTRADOS");
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


        static void DetalhesDoUsuario(List<string> cpfs, List<string> titulares, List<string> senhas, List<string> nomesSociais, List<string> numerosContas, List<double> saldos)
        {
            Titulos("DETALHES CADASTRO DO CLIENTE");
            int indiceCPF = PesquisaIndice(cpfs, "Detalhes");

            if (indiceCPF == -1)
            {
                ConsoleWriteLine("CPF não encontrado!");
                LimpaTela();
            }
            else if (numerosContas.Count == 0)
            {
                ConsoleWriteLine($"Nome: {titulares[indiceCPF]} | CPF: {cpfs[indiceCPF]} | Nome Social: {nomesSociais[indiceCPF]} | Saldo: R$ {saldos[indiceCPF].ToString("F2")}");
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

        static void DeletarUsuario(List<string> cpfs, List<string> titulares, List<string> senhas, List<string> nomesSociais, List<string> numerosContas, List<double> saldos)
        {
            Titulos("DELETAR CLIENTE");
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
            ConsoleWriteLine($"{motivo}");
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



            int opcao;

            do
            {
                MenuPrincipal();

               opcao = int.Parse(Console.ReadLine());
                ConsoleWriteLine("");
                ConsoleWriteLine("");

                switch (opcao)
                {
                    case 0:
                        ConsoleWriteLine("Encerrando o sistema...");
                        break;
                    case 1:
                        Clear();
                        AdicionarNovoUsuario(cpfs, titulares, senhas, nomesSociais, numerosContas, saldos);
                        break;
                    case 2:
                        Clear();
                        DeletarUsuario(cpfs, titulares, senhas, nomesSociais, numerosContas, saldos);
                        
                        break;
                    case 3:
                        Clear();
                        ListarClientesCadastrados(cpfs, titulares);
                        
                        break;
                    case 4:
                        Clear();
                        DetalhesDoUsuario(cpfs, titulares, senhas, nomesSociais, numerosContas, saldos);
                        break;
                    case 6:
                        Clear();
                        ShowSubmenuManutencao(cpfs, titulares, senhas, nomesSociais, numerosContas, saldos);
                        break;
                }



                if (opcao != 0)
                {
                    
                    ConsoleWriteLine("");
                    ConsoleWriteLine("--------");
                    ConsoleWriteLine("");
                }


            } while (opcao != 0);


        }


    }
}