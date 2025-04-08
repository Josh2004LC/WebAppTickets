using System.ComponentModel.DataAnnotations;

namespace TicketsMVC.Models
{
    public class User
    {
        public int us_identificador { get; set; }

        [Required(ErrorMessage = "El nombre completo es obligatorio")]
        [Display(Name = "Nombre Completo")]
        public string us_nombre_completo { get; set; }

        [Required(ErrorMessage = "El correo es obligatorio")]
        [EmailAddress(ErrorMessage = "Formato de correo inválido")]
        [Display(Name = "Correo")]
        public string us_correo { get; set; }

        [Required(ErrorMessage = "La clave es obligatoria")]
        [Display(Name = "Clave")]
        [DataType(DataType.Password)]
        public string us_clave { get; set; }

        [Required(ErrorMessage = "El rol es obligatorio")]
        [Display(Name = "Rol")]
        public int us_ro_identificador { get; set; }

        [Display(Name = "Estado")]
        public string us_estado { get; set; }

        [Display(Name = "Fecha de Creación")]
        public DateTime us_fecha_adicion { get; set; }

        [Display(Name = "Creado Por")]
        public string us_adicionado_por { get; set; }

        [Display(Name = "Fecha de Modificación")]
        public DateTime? us_fecha_modificacion { get; set; }

        [Display(Name = "Modificado Por")]
        public string us_modificado_por { get; set; }

        // Navigation property for display purposes
        [Display(Name = "Rol")]
        public string RoleName { get; set; }
    }
}
