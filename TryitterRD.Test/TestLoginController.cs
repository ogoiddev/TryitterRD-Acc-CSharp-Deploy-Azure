// using TryitterRD.Model;
// using Microsoft.AspNetCore.Mvc.Testing;
// using System.Net.Http.Json;
// using System.Net;

// namespace TryitterRD.Test;

// public class TestLoginController : IClassFixture<WebApplicationFactory<Program>>
// {
//   private readonly WebApplicationFactory<Program> _factory;

//   public TestLoginController(WebApplicationFactory<Program> factory)
//   {
//     _factory = factory;
//   }


//   [Theory(DisplayName = "Teste para login de pessoa com Status OK")]
//   [InlineData("huguinho@teste.com", "Hugo123")]
//   [InlineData("zezinho@teste.com", "Jose123")]
//   [InlineData("luizinho@teste.com", "Luiz123")]  
//   public async Task TestLoginSuccess(string email, string password)
//   {
//     // throw new NotImplementedException();

//     var client = _factory.CreateClient();

//     User user = new() { UserId = 01, Name = "Abobo", Password = password, Email = email };

//     var response = await client.PostAsJsonAsync("/login", user);

//     response.StatusCode.Should().Be(HttpStatusCode.OK);
//   }

//   [Theory(DisplayName = "Teste para login de pessoa com Status Not Found")]
//   [InlineData("huguinho@teste.com", "Hugo123")]
//   [InlineData("zezinho@teste.com", "Jose123")]
//   [InlineData("luizinho@teste.com", "Luiz123")] 
//   public async Task TestLoginNotFoundFail(string email, string password)
//   {
//     throw new NotImplementedException();

//     var client = _factory.CreateClient();

//     User instance = new() { Email = email, Password = password };

//     var response = await client.PostAsJsonAsync("/login", instance);

//     response.StatusCode.Should().Be(HttpStatusCode.NotFound);
//   }

//   [Theory(DisplayName = "Teste para login de pessoa com Status Bad Request")]
//   [InlineData("", "hugo123")]
//   [InlineData("zezinho@teste.co", "")]
//   [InlineData("", "")]
//   public async Task TestLoginBadRequestFail(string email, string password)
//   {
//     throw new NotImplementedException();

//     var client = _factory.CreateClient();

//     User instance = new() { Email = email, Password = password };

//     var response = await client.PostAsJsonAsync("/login", instance);

//     response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
//   }
// }
