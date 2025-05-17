using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace RoboGebV6.Models
{
    public class Datainitilazer : DropCreateDatabaseIfModelChanges<RacerContext>
    {

        protected override void Seed(RacerContext context)
        {


            base.Seed(context);
        }

    }
}