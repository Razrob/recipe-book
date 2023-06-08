using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static partial class GlobalModel
{
    public static Model Data { get; private set; }
    public static bool DataLoaded => Data != null;

    [RuntimeInitializeOnLoadMethod]
    private static void Initialize()
    {
        if (Data != null)
            return;

        Data = new Model();
    }
}
