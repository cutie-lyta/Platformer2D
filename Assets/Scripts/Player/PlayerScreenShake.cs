using Cinemachine;
using UnityEngine;

public class PlayerScreenShake : MonoBehaviour
{
    [Header("Explosion")]
    [SerializeField]
    private CinemachineImpulseSource _explosionImpulse;
    [SerializeField]
    private Vector3 _explosionDirection;
    [SerializeField]
    private float _explosionStrengthBase;

    [Header("Death")]
    [SerializeField]
    private CinemachineImpulseSource _deathImpulse;
    [SerializeField]
    private Vector3 _deathDirection;
    [SerializeField]
    private float _deathStrengthBase;

    // Start is called before the first frame update
    void Start()
    {
        PlayerMain.Instance.Slam.Slamming += SlamShake;
        PlayerMain.Instance.Death.PlayerDies += DeathShake;
    }

    private void SlamShake(int strength)
    {
        _explosionImpulse.GenerateImpulse(_explosionDirection.normalized * (strength * strength + _explosionStrengthBase));
    }

    private void DeathShake()
    {
        Debug.Log("Shake");
        _explosionImpulse.GenerateImpulse(_deathDirection.normalized * (_deathStrengthBase));
    }
}
