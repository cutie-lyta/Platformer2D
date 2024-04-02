using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInputHandler))]
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float _acceleration;
    [SerializeField]
    private float _maxSpeed;

    [SerializeField]
    private float _playerHeight;

    private Vector2 _dir;
    private Rigidbody2D _rb;

    private float _drag;

    private void Awake()
    {
        var input = GetComponent<PlayerInputHandler>();
        input.Movement += OnMovement;
        _rb = GetComponent<Rigidbody2D>();
        _drag = _rb.drag;
    }

    private void FixedUpdate()
    {
        if (Mathf.Abs(_rb.velocity.x) < Mathf.Abs(_maxSpeed))
        {
            _rb.AddForce(_dir * _acceleration, ForceMode2D.Force);
        }

        RaycastHit2D grounded = Physics2D.Raycast(transform.position, Vector2.down, ((_playerHeight * 0.5f) + 0.1f));

        if (grounded.collider != null)
        {
            Debug.Log("I'm on the ground, biiiiiitch.");
            _rb.drag = _drag;
        }
        else
        {
            Debug.Log("I'm in the air motherfucker.");
            _rb.drag = 0.0f;
        }
    }

    private void OnMovement(InputAction.CallbackContext ctx)
    {
        _dir = new Vector2(ctx.ReadValue<Vector2>().x, 0).normalized;
    }
}
