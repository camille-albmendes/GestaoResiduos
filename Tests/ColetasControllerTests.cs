using GestaoDeResiduos.Data;
using GestaoDeResiduos.Models;
using GestaoDeResiduos.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Xunit;

public class ColetasControllerTests
{
    [Fact]
    public async Task Quando_BuscarDadosProximaColetaNumaResidencia_Entao_RetornaTextoProximaColeta()
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
        residencia.SinalizarLixoParaColeta();

        var coletasController = new ColetasController(context);
        var result = await coletasController.GetProximaColeta(residencia.Logradouro);

        Assert.IsType<OkObjectResult>(result.Result);
        Assert.Equal(
            $"Próxima coleta será em: {residencia.DataProximaColeta}",
            (result.Result as OkObjectResult).Value
        );
    }
    
    [Fact]
    public async Task Quando_BuscarDadosProximaColetaNumaResidencia_E_NaoExistir_Entao_RetornaNotFound()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;
        var context = new ApplicationDbContext(options);
        var controller = new ColetasController(context);

        var result = await controller.GetProximaColeta("Rua dos bobos");

        Assert.IsType<NotFoundObjectResult>(result.Result);
        Assert.Equal(
            "Não há residências com lixo para coleta neste logradouro.",
            (result.Result as NotFoundObjectResult).Value
        );
    }
}