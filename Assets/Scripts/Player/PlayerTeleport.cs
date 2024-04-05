using DG.Tweening;
using System.Collections;
using System;
using UnityEngine;
using UnityEngine.InputSystem;

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

    [SerializeField]
    private Material _material;

    [SerializeField]
    private GameObject _arrow;

    [Tooltip("Debug flag -> AddForce or Velocity")]
    [SerializeField]
    private bool _testForce;

    private Rigidbody2D _rb;
    private SpriteRenderer _spriteRenderer;
    private Material _default;
    private float _angle;

    Vector2 _dir;

    int availableTP;

    int _frameCounter;

    // Start is called before the first frame update
    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _default = _spriteRenderer.material;
        PlayerMain.Instance.Input.Teleport += OnTeleport;
        PlayerMain.Instance.Input.Movement += OnMovement;
        availableTP = _maxTP;
    }

    void OnMovement(InputAction.CallbackContext ctx)
    {
        var readDir = ctx.ReadValue<Vector2>().normalized;

        if (readDir != Vector2.zero)
        {
            _arrow.SetActive(true);
            var angle = Mathf.Atan2(readDir.x, -readDir.y) * (180 / Mathf.PI) + 180;

            angle = Math.QuantizeAngle(angle, 12);
            _angle = angle;
            _arrow.transform.rotation = Quaternion.Euler(0, 0, _angle);

            angle *= (Mathf.PI / 180);
            _dir = new Vector2(-Mathf.Sin(angle), Mathf.Cos(angle));
            _arrow.transform.position = this.transform.position + (Vector3)_dir;
        }

        else
        {
            _arrow.SetActive(false);
        }

    }

    void OnTeleport(InputAction.CallbackContext ctx)
    {
        _arrow.SetActive(false);
        if (ctx.performed && availableTP > 0 && _frameCounter > 14)
        {
            if (_dir == Vector2.zero) return;

            var vect = transform.position + (Vector3)(_dir * _distance);

            var cast = Physics2D.Raycast(vect, _dir, -0.001f);

            var trail = Instantiate(_trail, this.transform.position, Quaternion.Euler(0, 0, Mathf.Atan2(_dir.x, -_dir.y) * (180 / Mathf.PI)));

            if (cast.collider == null)
            {
                transform.position += (Vector3)(_dir * _distance);

            }
            else
            {
                var newCast = Physics2D.Raycast(transform.position, _dir, _distance);
                if (transform.position == (Vector3)(newCast.point - (_dir * 0.75f))) return;
                transform.position = newCast.point - (_dir * 0.75f);

            }

            //Instantiate(_particle, transform.position, transform.rotation);
            trail.transform.DOMove(transform.position, .2f);

            StartCoroutine(Glow());

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

    public IEnumerator Glow()
    {
        _spriteRenderer.material = _material;
        DOTween.To(() => _spriteRenderer.material.GetFloat("_Intensity"), x => _spriteRenderer.material.SetFloat("_Intensity", x), 0.0f, 0.3f);
        yield return new WaitForSeconds(0.3f);
        _spriteRenderer.material = _default;
    }
}
