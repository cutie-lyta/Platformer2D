using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerTeleport : MonoBehaviour
{
    [SerializeField]
    private float _distance;

    [SerializeField]
    private float _speed;

    [SerializeField]
    private int _maxTP;

    [Tooltip("Debug flag -> AddForce or Velocity")]
    [SerializeField]
    private bool _testForce;

    private Rigidbody2D _rb;

    Vector2 _dir;

    int availableTP;

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        PlayerMain.Instance.Input.Teleport += OnTeleport;
        PlayerMain.Instance.Input.Movement += OnMovement;
        availableTP = _maxTP;
    }

    void OnMovement(InputAction.CallbackContext ctx)
    {
        _dir = ctx.ReadValue<Vector2>().normalized;
    }

    void OnTeleport(InputAction.CallbackContext ctx)
    {
        if (ctx.performed && availableTP > 0)
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

                availableTP--;
            }
        }
    }

    void FixedUpdate()
    {
        if (PlayerMain.Instance.Movement.IsGrounded) availableTP = _maxTP;

    }
}
