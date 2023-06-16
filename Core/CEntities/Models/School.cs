using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.CEntities.Models
{
    public class School : IEntity
    {
        public int Id { get; set; }
        public string ShortName { get; set; }
        public string City { get; set; }
        public string County { get; set; }
        public string District { get; set; }
        public string Url { get; set; }
        public bool Status { get; set; } // okul sistemden çıkarsa false olur
    }
}
