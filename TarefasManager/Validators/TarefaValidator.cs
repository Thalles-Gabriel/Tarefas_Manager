using FluentValidation;
using TarefasManager.Models;

namespace TarefasManager.Validators;

public class TarefaValidator : AbstractValidator<Tarefa>
{
    public TarefaValidator()
    {
        this.RuleLevelCascadeMode = CascadeMode.Stop;

        RuleFor(tarefa => tarefa.Id)
            .GreaterThan(0)
            .NotNull();

        RuleFor(tarefa => tarefa.Data)
            .NotNull()
            .Must(DataValida);
        
        RuleFor(tarefa => tarefa.Status)
            .NotNull()
            .IsInEnum();

        RuleFor(tarefa => tarefa.Descricao)
            .Length(0, 400);

        RuleFor(tarefa => tarefa.Titulo)
            .NotNull()
            .Length(3, 100);
    }

    private bool DataValida(DateTime dataTarefa)
    {
        var comparacaoData = DateTime.Compare(dataTarefa, DateTime.Now);
        var dataNaoEhFuturo = comparacaoData <= 0;
        return dataNaoEhFuturo;
    }
}
