using System;

namespace AtividadeComunicação
{
    class SocketsProgram
    {
        // Se quiseres testar só os sockets via terminal, podes renomear este método para Main
        // ou chamá-lo a partir do Program.cs principal quando precisares.
        public static void ExecutarSockets()
        {
            Console.WriteLine("========================================");
            Console.WriteLine("  Atividade de Sockets TCP em C#");
            Console.WriteLine("========================================");
            Console.WriteLine("Escolha o modulo que deseja executar:");
            Console.WriteLine("1 - Servidor Básico");
            Console.WriteLine("2 - Cliente Básico");
            Console.WriteLine("3 - Servidor Echo");
            Console.WriteLine("4 - Cliente Echo");
            Console.WriteLine("5 - Servidor Chat");
            Console.WriteLine("6 - Cliente Chat");
            Console.Write("\nDigite a opção desejada: ");

            string opcao = Console.ReadLine();

            switch (opcao)
            {
                case "1":
                    ServidorBasico.Principal();
                    break;
                case "2":
                    ClienteBasico.Principal();
                    break;
                case "3":
                    ServidorEcho.Principal();
                    break;
                case "4":
                    ClienteEcho.Principal();
                    break;
                case "5":
                    ServidorChat.Principal();
                    break;
                case "6":
                    ClienteChat.Principal();
                    break;
                default:
                    Console.WriteLine("Opção inválida!");
                    break;
            }
        }
    }
}