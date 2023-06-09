using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AutoSingletonMono<T> : MonoBehaviour where T : AutoSingletonMono<T>
{
    public static T Instance { get; private set; }

    protected void Awake()
    {
        Initialize();
    }

    private void Initialize()
    {
        if (Instance != null)
            return;

        Instance = (T)this;
    }
}
