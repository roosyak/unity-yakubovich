using UnityEngine; 
using PixselCrew.Utils.Disposables;
using PixselCrew.Model;
using System;

namespace PixselCrew.UI
{
    public class QuickInventoryController : MonoBehaviour
    {
        [SerializeField] private Transform _conteiner;
        [SerializeField] private GameObject _prefab;
        private readonly CompositeDisposable _trash = new CompositeDisposable();

        private GameSession _session;

        private void Start()
        {
            _session = FindObjectOfType<GameSession>();
            // subscribe model 
            Rebuild();
        }

        private void Rebuild()
        { 

        }
    }
}
