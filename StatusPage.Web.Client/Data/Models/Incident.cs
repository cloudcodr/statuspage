using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace StatusPage.Data.Models
{
    [Table("Incident")]
    public class Incident
    {
        public Incident()
        {
            this.ID = Guid.NewGuid();
            this.Planned = false;
        }

        [Required]
        public Guid ID { get; set; }
        [Required]
        [StringLength(250)]
        public string Title { get; set; }
        public string Description { get; set; }
        [Required]
        [DataType(DataType.DateTime)]
        public DateTime EventDate { get; set; }
        [Required]
        public bool Planned { get; set; }

        public Guid? SystemID { get; set; }
        public System System { get; set; }
    }
}