using System.ComponentModel.DataAnnotations;

namespace TicketsMVC.Models
{
    public class Role
    {
        public int ro_identificador { get; set; }

        [Required(ErrorMessage = "La descripción es obligatoria")]
        [Display(Name = "Descripción")]
        public string ro_decripcion { get; set; }

        [Display(Name = "Fecha de Creación")]
        public DateTime ro_fecha_adicion { get; set; }

        [Display(Name = "Creado Por")]
        public string ro_adicionado_por { get; set; }

        [Display(Name = "Fecha de Modificación")]
        public DateTime? ro_fecha_modificacion { get; set; }

        [Display(Name = "Modificado Por")]
        public string ro_modificado_por { get; set; }
    }
}
