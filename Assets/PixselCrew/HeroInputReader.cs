using UnityEngine;

public class HeroInputReader : MonoBehaviour
{
   [SerializeField] private Hero _hero;

    private void Update()
    {
        var h = Input.GetAxis("Horizontal");
        _hero.SetDirection(h);

        if (Input.GetButtonUp("Fire1")) {
            _hero.SaySomething();
        }
    }
}
