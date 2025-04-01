using System;
using System.ComponentModel.DataAnnotations;

namespace SistemaTickets.Models
{
    public class Usuario
    {
        public int us_identificador { get; set; }

        [Required(ErrorMessage = "El nombre completo es obligatorio")]
        [Display(Name = "Nombre Completo")]
        public string us_nombre_completo { get; set; }

        [Required(ErrorMessage = "El correo electrónico es obligatorio")]
        [EmailAddress(ErrorMessage = "Formato de correo electrónico inválido")]
        [Display(Name = "Correo Electrónico")]
        public string us_correo { get; set; }

        [Display(Name = "Contraseña")]
        public string us_clave { get; set; }

        [Required(ErrorMessage = "El rol es obligatorio")]
        [Display(Name = "Rol")]
        public int us_ro_identificador { get; set; }

        [Required(ErrorMessage = "El estado es obligatorio")]
        [Display(Name = "Estado")]
        public string us_estado { get; set; }

        [Display(Name = "Fecha de creación")]
        public DateTime us_fecha_adicion { get; set; }

        [Display(Name = "Creado por")]
        public string us_adicionado_por { get; set; }

        [Display(Name = "Fecha de modificación")]
        public DateTime? us_fecha_modificacion { get; set; }

        [Display(Name = "Modificado por")]
        public string us_modificado_por { get; set; }
    }

    public class LoginViewModel
    {
        [Required(ErrorMessage = "El correo electrónico es obligatorio")]
        [EmailAddress(ErrorMessage = "Formato de correo electrónico inválido")]
        [Display(Name = "Correo Electrónico")]
        public string Correo { get; set; }

        [Required(ErrorMessage = "La contraseña es obligatoria")]
        [Display(Name = "Contraseña")]
        [DataType(DataType.Password)]
        public string Clave { get; set; }
    }
}