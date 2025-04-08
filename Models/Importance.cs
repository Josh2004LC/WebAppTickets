using System.ComponentModel.DataAnnotations;

namespace TicketsMVC.Models
{
    public class Importance
    {
        public int im_identificador { get; set; }

        [Required(ErrorMessage = "La descripción es obligatoria")]
        [Display(Name = "Descripción")]
        public string im_descripcion { get; set; }

        [Display(Name = "Fecha de Creación")]
        public DateTime im_fecha_adicion { get; set; }

        [Display(Name = "Creado Por")]
        public string im_adicionado_por { get; set; }

        [Display(Name = "Fecha de Modificación")]
        public DateTime? im_fecha_modificacion { get; set; }

        [Display(Name = "Modificado Por")]
        public string im_modificado_por { get; set; }
    }
}
