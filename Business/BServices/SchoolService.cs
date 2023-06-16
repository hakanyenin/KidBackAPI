using Data.Dal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.BServices
{
    public class SchoolService : ISchoolService
    {
        ISchoolDal _schoolDal;

        public SchoolService(ISchoolDal schoolDal)
        {
            _schoolDal = schoolDal;
        }
        public int GetByUrl(string requestUrl)
        {
            var school = _schoolDal.Get(s => s.Url == requestUrl && s.Status == true);
            if (school == null)
            {
                return 0;
            }
            return school.Id;
        }
    }
}
