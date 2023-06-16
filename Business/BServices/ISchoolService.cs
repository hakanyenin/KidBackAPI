using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.BServices
{
    public interface ISchoolService
    {
        public int GetByUrl(string requestUrl);
    }
}
