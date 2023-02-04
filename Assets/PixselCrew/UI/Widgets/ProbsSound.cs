using UnityEngine;

namespace PixselCrew.UI.Widgets
{
    /*
     проиграть звук на «Пробсе» (или объекте)
     */
    public class ProbsSound : MonoBehaviour
    {
        [SerializeField] private AudioClip _audioClip;
        private AudioSource _source;
        public void OnPlay()
        {
            if (_source == null)
                // ищем один раз
                _source = GameObject.FindWithTag("SfxAudioSource").GetComponent<AudioSource>();
            // проиграть один раз
            _source.PlayOneShot(_audioClip);
        }
    }
}