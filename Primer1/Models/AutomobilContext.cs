using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Primer1.Models
{
    public class AutomobilContext : DbContext
    {
        public AutomobilContext(DbContextOptions<AutomobilContext> opcije) : base(opcije)
        {

        }
        public DbSet<Automobil> Automobili { get; set; }
    }
}
