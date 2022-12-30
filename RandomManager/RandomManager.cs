using System;
namespace RandomManager;

public partial class RandomManager
{

    private static RandomManager? _instance;
    private Dictionary<string, RandomItem> items;


    private RandomManager()
    {
        items = new Dictionary<string, RandomItem>();
        items["main"] = new RandomItem();
    }

    public static RandomManager Instance 
    {
        get
        {
            if (_instance == null)
                _instance = new RandomManager();
            return _instance;
        }
    }

    public void init(List<string> keys)
    {
        foreach(string key in keys)
        {
            int seed = Instance.items["main"].Random.Next();
            Instance.items[key] = new RandomItem(seed);
        }
    }

    public void Reset()
    {
        Instance.items.Clear();
        Instance.items["main"] = new RandomItem();
    }

    public void Reset(int seed)
    {
        Instance.items.Clear();
        Instance.items["main"] = new RandomItem(seed);
    }
}