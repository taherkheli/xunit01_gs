using Microsoft.VisualBasic;
using System;
using Xunit;

namespace GameEngine.Tests
{
  [Trait("Category", "Enemy")]
  public class EnemyFactoryShould
  {
    [Fact]
    public void CreateNormalEnemyByDefault()
    {
      var sut = new EnemyFactory();
      
      var enemy = sut.Create("Zombie");

      Assert.IsType<NormalEnemy>(enemy);
    }

    [Fact(Skip = "No need to run this!")]
    public void CreateNormalEnemyByDefault_NotType()
    {
      var sut = new EnemyFactory();

      var enemy = sut.Create("Zombie");

      Assert.IsNotType<DateAndTime>(enemy);
    }

    [Fact]
    public void CreateBossEnemyWhenAsked()
    {
      var sut = new EnemyFactory();

      var enemy = sut.Create("Zombie King", true);

      Assert.IsType<BossEnemy>(enemy);
    }

    [Fact]
    public void CreateBossEnemyWhenAsked_CastReturnedType()
    {
      var sut = new EnemyFactory();

      var enemy = sut.Create("Zombie King", true);

      var boss = Assert.IsType<BossEnemy>(enemy);
      Assert.Equal("Zombie King", boss.Name);
    }

    [Fact]
    public void CreateBossEnemyWhenAsked_AssertAssignableTypes()
    {
      var sut = new EnemyFactory();

      var enemy = sut.Create("Zombie King", true);

      //Assert.IsType<Enemy>(enemy);  //fails due to strct type checking
      Assert.IsAssignableFrom<Enemy>(enemy);
    }

    [Fact]
    public void CreateSeparateInstances()
    {
      var sut = new EnemyFactory();

      var enemy1 = sut.Create("Zombie");
      var enemy2 = sut.Create("Zombie");

      Assert.NotSame(enemy1, enemy2);

      //since enemy1 n enemy2 are Ref types, equal by default checks Ref equality n will do the same
      Assert.NotEqual(enemy1, enemy2);
    }

    [Fact]
    public void NotAllowNullName()
    {
      var sut = new EnemyFactory();

      //Assert.Throws<ArgumentNullException>( () => sut.Create(null) );      
      Assert.Throws<ArgumentNullException>("name" , () => sut.Create(null));   //more specific as we name the parameter
    }

    [Fact]
    public void OnlyAllowKingOrQueenBossEnemies()
    {
      var sut = new EnemyFactory();
      
      var e = Assert.Throws<EnemyCreationException>(() => sut.Create("Dudda G", true));
      Assert.Equal("Dudda G", e.RequestedEnemyName);
    }
  }
}
