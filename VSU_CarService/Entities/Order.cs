using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VSU_CarService.Entities
{
    public class Order
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public DateTime TimeStamp { get; set; }
        public OrderStatuses OrderStatuse { get; set; }
        public Guid? CustomerId { get; set; }
        public Guid? CustomerCarId { get; set; }
        public Guid? ServiceMasterId { get; set; }
        public Customer Customer { get; set; }
        public CustomerCar CustomerCar { get; set; }
        public ServiceMaster ServiceMaster { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; }
        public Order()
        {
            OrderItems = new HashSet<OrderItem>();
        }
    }

    public enum OrderStatuses
    {
        InWork, Suspended, Closed
    }
}
