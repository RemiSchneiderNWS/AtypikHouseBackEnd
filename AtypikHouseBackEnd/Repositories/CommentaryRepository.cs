using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Database;
using Payloads;

namespace Repositories;
public class CommentaryRepository : Repository<Commentarys>
{
    private readonly AdvertRepository repository;
    public CommentaryRepository(AppDbContext ctx , AdvertRepository advertRepository) : base(ctx)
    {
        this.repository = advertRepository;
        Set = ctx.Commentarys;
    }
    public Commentarys iniCommentary(CommentaryPayload commnentaryPayload)
    {
        Commentarys commentarys = new Commentarys();
        commentarys.Text = commnentaryPayload.Text;
        commentarys.Advert = repository.FromId(commnentaryPayload.advertId);
        return commentarys; 
    }
    public IEnumerable<Commentarys> GetByAdvert(int id)
    {
       return Set.Include(i => i.User).Where( Commentaries => Commentaries.Advert.Id == id).ToList();
    }
}

