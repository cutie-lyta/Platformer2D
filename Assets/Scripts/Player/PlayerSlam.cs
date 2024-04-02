using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerSlam : MonoBehaviour
{
    [SerializeField]
    private float _initJumpForce;

    [SerializeField]
    private float _gravScale;

    private Rigidbody2D _rb;

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();

        var input = GetComponent<PlayerInputHandler>();
        input.Slam += OnSlam;
    }

    void OnSlam(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            _rb.velocity = new Vector2(_rb.velocity.x, 0);
            _rb.AddForce(new Vector2(0, _initJumpForce), ForceMode2D.Impulse);
            _rb.gravityScale = _gravScale/2;
            Invoke("ChangeGravity", 0.3f);
        }
    }

    private void ChangeGravity()
    {
        _rb.gravityScale = _gravScale;
    }
}
