
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
            // Obter o endereço IP configurado no ambiente
            // Se não existir, utilizar localhost
            string ip = Environment.GetEnvironmentVariable("SERVER_HOST")
                        ?? "127.0.0.1";

            IPAddress enderecoIP = IPAddress.Parse(ip);
            int porta = 5000;

            // Criar o servidor TCP
            TcpListener servidor = new TcpListener(enderecoIP, porta);

            try
            {
                // Iniciar a escuta por conexões
                servidor.Start();
                Console.WriteLine(
                    $"Servidor escutando em {enderecoIP}:{porta}"
                );

                // Aguardar conexão de um cliente
                TcpClient conexao = servidor.AcceptTcpClient();

                Console.WriteLine(
                    $"Conexão estabelecida com {conexao.Client.RemoteEndPoint}"
                );

                // Preparar a mensagem
                string mensagem = "Bem-vindo ao servidor!";
                byte[] dados = Encoding.UTF8.GetBytes(mensagem);

                // Enviar mensagem ao cliente
                using (NetworkStream stream = conexao.GetStream())
                {
                    stream.Write(dados, 0, dados.Length);
                }

                // Fechar a conexão
                conexao.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(
                    $"Erro no servidor: {ex.Message}"
                );
            }
            finally
            {
                // Encerrar o servidor
                servidor.Stop();
                Console.WriteLine("Servidor encerrado");
            }
        }
    }
}
