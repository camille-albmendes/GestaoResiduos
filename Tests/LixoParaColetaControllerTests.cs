using Xunit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GestaoDeResiduos.Controllers;
using GestaoDeResiduos.Data;
using GestaoDeResiduos.Models;
using Json.Schema;
using System.Text.Json.Nodes;
using Newtonsoft.Json;

public class LixoParaColetaControllerTests
{
    [Fact]
    public async Task Quando_SinalizarLixoParaColeta_Entao_RetornaNoContent()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;
        var context = new ApplicationDbContext(options);
        var residencia = new Residencia
        {
            Logradouro = "Rua Teste",
            Numero = "123",
            Complemento = "Apto 101",
            Cep = "12345-678"
        };

        context.Residencias.Add(residencia);
        context.SaveChanges();

        var controller = new LixoParaColetaController(context);
        var result = await controller.SinalizarLixoParaColeta(residencia.Id);

        Assert.IsType<NoContentResult>(result);
        Assert.True(residencia.LixoParaColeta);
        Assert.NotNull(residencia.DataProximaColeta);

        var schema = JsonSchema.FromFile("Tests/resources/schemas/sinalizar-lixo-para-coleta.json");
        Assert.True(schema.Evaluate(
            JsonObject.Parse(JsonConvert.SerializeObject(result))
        ).IsValid);
    }
    
    [Fact]
    public async Task Quando_SinalizarLixoParaColeta_E_ResidenciaNaoExistir_Entao_RetornaNotFound()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;
        var context = new ApplicationDbContext(options);
        var controller = new LixoParaColetaController(context);
        var result = await controller.SinalizarLixoParaColeta(123456);

        Assert.IsType<NotFoundResult>(result);

        var schema = JsonSchema.FromFile("Tests/resources/schemas/sinalizar-lixo-para-coleta-inexistente.json");
        Assert.True(schema.Evaluate(
            JsonObject.Parse(JsonConvert.SerializeObject(result))
        ).IsValid);
    }
}
