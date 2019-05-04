using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VSU_CarService.Entities
{
    public class CarModel
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public Guid? CardBrandId { get; set; }
        [Column(TypeName = "VARCHAR")]
        [StringLength(50)]
        public string Name { get; set; }
        public int ModelYear { get; set; }
        public CarBrand CarBrand { get; set; }
        public ICollection<CustomerCar> CustomerCars { get; set; }

        public CarModel()
        {
            CustomerCars = new HashSet<CustomerCar>();
        }
    }
}
