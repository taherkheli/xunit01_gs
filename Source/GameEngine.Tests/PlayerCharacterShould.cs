using System;
using System.Collections.Generic;
using Xunit;

namespace GameEngine.Tests
{
  public class PlayerCharacterShould
  {
    [Fact]
    public void BeInexperiencedWhenNew()
    {
      var sut = new PlayerCharacter();
      Assert.True(sut.IsNoob);
    }

    [Fact]
    public void CalculateFullName()
    {
      var sut = new PlayerCharacter();

      sut.FirstName = "Sarah";
      sut.LastName = "Smith";

      Assert.StartsWith("Sarah", sut.FullName);
      Assert.EndsWith("Smith", sut.FullName);
      Assert.Equal("Sarah Smith", sut.FullName);
    }

    [Fact]
    public void CalculateFullName_IgnoreCase()
    {
      var sut = new PlayerCharacter();

      sut.FirstName = "SARAH";
      sut.LastName = "SMITH";

      Assert.Equal("Sarah Smith", sut.FullName, ignoreCase: true);
    }

    [Fact]
    public void CalculateFullName_SubString()
    {
      var sut = new PlayerCharacter();

      sut.FirstName = "Sarah";
      sut.LastName = "Smith";

      Assert.Contains("ah Sm", sut.FullName);
    }

    [Fact]
    public void CalculateFullName_RegExToForPatterns()
    {
      var sut = new PlayerCharacter();

      sut.FirstName = "Sarah";
      sut.LastName = "Smith";

      //checks correct casing -> starts with capital followed by small
      Assert.Matches("[A-Z]{1}[a-z]+ [A-Z]{1}[a-z]+", sut.FullName);
    }

    [Fact]
    public void StartsWithDefaultHealth()
    {
      var sut = new PlayerCharacter();

      Assert.Equal(100, sut.Health);
    }

    [Fact]
    public void StartsWithDefaultHealth_NotEqual()
    {
      var sut = new PlayerCharacter();

      Assert.NotEqual(0, sut.Health);
    }

    [Fact]
    public void IncreaseHealthAfterSleeping()
    {
      var sut = new PlayerCharacter();

      sut.Sleep(); //expect increase between 1 to 100 inclusive

      //Assert.True(sut.Health >= 101 && sut.Health <= 200);   //does not give an informative error message
      Assert.InRange(sut.Health, 101, 200);
    }

    [Fact]
    public void NotHaveNickNameByDefault()
    {
      var sut = new PlayerCharacter();

      Assert.Null(sut.Nickname);
    }

    [Fact]
    public void HaveALongBow()
    {
      var sut = new PlayerCharacter();

      Assert.Contains("Long Bow", sut.Weapons);
    }

    [Fact]
    public void NotHaveAStaffOfWonder()
    {
      var sut = new PlayerCharacter();

      Assert.DoesNotContain("Staff Of Wonder", sut.Weapons);
    }

    [Fact]
    public void HaveAtleastOneKindOfSword()
    {
      var sut = new PlayerCharacter();

      Assert.Contains(sut.Weapons, w => w.Contains("Sword"));
    }

    [Fact]
    public void HaveAllExpectedWeapons()
    {
      var sut = new PlayerCharacter();
      var expectedWeapons = new List<string>
      {
        "Long Bow",
        "Short Bow",
        "Short Sword",
      };

      Assert.Equal(expectedWeapons, sut.Weapons);
    }

    [Fact]
    public void HaveNoEmptyDefaultWeapons()
    {
      var sut = new PlayerCharacter();

      Assert.All(sut.Weapons, w => Assert.False(string.IsNullOrWhiteSpace(w)));
    }

    [Fact]
    public void RaiseSleepEvent()
    {
      var sut = new PlayerCharacter();

      Assert.Raises<EventArgs>( handler => sut.PlayerSlept += handler,
                                handler => sut.PlayerSlept -= handler,
                                () => sut.Sleep());
    }

    [Fact]
    public void RaisePropertyChangedEvent()
    {
      var sut = new PlayerCharacter();

      Assert.PropertyChanged(sut, "Health", () => sut.TakeDamage(10));  // requires the class under test to be implementing INotifyPropertyChanged 
    }
  }
}
