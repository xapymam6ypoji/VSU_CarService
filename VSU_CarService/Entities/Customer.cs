using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VSU_CarService.Entities
{
    public class Customer
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        [Column(TypeName = "VARCHAR")]
        [StringLength(50)]
        public string FirstName { get; set; }
        [Column(TypeName = "VARCHAR")]
        [StringLength(50)]
        public string MiddleName { get; set; }
        [Column(TypeName = "VARCHAR")]
        [StringLength(50)]
        public string LastName { get; set; }
        [Column(TypeName = "VARCHAR")]
        [StringLength(25)]
        public string PhoneNumber { get; set; }
        public ICollection<CustomerCar> CustomerCars { get; set; }
        public ICollection<Order> Orders { get; set; }
        public Customer()
        {
            CustomerCars = new HashSet<CustomerCar>();
            Orders = new HashSet<Order>();
        }
    }
}
