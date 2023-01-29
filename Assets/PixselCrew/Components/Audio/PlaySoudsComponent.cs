using UnityEngine;
using System;

namespace PixselCrew.Components
{
    public class PlaySoudsComponent : MonoBehaviour
    {
        [SerializeField] private AudioData[] _sound;
        private AudioSource _source;

        public void Play(string id)
        {
            foreach (var audioData in _sound)
                if (audioData.Id == id)
                {

                    if (_source == null)
                        // ищем один раз
                        _source = GameObject.FindWithTag("SfxAudioSource").GetComponent<AudioSource>();
                    _source.PlayOneShot(audioData.Clip);
                    break;
                }
        }

        [Serializable]
        public class AudioData
        {
            [SerializeField] private string _id;
            [SerializeField] private AudioClip _clip;

            public string Id => _id;
            public AudioClip Clip => _clip;
        }
    }
}
