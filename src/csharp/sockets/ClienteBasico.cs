using System;
using System.Net.Sockets;
using System.Text;

namespace AtividadeComunicação
{
    public static class ClienteBasico
    {
        public static void Principal()
        {
            string ipServidor = "127.0.0.1";
            int porta = 5000;

            try
            {
                // Criar conexão com o servidor
                TcpClient cliente = new TcpClient(ipServidor, porta);
                Console.WriteLine("Conectado ao servidor!");

                // Receber mensagem
                using (NetworkStream stream = cliente.GetStream())
                {
                    byte[] buffer = new byte[1024];
                    int bytesLidos = stream.Read(buffer, 0, buffer.Length);
                    string mensagem = Encoding.UTF8.GetString(buffer, 0, bytesLidos);

                    Console.WriteLine($"Recebido: {mensagem}");
                }

                // Fechar a conexão
                cliente.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro no cliente: {ex.Message}");
            }
        }
    }
}