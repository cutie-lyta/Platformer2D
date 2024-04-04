using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerSlam : MonoBehaviour
{
    public event Action Slamming;


    [SerializeField]
    private float _initJumpForce;

    [SerializeField]
    private float _gravScale;

    [SerializeField]
    private int[] _framesForSlamStage;
    
    [SerializeField]
    private Collider2D _trigger;



    private Rigidbody2D _rb;
    private TrailRenderer _tr;

    private bool _slamming;
    private int _slamStage = 0;
    private int _frameCounter;

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _tr = GetComponent<TrailRenderer>();
        _tr.enabled = false;
        _trigger.enabled = false;

        PlayerMain.Instance.Input.Slam += InitiateSlam;
    }

    private void FixedUpdate()
    {
        if (_slamming)
        {
            if (_frameCounter >= _framesForSlamStage[_slamStage])
            {
                _slamStage++;
                Debug.Log("Slam Stage " + _slamStage);
            }

            _frameCounter++;
        }
    }

    void InitiateSlam(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            _slamStage = 0;
            _frameCounter = 0;

            _rb.velocity = new Vector2(_rb.velocity.x, 0);
            _rb.AddForce(new Vector2(0, _initJumpForce), ForceMode2D.Impulse);
            _rb.gravityScale = _gravScale / 2;

            Invoke("BeginSlam", 0.3f);
        }
    }

    private void BeginSlam()
    {
        _slamming = true;
        _tr.enabled = true;
        _rb.gravityScale = _gravScale;
    }

    public void PerformingSlamificationOnThee()
    {
        _slamming = false;
        _tr.enabled = false;
        _trigger.enabled = false;
        this.transform.rotation = Quaternion.Euler(0, 0, 0);

        Slamming?.Invoke();
    }
}
