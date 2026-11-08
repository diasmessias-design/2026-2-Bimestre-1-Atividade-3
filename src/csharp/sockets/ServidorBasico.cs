

using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace AtividadeComunicação
{
    public static class ServidorBasico
    {
        public static void Principal()
        {
            string host = Environment.GetEnvironmentVariable("SERVER_HOST") ?? "127.0.0.1";
            IPAddress enderecoIP = IPAddress.Parse(host);
            int porta = 5000;

            TcpListener servidor = new TcpListener(enderecoIP, porta);

            try
            {
                servidor.Start();
                Console.WriteLine($"Servidor escutando em {enderecoIP}:{porta}");

                using (TcpClient cliente = servidor.AcceptTcpClient())
                {
                    Console.WriteLine($"Cliente conectado: {cliente.Client.RemoteEndPoint}");

                    using (NetworkStream stream = cliente.GetStream())
                    {
                        string mensagem = "Bem-vindo ao servidor!";
                        byte[] dados = Encoding.UTF8.GetBytes(mensagem);

                        stream.Write(dados, 0, dados.Length);

                        Console.WriteLine($"Mensagem enviada: {mensagem}");
                    }
                }

                Console.WriteLine("Servidor encerrado.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro no servidor: {ex.Message}");
            }
            finally
            {
                servidor.Stop();
            }
        }
    }
}
