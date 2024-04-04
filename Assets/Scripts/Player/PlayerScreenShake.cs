using Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerScreenShake : MonoBehaviour
{
    [SerializeField]
    private Vector3 _impulseDirection;
    [SerializeField]
    private float _impulseStrength;

    private bool _isSlamming;
    private int _framecounter;

    private CinemachineImpulseSource _impulse;

    // Start is called before the first frame update
    void Start()
    {
        _impulse = GetComponent<CinemachineImpulseSource>();

        PlayerMain.Instance.Input.Slam += OnSlam;
    }

    void OnSlam(InputAction.CallbackContext ctx)
    {
        _isSlamming = true;
        _framecounter = 0;
    }

    private void FixedUpdate()
    {
        if (PlayerMain.Instance.Movement.IsGrounded && _isSlamming && _framecounter > 4)
        {
            _isSlamming = false;
            SlamShake();
        }
        _framecounter++;
    }

    private void SlamShake()
    {
        _impulse.GenerateImpulse(_impulseDirection.normalized * _impulseStrength);
    }
}
