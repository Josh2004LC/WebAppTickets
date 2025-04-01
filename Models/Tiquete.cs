using System;
using System.ComponentModel.DataAnnotations;

namespace SistemaTickets.Models
{
    public class Tiquete
    {
        public int ti_identificador { get; set; }

        [Required(ErrorMessage = "El asunto es obligatorio")]
        [Display(Name = "Asunto")]
        public string ti_asunto { get; set; }

        [Required(ErrorMessage = "La categoría es obligatoria")]
        [Display(Name = "Categoría")]
        public string ti_categoria { get; set; }

        [Required(ErrorMessage = "Debe asignar el ticket a un usuario")]
        [Display(Name = "Asignado a")]
        public int ti_us_id_asigna { get; set; }

        [Required(ErrorMessage = "La urgencia es obligatoria")]
        [Display(Name = "Urgencia")]
        public string ti_urgencia { get; set; }

        [Required(ErrorMessage = "La importancia es obligatoria")]
        [Display(Name = "Importancia")]
        public string ti_importancia { get; set; }

        [Required(ErrorMessage = "El estado es obligatorio")]
        [Display(Name = "Estado")]
        public string ti_estado { get; set; }

        [Display(Name = "Solución")]
        public string ti_solucion { get; set; }

        [Display(Name = "Fecha de creación")]
        public DateTime ti_fecha_adicion { get; set; }

        [Display(Name = "Creado por")]
        public string ti_adicionado_por { get; set; }

        [Display(Name = "Fecha de modificación")]
        public DateTime? ti_fecha_modificacion { get; set; }

        [Display(Name = "Modificado por")]
        public string ti_modificado_por { get; set; }
    }
}