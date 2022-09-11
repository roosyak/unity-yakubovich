using UnityEngine;

public class Hero : MonoBehaviour
{
    [SerializeField] private float _speed = 1f;
    [SerializeField] private float _jumpSpeed = 1f;
    [SerializeField] private LayerCheck _groundCheck;

    private Vector2 _direction;
    private Rigidbody2D _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }
    public void SetDirection(Vector2 direction)
    {
        _direction = direction;
    }

    private void FixedUpdate()
    {
        _rigidbody.velocity = new Vector2(_direction.x * _speed, _rigidbody.velocity.y);
        var _isJumping = _direction.y > 0;
        if (_isJumping)
        {
            if (IsGrounded() && _rigidbody.velocity.y <= 0)
            {
                _rigidbody.AddForce(Vector2.up * _jumpSpeed, ForceMode2D.Impulse);
            }
        }
        else if (_rigidbody.velocity.y > 0)
        {
            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, _rigidbody.velocity.y * 0.5f);
        }
    }


    /// <summary>
    /// стоит ли герой на поверхности  
    /// </summary>
    /// <returns></returns>
    private bool IsGrounded()
    {
        return _groundCheck.IsTouchingLayer;
    }

    public void SaySomething()
    {
        Debug.Log("SaySomething");
    }
}
