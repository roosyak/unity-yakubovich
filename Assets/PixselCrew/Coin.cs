using UnityEngine;

namespace PixselCrew
{
    public class Coin : MonoBehaviour
    {
        /// <summary>
        /// наминал монеты
        /// </summary>
        [SerializeField] private int _price = 1;
        private PixselCrew.Creatures.Hero _hero;

        private void Start()
        {
            _hero = FindObjectOfType<PixselCrew.Creatures.Hero>();
        }

        /// <summary>
        /// добавить наминал монеты герою
        /// </summary>
        public void SetPrice()
        {
            _hero?.AddCoin(_price);
        }
    }
}
