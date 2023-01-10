using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace IMDB.DataLayer
{
    public class ContextDB:DbContext
    {
        public ContextDB(DbContextOptions<ContextDB> options) : base(options)
        {

        }
        public DbSet<User> Users { get; set; }
        public DbSet<Specialty> Specialties { get; set; }

        public DbSet<SpecialtyUser> SpecialtiesUsers { get; set; }

        public DbSet<Connection> Connections { get; set; }
    }
}
