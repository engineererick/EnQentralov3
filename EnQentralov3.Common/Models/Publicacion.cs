namespace EnQentralov3.Common.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class Publicacion
    {
        [Key]
        public int PubId { get; set; }
        [Required]
        public string Tipo { get; set; }
        public string Titulo { get; set; }
        [StringLength(50)]
        public string Descripcion { get; set; }
        [Display(Name = "Imagen")]
        public string ImagePath { get; set; }
        [DataType(DataType.Date)]
        public DateTime Fecha { get; set; }
        public string Lugar { get; set; }
        public string UsuPub { get; set; }

        public override string ToString()
        {
            return this.Descripcion;
        }
    }
}