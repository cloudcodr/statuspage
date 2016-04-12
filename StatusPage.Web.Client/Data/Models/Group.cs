using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace StatusPage.Data.Models
{
    [Table("Group")]
    public class Group
    {
        public Group()
        {
            this.ID = Guid.NewGuid();
        }

        [Required]
        public Guid ID { get; set; }
        [Required]
        [StringLength(250)]
        public string Title { get; set; }
        public virtual ICollection<System> Systems { get; set; }
    }
}