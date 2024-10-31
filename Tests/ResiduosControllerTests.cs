using GestaoDeResiduos.Data;
using GestaoDeResiduos.Models;
using GestaoDeResiduos.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Xunit;

public class ResiduosControllerTests
{

    private ApplicationDbContext CriarResiduosMock() {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;
        var context = new ApplicationDbContext(options);

        var residuoReciclavel = new Resíduo
        {
            Tipo = "Reciclável",
            Descrição = "Pode ser reaproveitado"
        };
        var residuoOrganico = new Resíduo
        {
            Tipo = "Orgânico",
            Descrição = "Não pode ser reaproveitado"
        };
        var residuoToxico = new Resíduo
        {
            Tipo = "Tóxico",
            Descrição = "Necessita de atenção especial"
        };

        context.Resíduos.Add(residuoReciclavel);
        context.Resíduos.Add(residuoOrganico);
        context.Resíduos.Add(residuoToxico);
        context.SaveChanges();

        return context;
    }

    private Resíduo CriarResiduo(ApplicationDbContext context)
    {
        var residuoReciclavel = new Resíduo
        {
            Tipo = "Reciclável",
            Descrição = "Pode ser reaproveitado"
        };
        
        context.Resíduos.Add(residuoReciclavel);
        context.SaveChanges();

        return residuoReciclavel;
    }

    [Fact]
    public async Task Quando_BuscarTodosOsResiduos_Entao_RetornaTodosOsResiduosExistentes()
    {
        var context = CriarResiduosMock();

        var residuosController = new ResiduosController(context);
        var result = await residuosController.GetResiduos();

        Assert.IsAssignableFrom<IEnumerable<Resíduo>>(result.Value);
    }
    
    [Fact]
    public async Task Quando_CriarResiduo_RetornaCreated()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;
        var context = new ApplicationDbContext(options);
        var residuosController = new ResiduosController(context);
        var residuoReciclavel = new Resíduo
        {
            Tipo = "Reciclável",
            Descrição = "Pode ser reaproveitado"
        };

        var result = await residuosController.PostResíduo(residuoReciclavel);
        var createdAtActionResult = result.Result as CreatedAtActionResult;

        Assert.NotNull(createdAtActionResult);
        Assert.Equal(201, createdAtActionResult.StatusCode);
        Assert.IsType<Resíduo>(createdAtActionResult.Value);
    }
    
    [Fact]
    public async Task Quando_BuscarResiduo_Entao_RetornaOResiduo()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;
        var context = new ApplicationDbContext(options);

        var residuoReciclavel = CriarResiduo(context);

        var residuosController = new ResiduosController(context);
        var result = await residuosController.GetResíduo(residuoReciclavel.Id);

        Assert.IsType<Resíduo>(result.Value);
        Assert.Equal(result.Value.Id, residuoReciclavel.Id);
    }
    
    [Fact]
    public async Task Quando_BuscarResiduo_E_NaoExistir_Entao_RetornaNotFound()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;
        var context = new ApplicationDbContext(options);

        var residuosController = new ResiduosController(context);
        var result = await residuosController.GetResíduo(123456);

        Assert.IsType<NotFoundResult>(result.Result);
    }
    
    [Fact]
    public async Task Quando_DeletarResiduo_Entao_RetornaNoContent()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;
        var context = new ApplicationDbContext(options);
        var residuosController = new ResiduosController(context);

        var residuoReciclavel = CriarResiduo(context);

        var result = await residuosController.DeleteResíduo(residuoReciclavel.Id);

        Assert.IsType<NoContentResult>(result);

        var resultDelete = await residuosController.GetResíduo(residuoReciclavel.Id);

        Assert.IsType<NotFoundResult>(resultDelete.Result);
    }
    
    [Fact]
    public async Task Quando_DeletarResiduo_E_NaoExistir_Entao_RetornaNotFound()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;
        var context = new ApplicationDbContext(options);

        var residuosController = new ResiduosController(context);
        var result = await residuosController.DeleteResíduo(123456);

        Assert.IsType<NotFoundResult>(result);
    }
}