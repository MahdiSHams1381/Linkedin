using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;
using Microsoft.EntityFrameworkCore;
using static Domain.ChatModels;

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

        public DbSet<Connection> UserConnection { get; set; }

        public DbSet<Message> Messages { get; set; }
        public DbSet<UserRoom> UserRooms { get; set; }
        public DbSet<Room> Rooms { get; set; }


    }
}
