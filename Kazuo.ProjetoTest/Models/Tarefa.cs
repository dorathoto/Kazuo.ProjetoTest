using System.Text.Json.Serialization;

namespace Kazuo.ProjetoTest.Models
{
    public class Tarefa
    {
        public Guid TarefaId { get; set; }

        public DateTime DataRealizacaoTarefa { get; set; }//opcional

        public Guid UsuarioId { get; set; }

        [JsonIgnore]
        public virtual Usuario Usuario { get; set; }
    }
}
