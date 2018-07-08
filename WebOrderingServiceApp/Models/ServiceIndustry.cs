using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class ServiceIndustry
    {
        [Key]
        public int ServiceIndustryId { get; set; }
        //[Required]
        public string Name { get; set; }
        //[Required]
        public string Description { get; set; }

        public ICollection<Executor> Executors { get; set; }
        //[ForeignKey("ServiceIndustryTypeId")]
        public int ServiceIndustryTypeId { get; set; }
        public ServiceIndustryType ServiceIndustryType { get; set; }

        public ServiceIndustry()
        {
            this.Executors = new List<Executor>();

        }
    }
}
