Feature: Consulta de Próxima Coleta

  Scenario: Consultar a próxima coleta para um logradouro com residências que possuem lixo para coleta
    Given que existe uma residência no logradouro "Rua A" com lixo para coleta
    And a próxima coleta está marcada para "2024-11-01"
    When o usuário solicita a próxima coleta para o logradouro "Rua A"
    Then o sistema deve retornar "Próxima coleta será em: 2024-11-01"

  Scenario: Consultar a próxima coleta para um logradouro sem residências com lixo para coleta
    Given que não há residências com lixo para coleta no logradouro "Rua B"
    When o usuário solicita a próxima coleta para o logradouro "Rua B"
    Then o sistema deve retornar "Não há residências com lixo para coleta neste logradouro."
