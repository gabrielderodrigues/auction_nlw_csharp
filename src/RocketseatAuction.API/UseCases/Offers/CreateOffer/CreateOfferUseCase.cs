using RocketseatAuction.API.Communication.Requests;
using RocketseatAuction.API.Contracts;
using RocketseatAuction.API.Entities;
using RocketseatAuction.API.Services;

namespace RocketseatAuction.API.UseCases.Offers.CreateOffer;

public class CreateOfferUseCase
{
    private readonly ILoggedUser _loggedUser;
    private readonly IOfferRepository _offerRepository;

    public CreateOfferUseCase(
        ILoggedUser loggedUser, 
        IOfferRepository offerRepository)
    {
        _loggedUser = loggedUser;
        _offerRepository = offerRepository;
    }

    public int Execute(int itemId, RequestCreateOfferJson request)
    {
        var loggedUser = _loggedUser.User();

        var offer = new Offer
        {
            CreatedOn = DateTime.Now,
            ItemId = itemId,
            Price = request.Price,
            UserId = loggedUser.Id
        };

        _offerRepository.Save(offer);

        return offer.Id;
    }
}