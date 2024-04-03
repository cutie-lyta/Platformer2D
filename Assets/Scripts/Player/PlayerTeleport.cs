using System;
using UnityEngine;
using UnityEngine.InputSystem;
using DG.Tweening;

public class PlayerTeleport : MonoBehaviour
{
    public event Action Teleport;

    [SerializeField]
    private float _distance;

    [SerializeField]
    private float _speed;

    [SerializeField]
    private int _maxTP;

    [SerializeField]
    private GameObject _particle;

    [SerializeField]
    private GameObject _trail;
    
    [Tooltip("Debug flag -> AddForce or Velocity")]
    [SerializeField]
    private bool _testForce;

    private Rigidbody2D _rb;

    Vector2 _dir;

    int availableTP;

    int _frameCounter;

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
        if (ctx.performed && availableTP > 0 && _frameCounter > 14)
        {
            if (_dir == Vector2.zero) return;

            var vect = transform.position + (Vector3)(_dir * _distance);
            print(vect);

            var cast = Physics2D.Raycast(vect, _dir, 0.1f);

            print(cast.collider);

            var trail = Instantiate(_trail, this.transform.position, this.transform.rotation);

            if (cast.collider == null)
            {
                transform.position += (Vector3)(_dir * _distance);
                Instantiate(_particle, transform.position, transform.rotation);

            }
            else
            {
                var newCast = Physics2D.Raycast(transform.position, _dir, _distance);
                transform.position = newCast.point - (_dir * 0.75f);
            }

            Instantiate(_particle, transform.position, transform.rotation);
            trail.transform.DOMove(transform.position, .1f);


            if (_testForce) _rb.AddForce(_dir * _speed, ForceMode2D.Impulse);
            else _rb.velocity = _dir * _speed;

            Teleport?.Invoke();

            availableTP--;
            _frameCounter = 0;
        }
    }

    void FixedUpdate()
    {
        if (PlayerMain.Instance.Movement.IsGrounded) availableTP = _maxTP;
        _frameCounter++;
    }
}
