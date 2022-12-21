using System;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;

namespace ByteBank
{
    public class ByteBank
    {
        static void MenuPrincipal()
        {
            Console.WriteLine("1 - Inserir novo Cliente");
            Console.WriteLine("2 - Deletar um Cliente");
            Console.WriteLine("3 - Listas Clientes Registrados");
            Console.WriteLine("4 - Detalhes de um Cliente");
            Console.WriteLine("5 - Saldo da conta");
            Console.WriteLine("6 - Manutenção de contas");
            Console.WriteLine("0 - Para sair do programa");
            Console.Write("Digite a opção desejada: ");
        }


        static void ShowSubmenuManutencao(List<string> cpfs, List<string> titulares, List<double> saldos)
        {

            Console.Write("Digite o CPF: ");
            string cpfDigitado = Console.ReadLine();
            //Console.WriteLine(achou);
            Console.WriteLine("2 = Transferencia");

        }

        static void AdicionarNovoUsuario(List<string> cpfs, List<string> titulares, List<string> senhas, List<string> nomesSociais, List<string> numerosContas, List<double> saldos)
        {
            Console.Write("Digite o CPF: ");
            cpfs.Add(Console.ReadLine());
            Console.Write("Digite o nome: ");
            titulares.Add(Console.ReadLine());
            Console.Write("Digite o nome Social: ");
            nomesSociais.Add(Console.ReadLine());
            Console.Write("Digite sua senha: ");
            senhas.Add(Console.ReadLine());
            numerosContas.Add("0");

            saldos.Add(0);

        }

        static void ListarClientesCadastrados(List<string> cpfs, List<string> titulares)
        {


            for (int i = 0; i <= cpfs.Count; i++)
            {
                if (cpfs.Count == 0)
                {
                    Console.WriteLine("Não existe cliente cadastrado!");
                }
                else
                {
                    Console.WriteLine($"Nome: {titulares[i]} | CPF: {cpfs[i]}");
                }
            }
        }


        static void DetalhesDoUsuario(List<string> cpfs, List<string> titulares, List<string> senhas, List<string> nomesSociais, List<string> numerosContas, List<double> saldos)
        {
            int indiceCPF = PesquisaIndice(cpfs);

            if (indiceCPF == -1)
            {
                Console.WriteLine("CPF não encontrado!");
            }
            else if (numerosContas.Count == 0)
            {
                Console.WriteLine($"Nome: {titulares[indiceCPF]} | CPF: {cpfs[indiceCPF]} | Nome Social: {nomesSociais[indiceCPF]} | Saldo: R${saldos[indiceCPF].ToString("F2")}");
            }
            else
            {
                detalhesComConta(indiceCPF, cpfs, titulares, nomesSociais, numerosContas, saldos);
            }
        }

        static void detalhesComConta(int i, List<string> cpfs, List<string> titulares, List<string> nomesSociais, List<string> numerosContas, List<double> saldos)
        {
            Console.WriteLine($"Nome: {titulares[i]} | CPF: {cpfs[i]} | Nome Social: {nomesSociais[i]} | Numero Conta: {numerosContas[i]} | Saldo: R${saldos[i].ToString("F2")}");
        }

        static void DeletarUsuario(List<string> cpfs, List<string> titulares, List<string> senhas, List<string> nomesSociais, List<string> numerosContas, List<double> saldos)
        {
            //Console.Write("Digite o CPF para deletar: ");
            //string cpfParaDeletar = Console.ReadLine();
            //int indiceParaDeletar = cpfs.FindIndex(cpf => cpf == cpfParaDeletar);

            int indiceParaDeletar = PesquisaIndice(cpfs);

            if (indiceParaDeletar == -1)
            {
                Console.WriteLine("CPF não encontrado!");
            }

            cpfs.RemoveAt(indiceParaDeletar);
            titulares.RemoveAt(indiceParaDeletar);
            senhas.RemoveAt(indiceParaDeletar);
            nomesSociais.RemoveAt(indiceParaDeletar);
            numerosContas.RemoveAt(indiceParaDeletar);
            saldos.RemoveAt(indiceParaDeletar);

            Console.WriteLine("Usuario deletado com sucesso!");
        }

        static int PesquisaIndice(List<string> cpfs)
        {
            Console.Write("Digite o CPF: ");
            //string cpfParaListar = Console.ReadLine();
            return cpfs.FindIndex(cpf => cpf == Console.ReadLine());
        }

        static double Soma(List<double> saldos)
        {
            return saldos.Sum();
        }


        public static void Main(string[] args)
        {

            Console.Title = "Byte Bank";

            // Console.WriteLine("Digite a quantidade de usuários");
            // int quantidaDeUsuarios = int.Parse(Console.ReadLine());

            // listas ref. clientes
            List<string> cpfs = new List<string>();
            List<string> titulares = new List<string>();
            List<string> senhas = new List<string>();
            List<string> nomesSociais = new List<string>();

            //listas ref. contas
            List<string> numerosContas = new List<string>();
            List<double> saldos = new List<double>();



            int option;

            do
            {
                MenuPrincipal();
                option = int.Parse(Console.ReadLine());


                switch (option)
                {
                    case 0:
                        Console.WriteLine("Encerrando o sistema...");
                        break;
                    case 1:
                        AdicionarNovoUsuario(cpfs, titulares, senhas, nomesSociais, numerosContas, saldos);
                        break;
                    case 2:
                        DeletarUsuario(cpfs, titulares, senhas, nomesSociais, numerosContas, saldos);
                        break;
                    case 3:
                        ListarClientesCadastrados(cpfs, titulares);
                        break;
                    case 4:
                        DetalhesDoUsuario(cpfs, titulares, senhas, nomesSociais, numerosContas, saldos);
                        break;
                    case 6:
                        ShowSubmenuManutencao(cpfs, titulares, saldos);
                        break;
                }



                if (option != 0)
                {
                    Console.WriteLine();
                    Console.WriteLine("--------");
                    Console.WriteLine();
                }


            } while (option != 0);


        }


    }
}