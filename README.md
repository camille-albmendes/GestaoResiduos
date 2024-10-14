Gestão de Resíduos - Smart City Project
Este projeto tem como objetivo gerenciar o processo de coleta de resíduos em uma cidade inteligente, permitindo a criação de residências, sinalização de coleta, consulta de datas de coleta e marcação de coletas como concluídas.

Tecnologias Utilizadas
.NET Core 8.0
ASP.NET MVC
Entity Framework Core
Oracle Database
MongoDB Atlas
REST API
Pré-requisitos
Antes de começar, certifique-se de ter os seguintes softwares instalados em sua máquina:

.NET SDK 8.0
Oracle Database ou uma conexão válida com o Oracle Cloud
MongoDB Atlas com a conexão configurada no projeto
Git
Visual Studio Code ou Visual Studio
Configuração do Projeto
1. Clonar o repositório
Clone o repositório para sua máquina local usando o comando:

bash
Copiar código
git clone https://github.com/usuario/repositorio-gestao-residuos.git
cd GestaoResiduos
2. Configuração da Base de Dados
O projeto utiliza duas bases de dados: Oracle e MongoDB.

Oracle Database:

Configure o Oracle Database com as tabelas T_LOGRADOUROS, T_RESIDENCIAS, T_COLETA, e T_COLETA_LOGRADOUROS.
Utilize scripts de SQL para criar as tabelas e populações iniciais de dados. Os scripts podem ser encontrados na pasta /Database/.
MongoDB Atlas:

Configure sua string de conexão no arquivo de configuração appsettings.json:
json
Copiar código
"MongoDbSettings": {
  "ConnectionString": "mongodb+srv://username:password@cluster0.mongodb.net",
  "DatabaseName": "GestaoResiduosDB",
  "CollectionName": "Coletas"
}
3. Configuração do Projeto
Abra o projeto na sua IDE favorita (Visual Studio ou VSCode):

bash
Copiar código
code .
No arquivo appsettings.json, configure a string de conexão para o Oracle e o MongoDB. Exemplo de configuração:

json
Copiar código
{
  "ConnectionStrings": {
    "OracleConnection": "User Id=myuser;Password=mypassword;Data Source=myoraclehost"
  },
  "MongoDbSettings": {
    "ConnectionString": "mongodb+srv://user:password@cluster.mongodb.net",
    "DatabaseName": "GestaoResiduosDB",
    "CollectionName": "Coletas"
  }
}
Migrations (caso necessário): Se houver alterações no modelo de dados do Entity Framework, rode as migrações para garantir que o banco de dados esteja sincronizado com os modelos do projeto:

bash
Copiar código
dotnet ef database update
4. Inicializando o Projeto
Para iniciar o projeto em modo de desenvolvimento, utilize o comando:

bash
Copiar código
dotnet run --project GestaoDeResiduos.csproj
5. Executando os Testes
Antes de rodar os testes, certifique-se de que o projeto está corretamente compilado:

bash
Copiar código
dotnet build --configuration Release
Depois, execute os testes:

bash
Copiar código
dotnet test GestaoDeResiduos.csproj --configuration Release --no-build
6. Rodando o Projeto
Acesse a aplicação em http://localhost:5000 ou a URL que for exibida no terminal.

Utilize os seguintes endpoints para interagir com a aplicação:

POST /residencias - Criação de uma nova residência.
POST /coletas/sinalizar - Sinalizar que uma residência tem lixo para ser coletado.
GET /coletas/proximas - Consultar a próxima data de coleta para um determinado logradouro.
PUT /coletas/finalizar - Sinalizar que a coleta foi concluída.

Faça um fork deste repositório.
Crie uma branch para suas modificações (git checkout -b feature/nova-funcionalidade).
Commit suas mudanças (git commit -m 'Adiciona nova funcionalidade').
Envie suas mudanças para o repositório (git push origin feature/nova-funcionalidade).
Abra um Pull Request.