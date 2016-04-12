using System;
//using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;

using System.Data.Entity.Validation;

namespace StatusPage.Data.Models
{
    public class StatusPageContext : DbContext
    {
        public StatusPageContext(bool disableCircularReference = false)
            : base("name=StatusPageContext")
        {
            Database.SetInitializer<StatusPageContext>(new DefaultDataInitializer());

            if (disableCircularReference)
            {
                // see: https://code.msdn.microsoft.com/Loop-Reference-handling-in-caaffaf7
                this.Configuration.LazyLoadingEnabled = false;
                this.Configuration.ProxyCreationEnabled = false;
            }
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        public virtual DbSet<Group> Groups { get; set; }
        public virtual DbSet<System> Systems { get; set; }
        public virtual DbSet<Incident> Incidents { get; set; }
    }

    //DropCreateDatabaseIfModelChanges<StatusPageContext> //  CreateDatabaseIfNotExists<StatusPageContext> // DropCreateDatabaseIfModelChanges<StatusPageContext>
    public class DefaultDataInitializer : DropCreateDatabaseIfModelChanges<StatusPageContext>
    {
        public override void InitializeDatabase(StatusPageContext context)
        {
            try
            {
                base.InitializeDatabase(context);
            }
            //catch (DbEntityValidationException e)
            //{
            //    e = e;
            //    foreach (var item in e.EntityValidationErrors)
            //    {
            //        var y = item;
            //        foreach (var v in item.ValidationErrors)
            //        {
            //            var u = v;
            //        }
            //    }
            //}
            catch (Exception e)
            {
                throw;
            }
        }
        protected override void Seed(StatusPageContext context)
        {
            var g1 = new Group { Title = "Default Group" };
            context.Groups.Add(g1);

            context.Systems.Add(new System { Title = "Default system 1", State = (int)StateEnum.OK, Group = g1 });
            context.Systems.Add(new System { Title = "Default system 2", State = (int)StateEnum.Warning, Group = g1, StateReason = "System is degraded. Planned maintenance." });
            context.Systems.Add(new System { Title = "Default system 3", State = (int)StateEnum.Error, Group = g1, StateReason = "System is down." });

            context.Incidents.Add(new Incident { Title = "System down", Description = "Default system 1 down per un-planned maintenance.", EventDate = DateTime.Now.AddDays(-2), Planned = false });
            context.Incidents.Add(new Incident { Title = "System down", Description = "Default system 2 down per planned maintenance.", EventDate = DateTime.Now.AddDays(-1), Planned = true });

            //EF will call SaveChanges itself
        }
    }
}