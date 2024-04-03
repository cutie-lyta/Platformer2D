using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerSlam : MonoBehaviour
{
    [SerializeField]
    private float _initJumpForce;

    [SerializeField]
    private float _gravScale;

    private Rigidbody2D _rb;
    private TrailRenderer _tr;

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _tr = GetComponent<TrailRenderer>();
        _tr.enabled = false;

        PlayerMain.Instance.Input.Slam += OnSlam;
    }

    void OnSlam(InputAction.CallbackContext ctx)
    {
        if (ctx.performed && !_tr.enabled)
        {
            _rb.velocity = new Vector2(_rb.velocity.x, 0);
            _rb.AddForce(new Vector2(0, _initJumpForce), ForceMode2D.Impulse);
            _rb.gravityScale = _gravScale / 2;
            _tr.enabled = true;
            Invoke("ChangeGravity", 0.3f);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        _tr.enabled = false;
    }

    private void ChangeGravity()
    {
        _rb.gravityScale = _gravScale;
    }
}
