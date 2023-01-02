using System;
namespace RandomManager;

[Serializable]
public class ForbiddenKeyException : Exception
{
    public ForbiddenKeyException() : base() { }
    public ForbiddenKeyException(string message) : base(message) { }
    public ForbiddenKeyException(string message, Exception inner) : base(message, inner) { }
}

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

    public static void Init(List<string> keys, int? seed = null, bool clear = true)
    {
        if (clear)
        {
            if (seed != null)
                Reset((int)seed);
            else
                Reset();
        }
        foreach(string key in keys)
            AddKey(key);
    }

    public static void AddKey(string key)
    {
        if (key == "main")
            throw new ForbiddenKeyException("The key main is reserved for internal use only");
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

    public static int NextKey(string key)
    {
        if (key == "main")
            throw new ForbiddenKeyException("The key main is reserved for internal use only");
        return Instance.items[key].Next();
    }
}