using ErrorOr;
using TarefasManager.Models;

namespace TarefasManager.Services;

public interface ITarefasService
{
    Task<ErrorOr<IEnumerable<Tarefa>>> ObterTodos(int indexComeco, int numeroItens);
    Task<ErrorOr<Tarefa>> ObterPorId(int id);
    Task<ErrorOr<Tarefa>> ObterPorTitulo(string titulo);
    Task<ErrorOr<IEnumerable<Tarefa>>> ObterPorData(DateTime data, int indexComeco, int numeroItens);
    Task<ErrorOr<IEnumerable<Tarefa>>> ObterPorStatus(EnumStatus status, int indexComeco, int numeroItens);
    Task<Error?> Atualizar(Tarefa tarefa);
    Task<Error?> Excluir(Tarefa tarefa);
    Task<Error?> Adicionar(Tarefa tarefa);
    Task<ErrorOr<IEnumerable<Tarefa>>> ObterFiltrado(DateTime? data, EnumStatus? status, string? titulo, int indexComeco, int numeroItens);
}
