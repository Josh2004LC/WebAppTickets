using System.ComponentModel.DataAnnotations;

namespace TicketsMVC.Models
{
    public class Ticket
    {
        public int ti_identificador { get; set; }

        [Required(ErrorMessage = "El asunto es obligatorio")]
        [Display(Name = "Asunto")]
        public string ti_asunto { get; set; }

        [Required(ErrorMessage = "La categoría es obligatoria")]
        [Display(Name = "Categoría")]
        public int ti_categoria { get; set; }

        [Display(Name = "Usuario Asignado")]
        public int ti_us_id_asigna { get; set; }

        [Required(ErrorMessage = "La urgencia es obligatoria")]
        [Display(Name = "Urgencia")]
        public int ti_urgencia { get; set; }

        [Required(ErrorMessage = "La importancia es obligatoria")]
        [Display(Name = "Importancia")]
        public int ti_importancia { get; set; }

        [Display(Name = "Estado")]
        public string ti_estado { get; set; }

        [Display(Name = "Solución")]
        public string ti_solucion { get; set; }

        [Display(Name = "Fecha de Creación")]
        public DateTime ti_fecha_adicion { get; set; }

        [Display(Name = "Creado Por")]
        public string ti_adicionado_por { get; set; }

        [Display(Name = "Fecha de Modificación")]
        public DateTime? ti_fecha_modificacion { get; set; }

        [Display(Name = "Modificado Por")]
        public string ti_modificado_por { get; set; }

        // Navigation properties for display purposes
        [Display(Name = "Categoría")]
        public string CategoryName { get; set; }

        [Display(Name = "Urgencia")]
        public string UrgencyName { get; set; }

        [Display(Name = "Importancia")]
        public string ImportanceName { get; set; }

        [Display(Name = "Usuario Asignado")]
        public string AssignedUserName { get; set; }
    }
}
