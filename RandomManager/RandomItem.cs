using System;
namespace RandomManager;

public class RandomItem
{

    private Random _random;
    private int _seed;

    public Random Random
    {
        get => _random;
    }

    public int Seed
    {
        get => _seed;
    }

    public RandomItem()
    {
        _seed = (int)DateTimeOffset.Now.ToUnixTimeSeconds();
        _random = new Random();
    }

    public RandomItem(int seed)
    {
        _seed = seed;
        _random = new Random(seed);
    }

}