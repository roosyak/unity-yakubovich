using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace PixelCrew
{
    public class CheckCircleOverlap : MonoBehaviour
    {
        [SerializeField] private float _radius = 1f;
        // [SerializeField] private string _tag;
        private Collider2D[] _interactionResult = new Collider2D[5];

        public GameObject[] GetObjectsInRange()
        {

            var size = Physics2D.OverlapCircleNonAlloc(transform.position, _radius, _interactionResult);

            var overlaps = new List<GameObject>();
            for (var i = 0; i < size; i++)
                overlaps.Add(_interactionResult[i].gameObject);

            return overlaps.ToArray();
        }

#if UNITY_EDITOR
        private void OnDrawGizmosSelected()
        {
            Handles.color = HandlesUtils.transparentRed;
            Handles.DrawSolidDisc(transform.position, Vector3.forward, _radius);
        }
#endif
    }
}
