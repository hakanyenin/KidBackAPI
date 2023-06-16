using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.CEntities.Models
{
    public class RoleClaim : IEntity
    {
        public int Id { get; set; }
        public int UserTypeId { get; set; }
        public string ClaimName { get; set; }
    }
}
