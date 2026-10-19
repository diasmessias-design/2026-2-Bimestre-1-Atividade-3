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
            // Definir endereço IP e porta
            IPAddress enderecoIP = IPAddress.Parse("127.0.0.1");
            int porta = 5000;

            // Criar um TcpListener (equivalente ao socket servidor)
            TcpListener servidor = new TcpListener(enderecoIP, porta);

            try
            {
                // Iniciar escuta por conexões (equivalente ao bind e listen)
                servidor.Start();
                Console.WriteLine($"Servidor escutando em localhost:{porta}");

                // Aguardar por uma conexão (equivalente ao accept)
                TcpClient conexao = servidor.AcceptTcpClient();
                Console.WriteLine($"Conexão estabelecida com {conexao.Client.RemoteEndPoint}");

                // Enviar mensagem
                string mensagem = "Bem-vindo ao servidor!";
                byte[] dados = Encoding.UTF8.GetBytes(mensagem);

                using (NetworkStream stream = conexao.GetStream())
                {
                    stream.Write(dados, 0, dados.Length);
                }

                // Fechar a conexão
                conexao.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro no servidor: {ex.Message}");
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
