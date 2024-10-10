using Xunit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GestaoDeResiduos.Controllers;
using GestaoDeResiduos.Data;
using GestaoDeResiduos.Models;
using System.Threading.Tasks;

public class ResidenciasControllerTests
{
    [Fact]
    public async Task PostResidencia_ReturnsCreatedAtAction()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;
        var context = new ApplicationDbContext(options);
        var controller = new ResidenciasController(context);
        var residencia = new Residencia { Logradouro = "Rua 1", Numero = "100", Cep = "12345-678" };

        var result = await controller.PostResidencia(residencia);

        var createdAtActionResult = result.Result as CreatedAtActionResult;
        Assert.NotNull(createdAtActionResult);
        Assert.Equal(201, createdAtActionResult.StatusCode);
    }
}
