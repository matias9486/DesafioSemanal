using System;
using System.ComponentModel.DataAnnotations;

namespace DesafioSemanal.Model
{
    public class Comment:BaseEntity
    {        
        [Required(ErrorMessage = "{0} es requerido.")]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }


        [Required(ErrorMessage = "{0} es requerido.")]
        [StringLength(100, ErrorMessage = "{0} no puede contener mas de 100 caracteres.")]
        //public string Comment { get; set; } //la propiedad no puede coincidir con el nombre de la clase
        public string Content { get; set; }


        [Required(ErrorMessage = "{0} es requerido.")]
        public User User { get; set; } //usuario que creo el Comment


        [Required(ErrorMessage = "{0} es requerido.")]
        public Post Post { get; set; } //sobre que post comentó
    }
}
