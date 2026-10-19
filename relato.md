# Relatório sobre implementação de comunicação entre tarefas em C#

## Introdução

Este relato faz parte do processo avaliativo da disciplina de Sistemas Operacionais no curso superior em Análise e Desenvolvimento de Sistemas, ofertado pela Diretoria Acadêmica de Gestão e Tecnologia da Informação do Campus Natal-Central do Instituto Federal de Educação, Ciência e Tecnologia do Rio Grande do Norte.

Tem como objetivo principal relatar as implementações de comunicação entre tarefas na linguagem C#.

O grupo de trabalho foi formado por Álvaro Luiz Barbalho de Souza Filho<br>Paulo Cesar Moreira da Silva<br>Pedro Messias Dias Neto

## Comunicação entre tarefas em C#

### Informações gerais

A comunicação entre tarefas tem como objetivo permitir que diferentes fluxos de execução troquem dados e coordenem suas atividades durante a execução de um programa. Neste trabalho, foram abordadas duas formas de comunicação: o compartilhamento de dados entre threads pertencentes ao mesmo processo, utilizando o modelo produtor-consumidor, e a comunicação entre processos por meio de sockets TCP.

Para a implementação foi utilizada a linguagem C#, com execução do projeto em containers Docker utilizando a imagem do .NET SDK 10. O Docker foi utilizado para fornecer um ambiente isolado e reproduzível para compilação e execução dos programas.

A configuração utilizada para execução foi baseada em um arquivo `Dockerfile`, responsável por copiar o projeto C# para o container, realizar sua compilação e executar a aplicação. O projeto também passou a contar com um menu interativo, permitindo selecionar as diferentes implementações desenvolvidas.

### Comunicação entre tarefas com linhas de execução no mesmo processo

A implementação utiliza duas linhas de execução (threads) dentro do mesmo processo: uma thread produtora e uma thread consumidora. Ambas compartilham o vetor `dados`, que é utilizado para realizar a comunicação entre as tarefas.

O método `ProduzirDados()` cria um vetor com 100 posições e o preenche com números pseudoaleatórios entre 0 e 110. O método `ConsumirDados()` acessa esse mesmo vetor e utiliza o método `Sum()` para calcular a soma dos valores produzidos.

No método `Principal()`, são criadas duas instâncias da classe `Thread`. A primeira recebe o método `ProduzirDados` e a segunda recebe o método `ConsumirDados`.

As chamadas `Start()` iniciam a execução das duas threads. Como elas pertencem ao mesmo processo, ambas possuem acesso ao vetor `dados`, permitindo o compartilhamento das informações produzidas.

Após iniciar as threads, foram utilizadas chamadas ao método `Join()`. O `Join()` faz com que a thread que executa o método `Principal()` aguarde o término das threads produtora e consumidora antes de continuar. Dessa forma, a mensagem `finalizou` somente é exibida depois que ambas terminam sua execução.

É importante observar que os dois `Join()` utilizados após os dois `Start()` garantem que o método `Principal()` aguarde o término das threads, mas não estabelecem, por si só, que a thread consumidora somente possa iniciar após a conclusão da thread produtora.

```csharp
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
```
A aplicação foi executada utilizando Docker. Foi criado um arquivo `Dockerfile` baseado na imagem do .NET SDK 10. O projeto C# foi copiado para o diretório `/app` do container e compilado utilizando `dotnet build`.

A imagem Docker da aplicação foi construída com o comando:

```bash
docker build -t atividade3 .
```
Após a construção da imagem, a aplicação foi executada com o comando:

```bash
docker run -it --rm atividade3
```
Para realizar os testes de comunicação utilizando sockets TCP, foram utilizadas duas sessões de terminal conectadas ao mesmo container Docker. Em uma delas, foi iniciado o servidor, responsável por aguardar as conexões, enquanto na outra foi executado o cliente, responsável por estabelecer a conexão e realizar a troca de mensagens.

Esse procedimento foi utilizado nas implementações Básico, Echo e Chat, permitindo observar o envio e recebimento de dados entre os processos, além do encerramento das conexões.

### Comunicação entre tarefas em processos diferentes no mesmo computador

Nesta etapa, foram implementados exemplos de comunicação entre processos utilizando sockets TCP na linguagem C#. Diferentemente da comunicação entre threads, que compartilham a memória de um mesmo processo, os sockets permitem a troca de informações entre aplicações por meio de conexões de rede.

