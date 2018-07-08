using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class ServiceIndustryType
    {
        [Key]
        public int ServiceIndustryTypeId { get; set; }

        //[Required]
        public string Name { get; set; }
        public string ServiceIndustryTypePhoto { get; set; }
        public ICollection<ServiceIndustry> ServiceIndustries{ get; set; }
        public ServiceIndustryType()
        {
            this.ServiceIndustries = new List<ServiceIndustry>();
        }
    }
}
