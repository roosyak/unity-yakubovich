using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.PixselCrew.UI.Widgets
{
    /*
     проиграть звук по нажатию кнопки
     */
    public class ButtonSound : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private AudioClip _audioClip;
        private AudioSource _source;
        public void OnPointerClick(PointerEventData eventData)
        {
            if (_source == null)
                // ищем один раз
                _source = GameObject.FindWithTag("SfxAudioSource").GetComponent<AudioSource>();
            // проиграть один раз
            _source.PlayOneShot(_audioClip);
        }
    }
}