# Seja bem-vindo ao nosso repositório do Projeto `TryitterRD`!


Projeto realizado por:


Diogo Martini Pantaleão

https://github.com/ogoiddev


Rodrigor de Oliveira

https://github.com/rodrigormjo


Este projeto tem por finalidade apresentar todo aprendizado realizado na Turma de Aceleração de C# 09 com a parceria oferecida pela Wiz e Trybe.


Nossos aprendizados e trabalho em equipe refletem nesta aplicação, apresentada inicialmente por este documento para dar instruções para que qualquer pessoa possa ver e entender como a aplicação funciona e pode ser utilizada.


Aqui você vai encontrar os detalhes e requisitos de como instalar e estruturar a aplicação à partir deste repositório.


# O que é o  TryitterRD?

É uma rede social totalmente baseada em texto para estudantes da Trybe. O objetivo é proporcionar um ambiente em que as pessoas poderão, por meio de textos e imagens, compartilhar suas experiências e também acessar posts que possam contribuir para seu aprendizado.

Este repositório apresenta uma API e um banco de dados desenvolvidos especialmente para atender as demandas Back-end do TryitterRD, enviando e recebendo requisições e respostas para diversos usuários da rede social.

# Requisitos iniciais


Para que seja possível instalar o TryitterRD na sua máquina em modo de desenvolvimento, é necessário instalar estes programas na sua estação de trabalho:


- Microsoft.AspNetCore.App 6.0.13 ou superior.

- Microsoft.NETCore.App 6.0.13 ou superior.

- Micrsoft SQL Server 2019 ou superior.

- Postman v10 ou superior.

- Editor de código fonte para C# (Recomendado VS Code ou Visual Studio)


# Orientações

<details>
  <summary><strong>Antes de começar...</strong></summary><br />

  1. Clone o repositório, use o comando:
  ```
  git clone git@github.com:ogoiddev/TryitterRD-Acc-CSharp-Deploy-Azure.git
  ```
  
  2. Entre na pasta do repositório que você acabou de clonar:
  ```
  cd TryitterRD-Acc-CSharp-Deploy-Azure
  ```  

  3. Instale as dependências, entre na pasta `src/` e execute o comando:
  ```
  dotnet restore
  ```
  
  Pronto o TryitterRD já está instalado à sua estação de trabalho! Agora só falta estabelecer a conexão com o banco de dados para começar a utilizar e testar.
  
</details>

</details>

<details>
  <summary><strong>Rodando os testes do TryitterRD</strong></summary><br/>
  
  ```
  dotnet test
  ```
  
</details>

<details>
  <summary><strong>Tipos de requisição da API</strong></summary><br />
  
  - POST /Login - ENDPOINT para realizar o login do usuário no Tryitter.
  
  
  
  - GET /api/User/{id} - ENPOINT para buscar por um usuário específico do Tryitter.
  - DELETE /api/User - ENPOINT para deletar a conta do usuário do Tryitter.
  - PUT /api/User - ENPOINT para realizar alterações na conta do usuário Tryitter.
  - POST /api/User - ENPOINT para criar um novo usuário no Tryitter.
  
  - POST /api/Post - ENDPOINT para que um usuário cadastrado consiga realizar uma postagem no Tryitter.
  - GET /api/Post/{id} - ENDPOINT permite busca por postagens realizadas por um determinado usuário.
  - PUT /api/Post/{id} - ENDPOINT permite usuário modificar ou atualizar uma postagem já realizada.
  - DELETE /api/Post/{id} - ENDPOINT permite usuário deletar postagens já realizadas anteriormente.
  
</details>
