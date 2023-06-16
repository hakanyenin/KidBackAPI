﻿using Core.CData;
using Core.CEntities.Models;
using Data.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Dal
{
    public class EfSchoolDal : EfEntityRepository<School, KidBackContext>, ISchoolDal
    {
    }
}
