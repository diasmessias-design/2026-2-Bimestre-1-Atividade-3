using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace AtividadeComunicação
{
    public static class ServidorEcho
    {
        public static void Principal()
        {
            IPAddress enderecoIP = IPAddress.Parse("127.0.0.1");
            int porta = 5000;

            TcpListener servidor = new TcpListener(enderecoIP, porta);

            try
            {
                servidor.Start();
                Console.WriteLine($"Servidor Echo a escutar em localhost:{porta}");

                TcpClient conexao = servidor.AcceptTcpClient();
                Console.WriteLine($"Conexão estabelecida com {conexao.Client.RemoteEndPoint}");

                using (NetworkStream stream = conexao.GetStream())
                {
                    byte[] buffer = new byte[1024];
                    int bytesLidos;

                    // Lê os dados enviados pelo cliente e devolve exatamente o mesmo (Echo)
                    while ((bytesLidos = stream.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        string mensagem = Encoding.UTF8.GetString(buffer, 0, bytesLidos);
                        Console.WriteLine($"Recebido: {mensagem}");

                        // Envia a mensagem de volta para o cliente
                        stream.Write(buffer, 0, bytesLidos);
                    }
                }

                conexao.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro no servidor Echo: {ex.Message}");
            }
            finally
            {
                servidor.Stop();
                Console.WriteLine("Servidor Echo encerrado.");
            }
        }
    }
}