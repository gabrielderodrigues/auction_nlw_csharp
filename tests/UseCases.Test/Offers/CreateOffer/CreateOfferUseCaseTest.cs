using Bogus;
using FluentAssertions;
using Moq;
using RocketseatAuction.API.Communication.Requests;
using RocketseatAuction.API.Contracts;
using RocketseatAuction.API.Entities;
using RocketseatAuction.API.Services;
using RocketseatAuction.API.UseCases.Offers.CreateOffer;
using Xunit;

namespace UseCases.Test.Offers.CreateOffer;

public class CreateOfferUseCaseTest
{
    private readonly Mock<ILoggedUser> _loggedUser = new();
    private readonly Mock<IOfferRepository> _offerRepository = new();

    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(3)]
    public void Success(int itemId)
    {
        // ARRANGE
        var request = new Faker<RequestCreateOfferJson>()
            .RuleFor(request => request.Price, f => f.Random.Decimal(10, 700))
            .Generate();

        _loggedUser.Setup(p => p.User())
            .Returns(new User());

        var useCase = new CreateOfferUseCase(_loggedUser.Object, _offerRepository.Object);

        //ACT
        var act = () => useCase.Execute(itemId, request);

        // ASSERT
        act.Should().NotThrow();
    }
}