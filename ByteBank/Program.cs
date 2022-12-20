using System;
using System.Security.Cryptography.X509Certificates;

namespace ByteBank
{
    public class ByteBank
    {
        static void showMenu()
        {
            Console.WriteLine("1 - Inserir novo usuario");
            Console.WriteLine("2 - Deletar um usuário");
            Console.WriteLine("3 - Detalhes de um usuário");
            Console.WriteLine("4 - Total armazenado no banco");
            Console.Write("Digite a opção desejada: ");
        }
        public static void Main(string[] args)
        {
            Console.WriteLine("Digite a quantidade de usuários");
            int quantidaDeUsuarios = int.Parse(Console.ReadLine());


            int option;

            do
            {
                showMenu();
                option = int.Parse(Console.ReadLine());
               
               
                switch (option)
                {
                    case 0: Console.WriteLine("Encerrando o sistema...");
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