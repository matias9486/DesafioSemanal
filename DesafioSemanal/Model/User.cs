
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace DesafioSemanal.Model
{
    public class User
    {
        [Key]
        public int UserId { get; set; }

        [Required(ErrorMessage = "{0} es requerido.")]
        [StringLength(20, ErrorMessage = "{0} no puede contener mas de 20 caracteres.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "{0} es requerido.")]
        [StringLength(20, ErrorMessage = "{0} no puede contener mas de 20 caracteres.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "{0} es requerido.")]
        [EmailAddress(ErrorMessage = "Por favor ingrese una dirección de e-mail válida!")]
        [StringLength(20, ErrorMessage = "{0} no puede contener mas de 20 caracteres.")]
        public string Email { get; set; }

        //prop.de navegacion Referencia
        public ICollection<Post> Posts { get; set; }
        public ICollection<Comment> Comments { get; set; }

        
    }
}
