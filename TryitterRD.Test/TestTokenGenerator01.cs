using TryitterRD.Model;
using TryitterRD.Services;

namespace TryitterRD.Test;

public class TestTokenGenerator01
{
  [Fact(DisplayName = "Verifica se o token é gerado com sucesso.")]
  public void TestTokenGeneratorSuccess()
  {
    // throw new NotImplementedException();

    User user = new() { UserId = 01, Name = "Abobo", Password = "Teste123", Email = "teste@teste.com" };

    string? token = TokenService.GenerateToken(user);

    token.Should().NotBeNullOrEmpty();
  }

  [Fact(DisplayName = "Verifica se o token é criado com um formato válido.")]
  public void TestTokenGeneratorKeysSuccess()
  {
    // throw new NotImplementedException();

    User user = new() { UserId = 01, Name = "Abobo", Password = "Teste123", Email = "teste@teste.com" };

    string? token = TokenService.GenerateToken(user);

    token.Split('.').Length.Should().Be(3);
  }
}
