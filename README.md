# Datlo - Sistema de Importação de arquivo CSV

Esta aplicação foi construida utilizando arquitetura hexagonal voltada para casos de uso, após a conversa inicial que tivemos, cheguei a conclusão que esse padrão de projeto pode
ser de extrema importancia para o futuro da Datlo, pois proporciona desacoplação com tecnologias externas, oq pode ser de grande utilidade para mudanças que provavelmente
acontecem no desenvolvimento de uma startup. Da forma com que este projeto esta estruturado, ao mudar a tecnologia de banco de dados ou mensageria, somente a camada de persistencia
seria **incrementada**. A aplicação e regras de negocios nao exigiria alteração.

## Estrutura:

A aplicação possui as seguintes hierarquias:
* **Adapter**:
    * **In**: Ponta do projeto onde serão recebidas as chamadas externas, no projeto consta a pasta controller que guarda o endpoint ImportController, que ficara responsavel por receber o arquivo de importação do usuario
    * **Out**: Implementa as interfaces de saida Port Out definidas na camada de aplicação, guarda a implementação do CsvProcessor
    * **Persistence**: Responsavel pelas classes de persistencia, como repositorios e a customização do DataReader

* **Application**:
    * **Ports**:
        * **In**: Interfaces dos casos de uso.
        * **Out**: Reponsavel pelas interfaces que serão usadas pela aplicação e implementadas pelo Adapter
        * **Usecases**: Camada responsavel pela regra de negocio do projeto, guarda a implementação dos casos de uso.

* **Configuration**:
    * **Database**: Configuracoes da aplicação, nesse caso configurada com o DbContext
    
* **Domain**:
    * **Entities**: Entidades de dominio.


## Como lidar com arquivos grandes:
Para lidar com um arquivo de importação grande utilizei um custom DataReader, para evitar de carregar o datatable completo na memória e permitir que o SqlBulkCopy leia os dados em Streaming e por lotes.

## Escalabilidade:
Em um projeto real, em que realmente receba arquivos muito grandes eu optaria pela configuração de uma mensageria utilizando Kafka, configurando-o enviar estas mensagens em lotes para os topicos atraves de um timeout,
assim garantindo a escalabilidade de varios workers para fazer a leitura dessas mensagens, evitando o lock da thread e podendo ser escalavel ao alocar mais ou menos workers conforme necessario.

## Testes:
Para testes utilizei a biblioteca de Nunit para testes unitarios e o NSubstitute para mock de dados, para testes integrados poderia ser configurado um TestContainer juntamente com o docker.


