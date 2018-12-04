using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnQentralov3.Domain.Models
{
    public class DataContext : DbContext
    {
        public DataContext() : base("DefaultConnection")
        {

        }

        public System.Data.Entity.DbSet<EnQentralov3.Common.Models.Publicacion> Publicacions { get; set; }

        //public System.Data.Entity.DbSet<EnQentralov3.Common.Models.Publicacion> Publicacions { get; set; }
    }
}
