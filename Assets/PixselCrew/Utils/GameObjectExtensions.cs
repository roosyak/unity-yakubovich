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
}