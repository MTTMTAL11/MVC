using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace RoboGebV6.Models
{
    public class RacerContext : DbContext
    {

        public RacerContext() : base("Dataconnection")
        {
            Datainitilazer datainitilazer = new Datainitilazer();
            // datainitilazer dtinitilzer = new datainitilazer();
            Database.SetInitializer(datainitilazer);
        }


        public DbSet<Racer> Racers { get; set; }
    }
}