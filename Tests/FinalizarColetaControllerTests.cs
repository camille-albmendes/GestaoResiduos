using GestaoDeResiduos.Data;
using GestaoDeResiduos.Models;
using GestaoDeResiduos.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Xunit;

public class FinalizarColetaControllerTests
{
    [Fact]
    public async Task Quando_SinalizarColetaRealizadaNumaResidencia_Entao_RetornaNoContent()
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
        residencia.SinalizarColetaRealizada();

        var finalizarColetasController = new FinalizarColetaController(context);
        var result = await finalizarColetasController.SinalizarColetaFinalizada(residencia.Id);

        Assert.IsType<NoContentResult>(result);
    }
    
    [Fact]
    public async Task Quando_SinalizarColetaRealizadaNumaResidencia_E_NaoExistir_Entao_RetornaNotFound()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;
        var context = new ApplicationDbContext(options);
        var finalizarColetasController = new FinalizarColetaController(context);

        var result = await finalizarColetasController.SinalizarColetaFinalizada(123456);

        Assert.IsType<NotFoundResult>(result);
    }
}