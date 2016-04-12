using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace StatusPage.Data.Models
{
    [Table("System")]
    public class System
    {
        public System()
        {
            this.ID = Guid.NewGuid();
            this.State = (int)StateEnum.OK;
        }

        [Required]
        public Guid ID { get; set; }
        [Required]
        [StringLength(250)]
        public string Title { get; set; }
        [StringLength(250)]
        public string SystemName { get; set; }
        [Required]
        public int State { get; set; }
        public string StateReason { get; set; }
        public virtual Group Group { get; set; }
        public Guid? GroupID { get; set; }

        public virtual ICollection<Incident> Incidents { get; set; }
    }

    [Flags]
    public enum StateEnum : int
    {
        OK = 0x1,
        Warning = 0x2,
        Error = 0x3
    }
}