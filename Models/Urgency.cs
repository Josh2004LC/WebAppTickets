using System.ComponentModel.DataAnnotations;

namespace TicketsMVC.Models
{
    public class Urgency
    {
        public int ur_identificador { get; set; }

        [Required(ErrorMessage = "La descripción es obligatoria")]
        [Display(Name = "Descripción")]
        public string ur_descripcion { get; set; }

        [Display(Name = "Fecha de Creación")]
        public DateTime ur_fecha_adicion { get; set; }

        [Display(Name = "Creado Por")]
        public string ur_adicionado_por { get; set; }

        [Display(Name = "Fecha de Modificación")]
        public DateTime? ur_fecha_modificacion { get; set; }

        [Display(Name = "Modificado Por")]
        public string ur_modificado_por { get; set; }
    }
}
