using System.Text.Json.Serialization;
using TarefasManager.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<TarefasManagerContext>();

builder.Services.AddControllers().AddJsonOptions(config =>
            config.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter())
        );

builder.Services.AddServices();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

