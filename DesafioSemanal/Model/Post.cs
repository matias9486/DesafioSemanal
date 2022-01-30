using DesafioSemanal.Interfaces;
using System;

using System.ComponentModel.DataAnnotations;


namespace DesafioSemanal.Model
{
    public class Post:IBaseEntity
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "{0} es requerido.")]
        [StringLength(20, ErrorMessage = "{0} no puede contener mas de 20 caracteres.")]
        public string Title { get; set; }

        [Required(ErrorMessage = "{0} es requerido.")]        
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        [Required(ErrorMessage = "{0} es requerido.")]
        [StringLength(100, ErrorMessage = "{0} no puede contener mas de 100 caracteres.")]
        public string Content { get; set; }


        [Required(ErrorMessage = "{0} es requerido.")]
        public User User { get; set; }
        
    }
}
