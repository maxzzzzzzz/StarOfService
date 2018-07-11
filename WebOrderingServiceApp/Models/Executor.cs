using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Executor
    {
        [Key]
        public int ExecutorId { get; set; }

        public double Price { get; set; }
        //[Required]
        public DateTime DateTime { get; set; }

        //[ForeignKey("UserId")]
        public string UserId { get; set; }
        public User User { get; set; }

        //[ForeignKey("ServiceIndustryId")]
        public int ServiceIndustryId { get; set; }
        public ServiceIndustry ServiceIndustry { get; set; }
    }
}
