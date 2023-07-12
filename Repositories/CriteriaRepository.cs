using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Database;


namespace Repositories;
public class CriteriaRepository : Repository<Criterias>
{
    public CriteriaRepository(AppDbContext ctx) : base(ctx)
    {
        Set = ctx.Criterias;
    }
    
  
}

