namespace RocketseatAuction.API.Communication.Requests;

public class RequestCreateOfferJson
{
    public decimal Price { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
}