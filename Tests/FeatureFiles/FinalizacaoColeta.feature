Feature: Finalização de Coleta

  Scenario: Sinalizar coleta como finalizada para uma residência existente
    Given que existe uma residência com ID 1 e lixo para coleta
    When o usuário sinaliza a coleta como finalizada para a residência com ID 1
    Then o sistema deve atualizar a residência para não ter lixo para coleta
    And a data de próxima coleta deve ser nula

  Scenario: Tentar sinalizar coleta como finalizada para uma residência inexistente
    Given que não existe uma residência com ID 999
    When o usuário tenta sinalizar a coleta como finalizada para a residência com ID 999
    Then o sistema deve retornar uma resposta de erro 404 indicando "Residência não encontrada"
