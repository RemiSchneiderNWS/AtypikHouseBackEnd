using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Database;
using Payloads;
using System.Collections;
using System.Globalization;

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
        advert.CriLimit = 2;
        advert.Created_at = DateTime.Now;
        advert.Price= advertPayload.Price;
        advert.Adress = advertPayload.Adress;
        advert.City = advertPayload.City;
        advert.Postal = advertPayload.Postal;
        advert.Describe = advertPayload.Describe;    
        return advert;
 
    }
    public IEnumerable<Advert> getAllAdverts()
    {
        return Set.Include(i => i.User);
    }

    public Advert getAdvertById(int id)
    {
        return Set.Include(i => i.User).First(a => a.Id == id);
    }

    public IEnumerable<Advert>? getBySearch(string indication)
    {
        IEnumerable<Advert> ListAdvertByName = Set.Where(adverts => EF.Functions.Like(adverts.Name, $"%{indication}%")).ToList();
        //IEnumerable<Advert> ListAdvertByName = Set.Where(Adverts => Adverts.Name.ToLowerInvariant().Contains(indication.ToLowerInvariant()));
        if (ListAdvertByName.Count() > 0)
        {
            return ListAdvertByName;
        }
        IEnumerable<Advert> ListAdvertByCity = Set.Where(adverts => EF.Functions.Like(adverts.City, $"%{indication}%")).ToList();
        //IEnumerable<Advert> ListAdvertByCity = Set.Where(Adverts => Adverts.City.Contains(indication, StringComparison.OrdinalIgnoreCase));
        if (ListAdvertByCity.Count() > 0)
        {
            return ListAdvertByCity;
        }
        IEnumerable<Advert>? ListNull = null;
        return ListNull;

    }
}

