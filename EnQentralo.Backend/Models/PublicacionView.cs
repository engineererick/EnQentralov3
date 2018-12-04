using EnQentralov3.Common.Models;
using System.Web;

namespace EnQentralo.Backend.Models
{
    public class PublicacionView : Publicacion
    {
        public HttpPostedFileBase ImageFile { get; set; }
    }
}