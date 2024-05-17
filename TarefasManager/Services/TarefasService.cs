using TarefasManager.Repositories;
using TarefasManager.Models;
using ErrorOr;
using System.Data.Common;

namespace TarefasManager.Services;

public class TarefasService : ITarefasService
{
    private readonly ITarefasRepository _tarefasRepository;

    public TarefasService(ITarefasRepository tarefasRepository)
    {
        _tarefasRepository = tarefasRepository;
    }

    public async Task<Error?> Adicionar(Tarefa tarefa)
    {
        try
        {
            var hasTarefa = ObterPorId(tarefa.Id);
            if (hasTarefa != null) 
                return TarefasError.ConflictingInsert;

            await _tarefasRepository.Adicionar(tarefa);
        }
        catch (DbException ex)
        {
            return TarefasError.HandleException(ex);
        }
        return null;
    }

    public async Task<Error?> Atualizar(Tarefa tarefa)
    {
        try
        {
            var hasTarefa = await _tarefasRepository.ObterPorId(tarefa.Id);
            if (hasTarefa is null) return TarefasError.NotFound;
            await _tarefasRepository.Atualizar(tarefa);
        }
        catch (DbException ex)
        {
            return TarefasError.HandleException(ex);
        }
        return null;
    }

    public async Task<Error?> Excluir(Tarefa tarefa)
    {
        try
        {
            var hasTarefa = await _tarefasRepository.ObterPorId(tarefa.Id);
            if (hasTarefa is null) return TarefasError.NotFound;
            await _tarefasRepository.Excluir(tarefa);
        }
        catch (DbException ex)
        {
            return TarefasError.HandleException(ex);
        }

        return null;
    }

    public async Task<ErrorOr<IEnumerable<Tarefa>>> ObterFiltrado(DateTime? data, EnumStatus? status, string? titulo, int indexComeco, int numeroItens)
    {
        try
        {
            var tarefas = await _tarefasRepository.ObterFiltrado(data, status, titulo, indexComeco, numeroItens);
            if (tarefas.Count() < 1) return TarefasError.NotFound;

            return ErrorOrFactory.From(tarefas);
        }
        catch (DbException ex)
        {
            return TarefasError.HandleException(ex);
        }
    }

    public async Task<ErrorOr<IEnumerable<Tarefa>>> ObterPorData(DateTime data, int indexComeco, int numeroItens)
    {
        try
        {
            var tarefas = await _tarefasRepository.ObterPorData(data, indexComeco, numeroItens); ;
            if (tarefas.Count() < 1) return TarefasError.NotFound;

            return ErrorOrFactory.From(tarefas);
        }
        catch (DbException ex)
        {
            return TarefasError.HandleException(ex);
        }
    }

    public async Task<ErrorOr<Tarefa>> ObterPorId(int id)
    {
        try
        {
            var tarefa = await _tarefasRepository.ObterPorId(id);
            if (tarefa is null) return TarefasError.NotFound;

            return tarefa;
        }
        catch (DbException ex)
        {
            return TarefasError.HandleException(ex);
        }
    }

    public async Task<ErrorOr<IEnumerable<Tarefa>>> ObterPorStatus(EnumStatus status, int indexComeco, int numeroItens)
    {
        try
        {
            var tarefas = await _tarefasRepository.ObterPorStatus(status, indexComeco, numeroItens);
            if (tarefas is null) return TarefasError.NotFound;

            return ErrorOrFactory.From(tarefas);
        }
        catch (DbException ex)
        {
            return TarefasError.HandleException(ex);
        }
    }

    public async Task<ErrorOr<Tarefa>> ObterPorTitulo(string titulo)
    {
        try
        {
            var tarefa = await _tarefasRepository.ObterPorTitulo(titulo);
            if (tarefa is null) return TarefasError.NotFound;

            return tarefa;

        }
        catch (DbException ex)
        {
            return TarefasError.HandleException(ex);
        }
    }

    public async Task<ErrorOr<IEnumerable<Tarefa>>> ObterTodos(int indexComeco, int numeroItens)
    {
        try
        {
            var tarefas = await _tarefasRepository.ObterTodos(indexComeco, numeroItens);
            if (tarefas is null) return TarefasError.NotFound;

            return ErrorOrFactory.From(tarefas);
        }
        catch (DbException ex)
        {
            return TarefasError.HandleException(ex);
        }
    }
}
