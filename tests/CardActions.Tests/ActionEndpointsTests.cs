using System.Net;
using System.Net.Http.Json;
using CardActions.API.Models.Api;
using Microsoft.AspNetCore.Mvc.Testing;

namespace CardActions.Tests;

public class ActionEndpointsTests : IClassFixture<WebApplicationFactory<Program>>
{
    private static readonly string LongString = new('a', 101);

    private readonly HttpClient _client;

    public ActionEndpointsTests(WebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task GetActions_ReturnsBadRequest_WhenNoQueryParameterIsPassed()
    {
        // Arrange, Act
        var response = await _client.GetAsync("/api/actions");

        // Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }
    
    [Fact]
    public async Task GetActions_ReturnsBadRequest_WhenCardNumberIsNotPassed()
    {
        // Arrange, Act
        var response = await _client.GetAsync("/api/actions?userId=User1");
        // Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }
    
    [Fact]
    public async Task GetActions_ReturnsBadRequest_WhenCardNumberIsTooLong()
    {
        // Arrange, Act
        var response = await _client.GetAsync($"/api/actions?userId=User1&cardNumber={LongString}");
        // Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }
    
    [Fact]
    public async Task GetActions_ReturnsBadRequest_WhenUserIdIsNotPassed()
    {
        // Arrange, Act
        var response = await _client.GetAsync("/api/actions?cardNuber=Card11");
        // Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }
    
    [Fact]
    public async Task GetActions_ReturnsBadRequest_WhenUserIdIsTooLong()
    {
        // Arrange, Act
        var response = await _client.GetAsync($"/api/actions?userId={LongString}&cardNuber=Card11");
        // Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }
    
    [Fact]
    public async Task GetActions_ReturnsBadRequest_WhenThereIsNoSuchCard()
    {
        // Arrange, Act
        var response = await _client.GetAsync($"/api/actions?userId=random&cardNuber=random");
        // Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Theory]
    [InlineData("User1", "Card11", new string [] { "ACTION3", "ACTION4","ACTION6","ACTION8","ACTION9","ACTION10","ACTION12","ACTION13" })]
    [InlineData("User1", "Card16", new string [] { "ACTION3", "ACTION4","ACTION9" })]
    [InlineData("User2", "Card28", new string [] { "ACTION3", "ACTION4","ACTION7","ACTION8","ACTION9","ACTION10","ACTION12","ACTION13" })]
    [InlineData("User2", "Card212", new string [] { "ACTION3", "ACTION4","ACTION8","ACTION9" })]
    [InlineData("User3", "Card315", new string [] { "ACTION3", "ACTION4","ACTION6","ACTION8","ACTION9","ACTION10","ACTION12","ACTION13" })]
    [InlineData("User3", "Card315", new string [] { "ACTION3", "ACTION4","ACTION5","ACTION9" })]
    public async Task GetActions_ReturnsAllowedActions_WhenDataIsCorrect(string userId, string cardNumber, string[] expectedActions)
    {
        // Arrange, Act
        var response = await _client.GetAsync($"/api/actions?userId={userId}&cardNumber={cardNumber}");

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        var actions = await response.Content.ReadFromJsonAsync<GetActionsResponse>();
        Assert.NotNull(actions);
        Assert.NotEmpty(actions.AllowedActions);
        Assert.Equivalent(expectedActions, actions.AllowedActions);
    }
}