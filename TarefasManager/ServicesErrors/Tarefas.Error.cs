using ErrorOr;

public static class TarefasError
{
    public static Error NotFound => Error.NotFound(
            "Tarefa.NotFound",
            "Tarefa não foi existe."
            );

    public static Error Failure => Error.Failure(
            "Tarefas.Failure",
            "Uma falha inesperada ocorreu. Contate o suporte."
            );

    public static Error Invalid => Error.Validation(
            "Tarefa.Validation",
            "O valor inserido não é válido."
            );

    public static Error ConflictingInsert => Error.Conflict(
            "Tarefas.Conflict",
            "Esta tarefa já existe."
            );

    public static Error HandleException(Exception exception)
    {
        var error = Error.Failure("Tarefas.Failure: " + exception.GetBaseException().Source,
                exception.GetBaseException().Message);
        return error;
    }
}
