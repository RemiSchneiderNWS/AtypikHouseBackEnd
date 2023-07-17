using Domain;
using Database;
using Payloads;

namespace Repositories;

public class AdvertImageRepository : Repository<AdvertImage>
{
    private readonly AdvertRepository advertRepository;
    
    public AdvertImageRepository(AppDbContext ctx, AdvertRepository advertRepository) : base(ctx)
    {
        Set = ctx.AdvertImages;
        this.advertRepository = advertRepository;  
    }

    public IEnumerable<AdvertImage> getImagesByAdvertId(int id)
    {
        return Set.Where(AdvertImage => AdvertImage.Advert.Id == id).ToList();
    }
    public AdvertImage iniAdvertImage(AdvertImagePayload advertPayload)
    {
        AdvertImage advertImage = new AdvertImage();
        advertImage.Name = advertPayload.Name;
        advertImage.Order = advertPayload.Order;
        advertImage.Advert = advertRepository.FromId(advertPayload.AdvertId);
        return advertImage;
    }

}
