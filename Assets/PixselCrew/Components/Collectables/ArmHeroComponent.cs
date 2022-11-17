using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PixselCrew.Components
{
    public class ArmHeroComponent : MonoBehaviour
    {
        public void ArmHero(GameObject go)
        {
            var hero = go.GetComponent<PixselCrew.Creatures.Hero>();
            if (hero != null)
            {
                hero.ArmHero();
            }
        }
    }
}
