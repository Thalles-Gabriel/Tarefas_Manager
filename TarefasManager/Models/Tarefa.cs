namespace TarefasManager.Models;

public class Tarefa
{
    public int Id { get; set; } 
    public string Titulo { get; set; } = null!;
    public string Descricao { get; set; } = null!;
    public DateTime Data { get; set; }
    public EnumStatus Status { get; set; }

    public Tarefa() {}

    public Tarefa(TarefaUpdateViewModel tarefa)
    {
        this.Titulo = tarefa.Titulo;
        this.Descricao = tarefa.Descricao;
        this.Data = tarefa.Data;
        this.Status = tarefa.Status;
    }
}
