using UnityEditor;
using UnityEngine;
using System.Collections;

namespace PixselCrew.Creatures
{
    // абстрактный класс патрулирования 
    public abstract class Patrol : MonoBehaviour
    {
        // метод запуска корутины патрулирования 
        public abstract IEnumerator DoPatrol();
    }
}