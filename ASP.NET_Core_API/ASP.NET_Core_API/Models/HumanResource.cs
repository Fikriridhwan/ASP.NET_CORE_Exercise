using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ASP.NET_Core_API.Models
{
    [Table("TB_M_HumanResource")]
    public class HumanResource
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
