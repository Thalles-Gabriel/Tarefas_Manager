using TarefasManager.Data;
using TarefasManager.Models;
using Microsoft.EntityFrameworkCore;

namespace TarefasManager.Repositories;

public class TarefasRepository : ITarefasRepository
{
    private readonly TarefasManagerContext _tarefaContext;

    public TarefasRepository(TarefasManagerContext tarefaContext)
    {
        _tarefaContext = tarefaContext;
    }

    public async Task Adicionar(Tarefa tarefa)
    {
        await _tarefaContext.Tarefas.AddAsync(tarefa);
        await _tarefaContext.SaveChangesAsync();
    }

    public async Task Atualizar(Tarefa tarefa)
    {
        var tarefaNova = await ObterPorId(tarefa.Id);
        tarefaNova = tarefa;
        _tarefaContext.Tarefas.Update(tarefaNova);
        await _tarefaContext.SaveChangesAsync();
    }

    public async Task Excluir(Tarefa tarefa)
    {
        _tarefaContext.Tarefas.Remove(tarefa);

        await _tarefaContext.SaveChangesAsync();
    }

    public async Task<IEnumerable<Tarefa>> ObterTodos(int indexComeco, int numeroItens)
    {
        return await _tarefaContext.Tarefas.Skip(indexComeco)
                                            .Take(numeroItens)
                                            .AsNoTracking()
                                            .ToListAsync();
    }

    public async Task<IEnumerable<Tarefa>> ObterFiltrado(DateTime? data, EnumStatus? status, string? titulo, int indexComeco, int numeroItens)
    {
        return await _tarefaContext.Tarefas.Where(x => data == null || x.Data == data)
                                            .Where(x => status == null || x.Status == status)
                                            .Where(x => titulo == null || x.Titulo == titulo)
                                            .AsNoTracking()
                                            .ToListAsync();
    }

    public async Task<IEnumerable<Tarefa>> ObterPorData(DateTime data, int indexComeco, int numeroItens)
    {
        return await _tarefaContext.Tarefas.Where(x => x.Data == data)
                                            .Skip(indexComeco)
                                            .Take(numeroItens)
                                            .AsNoTracking()
                                            .ToListAsync();
    }

    public async Task<Tarefa?> ObterPorId(int id)
    {
        return await _tarefaContext.Tarefas.FindAsync(id);
    }

    public async Task<IEnumerable<Tarefa>> ObterPorStatus(EnumStatus status, int indexComeco, int numeroItens)
    {
        return await _tarefaContext.Tarefas.Where(x => x.Status == status)
                                            .Skip(indexComeco)
                                            .Take(numeroItens)
                                            .AsNoTracking()
                                            .ToListAsync();
    }

    public async Task<Tarefa?> ObterPorTitulo(string titulo)
    {
        return await _tarefaContext.Tarefas.AsNoTracking()
                                            .FirstOrDefaultAsync(x => x.Titulo == titulo);
    }
}

