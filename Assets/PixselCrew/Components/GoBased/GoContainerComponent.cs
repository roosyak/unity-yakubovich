using UnityEngine;
using UnityEngine.Events;
using System;

namespace PixselCrew.Components
{
    public class GoContainerComponent : MonoBehaviour
    {
        [SerializeField] private GameObject[] _gos;
        [SerializeField] private DropEvent _onDrop;

        [ContextMenu("Drop")]
        public void Drop()
        {
            _onDrop.Invoke(_gos);
        }
    }

    [Serializable]
    public class DropEvent : UnityEvent<GameObject[]> { }
}
