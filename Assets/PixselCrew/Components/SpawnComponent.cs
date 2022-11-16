using UnityEngine;

/// <summary>
/// Создание префаба в игре в заданной позиции 
/// </summary>
public class SpawnComponent : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private GameObject _prefab;
    [SerializeField] private bool _invertXScale;

    [ContextMenu("Spawn")]
    public void Spawn()
    {
        var instante = Instantiate(_prefab, _target.position, Quaternion.identity);
        
        // присваеваем значения «мира» (а не относительные)
        var scale = _target.lossyScale;
        scale.x *= _invertXScale ? -1 : 1;
        instante.transform.localScale = scale;
    }
}