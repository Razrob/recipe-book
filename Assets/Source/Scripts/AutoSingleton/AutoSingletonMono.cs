using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AutoSingletonMono<T> : MonoBehaviour where T : AutoSingletonMono<T>
{
    public static T Instance { get; private set; }

    [RuntimeInitializeOnLoadMethod]
    private static void Initialize()
    {
        if (Instance != null)
            return;

        GameObject gameObject = new GameObject($"{typeof(T).Name}");
        Instance = gameObject.AddComponent<T>();
    }
}
