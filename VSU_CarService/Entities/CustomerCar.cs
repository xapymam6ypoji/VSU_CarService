using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VSU_CarService.Entities
{
    public class CustomerCar
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public Guid? CardModelId { get; set; }
        public Guid? CustomerId { get; set; }
        public CarModel CarModel { get; set; }
        public Customer Customer { get; set; }
        [Column(TypeName = "VARCHAR")]
        [StringLength(25)]
        public string RegistrationNumber { get; set; }
    }
}
