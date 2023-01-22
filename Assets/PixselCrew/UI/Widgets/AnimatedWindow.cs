using UnityEngine;

namespace PixselCrew.UI
{
    public class AnimatedWindow : MonoBehaviour
    {
        private Animator _animator;
        private static readonly int show = Animator.StringToHash("Show");
        private static readonly int hide = Animator.StringToHash("Hide");

        protected virtual void Start()
        {
            _animator = GetComponent<Animator>();
            _animator.SetTrigger(show);
        }

        public void Close()
        {
            _animator.SetTrigger(hide);
        }

        public virtual void OnCloseAnimationComplete()
        {
            Destroy(gameObject);
        }
    }
}
