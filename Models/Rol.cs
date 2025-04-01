using System;
using System.ComponentModel.DataAnnotations;

namespace SistemaTickets.Models
{
    public class Rol
    {
        public int ro_identificador { get; set; }

        [Required(ErrorMessage = "La descripción es obligatoria")]
        [Display(Name = "Descripción")]
        public string ro_decripcion { get; set; }

        [Display(Name = "Fecha de creación")]
        public DateTime ro_fecha_adicion { get; set; }

        [Display(Name = "Creado por")]
        public string ro_adicionado_por { get; set; }

        [Display(Name = "Fecha de modificación")]
        public DateTime? ro_fecha_modificacion { get; set; }

        [Display(Name = "Modificado por")]
        public string ro_modificado_por { get; set; }
    }
}