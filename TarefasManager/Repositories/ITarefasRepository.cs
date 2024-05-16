using TarefasManager.Models;

namespace TarefasManager.Repositories;

public interface ITarefasRepository
{
    Task<IEnumerable<Tarefa>> ObterTodos(int indexComeco, int numeroPaginas);
    Task<Tarefa?> ObterPorId(int id);
    Task<Tarefa?> ObterPorTitulo(string titulo);
    Task<IEnumerable<Tarefa>> ObterPorData(DateTime data, int indexComeco, int numeroItens);
    Task<IEnumerable<Tarefa>> ObterPorStatus(EnumStatus status, int indexComeco, int numeroItens);
    Task Atualizar(Tarefa tarefa);
    Task Excluir(Tarefa tarefa);
    Task Adicionar(Tarefa tarefa);
    Task<IEnumerable<Tarefa>> ObterFiltrado(DateTime? data, EnumStatus? status, string? titulo, int indexComeco, int numeroItens);
}
