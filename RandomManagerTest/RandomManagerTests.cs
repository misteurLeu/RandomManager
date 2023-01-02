using Xunit;
using RandomManager;
using System.Collections.Generic;

namespace RandomManagerTest;
public class RandomManagerTests
{
    [Fact]
    public void TestFirstCall()
    {
        /*
        ** test if Random manager is initialized at first call
        ** and if it contain an element under 'main' key
        */ 
        Assert.NotNull(RandomManager.RandomManager.Instance);
    }

    [Fact]
    public void TestAddAndDrawKey()
    {
        /*
        ** Test if a key is correctly added.
        ** if an exception is raised with forbidden key for draw and add
        ** and if the result of draw key is correct
        */
        RandomManager.RandomManager.Reset(111);
        Assert.Throws<ForbiddenKeyException>(delegate {
                RandomManager.RandomManager.AddKey("main");
        });
        Assert.Throws<ForbiddenKeyException>(delegate {
                RandomManager.RandomManager.NextKey("main");
        });
        RandomManager.RandomManager.AddKey("toto");
        int result = RandomManager.RandomManager.NextKey("toto");
        Assert.Equal(result, 1206392559);
    }

    [Fact]
    public void TestReset()
    {
        /*
        ** Test if the Random manager is correctly reset
        */
        RandomManager.RandomManager.AddKey("toto");
        RandomManager.RandomManager.Reset();
        Assert.Throws<KeyNotFoundException>(delegate {
                RandomManager.RandomManager.NextKey("toto");
        });
    }

    [Fact]
    public void TestResetWithSeed()
    {
        /*
        ** Test if the Random manager is correctly reset with the given seed
        */
        RandomManager.RandomManager.AddKey("toto");
        RandomManager.RandomManager.Reset(111);
        Assert.Throws<KeyNotFoundException>(delegate {
                RandomManager.RandomManager.NextKey("toto");
        });
        RandomManager.RandomManager.AddKey("toto");
        int result = RandomManager.RandomManager.NextKey("toto");
        Assert.Equal(result, 1206392559);
    }

    [Fact]
    public void TestInitWithoutClear()
    {
        /*
        ** Test if the init work correctl
        */
        RandomManager.RandomManager.Reset(111);
        RandomManager.RandomManager.AddKey("toto");
        RandomManager.RandomManager.Init(new List<string>{"tata", "titi", "tutu"}, clear: false);
        int resultToto = RandomManager.RandomManager.NextKey("toto");
        int resultTata = RandomManager.RandomManager.NextKey("tata");
        int resultTiti = RandomManager.RandomManager.NextKey("titi");
        int resultTutu = RandomManager.RandomManager.NextKey("tutu");
        Assert.Equal(resultToto, 1206392559);
        Assert.Equal(resultTata, 1906954088);
        Assert.Equal(resultTiti, 2025212759);
        Assert.Equal(resultTutu, 1144254750);
    }

    [Fact]
    public void TestInitWithClear()
    {
        /*
        ** Test if the init work correctly
        */
        RandomManager.RandomManager.Reset(111);
        RandomManager.RandomManager.AddKey("toto");
        RandomManager.RandomManager.Init(new List<string>{"tata", "titi", "tutu"}, seed: 111);
        int resultTata = RandomManager.RandomManager.NextKey("tata");
        int resultTiti = RandomManager.RandomManager.NextKey("titi");
        int resultTutu = RandomManager.RandomManager.NextKey("tutu");
        Assert.Equal(resultTata, 1206392559);
        Assert.Equal(resultTiti, 1906954088);
        Assert.Equal(resultTutu, 2025212759);
        Assert.Throws<KeyNotFoundException>(delegate {
                RandomManager.RandomManager.NextKey("toto");
        });
    }
}