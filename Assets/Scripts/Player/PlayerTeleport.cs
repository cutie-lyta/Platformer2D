using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerTeleport : MonoBehaviour
{
    [SerializeField]
    private float _distance;

    [SerializeField]
    private float _speed;

    [Tooltip("Debug flag -> AddForce or Velocity")]
    [SerializeField]
    private bool _testForce;

    private Rigidbody2D _rb;

    Vector2 _dir;

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        var input = GetComponent<PlayerInputHandler>();
        input.Teleport += OnTeleport;
        input.Movement += OnMovement;
    }

    void OnMovement(InputAction.CallbackContext ctx)
    {
        _dir = ctx.ReadValue<Vector2>().normalized;
    }

    void OnTeleport(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            if (_dir == Vector2.zero) return;

            var vect = transform.position + (Vector3)(_dir * _distance);
            print(vect);

            var cast = Physics2D.Raycast(vect, _dir, 0.1f);

            print(cast.collider);

            if (cast.collider == null)
            {
                transform.position += (Vector3)(_dir * _distance);

                if (_testForce) _rb.AddForce(_dir * _speed, ForceMode2D.Impulse);
                else _rb.velocity = _dir * _speed;
            }
        }
    }
}
