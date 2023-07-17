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

public class ActivityImageRepository : Repository<ActivityImage>
{
    private readonly ActivityRepository activityRepository;
    public ActivityImageRepository(AppDbContext ctx, ActivityRepository activityRepository) : base(ctx)
    {
        this.activityRepository = activityRepository;
        Set = ctx.ActivityImages;
    }

    public IEnumerable<ActivityImage> getImagesByActivityId(int id)
    {
        return Set.Where(ActivityImage => ActivityImage.Activity.Id == id).ToList();
    }

    public ActivityImage iniActivityImage(ActivityImagePayload activityimagePayload)
    {
        ActivityImage activityImage = new ActivityImage();  
        activityImage.Name = activityimagePayload.Name;
        activityImage.Order = activityimagePayload.Order;
        activityImage.Activity = activityRepository.FromId(activityimagePayload.ActivityId);
        return activityImage;
    }
}