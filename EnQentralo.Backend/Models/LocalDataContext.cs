using EnQentralov3.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EnQentralo.Backend.Models
{
    public class LocalDataContext : DataContext
    {
        public System.Data.Entity.DbSet<EnQentralov3.Common.Models.Publicacion> Publicacions { get; set; }
    }
}