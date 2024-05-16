using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using TarefasManager.Models;
using TarefasManager.Services;

namespace TarefasManager.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TarefasController : ApiController, ITarefasController
{
    private readonly ITarefasService _tarefasService;
    private readonly IValidator<Tarefa> _validator;

    public TarefasController(ITarefasService tarefasService, IValidator<Tarefa> validator)
    {
        _tarefasService = tarefasService;
        _validator = validator;
    }

    [HttpPost]
    public async Task<IActionResult> Adicionar([FromBody] Tarefa tarefa)
    {
        var validacao = await _validator.ValidateAsync(tarefa);
        if(!validacao.IsValid)
            return BadRequest(validacao.Errors);


        var errorResult = await _tarefasService.Adicionar(tarefa);

        if(errorResult.HasValue)
            return Problem(errorResult.Value);

        return CreatedAtAction(nameof(ObterPorId), new { id = tarefa.Id }, tarefa);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Atualizar([FromRoute] int id, [FromBody] TarefaUpdateViewModel tarefa)
    {
        var tarefaNova = new Tarefa(tarefa);
        var validacao = await _validator.ValidateAsync(tarefaNova);
        if(!validacao.IsValid)
            return BadRequest(validacao.Errors);

        var tarefaGet = await _tarefasService.ObterPorId(id);

        if(tarefaGet.IsError)
            return Problem(tarefaGet.FirstError);

        var errorResult = await _tarefasService.Atualizar(tarefaNova);

        if(errorResult.HasValue)
            return Problem(errorResult.Value);

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Excluir(int id)
    {
        var tarefa = await _tarefasService.ObterPorId(id);

        if(tarefa.IsError)
            return Problem(tarefa.FirstError);

        var errorResult = await _tarefasService.Excluir(tarefa.Value);

        if(errorResult.HasValue)
            return Problem(errorResult.Value);

        return NoContent();
    }

    [HttpGet("ObterTodos")]
    public async Task<IActionResult> ObterTodos(int index = 0, int numeroItens = 20)
    {
        var resultado = await _tarefasService.ObterTodos(index, numeroItens);

        return resultado.Match(
                tarefas => Ok(tarefas),
                errors => Problem(errors[0])
                );
    }

    [HttpGet("ObterFiltrado")]
    public async Task<IActionResult> ObterFiltrado(DateTime? data, string? titulo, EnumStatus? status, int index = 0, int numeroItens = 20)
    {
        var resultado = await _tarefasService.ObterFiltrado(data, status, titulo, index, numeroItens);

        return resultado.Match(
                tarefas => Ok(tarefas),
                errors => Problem(errors[0])
                );
    }

    [HttpGet("ObterPorData/{data}")]
    public async Task<IActionResult> ObterPorData(DateTime data, int index = 0, int numeroItens = 20)
    {
        var resultado = await _tarefasService.ObterPorData(data, index, numeroItens);

        return resultado.Match(
                tarefas => Ok(tarefas),
                errors => Problem(errors[0])
                );
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> ObterPorId(int id)
    {
        var resultado = await _tarefasService.ObterPorId(id);

        return resultado.Match(
                tarefa => Ok(tarefa),
                errors => Problem(errors[0])
                );
    }

    [HttpGet("ObterPorStatus/{status}")]
    public async Task<IActionResult> ObterPorStatus(EnumStatus status, int index = 0, int numeroItens = 20)
    {
        var resultado = await _tarefasService.ObterPorStatus(status, index, numeroItens);

        return resultado.Match(
                tarefas => Ok(tarefas),
                errors => Problem(errors[0])
                );
    }

    [HttpGet("ObterPorTitulo/{titulo}")]
    public async Task<IActionResult> ObterPorTitulo(string titulo)
    {
        var resultado = await _tarefasService.ObterPorTitulo(titulo);

        return resultado.Match(
                tarefas => Ok(tarefas),
                errors => Problem(errors[0])
                );
    }
}
