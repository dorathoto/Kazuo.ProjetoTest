using Kazuo.ProjetoTest.Models.Enums;
using Microsoft.AspNetCore.Identity;
using System.Text.Json.Serialization;

namespace Kazuo.ProjetoTest.Models
{
    public class Usuario : IdentityUser<Guid> //herdo de Identity User porém falo, que o id será um Guid e não um string
    {
        //qualquer propriedade extra, só pra exemplo
        //aqui vc colocaria todas propriedades a mais do que o identity padrão terá
        public TipoCorOlho? CorOlhos { get; set; }

        [JsonIgnore]
        public virtual ICollection<Tarefa>? Tarefas { get; set; }

    }
}
