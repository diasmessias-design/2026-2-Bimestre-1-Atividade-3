using System;
using System.Linq;

namespace AtividadeComunicação
{
    public static class Sequencial
    {
        private static int[] dados = new int[0];
        private static readonly Random random = new Random();

        public static void ProduzirDados()
        {
            Console.WriteLine("# produzir - iniciado");
            
            dados = new int[100];
            for (int i = 0; i < dados.Length; i++)
            {
                dados[i] = random.Next(0, 111);
            }

            Console.WriteLine($"# produzir [{string.Join(", ", dados)}]");
            Console.WriteLine("# produzir - terminado");
        }

        public static void ConsumirDados()
        {
            Console.WriteLine("### consumir - iniciado");
            Console.WriteLine($"### dados -> [{string.Join(", ", dados)}]");
            
            int resultado = dados.Sum();
            
            Console.WriteLine($"### resultado -> {resultado}");
            Console.WriteLine("### consumir - terminado");
        }

        public static void Principal()
        {
            Console.WriteLine("iniciou");

            ProduzirDados();
            ConsumirDados();

            Console.WriteLine("finalizou");
        }
    }
}