using System;

public abstract class ASingleton<T>
{
    private static T instance;

    private static T GetInstance()
    {
        if (instance == null)
        {
            instance = Activator.CreateInstance<T>();
        }
        return instance;
    }

    public static T Instance
    {
        get
        {
            return GetInstance();
        }
    }
}
