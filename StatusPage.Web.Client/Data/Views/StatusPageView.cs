using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using StatusPage.Data.Models;

namespace StatusPage.Data.Views
{
    public class StatusPageView : IDisposable
    {
        private StatusPageContext db;

        public StatusPageView()
        {
            db = new StatusPageContext();
        }

        public bool AllOperational
        {
            get
            {
                foreach (var s in Systems)
                {
                    if ((StateEnum)s.State != StateEnum.OK)
                        return false;
                }

                return true;
            }
        }

        public IEnumerable<StatusPage.Data.Models.System> Systems
        {
            get
            {
                return db.Systems.OrderBy(s => s.Title);
            }
        }
        public IEnumerable<Group> Groups
        {
            get
            {
                return db.Groups.OrderBy(g => g.Title);
            }
        }
        public IEnumerable<Incident> PastIncidents
        {
            get
            {
                DateTime compareDate = DateTime.Now.AddHours(-8);

                return db.Incidents.Where(i => i.EventDate <= compareDate);
            }
        }

        public void Dispose()
        {
            db.Dispose();
        }
    }
}