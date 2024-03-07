using Bogus;
using FluentAssertions;
using Moq;
using RocketseatAuction.API.Contracts;
using RocketseatAuction.API.Entities;
using RocketseatAuction.API.Enums;
using RocketseatAuction.API.UseCases.Auctions.GetCurrent;
using Xunit;

namespace UseCases.Test.Auctions.GetCurrent;
public class GetCurrentAuctionUseCaseTest
{
    private readonly Mock<IAuctionRepository> _auctionRepository = new();

    [Fact]
    public void ShouldNotReturnData()
    {
        // Arrange
        var useCase = new GetCurrentAuctionUseCase(_auctionRepository.Object);

        Auction? fakeAuction = null;

        _auctionRepository.Setup(p => p.GetCurrent())
            .Returns(fakeAuction);

        // Act
        var auction = useCase.Execute();

        // Assert
        // Assert.Null(auction); | This is from .NET features.
        auction.Should().BeNull();
    }

    [Fact]
    public void ShouldReturnData()
    {
        // Arrange
        var useCase = new GetCurrentAuctionUseCase(_auctionRepository.Object);

        var fakeAuction = new Faker<Auction>()
            .RuleFor(x => x.Id, faker => faker.Random.Number(1, 700))
            .RuleFor(x => x.Name, faker => faker.Lorem.Word())
            .RuleFor(x => x.Starts, faker => faker.Date.Past())
            .RuleFor(x => x.Ends, faker => faker.Date.Future())
            .RuleFor(x => x.Items, (faker, auction) => new List<Item>
            {
                new Item
                {
                    Id = faker.Random.Number(1, 700),
                    Name = faker.Commerce.ProductName(),
                    Brand = faker.Commerce.Department(),
                    BasePrice = faker.Random.Decimal(50, 1000),
                    Condition = faker.PickRandom<Condition>(),
                    AuctionId = auction.Id
                }
            }).Generate();

        _auctionRepository.Setup(p => p.GetCurrent())
            .Returns(fakeAuction);

        // Act
        var auction = useCase.Execute();

        // Assert
        // Assert.NotNull(auction); | This is from .NET features.
        auction.Should().NotBeNull();
        auction.Id.Should().Be(fakeAuction.Id);
        auction.Name.Should().Be(fakeAuction.Name);
    }
}
