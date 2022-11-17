using System;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace PixselCrew
{
    public class CheckCircleOverlap : MonoBehaviour
    {
        [SerializeField] private float _radius = 1f;
        [SerializeField] private LayerMask _mask;
        [SerializeField] private string[] _tags;
        [SerializeField] private OnOverlapEvent _onOverlap;
        // [SerializeField] private string _tag;
        private Collider2D[] _interactionResult = new Collider2D[10];

        /*public GameObject[] GetObjectsInRange()
        {

            var size = Physics2D.OverlapCircleNonAlloc(transform.position, _radius, _interactionResult);

            var overlaps = new List<GameObject>();
            for (var i = 0; i < size; i++)
                overlaps.Add(_interactionResult[i].gameObject);

            return overlaps.ToArray();
        }*/

#if UNITY_EDITOR
        private void OnDrawGizmosSelected()
        {
            Handles.color = HandlesUtils.transparentRed;
            Handles.DrawSolidDisc(transform.position, Vector3.forward, _radius);
        }
#endif
        public void Check()
        {
            var size = Physics2D.OverlapCircleNonAlloc(transform.position,
                _radius,
                _interactionResult,
                _mask);

            for (var i = 0; i < size; i++)
            {
                var isInTag = _tags.Any(tag => _interactionResult[i].CompareTag(tag));
                if (isInTag)
                {
                    _onOverlap?.Invoke(_interactionResult[i].gameObject);
                }
                //overlaps.Add(.gameObject);
            }
        }

        [Serializable]
        public class OnOverlapEvent : UnityEvent<GameObject>
        {
        }

    }
}
