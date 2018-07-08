using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Converter.Models
{
    public class ServiceIndustry
    {
        public int ServiceIndustryId { get; set; }
        public string Name { get; set; }
        public List<Executor> Executors { get; set; }
        public string Description { get; set; }
    }
}
