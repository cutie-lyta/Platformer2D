using Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerScreenShake : MonoBehaviour
{
    [SerializeField]
    private Vector3 _impulseDirection;
    [SerializeField]
    private float _impulseStrengthBase;

    private CinemachineImpulseSource _impulse;

    // Start is called before the first frame update
    void Start()
    {
        _impulse = GetComponent<CinemachineImpulseSource>();

        PlayerMain.Instance.Slam.Slamming += SlamShake;
    }

    private void SlamShake(int strength)
    {
        _impulse.GenerateImpulse(_impulseDirection.normalized * (strength * 2 + _impulseStrengthBase));
    }
}
