using UnityEngine;

public class Hero : MonoBehaviour
{
    [SerializeField] private float _speed;
    private float _dX, _dY;
    
    public void SetDirection(float dX, float dY) {
        _dX = dX;
        _dY = dY;
    }
    private void Update()
    {
        if (_dX != 0) { 
            var x = transform.position.x + (_dX * _speed * Time.deltaTime);
            transform.position = new Vector3(x, transform.position.y, transform.position.z);
        }
        if (_dY != 0)
        { 
            var y = transform.position.y + (_dY * _speed * Time.deltaTime);
            transform.position = new Vector3(transform.position.x, y, transform.position.z);
        }
    }
    public void SaySomething() {
        Debug.Log("SaySomething");
    }
}
