using FluentValidation;
using FluentValidation.TestHelper;
using TarefasManager.Validators;

namespace TarefasManager.Tests.Unit;

public class TarefaTests
{
    private TarefaValidator _validator;

    public TarefaTests(TarefaValidator validator)
    {
        _validator = validator;
    }

    [Fact]
    public void Int_Negativo_Ou_0_Deve_Voltar_Erro()
    {
        var tarefa = new Tarefa{
            Id = 0,
            Data = DateTime.Now,
            Descricao = "testeIntNegativo",
            Status = EnumStatus.Pendente,
            Titulo = "Teste"
        };

        var result = _validator.TestValidate(tarefa);

        result.ShouldHaveValidationErrorFor(tarefa => tarefa.Id);
    }

    [Fact]
    public void Data_Futuro_Deve_Voltar_Erro()
    {
        var tarefa = new Tarefa{
            Id = 1,
            Data = DateTime.Today.AddDays(1),
            Descricao = "testeDataAmanha",
            Status = EnumStatus.Pendente,
            Titulo = "Teste"
        };

        var result = _validator.TestValidate(tarefa);

        result.ShouldHaveValidationErrorFor(tarefa => tarefa.Data);
    }
}
