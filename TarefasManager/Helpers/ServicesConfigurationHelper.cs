using FluentValidation;
using TarefasManager.Models;
using TarefasManager.Repositories;
using TarefasManager.Services;
using TarefasManager.Validators;

namespace TarefasManager.Helpers;

public static class ServicesConfigurationHelper
{
    public static void AddServices(this IServiceCollection service)
    {
        service.AddScoped<ITarefasRepository, TarefasRepository>();
        service.AddScoped<ITarefasService, TarefasService>();
        service.AddScoped<IValidator<Tarefa>, TarefaValidator>();
        service.AddTransient<ExceptionHandlerHelper>();
    }
}
