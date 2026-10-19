using System;
using System.Net.Sockets;
using System.Text;

namespace AtividadeComunicação
{
    public static class ClienteChat
    {
        public static void Principal()
        {
            string ipServidor = "127.0.0.1";
            int porta = 5000;

            try
            {
                TcpClient cliente = new TcpClient(ipServidor, porta);
                Console.WriteLine("Conectado ao Servidor Chat!");
                Console.WriteLine("--- Início do Chat (digite 'sair' para encerrar) ---");

                using (NetworkStream stream = cliente.GetStream())
                {
                    byte[] buffer = new byte[1024];

                    while (true)
                    {
                        // Enviar mensagem para o servidor
                        Console.Write("Cliente: ");
                        string mensagemCliente = Console.ReadLine();
                        
                        if (string.IsNullOrEmpty(mensagemCliente))
                        {
                            mensagemCliente = "Olá!";
                        }

                        byte[] dadosEnvio = Encoding.UTF8.GetBytes(mensagemCliente);
                        stream.Write(dadosEnvio, 0, dadosEnvio.Length);

                        if (mensagemCliente.Trim().ToLower() == "sair")
                        {
                            Console.WriteLine("Encerrando o chat pelo cliente...");
                            break;
                        }

                        // Receber resposta do servidor
                        int bytesLidos = stream.Read(buffer, 0, buffer.Length);
                        if (bytesLidos == 0) break;

                        string respostaServidor = Encoding.UTF8.GetString(buffer, 0, bytesLidos);
                        Console.WriteLine($"\nServidor: {respostaServidor}");

                        if (respostaServidor.Trim().ToLower() == "sair")
                        {
                            Console.WriteLine("O servidor encerrou o chat.");
                            break;
                        }
                    }
                }

                cliente.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro no cliente Chat: {ex.Message}");
            }
        }
    }
}