using Microsoft.AspNetCore.Mvc;
using TarefasManager.Models;

public interface ITarefasController
{
    Task<IActionResult> ObterTodos(int index, int numeroItens);
    Task<IActionResult> ObterPorId(int id);
    Task<IActionResult> ObterPorTitulo(string titulo);
    Task<IActionResult> ObterPorData(DateTime data, int index, int numeroItens);
    Task<IActionResult> ObterPorStatus(EnumStatus status, int index, int numeroItens);
    Task<IActionResult> ObterFiltrado(DateTime? data, string? titulo, EnumStatus? status, int index, int numeroItens);
    Task<IActionResult> Adicionar(Tarefa tarefa);
    Task<IActionResult> Atualizar(int id, TarefaUpdateViewModel tarefa);
    Task<IActionResult> Excluir(int id);
}
