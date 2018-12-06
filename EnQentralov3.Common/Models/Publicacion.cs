namespace EnQentralov3.Common.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

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

        [NotMapped]
        public byte[] ImageArray { get; set; }


        public string ImageFullPath
        {
            get
            {
                if (string.IsNullOrEmpty(this.ImagePath))
                {
                    //var content = System.IO.File.ReadAllBytes(Current.Server.MapPath("~/images/facebook.png"));
                    return $"noimg.png";
                }

                return $"https://enqentralov3api.azurewebsites.net/{this.ImagePath.Substring(1)}";
            }
        }

        public override string ToString()
        {
            return this.Descripcion;
        }
    }
}