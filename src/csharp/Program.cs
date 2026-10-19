using System;

namespace AtividadeComunicação
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("==================================================");
                Console.WriteLine("   PAINEL DE EXECUÇÃO - ATIVIDADE DE COMUNICAÇÃO   ");
                Console.WriteLine("==================================================");
                Console.WriteLine("1 - Executar Programação Sequencial");
                Console.WriteLine("2 - Executar Produtor / Consumidor");
                Console.WriteLine("3 - Executar Servidor Básico (Sockets)");
                Console.WriteLine("4 - Executar Cliente Básico (Sockets)");
                Console.WriteLine("5 - Executar Servidor Echo (Sockets)");
                Console.WriteLine("6 - Executar Cliente Echo (Sockets)");
                Console.WriteLine("7 - Executar Servidor Chat (Sockets)");
                Console.WriteLine("8 - Executar Cliente Chat (Sockets)");
                Console.WriteLine("0 - Sair");
                Console.WriteLine("==================================================");
                Console.Write("Digite a opção desejada: ");

                string opcao = Console.ReadLine();

                Console.WriteLine("\n--------------------------------------------------");
                switch (opcao)
                {
                    case "1":
                        Console.WriteLine("=== EXECUTANDO SEQUENCIAL ===\n");
                        Sequencial.Principal();
                        Pausar();
                        break;
                    case "2":
                        Console.WriteLine("=== EXECUTANDO PRODUTOR / CONSUMIDOR ===\n");
                        ProdutorConsumidor.Principal();
                        Pausar();
                        break;
                    case "3":
                        ServidorBasico.Principal();
                        Pausar();
                        break;
                    case "4":
                        ClienteBasico.Principal();
                        Pausar();
                        break;
                    case "5":
                        ServidorEcho.Principal();
                        Pausar();
                        break;
                    case "6":
                        ClienteEcho.Principal();
                        Pausar();
                        break;
                    case "7":
                        ServidorChat.Principal();
                        Pausar();
                        break;
                    case "8":
                        ClienteChat.Principal();
                        Pausar();
                        break;
                    case "0":
                        Console.WriteLine("Saindo do programa...");
                        return;
                    default:
                        Console.WriteLine("Opção inválida! Tente novamente.");
                        Pausar();
                        break;
                }
            }
        }

        static void Pausar()
        {
            Console.WriteLine("\nPressione ENTER para voltar ao menu principal...");
            Console.ReadLine();
        }
    }
}