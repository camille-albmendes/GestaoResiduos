using Xunit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GestaoDeResiduos.Controllers;
using GestaoDeResiduos.Data;
using GestaoDeResiduos.Models;

public class ResidenciasControllerTests
{
    [Fact]
    public async Task Quando_CriarResidencia_RetornaCreated()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;
        var context = new ApplicationDbContext(options);
        var residenciasController = new ResidenciasController(context);
        var residencia = new Residencia
        {
            Logradouro = "Rua Teste",
            Numero = "123",
            Complemento = "Apto 101",  // A propriedade Complemento está sendo definida
            Cep = "12345-678"
        };


        var result = await residenciasController.PostResidencia(residencia);
        var createdAtActionResult = result.Result as CreatedAtActionResult;

        Assert.NotNull(createdAtActionResult);
        Assert.Equal(201, createdAtActionResult.StatusCode);
        Assert.IsType<Residencia>(createdAtActionResult.Value);
    }
}
