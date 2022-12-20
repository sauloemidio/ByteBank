using System;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;

namespace ByteBank
{
    public class ByteBank
    {
        static void showMenu(){
            Console.WriteLine("1 - Inserir novo usuario");
            Console.WriteLine("2 - Deletar um usuário");
            Console.WriteLine("3 - Listas contas registradas");
            Console.WriteLine("4 - Detalhes de um usuário");
            Console.WriteLine("5 - Total armazenado no banco");
            Console.WriteLine("6 - Manipular a conta");
            Console.WriteLine("0 - Para sair do programa");
            Console.Write("Digite a opção desejada: ");
        }

        static void AdicionarNovaConta(List<string> cpfs, List<string> titulares, List<string> senhas, List<double> saldos)
        {
            Console.Write("Digite o CPF: ");
            cpfs.Add(Console.ReadLine());
            Console.Write("Digite o nome: ");
            titulares.Add(Console.ReadLine());
            Console.Write("Digite sua senha: ");
            senhas.Add(Console.ReadLine());
            saldos.Add(0);
        }

        static void ListarContasRegistradas(List<string> cpfs, List<string> titulares, List<double> saldos)
        {
            for(int i = 0; i < cpfs.Count; i++)
            {
                Console.WriteLine($"Nome: {titulares[i]} | CPF: {cpfs[i]} | Saldo: R${saldos[i].ToString("F2")}");
            }
        }


        public static void Main(string[] args)
        {
            Console.WriteLine("Digite a quantidade de usuários");
            int quantidaDeUsuarios = int.Parse(Console.ReadLine());

            List<string> cpfs = new List<string>();
            List<string> titulares = new List<string>();
            List<string> senhas = new List<string>();
            List<double> saldos = new List<double>();


            int option;

            do
            {
                showMenu();
                option = int.Parse(Console.ReadLine());
               
               
                switch (option)
                {
                    case 0: 
                        Console.WriteLine("Encerrando o sistema...");
                        break;
                    case 1:
                        AdicionarNovaConta(cpfs, titulares, senhas, saldos);
                        break;
                    case 3:
                        ListarContasRegistradas(cpfs, titulares, saldos);
                        break;
                }

                
                
                if( option != 0)
                {
                    Console.WriteLine();
                    Console.WriteLine("--------");
                    Console.WriteLine();
                }
                

            } while (option != 0);
            
            
        }

       
    }
}