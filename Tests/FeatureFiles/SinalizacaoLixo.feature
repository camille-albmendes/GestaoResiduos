Feature: Sinalização de Lixo para Coleta

  Scenario: Sinalizar lixo para coleta para uma residência existente
    Given que existe uma residência com ID 1
    When o usuário sinaliza que há lixo para coleta para a residência com ID 1
    Then o sistema deve atualizar a residência para ter lixo para coleta
    And a próxima coleta deve ser marcada para uma semana a partir de hoje

  Scenario: Tentar sinalizar lixo para coleta para uma residência inexistente
    Given que não existe uma residência com ID 999
    When o usuário tenta sinalizar que há lixo para coleta para a residência com ID 999
    Then o sistema deve retornar uma resposta de erro 404 indicando "Residência não encontrada"
