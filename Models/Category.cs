using System.ComponentModel.DataAnnotations;

namespace TicketsMVC.Models
{
    public class Category
    {
        public int ca_identificador { get; set; }

        [Required(ErrorMessage = "La descripción es obligatoria")]
        [Display(Name = "Descripción")]
        public string ca_descripcion { get; set; }

        [Display(Name = "Fecha de Creación")]
        public DateTime ca_fecha_adicion { get; set; }

        [Display(Name = "Creado Por")]
        public string ca_adicionado_por { get; set; }

        [Display(Name = "Fecha de Modificación")]
        public DateTime? ca_fecha_modificacion { get; set; }

        [Display(Name = "Modificado Por")]
        public string ca_modificado_por { get; set; }
    }
}
