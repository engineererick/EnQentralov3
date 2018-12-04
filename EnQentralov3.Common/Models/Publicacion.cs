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
        public string Descripcion { get; set; }
        public DateTime Fecha { get; set; }
        public string Lugar { get; set; }
        public string UsuPub { get; set; }

        public override string ToString()
        {
            return this.Descripcion;
        }
    }
}