Feature: Cadastro de Residência

  Scenario: Cadastrar uma nova residência
    Given que não existe uma residência com logradouro "Rua C"
    When o usuário envia os dados da nova residência com logradouro "Rua C"
    Then o sistema deve criar a nova residência com sucesso
    And deve retornar o status 201 com os dados da residência cadastrada

  Scenario: Cadastrar uma residência com dados inválidos
    Given que o usuário insere dados obrigatórios ausentes para a residência
    When o sistema tenta cadastrar a residência
    Then o sistema deve retornar um erro indicando que os dados são inválidos
