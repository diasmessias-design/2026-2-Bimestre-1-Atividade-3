using System;
using System.Net.Sockets;
using System.Text;

namespace AtividadeComunicação
{
    public static class ClienteEcho
    {
        public static void Principal()
        {
            string ipServidor = "127.0.0.1";
            int porta = 5000;

            try
            {
                TcpClient cliente = new TcpClient(ipServidor, porta);
                Console.WriteLine("Conectado ao Servidor Echo!");

                using (NetworkStream stream = cliente.GetStream())
                {
                    Console.Write("Digite uma mensagem para enviar ao servidor: ");
                    string mensagem = Console.ReadLine();
                    
                    if (string.IsNullOrEmpty(mensagem))
                    {
                        mensagem = "Olá, Servidor Echo!";
                    }

                    byte[] dadosEnvio = Encoding.UTF8.GetBytes(mensagem);
                    stream.Write(dadosEnvio, 0, dadosEnvio.Length);

                    byte[] buffer = new byte[1024];
                    int bytesLidos = stream.Read(buffer, 0, buffer.Length);
                    string resposta = Encoding.UTF8.GetString(buffer, 0, bytesLidos);

                    Console.WriteLine($"Echo recebido do servidor: {resposta}");
                }

                cliente.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro no cliente Echo: {ex.Message}");
            }
        }
    }
}