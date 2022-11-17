using System.Collections;
using UnityEngine;

namespace PixselCrew.Creatures
{
    // патрулирование по двум точкам 
    public class PointPotrol : Patrol
    {
        [SerializeField] private Transform[] _points;
        [SerializeField] private float _treshold = 1f;
        private Creature _creature;
        private int _distenationPointIndex;

        private void Awake()
        {
            _creature = GetComponent<Creature>();
        }
        public override IEnumerator DoPatrol()
        {
            while (enabled)
            {
                if (IsOnPoint())
                    _distenationPointIndex = (int)Mathf.Repeat(_distenationPointIndex + 1, _points.Length);


                var direction = _points[_distenationPointIndex].position - transform.position;
                direction.y = 0;
                _creature.SetDirection(direction.normalized);

                yield return null;
            }
        }

        private bool IsOnPoint()
        {
            return (_points[_distenationPointIndex].position - transform.position).magnitude < _treshold;
        }
    }
}