Foram desenvolvidas três modalidades de comunicação: Básico, Echo e Chat. Todas utilizaram o endereço de loopback 127.0.0.1 e a porta 5000. Os testes foram realizados no ambiente Docker, utilizando dois processos independentes para representar o cliente e o servidor.

#### Servidor e Cliente Básico

A primeira implementação teve como objetivo estabelecer uma conexão TCP e realizar o envio de uma mensagem do servidor para o cliente.

O servidor utiliza a classe `TcpListener` para aguardar conexões na porta 5000. Quando um cliente estabelece uma conexão, o servidor envia a mensagem "Bem-vindo ao servidor!", utilizando um `NetworkStream`.

O cliente, por sua vez, utiliza a classe `TcpClient` para estabelecer a conexão, receber os dados e convertê-los em texto utilizando a codificação UTF-8.

Durante a execução no Docker, foi possível observar que o servidor recebeu a conexão e que o cliente apresentou corretamente a mensagem enviada. Após a comunicação, a conexão foi encerrada.

#### Servidor e Cliente Echo

A segunda implementação utilizou o modelo Echo, no qual o servidor recebe uma mensagem do cliente e devolve os mesmos dados.

Diferentemente da implementação básica, a comunicação ocorre nos dois sentidos: o cliente envia uma mensagem e aguarda a resposta do servidor.

Para realizar a comunicação, foram utilizados os métodos Read() e Write() da classe NetworkStream, responsáveis pela leitura e escrita dos dados transmitidos.

Durante o teste, o cliente enviou a mensagem "ola", que foi recebida pelo servidor e devolvida sem alterações. O resultado demonstrou o funcionamento da comunicação bidirecional utilizando sockets TCP.

#### Servidor e Cliente Chat

A terceira implementação teve como objetivo permitir a troca de várias mensagens entre cliente e servidor durante uma mesma conexão TCP.

Para isso, foram utilizados laços de repetição `while`, que mantêm a comunicação ativa enquanto os participantes não solicitam seu encerramento.

O funcionamento ocorre de forma alternada: o cliente envia uma mensagem, o servidor recebe e digita uma resposta, que é enviada de volta ao cliente. Esse processo continua até que um dos participantes digite o comando `sair`.

Durante a execução no Docker, foram realizadas trocas de mensagens entre os dois programas. O cliente enviou inicialmente "olá servidor", recebendo como resposta "olá cliente". Em seguida, enviou "Tudo funcionando?", recebendo a resposta "Sim, comunicação funcionando".

Por fim, o cliente enviou o comando `sair`, que foi reconhecido pelo servidor. A conexão foi encerrada e ambos os programas finalizaram normalmente.

Os resultados demonstraram a possibilidade de manter uma conexão TCP ativa para realizar múltiplas trocas de mensagens entre processos.

### Comunicação entre tarefas em processos diferentes em computadores diferentes


As implementações desenvolvidas utilizaram o endereço de loopback `127.0.0.1`, permitindo realizar os testes de comunicação entre cliente e servidor no mesmo ambiente de rede.

Embora os sockets TCP também permitam estabelecer comunicação entre computadores diferentes, essa modalidade não foi testada nesta etapa da atividade.

Para realizar esse tipo de comunicação, seria necessário configurar o servidor para aceitar conexões por uma interface de rede acessível a outros computadores, além de utilizar o endereço IP correspondente no cliente e garantir a conectividade entre os ambientes.

## Considerações finais 

A atividade permitiu implementar e testar diferentes formas de comunicação entre tarefas utilizando a linguagem C# e o ambiente Docker.

Na primeira parte, foi explorada a comunicação entre threads pertencentes ao mesmo processo, utilizando o modelo produtor-consumidor. Essa implementação permitiu observar o compartilhamento de dados e compreender melhor o funcionamento dos métodos `Start()` e `Join()`.

Na segunda parte, foram desenvolvidas implementações de comunicação entre processos utilizando sockets TCP, por meio dos exemplos Básico, Echo e Chat. Os testes permitiram observar o estabelecimento de conexões, o envio e recebimento de mensagens e o encerramento da comunicação.

A implementação do Chat também demonstrou como uma conexão pode ser mantida durante várias trocas de mensagens, permitindo uma interação entre cliente e servidor.

Além dos conceitos de comunicação entre tarefas, a atividade contribuiu para o aprendizado sobre a utilização do Docker como ambiente de execução e sobre a organização de aplicações C# que utilizam recursos de rede.