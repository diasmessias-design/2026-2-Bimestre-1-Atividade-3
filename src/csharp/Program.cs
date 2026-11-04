
using System;

namespace AtividadeComunicação
{
    class Program
    {
        static void Main(string[] args)
        {
            // Execução direta pelo Docker Compose
            if (args.Length > 0)
            {
                ExecutarOpcao(args[0]);
                return;
            }

            // Execução interativa pelo terminal
            while (true)
            {
                Console.Clear();

                Console.WriteLine("==============================================");
                Console.WriteLine(" PAINEL DE EXECUÇÃO - ATIVIDADE DE COMUNICAÇÃO");
                Console.WriteLine("==============================================");
                Console.WriteLine("1 - Executar Programação Sequencial");
                Console.WriteLine("2 - Executar Produtor / Consumidor");
                Console.WriteLine("3 - Executar Servidor Básico (Sockets)");
                Console.WriteLine("4 - Executar Cliente Básico (Sockets)");
                Console.WriteLine("5 - Executar Servidor Echo (Sockets)");
                Console.WriteLine("6 - Executar Cliente Echo (Sockets)");
                Console.WriteLine("7 - Executar Servidor Chat (Sockets)");
                Console.WriteLine("8 - Executar Cliente Chat (Sockets)");
                Console.WriteLine("0 - Sair");
                Console.WriteLine("==============================================");

                Console.Write("Digite a opção desejada: ");
                string? opcao = Console.ReadLine();

                if (opcao == null || opcao == "0")
                    return;

                ExecutarOpcao(opcao);

                Console.WriteLine("\nPressione ENTER para voltar ao menu...");
                if (Console.ReadLine() == null)
                    return;
            }
        }

        static void ExecutarOpcao(string opcao)
        {
            switch (opcao)
            {
                case "1":
                    Sequencial.Principal();
                    break;

                case "2":
                    ProdutorConsumidor.Principal();
                    break;

                case "3":
                    ServidorBasico.Principal();
                    break;

                case "4":
                    ClienteBasico.Principal();
                    break;

                case "5":
                    ServidorEcho.Principal();
                    break;

                case "6":
                    ClienteEcho.Principal();
                    break;

                case "7":
                    ServidorChat.Principal();
                    break;

                case "8":
                    ClienteChat.Principal();
                    break;

                case "0":
                    break;

                default:
                    Console.WriteLine("Opção inválida!");
                    break;
            }
        }
    }
}
