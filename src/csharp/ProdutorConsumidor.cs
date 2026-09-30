using System;
using System.Linq;
using System.Threading;

namespace AtividadeComunicação
{
    public static class ProdutorConsumidor
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

            Thread threadProdutor = new Thread(ProduzirDados);
            Thread threadConsumidor = new Thread(ConsumirDados);

            threadProdutor.Start();
            threadConsumidor.Start();
            threadProdutor.Join();
            threadConsumidor.Join();

            Console.WriteLine("finalizou");
        }
    }
}