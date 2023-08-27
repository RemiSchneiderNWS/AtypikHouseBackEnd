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

public class ActivityRepository : Repository<Activity>
{
    private readonly AdvertRepository advertRepository;    
    private readonly UserRepository userRepository;
    public ActivityRepository(AppDbContext ctx) : base(ctx)
    {
        Set = ctx.Activity;
    }
    public Activity iniActivity(ActivityPayload activityPayload)
    {
        Advert advert = advertRepository.FromId(activityPayload.AdvertId);
        User user = userRepository.FromId(activityPayload.UserId);

        Activity activity = new Activity();
        activity.Name= activityPayload.Name;
        activity.Advert = advert;
        activity.Adress = activityPayload.Adress;
        activity.City = activityPayload.City;    
        activity.Postal = activityPayload.Postal;
        activity.Describe = activityPayload.Describe;
        activity.User = user;
        return activity;

    }

    public IEnumerable<Activity> getActivityByAdvertId(int advertId)
    {
        return Set.Include(i => i.User).Where(i => i.Advert.Id == advertId);
    }
}