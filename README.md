# Gerenciador De Clientes

<p align="center">
<img src="http://img.shields.io/static/v1?label=STATUS&message=CONCLUIDO&color=GREEN&style=for-the-badge"/>
</p>

## 📁 Acesso ao projeto

Fazer o clone do projeto com o comando: git clone https://github.com/Rafazz04/GerenciadorDeClientes.git

## 🛠️ Abrir e rodar o projeto
**-Abra a pasta GerenciadorDeClientes**<br>
**-Abra a solução do projeto GerenciadorDeClientes.sln (Isso pelo Visual Studio)**<br>
**-Com o projeto aberto vá até a aba Ferramentas -> Geremciador de pacotes nuget -> abra o Console do gerenciador de pacotes -> Rode o comando Update-Database**<br>
**-Depois de rodar o migrations pode executar a aplicação(f5)**<br>

## 🔨 Funcionalidades do projeto
-``Cadastro de Clientes, telefones, emails e Endereços:`` Post<br>
-``Endpoint para buscar dados do cep integrado com https://viacep.com.br/:`` GetDadosDoEndereco<br>
-``Listagem de Clientes de forma paginada, telefones, emails e endereços:`` Get<br>
-``Lista um unico Cliente, lista todos os telefones que tem vinculo com esse cnpj, lista todos os emails que tem vinculo com esse cnpjm, e todos os endereços que tem vinculo com esse cnpj:`` GetByCnpj()<br>
-``Atualizar lista de Clientes:`` Put<br>
-``Deletar Clientes, telefones, emails e endereços:`` Delete<br>

## 👨🏻‍💻 Abordagens Técnicas

### Clean Architecture
Adotei a **Clean Architecture** como arquitetura do projeto, visando garantir maior organização e manutenibilidade a longo prazo. As vantagens incluem:

- **Separação de responsabilidades**: Cada camada opera de forma independente, permitindo que mudanças em uma não afetem as demais, promovendo modularidade e segurança.
- **Facilidade de teste**: A divisão clara das camadas facilita a realização de testes isolados, tanto para as regras de negócio quanto para a infraestrutura, garantindo maior confiabilidade.
- **Flexibilidade**: A arquitetura permite trocas simples de implementações, como a substituição de banco de dados ou a integração com novos serviços, sem comprometer a lógica central.

### Repository Pattern e Unit of Work
Implementei o **Repository Pattern** junto ao **Unit of Work** para gerenciar as operações com o banco de dados. As vantagens são:

- **Abstração no acesso a dados**: O código de acesso ao banco de dados é desacoplado da lógica de negócio, facilitando mudanças no sistema de armazenamento.
- **Gerenciamento eficiente de transações**: O **Unit of Work** assegura que todas as operações sejam tratadas como uma única transação, evitando inconsistências e garantindo a integridade dos dados.

### Integração com a API Via Cep
Para otimizar o cadastro de endereços, integrei o sistema à **API Via Cep**, permitindo a busca automática de dados a partir do CEP informado. Isso acelera o processo de cadastro e melhora a precisão das informações inseridas. Também no cadastro de cliente (post), quando voce digita o cep, ele pega o restante dos dados com essa Api externa e armazena esses dados na tabela de Endereço 

### Code First
Utilizei a abordagem **Code First** com o **Entity Framework**, gerando o banco de dados a partir dos modelos de domínio. O uso de **migrations** permite o controle versionado da evolução do banco de dados, facilitando a sincronização entre diferentes ambientes.

### Injeção de Dependência e Inversão de Controle (IoC)
Adotei os padrões de **Injeção de Dependência** e **Inversão de Controle (IoC)**, que proporcionam:

- **Desacoplamento de componentes**: As dependências são injetadas dinamicamente, permitindo fácil substituição sem grandes alterações no código.
- **Facilidade na manutenção e teste**: A injeção de dependências permite simular serviços e repositórios, melhorando a eficiência dos testes e a agilidade na manutenção.

### Validators
Para garantir a validação eficiente dos dados de entrada, implementei a biblioteca **FluentValidation**. Essa abordagem permite criar regras de validação de maneira fluida e expressiva, facilitando a leitura e manutenção do código.


## ✔️ Técnicas e Tecnologias utilizadas

- ``.Net 8``
- ``Enity Framework``
- ``Clean Architecture``
- ``Injeção de dependência``
- ``Inversão de controle``
- ``Repository Pattern``
- ``Unit of Work``
- ``Code-First``
- ``AutoMapper``
- ``FluentValidation``
- ``Integração com api externa``

