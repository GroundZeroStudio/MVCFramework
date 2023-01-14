using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TSingleton<T> where T : class, new()
{
    private static T instance;

    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new T();
            }
            return instance;
        }
    }

    public static void OnDestory()
    {
        instance = null;
    }
}

public class TSMono<T> : MonoBehaviour where T : class
{
    public static T Instance;

    protected virtual void Awake()
    {
        if (Instance == null)
            Instance = this as T;
    }

    private void OnDestroy()
    {
        Instance = null;
    }
}