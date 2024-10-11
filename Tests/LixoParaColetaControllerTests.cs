using Xunit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GestaoDeResiduos.Controllers;
using GestaoDeResiduos.Data;
using GestaoDeResiduos.Models;
using System.Threading.Tasks;

public class LixoParaColetaControllerTests
{
    [Fact]
    public async Task SinalizarLixoParaColeta_ReturnsNoContent()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;
        var context = new ApplicationDbContext(options);
         var residencia = new Residencia
{
    Logradouro = "Rua Teste",
    Numero = "123",
    Complemento = "Apto 101",  // A propriedade Complemento está sendo definida
    Cep = "12345-678"
};

        context.Residencias.Add(residencia);
        context.SaveChanges();
        var controller = new LixoParaColetaController(context);

        var result = await controller.SinalizarLixoParaColeta(residencia.Id);

        Assert.IsType<NoContentResult>(result);
    }
}
