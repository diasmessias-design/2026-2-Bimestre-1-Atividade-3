
using System;
using System.Net.Sockets;
using System.Text;

namespace AtividadeComunicação
{
    public static class ClienteBasico
    {
        public static void Principal()
        {
            // Obter o endereço do servidor pelo ambiente
            // Se não existir, utilizar localhost
            string ipServidor =
                Environment.GetEnvironmentVariable("SERVER_HOST")
                ?? "127.0.0.1";

            int porta = 5000;

            try
            {
                // Criar conexão com o servidor
                using (TcpClient cliente = new TcpClient(ipServidor, porta))
                {
                    Console.WriteLine("Conectado ao servidor!");

                    // Obter o fluxo de comunicação TCP
                    using (NetworkStream stream = cliente.GetStream())
                    {
                        // Preparar buffer para receber dados
                        byte[] buffer = new byte[1024];

                        // Receber mensagem do servidor
                        int bytesLidos = stream.Read(
                            buffer, 0, buffer.Length
                        );

                        // Converter os bytes recebidos em texto
                        string mensagem = Encoding.UTF8.GetString(
                            buffer, 0, bytesLidos
                        );

                        // Exibir mensagem recebida
                        Console.WriteLine($"Recebido: {mensagem}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(
                    $"Erro no cliente: {ex.Message}"
                );
            }
        }
    }
}
