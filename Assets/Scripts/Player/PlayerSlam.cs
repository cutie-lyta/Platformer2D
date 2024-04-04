using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerSlam : MonoBehaviour
{
    [SerializeField]
    private float _initJumpForce;

    [SerializeField]
    private float _gravScale;

    [SerializeField]
    private Collider2D _trigger;

    private Rigidbody2D _rb;
    private TrailRenderer _tr;
    private int _frameCounter;

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _tr = GetComponent<TrailRenderer>();
        _tr.enabled = false;
        _trigger.enabled = false;

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
            _frameCounter = 0;
            _trigger.enabled = true;
            Invoke("ChangeGravity", 0.3f);
        }
    }

    private void FixedUpdate()
    {
        if (PlayerMain.Instance.Movement.IsGrounded && _frameCounter > 4)
        {
            _tr.enabled = false;
            _trigger.enabled = false;
            this.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        _frameCounter++;
    }

    private void ChangeGravity()
    {
        _rb.gravityScale = _gravScale;
    }
}
