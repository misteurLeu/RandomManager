using System;
namespace RandomManager;

public partial class RandomManager
{

    private static RandomManager? _instance;
    private Dictionary<string, RandomItem> items;


    private RandomManager()
    {
        int seed = (int)DateTimeOffset.Now.ToUnixTimeSeconds();

        items = new Dictionary<string, RandomItem>();
        items["main"] = new RandomItem(seed);
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

    public static void Init(List<string> keys, bool clear = true)
    {
        if (clear)
            Instance.items.Clear();
        foreach(string key in keys)
        {
            AddKey(key);
        }
    }

    public static void AddKey(string key)
    {
        int seed = Instance.items["main"].Next();
        Instance.items[key] = new RandomItem(seed);
    }

    public static void Reset()
    {
        int seed = (int)DateTimeOffset.Now.ToUnixTimeSeconds();

        Instance.items.Clear();
        Instance.items["main"] = new RandomItem(seed);
    }

    public static void Reset(int seed)
    {
        Instance.items.Clear();
        Instance.items["main"] = new RandomItem(seed);
    }
}