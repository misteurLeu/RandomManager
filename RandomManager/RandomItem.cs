using System;
namespace RandomManager;

public class RandomItem: Random
{
    private int _seed;
    private int _callNumber;

    public int Seed
    {
        get => _seed;
    }

    public int CallNumber
    {
        get => _callNumber;
    }

    public override int Next()
    {
        _callNumber += 1;
        return base.Next();
    }

    public RandomItem(int seed): base(seed)
    {
        _seed = seed;
        _callNumber = 0;
    }
    
    public RandomItem(int seed, int numberOfCalls): base(seed)
    {
        _seed = seed;
        _callNumber = numberOfCalls;
        for(int i = 0; i < numberOfCalls; i++)
            this.Next();
    }
}