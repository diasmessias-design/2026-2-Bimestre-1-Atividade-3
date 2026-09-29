using System;

namespace AtividadeComunicação
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== EXECUTANDO SEQUENCIAL ===");
            Sequencial.Principal();

            Console.WriteLine("\n=== EXECUTANDO PRODUTOR / CONSUMIDOR ===");
            ProdutorConsumidor.Principal();
        }
    }
}