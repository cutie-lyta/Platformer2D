using System;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class PlayerSlam : MonoBehaviour
{
    public event Action<int> Slamming;
    public event Action<int> SlamEnded;

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

    private Coroutine _coroutine;

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
            if (_slamStage < _framesForSlamStage.Length && _frameCounter >= _framesForSlamStage[_slamStage])
            {
                _slamStage++;
                Debug.Log("Slam Stage " + _slamStage);
            }

            _frameCounter++;
        }
        _trigger.transform.rotation = Quaternion.Euler(0, 0, 0);
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

            PlayerMain.Instance.Input.Slam -= InitiateSlam;
            PlayerMain.Instance.Movement.PlayerLand += PerformingSlamificationOnThee;

            _coroutine = StartCoroutine(BeginSlam());
        }
    }

    private IEnumerator BeginSlam()
    {
        yield return new WaitForSeconds(0.3f);
        _slamming = true;
        _tr.enabled = true;
        _trigger.enabled = true;
        _rb.gravityScale = _gravScale;
        PlayerMain.Instance.Death.SendMessage("Invincibility");
    }

    public void PerformingSlamificationOnThee()
    {
        if (_coroutine != null) StopCoroutine(_coroutine);

        _slamming = false;
        _tr.enabled = false;
        _trigger.enabled = false;

        this.transform.rotation = Quaternion.Euler(0, 0, 0);

        PlayerMain.Instance.Input.Slam += InitiateSlam;
        PlayerMain.Instance.Movement.PlayerLand -= PerformingSlamificationOnThee;
        PlayerMain.Instance.Death.SendMessage("UnInvincibility");

        Slamming?.Invoke(_slamStage);
    }
}
