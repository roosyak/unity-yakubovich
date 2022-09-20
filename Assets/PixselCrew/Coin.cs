using UnityEngine;

namespace PixelCrew
{
    public class Coin : MonoBehaviour
    {
        /// <summary>
        /// наминал монеты
        /// </summary>
        [SerializeField] private float _price = 1f;
        private Hero _hero;

        private void Start()
        {
            _hero = FindObjectOfType<Hero>();
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
