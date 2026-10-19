using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace AtividadeComunicação
{
    public static class ServidorChat
    {
        public static void Principal()
        {
            IPAddress enderecoIP = IPAddress.Parse("127.0.0.1");
            int porta = 5000;

            TcpListener servidor = new TcpListener(enderecoIP, porta);

            try
            {
                servidor.Start();
                Console.WriteLine($"Servidor Chat a escutar em localhost:{porta}");
                Console.WriteLine("A aguardar ligação do cliente...");

                TcpClient conexao = servidor.AcceptTcpClient();
                Console.WriteLine($"Cliente conectado: {conexao.Client.RemoteEndPoint}");
                Console.WriteLine("--- Início do Chat (digite 'sair' para encerrar) ---");

                using (NetworkStream stream = conexao.GetStream())
                {
                    byte[] buffer = new byte[1024];

                    while (true)
                    {
                        // Receber mensagem do cliente
                        int bytesLidos = stream.Read(buffer, 0, buffer.Length);
                        if (bytesLidos == 0) break;

                        string mensagemCliente = Encoding.UTF8.GetString(buffer, 0, bytesLidos);
                        Console.WriteLine($"\nCliente: {mensagemCliente}");

                        if (mensagemCliente.Trim().ToLower() == "sair")
                        {
                            Console.WriteLine("O cliente encerrou o chat.");
                            break;
                        }

                        // Enviar resposta do servidor
                        Console.Write("Servidor: ");
                        string respostaServidor = Console.ReadLine();
                        
                        byte[] dadosResposta = Encoding.UTF8.GetBytes(respostaServidor);
                        stream.Write(dadosResposta, 0, dadosResposta.Length);

                        if (respostaServidor.Trim().ToLower() == "sair")
                        {
                            Console.WriteLine("Encerrando o chat pelo servidor...");
                            break;
                        }
                    }
                }

                conexao.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro no servidor Chat: {ex.Message}");
            }
            finally
            {
                servidor.Stop();
                Console.WriteLine("Servidor Chat encerrado.");
            }
        }
    }
}