using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VSU_CarService.Entities
{
    public class OrderItem
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public Guid? OrderId { get; set; }
        public Guid? WorkTypeId { get; set; }
        public Order Order { get; set; }
        public WorkType WorkType { get; set; }
    }
}
