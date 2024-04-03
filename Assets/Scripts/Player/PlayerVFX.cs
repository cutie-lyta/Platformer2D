using UnityEngine;
using UnityEngine.VFX;

public class PlayerVFX : MonoBehaviour
{
    private VisualEffect _vfx;

    private bool isSlamming = false;
    private int _frameCounter;

    void Awake()
    {
        PlayerMain.Instance.Input.Slam += (ctx) => { isSlamming = true; _frameCounter = 0; };

        _vfx = GetComponentInChildren<VisualEffect>();
    }

    private void FixedUpdate()
    {
        if (PlayerMain.Instance.Movement.IsGrounded && isSlamming && _frameCounter > 4)
        {
            _vfx.Play();
            isSlamming = false;
        }
        _frameCounter++;
    }
}
