using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using UserWebAPI.Models;

namespace UserWebAPI.Data
{
    public class DataContext : DbContext
    {
        //Crear el Contexto
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        { }

        public DbSet<User> Users { get; set; }

    }
}
