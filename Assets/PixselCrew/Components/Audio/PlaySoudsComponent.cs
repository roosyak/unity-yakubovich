using UnityEngine;
using System;

namespace PixselCrew.Components
{
    public class PlaySoudsComponent : MonoBehaviour
    {
        [SerializeField] private AudioSource _source;
        [SerializeField] private AudioData[] _sound;

        public void Play(string id)
        {
            foreach (var audioData in _sound)
                if (audioData.Id == id)
                {
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
