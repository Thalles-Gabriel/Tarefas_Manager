using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;

namespace TarefasManager.Tests.Integration;

public class TarefasControllerTests : IClassFixture<WebApplicationFactory<Program>>, IAsyncLifetime
{
    private readonly WebApplicationFactory<Program> _factory;
    private readonly HttpClient _client;
    private const int _id = 1000000;

    public TarefasControllerTests(WebApplicationFactory<Program> factory)
    {
        _factory = factory;
        _client = _factory.CreateClient();
    }

    [Fact]
    public async Task Adicionar_Tarefa_Deve_Retornar_Created()
    {
        var novaTarefa = new Tarefa()
        {
            Id = _id,
            Data = DateTime.Now,
            Descricao = "Teste de integracao add",
            Status = EnumStatus.Pendente,
            Titulo = "Testing"
        };

        var content = new StringContent(JsonSerializer.Serialize(novaTarefa));
        var response = await _client.PostAsync("api/tarefas", content);

        Assert.True(response.StatusCode == HttpStatusCode.Created);
    }

    [Fact]
    public async Task Obter_Tarefa_Por_Id_Deve_Retornar_Ok()
    {
        var response = await _client.GetAsync("api/tarefas/" + _id);

        Assert.True(response.StatusCode == HttpStatusCode.OK);
    }

    [Fact]
    public async Task Atualizar_Tarefa_Deve_Retornar_NoContent()
    {
        var novaTarefa = new Tarefa()
        {
            Id = _id,
            Data = DateTime.Now,
            Descricao = "Teste de integracao atualizado",
            Status = EnumStatus.Finalizado,
            Titulo = "Testing finished"
        };

        var content = new StringContent(JsonSerializer.Serialize(novaTarefa));

        var response = await _client.PutAsync("api/tarefas/" + novaTarefa.Id, content);
        
        Assert.True(response.StatusCode == HttpStatusCode.NoContent);
    }

    [Fact]
    public async Task Excluir_Tarefa_Deve_Retornar_NoContent()
    {
        var response = await _client.DeleteAsync("api/tarefas/" + _id);

        Assert.True(response.StatusCode == HttpStatusCode.NoContent);
    }

    public async Task DisposeAsync()
    {
        await _factory.DisposeAsync();
    }

    public async Task InitializeAsync()
    {
        await Adicionar_Tarefa_Deve_Retornar_Created();
        await Obter_Tarefa_Por_Id_Deve_Retornar_Ok();
        await Atualizar_Tarefa_Deve_Retornar_NoContent();
        await Excluir_Tarefa_Deve_Retornar_NoContent();
    }
}
