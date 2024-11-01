using Xunit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GestaoDeResiduos.Controllers;
using GestaoDeResiduos.Data;
using GestaoDeResiduos.Models;
using Json.Schema;
using System.Text.Json.Nodes;
using Newtonsoft.Json;

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
            Complemento = "Apto 101",
            Cep = "12345-678"
        };

        var result = await residenciasController.PostResidencia(residencia);
        var createdAtActionResult = result.Result as CreatedAtActionResult;

        Assert.NotNull(createdAtActionResult);
        Assert.Equal(201, createdAtActionResult.StatusCode);
        Assert.IsType<Residencia>(createdAtActionResult.Value);

        var schema = JsonSchema.FromFile("Tests/resources/schemas/criar-residencia.json");
        Assert.True(schema.Evaluate(
            JsonObject.Parse(JsonConvert.SerializeObject(result))
        ).IsValid);
    }
}
