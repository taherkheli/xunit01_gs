using System;
using System.Collections.Generic;
using Xunit;
using Xunit.Abstractions;

namespace GameEngine.Tests
{
  public class PlayerCharacterShould : IDisposable
  {
    private readonly PlayerCharacter _sut;
    private readonly ITestOutputHelper _output;

    public PlayerCharacterShould(ITestOutputHelper output)
    {
      _output = output;

      _output.WriteLine("Creating new PlayerCharacter");
      _sut = new PlayerCharacter();
    }

    [Fact]
    public void BeInexperiencedWhenNew()
    {
      Assert.True(_sut.IsNoob);
    }

    [Fact]
    public void CalculateFullName()
    {
      _sut.FirstName = "Sarah";
      _sut.LastName = "Smith";

      Assert.StartsWith("Sarah", _sut.FullName);
      Assert.EndsWith("Smith", _sut.FullName);
      Assert.Equal("Sarah Smith", _sut.FullName);
    }

    [Fact]
    public void CalculateFullName_IgnoreCase()
    {
      _sut.FirstName = "SARAH";
      _sut.LastName = "SMITH";

      Assert.Equal("Sarah Smith", _sut.FullName, ignoreCase: true);
    }

    [Fact]
    public void CalculateFullName_SubString()
    {
      _sut.FirstName = "Sarah";
      _sut.LastName = "Smith";

      Assert.Contains("ah Sm", _sut.FullName);
    }

    [Fact]
    public void CalculateFullName_RegExToForPatterns()
    {
      _sut.FirstName = "Sarah";
      _sut.LastName = "Smith";

      //checks correct casing -> starts with capital followed by small
      Assert.Matches("[A-Z]{1}[a-z]+ [A-Z]{1}[a-z]+", _sut.FullName);
    }

    [Fact]
    public void StartsWithDefaultHealth()
    {
      Assert.Equal(100, _sut.Health);
    }

    [Fact]
    public void StartsWithDefaultHealth_NotEqual()
    {
      Assert.NotEqual(0, _sut.Health);
    }

    [Fact]
    public void IncreaseHealthAfterSleeping()
    {
      _sut.Sleep(); //expect increase between 1 to 100 inclusive

      //Assert.True(_sut.Health >= 101 && _sut.Health <= 200);   //does not give an informative error message
      Assert.InRange(_sut.Health, 101, 200);
    }

    [Fact]
    public void NotHaveNickNameByDefault()
    {
      Assert.Null(_sut.Nickname);
    }

    [Fact]
    public void HaveALongBow()
    {
      Assert.Contains("Long Bow", _sut.Weapons);
    }

    [Fact]
    public void NotHaveAStaffOfWonder()
    {
      Assert.DoesNotContain("Staff Of Wonder", _sut.Weapons);
    }

    [Fact]
    public void HaveAtleastOneKindOfSword()
    {
      Assert.Contains(_sut.Weapons, w => w.Contains("Sword"));
    }

    [Fact]
    public void HaveAllExpectedWeapons()
    {
      var expectedWeapons = new List<string>
      {
        "Long Bow",
        "Short Bow",
        "Short Sword",
      };

      Assert.Equal(expectedWeapons, _sut.Weapons);
    }

    [Fact]
    public void HaveNoEmptyDefaultWeapons()
    {
      Assert.All(_sut.Weapons, w => Assert.False(string.IsNullOrWhiteSpace(w)));
    }

    [Fact]
    public void RaiseSleepEvent()
    {
      Assert.Raises<EventArgs>( handler => _sut.PlayerSlept += handler,
                                handler => _sut.PlayerSlept -= handler,
                                () => _sut.Sleep());
    }

    [Fact]
    public void RaisePropertyChangedEvent()
    {
      Assert.PropertyChanged(_sut, "Health", () => _sut.TakeDamage(10));  // requires the class under test to be implementing INotifyPropertyChanged 
    }

    public void Dispose()
    {
      _output.WriteLine($"Disposing PlayerCharacter {_sut.FirstName}");

      //_sut.Dispose();
    }
  }
}
