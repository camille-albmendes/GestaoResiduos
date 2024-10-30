Feature: Exclusão de Resíduo

  Scenario: Excluir um resíduo existente
    Given que existe um resíduo com ID 2
    When o usuário solicita a exclusão do resíduo com ID 2
    Then o sistema deve excluir o resíduo com sucesso
    And deve retornar o status 204

  Scenario: Tentar excluir um resíduo inexistente
    Given que não existe um resíduo com ID 999
    When o usuário tenta excluir o resíduo com ID 999
    Then o sistema deve retornar uma resposta de erro 404 indicando "Resíduo não encontrado"
