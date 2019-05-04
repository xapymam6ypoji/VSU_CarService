using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VSU_CarService.Entities
{
    public class WorkType
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Column(TypeName = "VARCHAR")]
        [StringLength(30)]
        public string Name { get; set; }

        [Column(TypeName = "VARCHAR")]
        [StringLength(250)]
        public string Description { get; set; }

        public double WorkingHours { get; set; }
        public double Price { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; }
        public WorkType()
        {
            OrderItems = new HashSet<OrderItem>();
        }
    }
}


