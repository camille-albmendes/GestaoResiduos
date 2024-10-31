using Xunit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GestaoDeResiduos.Controllers;
using GestaoDeResiduos.Data;
using GestaoDeResiduos.Models;

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
    }
}
