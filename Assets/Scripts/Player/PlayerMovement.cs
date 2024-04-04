using System;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.ParticleSystem;

[RequireComponent(typeof(PlayerInputHandler))]
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    public event Action PlayerLand;

    [SerializeField]
    private float _acceleration;
    [SerializeField]
    private float _maxGroundSpeed;
    [SerializeField]
    private float _maxAirSpeed;

    [SerializeField]
    private float _playerHeight;

    [SerializeField]
    private GameObject _partMovement;

    private Vector2 _dir;
    private LayerMask _whatIsGround;
    private Rigidbody2D _rb;
    private GameObject _part;

    private float _initDrag;
    private float _initGravScale;

    public bool IsGrounded { get; private set; } = true;

    // Local Use
    private bool _prevIsGrounded;

    private void Awake()
    {
        PlayerMain.Instance.Input.Movement += OnMovement;

        _rb = GetComponent<Rigidbody2D>();
        _initDrag = _rb.drag;
        _initGravScale = _rb.gravityScale;

        _whatIsGround = LayerMask.GetMask("Ground");
    }

    private void FixedUpdate()
    {
        _rb.AddForce(_dir * _acceleration, ForceMode2D.Force);

        _prevIsGrounded = IsGrounded;

        IsGrounded = Physics2D.Raycast(transform.position, Vector2.down, (_playerHeight * 0.5f) + 0.05f, _whatIsGround);

        if (IsGrounded)
        {
            _rb.drag = _initDrag;
            _rb.gravityScale = _initGravScale;
            SpeedControl(_maxGroundSpeed);
        }
        else
        {
            _rb.drag = 0.0f;
            SpeedControl(_maxAirSpeed);
        }

        if (_rb.velocity != Vector2.zero && IsGrounded)
        {
            _partMovement.transform.position = new Vector3(0, 0.85f, 0);
            if (_part == null)
            {
                _part = Instantiate(_partMovement, this.transform);
            }
        }
        else
        {
            Destroy(_part);
        }
        
        

        if (!_prevIsGrounded && _prevIsGrounded != IsGrounded)
        {
            PlayerLand?.Invoke();
        }
    }

    private void OnMovement(InputAction.CallbackContext ctx)
    {
        _dir = new Vector2(ctx.ReadValue<Vector2>().x, 0).normalized;
    }

    private void SpeedControl(float maxSpeed)
    {
        if (Mathf.Abs(_rb.velocity.x) > Mathf.Abs(maxSpeed))
        {
            float veloX = Mathf.Clamp(_rb.velocity.x + _dir.x * _acceleration, maxSpeed * -1, maxSpeed);
            _rb.velocity = new Vector2(veloX, _rb.velocity.y);
        }
    }
}
