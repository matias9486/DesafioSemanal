using System.ComponentModel.DataAnnotations;

namespace DesafioSemanal.Model
{
    public class BaseEntity
    {
        [Key]
        public int Id { get; set; }
    }
}
