namespace TarefasManager.Models;

public class TarefaUpdateViewModel
{
    public string Titulo { get; set; } = null!;
    public string Descricao { get; set; } = null!;
    public DateTime Data { get; set; }
    public EnumStatus Status { get; set; }
}
