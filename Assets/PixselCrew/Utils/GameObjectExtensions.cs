using UnityEngine;


public static class GameObjectExtentions
{
    /// <summary>
    /// входит ли объект в слой
    /// </summary> 
    /// <param name="layer">слой проверки</param>
    /// <returns></returns>
    public static bool IsInLayer(this GameObject go, LayerMask layer)
    {

        return layer == (layer | 1 << go.layer);
    }

    public static TInterfaceType GetInterface<TInterfaceType>(this GameObject go)
    {
        var com = go.GetComponents<Component>();
        foreach (var c in com)
            if (c is TInterfaceType type)
                return type;
        return default;
    }
}