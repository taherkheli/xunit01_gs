using System.Runtime.InteropServices.ComTypes;
using Xunit;
using Xunit.Abstractions;

namespace GameEngine.Tests
{
  public class BossEnemyShould
  {
    private readonly ITestOutputHelper _output;

    public BossEnemyShould(ITestOutputHelper output)
    {
      _output = output;
    }

    [Fact]
    [Trait("Category", "Boss")]

    public void HaveCorrectPower()
    {
      BossEnemy sut = new BossEnemy();

      _output.WriteLine("Creating Boss Enemy");

      Assert.Equal(166.667, sut.TotalSpecialAttackPower, 3);
    }
  }
}
