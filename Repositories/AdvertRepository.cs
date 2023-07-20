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
public class AdvertRepository : Repository<Advert>
{
    public AdvertRepository(AppDbContext ctx) : base(ctx)
    {
        Set = ctx.Adverts;
    }
 
    public Advert iniAdvert(AdvertPayload advertPayload)
    {

        Advert advert = new Advert();
        advert.Name= advertPayload.Name;
        advert.Type= advertPayload.Type;
        advert.Tenants= advertPayload.Tenants;
        advert.Status = advertPayload.Status;
        advert.Up= advertPayload.Up;
        advert.CriLimit= advertPayload.CriLimit;
        advert.Price= advertPayload.Price;
        advert.Adress = advertPayload.Adress;
        advert.City = advertPayload.City;
        advert.Postal = advertPayload.Postal;
        advert.Describe = advertPayload.Describe;    
        return advert;
    }
}

