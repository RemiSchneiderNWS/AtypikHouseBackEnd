using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Database;


namespace Repositories;
public class CommentaryRepository : Repository<Commentarys>
{
    public CommentaryRepository(AppDbContext ctx) : base(ctx)
    {
        Set = ctx.Commentarys;
    }
    
    public IEnumerable<Commentarys> GetByAdvert(int id)
    {
       return Set.Where( Commentaries => Commentaries.Advert.Id == id).ToList();
    }
}

