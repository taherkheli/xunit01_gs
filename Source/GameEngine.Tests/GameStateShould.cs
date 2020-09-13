using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Xunit.Abstractions;

namespace GameEngine.Tests
{
  public class GameStateShould : IClassFixture<GameStateFixture>
  {
    private readonly GameStateFixture _gameStateFixture;
    private readonly ITestOutputHelper _testOutputHelper;

    public GameStateShould(GameStateFixture gameStateFixture, ITestOutputHelper testOutputHelper)
    {
      _gameStateFixture = gameStateFixture;
      _testOutputHelper = testOutputHelper;
    }

    [Fact]
    public void DamageAllPlayersWhenEarthquake()
    {
      _testOutputHelper.WriteLine($"GameState ID={_gameStateFixture.State.Id}");
      var player1 = new PlayerCharacter();
      var player2 = new PlayerCharacter();

      _gameStateFixture.State.Players.Add(player1);
      _gameStateFixture.State.Players.Add(player2);

      var expectedHealthAfterEarthquake = player1.Health - GameState.EarthquakeDamage;
      _gameStateFixture.State.Earthquake();

      Assert.Equal(expectedHealthAfterEarthquake, player1.Health);
      Assert.Equal(expectedHealthAfterEarthquake, player2.Health);
    }

    [Fact]
    public void Reset()
    {
      _testOutputHelper.WriteLine($"GameState ID={_gameStateFixture.State.Id}");
      var player1 = new PlayerCharacter();
      var player2 = new PlayerCharacter();

      _gameStateFixture.State.Players.Add(player1);
      _gameStateFixture.State.Players.Add(player2);
      _gameStateFixture.State.Reset();

      Assert.Empty(_gameStateFixture.State.Players);
    }
  }
}

