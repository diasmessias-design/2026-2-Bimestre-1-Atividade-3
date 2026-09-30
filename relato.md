# Relatório sobre implementação de comunicação entre tarefas em C#

## Introdução

Este relato faz parte do processo avaliativo da disciplina de Sistemas Operacionais no curso superior em Análise e Desenvolvimento de Sistemas, ofertado pela Diretoria Acadêmica de Gestão e Tecnologia da Informação do Campus Natal-Central do Instituto Federal de Educação, Ciência e Tecnologia do Rio Grande do Norte.

Tem como objetivo principal relatar as implementações de comunicação entre tarefas na linguagem C#.

O grupo de trabalho foi formado por Álvaro Luiz Barbalho de Souza Filho<br>Paulo Cesar Moreira da Silva<br>Pedro Messias Dias Neto

## Comunicação entre tarefas em C#

### Informações gerais

A comunicação entre tarefas tem como objetivo permitir que diferentes fluxos de execução troquem dados e coordenem suas atividades durante a execução de um programa. Neste trabalho, foi utilizado o modelo produtor-consumidor, no qual uma tarefa é responsável por produzir os dados e outra por consumi-los. 

Para a implementação foi utilizada a linguagem C#, com execução do projeto em um container Docker utilizando a imagem do .NET SDK 10. O Docker foi utilizado para fornecer um ambiente isolado e reproduzível para compilação e execução do programa.

A configuração utilizada para execução foi baseada em um arquivo `Dockerfile`, responsável por copiar o projeto C# para o container, realizar sua compilação e executar a aplicação.

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
docker build -t atividade-so .
```
Após a construção da imagem, a aplicação foi executada com o comando:

```bash
docker run atividade-so
```


### Comunicação entre tarefas em processos diferentes no mesmo computador

Implementação ainda não realizada nesta etapa da atividade

### Comunicação entre tarefas em processos diferentes em computadores diferentes

Implementação ainda não realizada nesta etapa da atividade

## Considerações finais da primeira parte

A implementação desenvolvida permitiu executar e testar a comunicação entre tarefas utilizando linhas de execução no mesmo processo. O código foi executado em um container Docker e foi possível observar o funcionamento das threads produtora e consumidora e o compartilhamento dos dados entre elas.

Durante a atividade foi possível compreender melhor a diferença entre execução sequencial e concorrente, além do funcionamento dos métodos `Start()` e `Join()`. O uso de `Join()` permitiu fazer com que a thread principal aguardasse o término das threads produtora e consumidora antes de finalizar sua execução.

Também foi possível aprender na prática o processo de configuração e utilização do Docker para executar uma aplicação C#. 