using System;
using Xunit;
using RandomManager;

namespace RandomManagerTest;

public class RandomItemTests
{
    [Fact]
    public void TestInitWithSeed()
    {
        int seed = 45;
        Random to_test = new Random(seed);
        RandomManager.RandomItem value = new RandomManager.RandomItem(seed);

        Assert.Equal(to_test.Next(), value.Random.Next());
    }
}