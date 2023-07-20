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
public class CriteriaAdvertRepository : Repository<CriteriaAdvert>
{
    private readonly AdvertRepository advertRepository;
    private readonly CriteriaRepository criteriaRepository;
    public CriteriaAdvertRepository(AppDbContext ctx, AdvertRepository advertRepository, CriteriaRepository criteriaRepository ) : base(ctx)
    {
        Set = ctx.CriteriaAdverts;
        this.advertRepository = advertRepository;
        this.criteriaRepository = criteriaRepository;
    }
    
   public CriteriaAdvert iniCriteriaAdvert(CriteriaAdvertPayload criteriaAdvertPayload)
    {
        CriteriaAdvert criteriaAdvert = new CriteriaAdvert();
        criteriaAdvert.Advert = advertRepository.FromId(criteriaAdvertPayload.AdvertId);
        criteriaAdvert.Criteria = criteriaRepository.FromId(criteriaAdvertPayload.CriteriaId);
        return criteriaAdvert; 
    }
}

